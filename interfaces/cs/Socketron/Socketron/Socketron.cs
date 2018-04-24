using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Socketron {
	public enum DataType {
		Text = 0,
	}

	public class ProcessType {
		public const string Browser = "browser";
		public const string Renderer = "renderer";
	}

	public delegate void Callback(SocketronData data);

	public class Socketron: EventEmitter {
		public string ID = string.Empty;
		public bool IsDebug = true;
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
				Function = function,
				Arguments = args
			};
			if (callback != null) {
				data.SequenceId = _sequenceId;
				_callbacks[_sequenceId++] = callback;
			}
			//Console.WriteLine("data: " + data.Stringify());
			Write(data.ToBuffer(DataType.Text, Encoding));
		}

		public void Log(string text, Callback callback = null) {
			WriteTextData(
				ProcessType.Renderer,
				"console.log",
				JsonObject.Array(text),
				callback
			);
		}

		public void ExecuteJavaScript(string script, Callback callback = null) {
			WriteTextData(
				ProcessType.Renderer,
				"executeJavaScript",
				JsonObject.Array(script),
				callback
			);
		}

		public void ExecuteJavaScript(string[] scriptList, Callback callback = null) {
			string script = string.Join("\n", scriptList);
			ExecuteJavaScript(script, callback);
		}

		public void InsertJavaScript(string url, Callback callback = null) {
			WriteTextData(
				ProcessType.Renderer,
				"insertJavaScript",
				JsonObject.Array(url),
				callback
			);
		}

		public void InsertCSS(string css, Callback callback = null) {
			WriteTextData(
				ProcessType.Renderer,
				"insertCSS",
				JsonObject.Array(css),
				callback
			);
		}

		//public void Command(string command, Callback callback = null) {
		//	_WriteText("browser", "command", command, callback);
		//}

		public void ExecuteJavaScriptNode(string script, Callback callback = null) {
			WriteTextData(
				ProcessType.Browser,
				"executeJavaScript",
				JsonObject.Array(script),
				callback
			);
		}

		public void ShowOpenDialog(object options, Callback callback = null) {
			WriteTextData(
				ProcessType.Browser,
				"dialog.showOpenDialog",
				JsonObject.Array(options),
				callback
			);
		}

		public void GetUserAgent(Callback callback = null) {
			WriteTextData(
				ProcessType.Renderer,
				"window.navigator.userAgent",
				JsonObject.Array(),
				callback
			);
		}

		public void GetProcessType(Callback callback = null) {
			WriteTextData(
				ProcessType.Browser,
				"process.type",
				JsonObject.Array(),
				callback
			);
		}

		public void GetProcessMemoryInfo(Callback callback = null) {
			WriteTextData(
				ProcessType.Browser,
				"process.getProcessMemoryInfo",
				JsonObject.Array(),
				callback
			);
		}

		public void GetNavigator(Callback callback = null) {
			WriteTextData(
				ProcessType.Renderer,
				"window.navigator",
				JsonObject.Array(),
				callback
			);
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

			switch (data.Function) {
				case "id":
					ID = data.Arguments[0] as string;
					DebugLog("ID: {0}", ID);
					break;
				case "callback":
					if (data.SequenceId == null) {
						break;
					}
					ushort sequenceId = (ushort)data.SequenceId;
					if (_callbacks.ContainsKey(sequenceId)) {
						Callback callback = _callbacks[sequenceId];
						callback?.Invoke(data);
						_callbacks.Remove(sequenceId);
					}
					break;
				case "return":
					JsonObject json = new JsonObject(data.Arguments[0] as Dictionary<string, object>);
					//DebugLog("Return: {0}", data.Arguments[0].GetType());
					DebugLog("Return: {0}: {1}", json["key"], json["value"]);
					break;
			}
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
