using System;
using System.Collections;
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

		public void ExecuteJavaScript(string script, Callback callback = null) {
			SocketronData data = new SocketronData() {
				Type = Type,
				Func = "executeJavaScript",
				Args = JsonObject.Array(script)
			};
			Emit("text", data, callback);
		}

		public void ExecuteJavaScript(string[] scriptList, Callback callback = null) {
			string script = string.Join("\n", scriptList);
			ExecuteJavaScript(script, callback);
		}

		public void ShowOpenDialog(JsonObject options, Callback callback = null) {
			SocketronData data = new SocketronData() {
				Type = Type,
				Func = "dialog.showOpenDialog",
				Args = JsonObject.Array(options)
			};
			Emit("text", data, callback);
		}

		public async Task<string> GetProcessType() {
			SocketronData data = new SocketronData() {
				Type = Type,
				Func = "process.type",
				Args = JsonObject.Array()
			};
			return await CallAsync<string>(data);
		}

		protected async Task<T> CallAsync<T>(SocketronData socketronData, int timeout = 1000) where T: class {
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

			Callback callback = (data) => {
				result = data as T;
				done = true;
			};
			Emit("text", socketronData, callback);
			return await task;
		}

		public void GetProcessMemoryInfo(Callback callback = null) {
			SocketronData data = new SocketronData() {
				Type = Type,
				Func = "process.getProcessMemoryInfo",
				Args = JsonObject.Array()
			};
			Emit("text", data, callback);
		}
	}

	public class RendererProcess : EventEmitter {
		const string Type = ProcessType.Renderer;

		public RendererProcess() {
		}

		public void Log(string text, Callback callback = null) {
			SocketronData data = new SocketronData() {
				Type = Type,
				Func = "console.log",
				Args = JsonObject.Array(text)
			};
			Emit("text", data, callback);
		}

		public void ExecuteJavaScript(string script, Callback callback = null) {
			SocketronData data = new SocketronData() {
				Type = Type,
				Func = "executeJavaScript",
				Args = JsonObject.Array(script)
			};
			Emit("text", data, callback);
		}

		public void ExecuteJavaScript(string[] scriptList, Callback callback = null) {
			string script = string.Join("\n", scriptList);
			ExecuteJavaScript(script, callback);
		}

		public void InsertJavaScript(string url, Callback callback = null) {
			SocketronData data = new SocketronData() {
				Type = Type,
				Func = "insertJavaScript",
				Args = JsonObject.Array(url)
			};
			Emit("text", data, callback);
		}

		public void InsertCSS(string css, Callback callback = null) {
			SocketronData data = new SocketronData() {
				Type = Type,
				Func = "insertCSS",
				Args = JsonObject.Array(css)
			};
			Emit("text", data, callback);
		}

		public void GetUserAgent(Callback callback = null) {
			SocketronData data = new SocketronData() {
				Type = Type,
				Func = "window.navigator.userAgent",
				Args = JsonObject.Array()
			};
			Emit("text", data, callback);
		}

		public void GetNavigator(Callback callback = null) {
			SocketronData data = new SocketronData() {
				Type = Type,
				Func = "window.navigator",
				Args = JsonObject.Array()
			};
			Emit("text", data, callback);
		}
	}

	public class Socketron: EventEmitter {
		public string ID = string.Empty;
		public bool IsDebug = true;
		public MainProcess Main;
		public RendererProcess Renderer;
		SocketronClient _client;
		Dictionary<ushort, Callback> _callbacks;
		ushort _sequenceId = 0;

		public Socketron() {
			_client = new SocketronClient();
			_client.On("debug", (args) => {
				Emit("debug", args[0]);
			});
			_client.On("connect", (args) => {
				Emit("connect");
			});
			_client.On("data", _OnData);

			_callbacks = new Dictionary<ushort, Callback>();

			Main = new MainProcess();
			Main.On("text", (args) => {
				SocketronData data = args[0] as SocketronData;
				Callback callback = args[1] as Callback;
				if (callback != null) {
					data.SequenceId = _sequenceId;
					_callbacks[_sequenceId++] = callback;
				}
				Console.WriteLine("data: " + data.Stringify());
				Write(data.ToBuffer(DataType.Text, Encoding));
			});

			Renderer = new RendererProcess();
			Renderer.On("text", (args) => {
				SocketronData data = args[0] as SocketronData;
				Callback callback = args[1] as Callback;
				if (callback != null) {
					data.SequenceId = _sequenceId;
					_callbacks[_sequenceId++] = callback;
				}
				Write(data.ToBuffer(DataType.Text, Encoding));
			});
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
			Task task = _client.Connect(hostname, port);
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
				Args = args
			};
			if (callback != null) {
				data.SequenceId = _sequenceId;
				_callbacks[_sequenceId++] = callback;
			}
			//Console.WriteLine("data: " + data.Stringify());
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
					ID = data.Args as string;
					DebugLog("ID: {0}", ID);
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
			if (!_callbacks.ContainsKey(sequenceId)) {
				return;
			}
			Callback callback = _callbacks[sequenceId];
			callback?.Invoke(data.Args);
			_callbacks.Remove(sequenceId);
		}

		protected void _OnEvent(SocketronData data) {
			JsonObject json = new JsonObject(data.Args);
			//DebugLog("Return: {0}", data.Arguments[0].GetType());

			string eventName = null;
			object args = null;
			foreach (string key in json.Keys) {
				eventName = key;
				args = json[key];
				break;
			}
			//DebugLog("Return: {0}: {1}", eventName, args);
			if (args is object[]) {
				Emit(eventName, args as object[]);
				return;
			}
			Emit(eventName, args);
		}

		protected void DebugLog(string format, params object[] args) {
			if (!IsDebug) {
				return;
			}
			format = string.Format("[{0}] {1}", typeof(Socketron).Name, format);
			Emit("debug", string.Format(format, args));
		}
	}
}
