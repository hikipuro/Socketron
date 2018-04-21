using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Socketron {
	public enum DataType {
		Null = 0,
		Log,
		Run,
		ImportScript,
		Command,
		AppendStyle,
		Callback,
		ID,
		Return,
		ShowOpenDialog
	}

	public class Socketron: EventEmitter {
		public string ID = string.Empty;
		public bool IsDebug = true;
		SocketronClient _client;
		Dictionary<ushort, Action> _callbacks;
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

			_callbacks = new Dictionary<ushort, Action>();
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

		//public void Write(string str) {
		//	byte[] bytes = Encoding.GetBytes(str);
		//	Write(bytes);
		//}

		public void Log(string message, Action callback = null) {
			_WriteText(DataType.Log, message, callback);
		}

		public void ExecuteJavaScript(string script, Action callback = null) {
			_WriteText(DataType.Run, script, callback);
		}

		public void ExecuteJavaScript(string[] scriptList, Action callback = null) {
			string script = string.Join("\n", scriptList);
			_WriteText(DataType.Run, script, callback);
		}

		public void InsertJavaScript(string url, Action callback = null) {
			_WriteText(DataType.ImportScript, url, callback);
		}

		public void InsertCSS(string css, Action callback = null) {
			_WriteText(DataType.AppendStyle, css, callback);
		}

		public void Command(string command, Action callback = null) {
			_WriteText(DataType.Command, command, callback);
		}

		public void ShowOpenDialog(string options, Action callback = null) {
			_WriteText(DataType.ShowOpenDialog, options, callback);
		}

		protected void _WriteText(DataType dataType, string text, Action callback) {
			Buffer buffer = Packet.CreateData(
				dataType, _sequenceId, text, Encoding
			);
			_callbacks[_sequenceId++] = callback;
			Write(buffer);
		}

		protected void _OnData(object[] args) {
			//Emit("data", args);
			Packet packet = (Packet)args[0];
			switch (packet.DataType) {
				case DataType.ID:
					ID = packet.GetStringData();
					DebugLog("ID: {0}", ID);
					break;
				case DataType.Callback:
					ushort sequenceId = packet.SequenceId;
					if (_callbacks.ContainsKey(sequenceId)) {
						Action callback = _callbacks[sequenceId];
						callback?.Invoke();
						_callbacks.Remove(sequenceId);
					}
					//DebugLog("Callback: {0}, {1}", sequenceId, packet.GetStringData());
					break;
				case DataType.Return:
					string returnText = packet.GetStringData();
					string[] keyValue = returnText.Split(',');
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
