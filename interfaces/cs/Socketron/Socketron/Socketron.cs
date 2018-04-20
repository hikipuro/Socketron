﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Socketron {
	public enum DataType {
		Null = 0,
		Log,
		Run,
		Import,
		Command
	}

	public class Socketron: EventEmitter {
		SocketronClient _client;
		DataCreator _dataCreator;
		Dictionary<ushort, Action> _callbacks;

		public Socketron() {
			_client = new SocketronClient();
			_client.On("debug", (args) => {
				Emit("debug", args[0]);
			});
			_client.On("connect", (args) => {
				Emit("connect");
			});
			_client.On("data", (args) => {
				Emit("data", args);
				Packet packet = (Packet)args[0];
				ushort sequenceId = packet.SequenceId;
				if (_callbacks.ContainsKey(sequenceId)) {
					Action callback = _callbacks[sequenceId];
					callback?.Invoke();
					_callbacks.Remove(sequenceId);
				}
			});

			_dataCreator = new DataCreator();
			_callbacks = new Dictionary<ushort, Action>();
		}

		public bool IsConnected {
			get { return _client.IsConnected; }
		}

		public Encoding Encoding {
			get { return _client.Encoding; }
			set {
				_client.Encoding = value;
				_dataCreator.Encoding = value;
			}
		}

		public int Timeout {
			get { return _client.Timeout; }
			set { _client.Timeout = value; }
		}

		public void Write(byte[] bytes) {
			_client.Write(bytes);
		}

		//public void Write(string str) {
		//	byte[] bytes = Encoding.GetBytes(str);
		//	Write(bytes);
		//}

		public void SendLog(string message, Action callback = null) {
			Write(_dataCreator.Create(DataType.Log, message));
			_callbacks[_dataCreator.SequenceId] = callback;
		}

		public void Run(string script, Action callback = null) {
			Write(_dataCreator.Create(DataType.Run, script));
			_callbacks[_dataCreator.SequenceId] = callback;
		}

		public void ImportScript(string url, Action callback = null) {
			Write(_dataCreator.Create(DataType.Import, url));
			_callbacks[_dataCreator.SequenceId] = callback;
		}

		public void Command(string command) {
			Write(_dataCreator.Create(DataType.Command, command));
		}

		public void Connect(string hostname, int port = 3000) {
			Task task = _client.Connect(hostname, port);
		}

		public void Close() {
			_client.Close();
		}
	}
}