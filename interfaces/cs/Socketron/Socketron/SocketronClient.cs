using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Socketron {
	public enum ReadState {
		Type = 0,
		CommandLength,
		Command
	}

	internal class SocketronClient: EventEmitter {
		public const int ReadBufferSize = 1024;
		public bool IsDebug = true;

		protected TcpClient _tcpClient;
		protected NetworkStream _stream;
		protected int _timeout = 10000;
		protected Encoding _encoding = Encoding.UTF8;
		protected Packet _packet = new Packet();

		public SocketronClient() {
		}

		public bool IsConnected {
			get {
				if (_tcpClient != null && _tcpClient.Connected) {
					return true;
				}
				return false;
			}
		}

		public int Timeout {
			get { return _timeout; }
			set {
				_timeout = value;
				if (_stream != null) {
					_stream.ReadTimeout = value;
					_stream.WriteTimeout = value;
				}
			}
		}

		public Encoding Encoding {
			get { return _encoding; }
			set {
				_encoding = value;
				_packet.Encoding = value;
			}
		}

		public async Task Connect(string hostname, int port) {
			Close();
			_tcpClient = new TcpClient();
			await _tcpClient.ConnectAsync(hostname, port);

			DebugLog("Connected (Remote: {0}:{1} Local: {2}:{3})",
				((IPEndPoint)_tcpClient.Client.RemoteEndPoint).Address,
				((IPEndPoint)_tcpClient.Client.RemoteEndPoint).Port,
				((IPEndPoint)_tcpClient.Client.LocalEndPoint).Address,
				((IPEndPoint)_tcpClient.Client.LocalEndPoint).Port
			);

			_stream = _tcpClient.GetStream();
			_stream.ReadTimeout = _timeout;
			_stream.WriteTimeout = _timeout;

			Emit("connect");
			Task task = Read();
		}

		public void Close() {
			if (_stream != null) {
				_stream.Close();
				_stream = null;
			}
			if (_tcpClient != null) {
				_tcpClient.Close();
				//_tcpClient = null;
			}
		}

		public void Write(byte[] bytes) {
			try {
				_stream.WriteAsync(bytes, 0, bytes.Length);
			} catch (IOException) {
				Close();
			} catch (NullReferenceException) {
				Close();
			}
		}

		protected async Task Read() {
			byte[] bytes = new byte[ReadBufferSize];
			while (_tcpClient.Connected && _stream.CanRead) {
				do {
					int bytesReaded = await _stream.ReadAsync(bytes, 0, bytes.Length);
					if (bytesReaded == 0) {
						Close();
						return;
					}
					OnData(bytes, bytesReaded);
				} while (_stream.DataAvailable);
				Thread.Sleep(1);
			}
		}

		protected void OnData(byte[] data, int bytesReaded) {
			if (data != null) {
				_packet.Data.Write(data, 0, bytesReaded);
			}

			uint offset = _packet.DataOffset;
			uint remain = (uint)_packet.Data.Length - offset;

			switch (_packet.State) {
				case ReadState.Type:
					if (remain < 1) {
						return;
					}
					break;
				case ReadState.CommandLength:
					if (remain < 2) {
						return;
					}
					break;
				case ReadState.Command:
					if (remain < _packet.DataLength) {
						return;
					}
					break;
			}

			switch (_packet.State) {
				case ReadState.Type:
					_packet.DataType = (DataType)_packet.Data[offset];
					_packet.DataOffset += 1;
					_packet.State = ReadState.CommandLength;
					break;
				case ReadState.CommandLength:
					_packet.DataLength = _packet.Data.ReadUInt16LE(offset);
					_packet.DataOffset += 2;
					_packet.State = ReadState.Command;
					break;
				case ReadState.Command:
					if (_packet.DataType == DataType.Text) {
						string text = _packet.GetStringData();
						Console.WriteLine("Packet: {0}", text);
						Emit("data", SocketronData.Parse(text));
					}
					_packet.Data = _packet.Data.Slice(offset + _packet.DataLength);
					_packet.DataOffset = 0;
					_packet.State = ReadState.Type;
					break;
			}

			OnData(null, 0);
		}

		protected void DebugLog(string format, params object[] args) {
			if (!IsDebug) {
				return;
			}
			format = string.Format("[{0}] {1}", typeof(SocketronClient).Name, format);
			Emit("debug", string.Format(format, args));
		}
	}
}
