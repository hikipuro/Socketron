using System;
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

		public void Log(string text, Callback callback = null) {
			_WriteText(ProcessType.Renderer, "console.log", text, callback);
		}

		public void ExecuteJavaScript(string script, Callback callback = null) {
			_WriteText(ProcessType.Renderer, "executeJavaScript", script, callback);
		}

		public void ExecuteJavaScript(string[] scriptList, Callback callback = null) {
			string script = string.Join("\n", scriptList);
			ExecuteJavaScript(script, callback);
		}

		public void InsertJavaScript(string url, Callback callback = null) {
			_WriteText(ProcessType.Renderer, "insertJavaScript", url, callback);
		}

		public void InsertCSS(string css, Callback callback = null) {
			_WriteText(ProcessType.Renderer, "insertCSS", css, callback);
		}

		//public void Command(string command, Callback callback = null) {
		//	_WriteText("browser", "command", command, callback);
		//}

		public void ExecuteJavaScriptNode(string script, Callback callback = null) {
			_WriteText(ProcessType.Browser, "executeJavaScript", script, callback);
		}

		public void ShowOpenDialog(string options, Callback callback = null) {
			_WriteText(ProcessType.Browser, "showOpenDialog", options, callback);
		}

		protected void _WriteText(string type, string function, string text, Callback callback) {
			SocketronData data = new SocketronData();
			;
			data.Type = type;
			data.Function = function;
			data.Command = text;

			if (callback != null) {
				data.SequenceId = _sequenceId;
				_callbacks[_sequenceId++] = callback;
			}
			Write(data.ToBuffer(DataType.Text, Encoding));
		}

		protected void _OnData(object[] args) {
			SocketronData data = args[0] as SocketronData;
			if (data == null) {
				return;
			}

			//Console.WriteLine("data.Function: " + data.Function);
			switch (data.Function) {
				case "id":
					ID = data.Command;
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
					string[] keyValue = data.Command.Split(',');
					if (keyValue.Length >= 2) {
						Emit("return", keyValue[0], keyValue[1]);
					}
					//DebugLog("Return: {0}", returnText);
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
