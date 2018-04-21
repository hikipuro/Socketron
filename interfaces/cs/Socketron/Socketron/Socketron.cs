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
	}

	public class Socketron: EventEmitter {
		public string ID = string.Empty;
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

		public void Write(byte[] bytes) {
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
			Buffer buffer = Packet.CreateData(
				DataType.Log, _sequenceId, message, Encoding
			);
			_callbacks[_sequenceId++] = callback;
			Write(buffer);
		}

		public void Run(string script, Action callback = null) {
			Buffer buffer = Packet.CreateData(
				DataType.Run, _sequenceId, script, Encoding
			);
			_callbacks[_sequenceId++] = callback;
			Write(buffer);
		}

		public void ImportScript(string url, Action callback = null) {
			Buffer buffer = Packet.CreateData(
				DataType.ImportScript, _sequenceId, url, Encoding
			);
			_callbacks[_sequenceId++] = callback;
			Write(buffer);
		}

		public void Command(string command, Action callback = null) {
			Buffer buffer = Packet.CreateData(
				DataType.Command, _sequenceId, command, Encoding
			);
			_callbacks[_sequenceId++] = callback;
			Write(buffer);
		}

		public void AppendStyle(string css, Action callback = null) {
			Buffer buffer = Packet.CreateData(
				DataType.AppendStyle, _sequenceId, css, Encoding
			);
			_callbacks[_sequenceId++] = callback;
			Write(buffer);
		}

		public void Connect(string hostname, int port = 3000) {
			Task task = _client.Connect(hostname, port);
		}

		public void Close() {
			ID = string.Empty;
			_client.Close();
		}

		protected void _OnData(object[] args) {
			Emit("data", args);
			Packet packet = (Packet)args[0];
			switch (packet.DataType) {
				case DataType.ID:
					ID = packet.GetStringData();
					Console.WriteLine("ID: {0}", ID);
					break;
				case DataType.Callback:
					ushort sequenceId = packet.SequenceId;
					if (_callbacks.ContainsKey(sequenceId)) {
						Action callback = _callbacks[sequenceId];
						callback?.Invoke();
						_callbacks.Remove(sequenceId);
					}
					break;
				case DataType.Return:
					Console.WriteLine("Return: {0}", packet.GetStringData());
					break;
			}
		}
	}
}
