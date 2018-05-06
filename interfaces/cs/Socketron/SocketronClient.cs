using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

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

		public MainProcess() {
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

		/*
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
		//*/
	}

	public class SocketronClient: EventEmitter {
		public string ID = string.Empty;
		public Config Config = new Config();
		public MainProcess Main;
		public RendererProcess Renderer;
		public CallbackManager Callbacks;
		SocketClient _socketClient;
		Dictionary<ushort, Callback> _successList;
		Dictionary<ushort, Callback> _errorList;
		ushort _sequenceId = 0;
		static Dictionary<Thread, SocketronClient> _Clients;

		static SocketronClient() {
			_Clients = new Dictionary<Thread, SocketronClient>();
		}

		public static SocketronClient Current {
			get {
				Thread thread = Thread.CurrentThread;
				if (_Clients.ContainsKey(thread)) {
					return _Clients[thread];
				}
				return null;
			}
		}

		public static SocketronClient GetCurrent() {
			Thread thread = Thread.CurrentThread;
			if (_Clients.ContainsKey(thread)) {
				return _Clients[thread];
			}
			string message = "Socketron instance is not found in this thread.";
			throw new InvalidOperationException(message);
		}

		public static void Execute(string script) {
			SocketronClient client = GetCurrent();
			client.Main.ExecuteJavaScript(script);
		}

		public static T ExecuteBlocking<T>(string script) {
			SocketronClient client = GetCurrent();
			return client.ExecuteJavaScriptBlocking<T>(script);
		}

		public SocketronClient() {
			_socketClient = new SocketClient(Config);
			_socketClient.On("debug", (args) => {
				if (!Config.IsDebug) {
					return;
				}
				Emit("debug", args[0]);
			});
			_socketClient.On("connect", _OnConnect);
			_socketClient.On("data", _OnData);

			_successList = new Dictionary<ushort, Callback>();
			_errorList = new Dictionary<ushort, Callback>();
			Callbacks = new CallbackManager();

			Main = new MainProcess();
			Main.On("text", _OnText);

			Renderer = new RendererProcess();
			Renderer.On("text", _OnText);
		}

		public bool IsConnected {
			get { return _socketClient.IsConnected; }
		}

		public void Connect(string hostname, int port = 3000) {
			_socketClient.Connect(hostname, port);
		}

		public void Close() {
			ID = string.Empty;
			NodeModule.DisposeAll();
			_socketClient.Close();
		}

		public void Write(byte[] bytes) {
			if (!_socketClient.IsConnected) {
				return;
			}
			_socketClient.Write(bytes);
		}

		public void Write(LocalBuffer buffer) {
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
			Write(data.ToBuffer(DataType.Text, Config.Encoding));
		}

		public void RemoveObject(int id) {
			if (id <= 0) {
				return;
			}
			Callbacks.RemoveInstanceEvents(id);
			string script = string.Format(
				"this._removeObjectReference({0});",
				id
			);
			//Console.WriteLine("RemoveObject: " + id);
			Main.ExecuteJavaScript(script);
		}

		public T ExecuteJavaScriptBlocking<T>(string script) {
			ManualResetEvent resetEvent = new ManualResetEvent(false);
			T value = default(T);
			Type typeofT = typeof(T);

			Main.ExecuteJavaScript(script, (result) => {
				if (result == null) {
					resetEvent.Set();
					return;
				}
				if (typeofT == typeof(object)) {
					value = (T)result;
					resetEvent.Set();
					return;
				}
				value = (T)Convert.ChangeType(result, typeofT);
				resetEvent.Set();
			}, (result) => {
				Console.Error.WriteLine("error: " + GetType().Name + ".ExecuteJavaScriptBlocking");
				throw new InvalidOperationException(result as string);
			});

			resetEvent.WaitOne();
			return value;
		}

		protected void _OnConnect(object[] args) {
			Thread t = new Thread(() => {
				Thread thread = Thread.CurrentThread;
				thread.Name = typeof(SocketronClient).Name + " Connection";
				if (!_Clients.ContainsKey(thread)) {
					_Clients.Add(thread, this);
				}
				Emit("connect");
				_Clients.Remove(thread);
			});
			t.Start();
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
			if (Config.IsDebug) {
				_DebugLog("send: {0}", data.Stringify());
			}
			Write(data.ToBuffer(DataType.Text, Config.Encoding));
		}

		protected void _OnData(object[] args) {
			SocketronData data = args[0] as SocketronData;
			if (data == null) {
				return;
			}
			//Console.WriteLine("data.Func: " + data.Func);
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
				if (data.Status == "error") {
					throw new InvalidOperationException(data.Params as string);
				}
				return;
			}
			ushort sequenceId = (ushort)data.SequenceId;

			if (data.Status == "error") {
				if (_errorList.ContainsKey(sequenceId)) {
					Callback error = _errorList[sequenceId];
					error?.Invoke(data.Params);
					_successList.Remove(sequenceId);
					_errorList.Remove(sequenceId);
				} else {
					throw new InvalidOperationException(data.Params as string);
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
			//_DebugLog("Return: {0}: {1}", eventName, args);

			if (eventName == "__event") {
				Thread thread = Thread.CurrentThread;
				if (!_Clients.ContainsKey(thread)) {
					_Clients.Add(thread, this);
				}

				object[] list = args as object[];
				long instanceId = Convert.ToInt64(list[0]);
				string eventId = list[1] as string;
				long callbackId = Convert.ToInt64(list[2]);
				object[] callbackParams = null;
				if (list.Length >= 4) {
					callbackParams = new object[list.Length - 3];
					Array.Copy(list, 2, callbackParams, 0, list.Length - 3);
				}
				/*
				Callback callback = null;

				className = "Socketron." + Char.ToUpper(className[0]) + className.Substring(1);
				Type type = GetType().Assembly.GetType(className);
				if (type == null) {
					_Clients.Remove(thread);
					return;
				}
				MethodInfo method = type.GetMethod(
					"GetCallbackFromId",
					BindingFlags.Static | BindingFlags.Public
				);
				if (method == null) {
					_Clients.Remove(thread);
					return;
				}
				callback = method.Invoke(null, new[] { (object)callbackId }) as Callback;
				callback?.Invoke(callbackParams);
				//*/

				CallbackItem item = Callbacks.GetItem(instanceId, eventId, callbackId);
				item?.Callback?.Invoke(callbackParams);

				_Clients.Remove(thread);

				/*
				switch (className) {
					case BrowserWindow.Name:
						callback = BrowserWindow.GetCallbackFromId((ushort)callbackId);
						callback?.Invoke(callbackParams);
						break;
					case WebContents.Name:
						callback = WebContents.GetCallbackFromId((ushort)callbackId);
						callback?.Invoke(callbackParams);
						break;
				}
				//*/
				return;
			}

			if (args is object[]) {
				Thread thread = Thread.CurrentThread;
				if (!_Clients.ContainsKey(thread)) {
					_Clients.Add(thread, this);
				}
				Emit(eventName, args as object[]);
				_Clients.Remove(thread);
				return;
			}
			Emit(eventName, args);
		}

		protected void _DebugLog(string format, params object[] args) {
			if (!Config.IsDebug) {
				return;
			}
			format = string.Format("[{0}] {1}", typeof(SocketronClient).Name, format);
			Emit("debug", string.Format(format, args));
		}
	}
}
