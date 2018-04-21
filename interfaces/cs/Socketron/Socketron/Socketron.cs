using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Socketron {
	public enum DataType {
		Text = 0,
	}

	public delegate void Callback(Command command);

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
			_WriteText("browser", "console.log", text, callback);
		}

		public void ExecuteJavaScript(string script, Callback callback = null) {
			_WriteText("browser", "executeJavaScript", script, callback);
		}

		public void ExecuteJavaScript(string[] scriptList, Callback callback = null) {
			string script = string.Join("\n", scriptList);
			ExecuteJavaScript(script, callback);
		}

		public void InsertJavaScript(string url, Callback callback = null) {
			_WriteText("browser", "insertJavaScript", url, callback);
		}

		public void InsertCSS(string css, Callback callback = null) {
			_WriteText("browser", "insertCSS", css, callback);
		}

		//public void Command(string command, Callback callback = null) {
		//	_WriteText("browser", "command", command, callback);
		//}

		public void ShowOpenDialog(string options, Callback callback = null) {
			_WriteText("node", "showOpenDialog", options, callback);
		}

		protected void _WriteText(string type, string function, string text, Callback callback) {
			Command command = new Command();
			;
			command.Type = type;
			command.Function = function;
			command.Data = text;

			if (callback != null) {
				command.SequenceId = _sequenceId;
				_callbacks[_sequenceId++] = callback;
			}

			Buffer buffer = Packet.CreateTextData(command, Encoding);
			Write(buffer);
		}

		protected void _OnData(object[] args) {
			//Emit("data", args);
			Packet packet = (Packet)args[0];
			if (packet == null) {
				return;
			}
			string json = packet.GetStringData();
			//Console.WriteLine(json);

			Command command = Command.FromJson(json);
			//Console.WriteLine("command.Function: " + command.Function);
			switch (command.Function) {
				case "id":
					ID = command.Data;
					DebugLog("ID: {0}", ID);
					break;
				case "callback":
					if (command.SequenceId == null) {
						break;
					}
					ushort sequenceId = (ushort)command.SequenceId;
					if (_callbacks.ContainsKey(sequenceId)) {
						Callback callback = _callbacks[sequenceId];
						callback?.Invoke(command);
						_callbacks.Remove(sequenceId);
					}
					break;
				case "return":
					string[] keyValue = command.Data.Split(',');
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
