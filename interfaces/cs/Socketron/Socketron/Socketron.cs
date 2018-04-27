﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Socketron {
	public enum DataType {
		Text = 0,
	}

	public class ProcessType {
		public const string Browser = "browser";
		public const string Renderer = "renderer";
	}

	public delegate void Callback(object result);
	public delegate void Callback<T>(T result);

	public class MainProcess : EventEmitter {
		const string Type = ProcessType.Browser;
		public App App;

		public MainProcess() {
			App = new App();
			App.On("text", (args) => {
				Emit("text", args[0], args[1]);
			});
		}

		public void ExecuteJavaScript(string script, Callback success = null, Callback error = null) {
			SocketronData data = new SocketronData() {
				Type = Type,
				Func = "executeJavaScript",
				Params = new object[] { script }
			};
			Emit("text", data, success, error);
		}

		public void ExecuteJavaScript(string[] scriptList, Callback success = null, Callback error = null) {
			string script = string.Join("\n", scriptList);
			ExecuteJavaScript(script, success, error);
		}

		public async Task<object> ExecuteJavaScriptAsync(string script) {
			SocketronData data = new SocketronData() {
				Type = Type,
				Func = "executeJavaScript",
				Params = new object[] { script }
			};
			return await _CallAsync<object>(data);
		}

		public async Task<object> ExecuteJavaScriptAsync(string[] scriptList) {
			string script = string.Join("\n", scriptList);
			return await ExecuteJavaScriptAsync(script);
		}

		protected async Task<T> _CallAsync<T>(SocketronData socketronData, int timeout = 10000) where T: class {
			T result = null;
			bool done = false;
			var tokenSource = new CancellationTokenSource();
			tokenSource.CancelAfter(timeout);
			Task<T> task = Task.Run(async () => {
				while (!done) {
					await Task.Delay(1);
					if (tokenSource.IsCancellationRequested) {
						break;
					}
				}
				return result;
			}, tokenSource.Token);

			Callback success = (data) => {
				result = data as T;
				done = true;
			};
			Callback error = (data) => {
				done = true;
			};
			Emit("text", socketronData, success, error);
			return await task;
		}
	}

	public class RendererProcess : EventEmitter {
		const string Type = ProcessType.Renderer;

		public RendererProcess() {
		}

		public void ExecuteJavaScript(string script, Callback success = null, Callback error = null) {
			SocketronData data = new SocketronData() {
				Func = "executeJavaScript",
				Params = new object[] { script }
			};
			Emit("text", data, success, error);
		}

		public void ExecuteJavaScript(string[] scriptList, Callback success = null, Callback error = null) {
			string script = string.Join("\n", scriptList);
			ExecuteJavaScript(script, success, error);
		}

		public void InsertJavaScript(string url, Callback success = null) {
			SocketronData data = new SocketronData() {
				Func = "insertJavaScript",
				Params = new object[] { url }
			};
			Emit("text", data, success, null);
		}

		public void InsertCSS(string css, Callback success = null) {
			SocketronData data = new SocketronData() {
				Func = "insertCSS",
				Params = new object[] { css }
			};
			Emit("text", data, success, null);
		}

		public async Task<object> ExecuteJavaScriptAsync(string script) {
			SocketronData data = new SocketronData() {
				Func = "executeJavaScript",
				Params = new object[] { script }
			};
			return await _CallAsync<object>(data);
		}

		public async Task<object> ExecuteJavaScriptAsync(string[] scriptList) {
			string script = string.Join("\n", scriptList);
			return await ExecuteJavaScriptAsync(script);
		}

		public async Task<object> InsertJavaScriptAsync(string url) {
			SocketronData data = new SocketronData() {
				Func = "insertJavaScript",
				Params = new object[] { url }
			};
			return await _CallAsync<object>(data);
		}

		public async Task<object> InsertCSSAsync(string css) {
			SocketronData data = new SocketronData() {
				Func = "insertCSS",
				Params = new object[] { css }
			};
			return await _CallAsync<object>(data);
		}

		protected async Task<T> _CallAsync<T>(SocketronData socketronData, int timeout = 10000) where T : class {
			T result = null;
			bool done = false;
			var tokenSource = new CancellationTokenSource();
			tokenSource.CancelAfter(timeout);
			Task<T> task = Task.Run(async () => {
				while (!done) {
					await Task.Delay(1);
					if (tokenSource.IsCancellationRequested) {
						break;
					}
				}
				return result;
			}, tokenSource.Token);

			Callback success = (data) => {
				result = data as T;
				done = true;
			};
			Callback error = (data) => {
				done = true;
			};
			Emit("text", socketronData, success, error);
			return await task;
		}
	}

	public class Socketron: EventEmitter {
		public string ID = string.Empty;
		public bool IsDebug = true;
		public MainProcess Main;
		public RendererProcess Renderer;
		SocketronClient _client;
		Dictionary<ushort, Callback> _successList;
		Dictionary<ushort, Callback> _errorList;
		ushort _sequenceId = 0;

		public Socketron() {
			_client = new SocketronClient();
			_client.On("debug", (args) => {
				if (!IsDebug) {
					return;
				}
				Emit("debug", args[0]);
			});
			_client.On("connect", (args) => {
				Emit("connect");
			});
			_client.On("data", _OnData);

			_successList = new Dictionary<ushort, Callback>();
			_errorList = new Dictionary<ushort, Callback>();

			Main = new MainProcess();
			Main.On("text", _OnText);

			Renderer = new RendererProcess();
			Renderer.On("text", _OnText);
		}

		public bool IsConnected {
			get { return _client.IsConnected; }
		}

		public Encoding Encoding {
			get { return _client.Encoding; }
			set { _client.Encoding = value; }
		}

		public int Timeout {
			get { return _client.Timeout; }
			set { _client.Timeout = value; }
		}

		public void Connect(string hostname, int port = 3000) {
			Task task = Task.Run(async () => {
				await _client.Connect(hostname, port);
			});
			task.Wait();
		}

		public void Close() {
			ID = string.Empty;
			_client.Close();
		}

		public void Write(byte[] bytes) {
			if (!_client.IsConnected) {
				return;
			}
			_client.Write(bytes);
		}

		public void Write(Buffer buffer) {
			byte[] bytes = buffer.ToByteArray();
			Write(bytes);
		}

		public void WriteTextData(string type, string function, object[] args = null, Callback callback = null) {
			SocketronData data = new SocketronData() {
				Type = type,
				Func = function,
				Params = args
			};
			if (callback != null) {
				data.SequenceId = _sequenceId;
				_successList[_sequenceId++] = callback;
			}
			//Console.WriteLine("data: " + data.Stringify());
			Write(data.ToBuffer(DataType.Text, Encoding));
		}

		protected void _OnText(object[] args) {
			SocketronData data = args[0] as SocketronData;
			Callback success = args[1] as Callback;
			Callback error = args[2] as Callback;
			if (success != null) {
				data.SequenceId = _sequenceId;
				_successList[_sequenceId] = success;
				if (error != null) {
					_errorList[_sequenceId] = error;
				}
				_sequenceId++;
			}
			if (IsDebug) {
				_DebugLog("send: {0}", data.Stringify());
			}
			Write(data.ToBuffer(DataType.Text, Encoding));
		}

		protected void _OnData(object[] args) {
			SocketronData data = args[0] as SocketronData;
			if (data == null) {
				return;
			}
			//Console.WriteLine("data.Command: " + data.Command + " " + data.Command.Text);
			//Console.WriteLine("data.Function: " + data.Function);
			//Console.WriteLine("data.Arguments: " + data.Arguments);
			//Console.WriteLine("data.SequenceId: " + data.SequenceId);
			//Console.WriteLine("sequenceId: " + data.SequenceId);

			switch (data.Func) {
				case "id":
					ID = data.Params as string;
					//DebugLog("ID: {0}", ID);
					break;
				case "callback":
					_OnCallback(data);
					break;
				case "event":
					_OnEvent(data);
					break;
			}
		}

		protected void _OnCallback(SocketronData data) {
			if (data.SequenceId == null) {
				return;
			}
			ushort sequenceId = (ushort)data.SequenceId;

			if (data.Status == "error") {
				if (_errorList.ContainsKey(sequenceId)) {
					Callback error = _errorList[sequenceId];
					error?.Invoke(data.Params);
					_successList.Remove(sequenceId);
					_errorList.Remove(sequenceId);
				}
				return;
			}
			if (_successList.ContainsKey(sequenceId)) {
				Callback success = _successList[sequenceId];
				success?.Invoke(data.Params);
				_successList.Remove(sequenceId);
				_errorList.Remove(sequenceId);
			}
		}

		protected void _OnEvent(SocketronData data) {
			JsonObject json = new JsonObject(data.Params);
			//DebugLog("Return: {0}", data.Arguments[0].GetType());

			string eventName = json["name"] as string;
			object args = json["args"];
			//DebugLog("Return: {0}: {1}", eventName, args);
			if (args is object[]) {
				EmitTask(eventName, args as object[]);
				return;
			}
			EmitTask(eventName, args);
		}

		protected void _DebugLog(string format, params object[] args) {
			if (!IsDebug) {
				return;
			}
			format = string.Format("[{0}] {1}", typeof(Socketron).Name, format);
			EmitTask("debug", string.Format(format, args));
		}
	}
}
