using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socketron {
	public enum ReadState {
		Type = 0,
		Sequence,
		Length,
		Data
	}

	public class Packet {
		public MemoryStream Data = null;
		public DataType DataType = DataType.Null;
		public ushort SequenceId = 0;
		public uint DataLength = 0;
		public uint DataOffset = 0;
		public ReadState State = ReadState.Type;

		public Packet() {
			Data = new MemoryStream();
		}

		public Packet Clone() {
			Packet packet = new Packet();
			packet.Data = Data;
			packet.DataType = DataType;
			packet.SequenceId = SequenceId;
			packet.DataLength = DataLength;
			packet.DataOffset = DataOffset;
			packet.State = State;
			return packet;
		}

		public byte ReadUint8(uint offset) {
			return Data.GetBuffer()[offset];
		}

		public ushort ReadUint16LE(uint offset) {
			byte[] buffer = Data.GetBuffer();
			ushort result = buffer[offset];
			result |= (ushort)(buffer[offset + 1] << 8);
			return result;
		}

		public uint ReadUint32LE(uint offset) {
			byte[] buffer = Data.GetBuffer();
			uint result = buffer[offset];
			result |= (uint)(buffer[offset + 1] << 8);
			result |= (uint)(buffer[offset + 2] << 16);
			result |= (uint)(buffer[offset + 3] << 24);
			return result;
		}

		public void SliceData(uint offset) {
			uint length = (uint)Data.Length - offset;
			byte[] data = new byte[length];
			Data.Position = offset;
			Data.Read(data, 0, (int)length);
			Data = new MemoryStream();
			Data.Write(data, 0, data.Length);
		}

		public string GetStringData(Encoding encoding = null) {
			if (encoding == null) {
				encoding = Encoding.UTF8;
			}
			return encoding.GetString(
				Data.GetBuffer(),
				(int)DataOffset,
				(int)Data.Length - (int)DataOffset
			);
		}
	}

	internal class SocketronClient: EventEmitter {
		public const int ReadBufferSize = 1024;
		public Encoding Encoding = Encoding.UTF8;

		protected TcpClient _tcpClient;
		protected NetworkStream _stream;
		protected int _timeout = 10000;
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

		public async Task Connect(string hostname, int port) {
			Close();
			_tcpClient = new TcpClient();
			await _tcpClient.ConnectAsync(hostname, port);

			Emit("debug", string.Format("[Connected] Remote: ({0}:{1}) Local: ({2}:{3})",
				((IPEndPoint)_tcpClient.Client.RemoteEndPoint).Address,
				((IPEndPoint)_tcpClient.Client.RemoteEndPoint).Port,
				((IPEndPoint)_tcpClient.Client.LocalEndPoint).Address,
				((IPEndPoint)_tcpClient.Client.LocalEndPoint).Port)
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
			}
		}

		protected void OnData(byte[] data, int bytesReaded) {
			if (data != null) {
				_packet.Data.Write(data, 0, bytesReaded);
			}

			uint offset = _packet.DataOffset;
			uint remain = (uint)_packet.Data.Length - offset;

			//Console.WriteLine("*** DEBUG 1: " + _packet.State);
			//Console.WriteLine("*** DEBUG 2: " + _packet.Data.Length);
			//Console.WriteLine("*** DEBUG 3: " + offset);
			switch (_packet.State) {
				case ReadState.Type:
					if (remain < 1) {
						return;
					}
					break;
				case ReadState.Sequence:
					if (remain < 2) {
						return;
					}
					break;
				case ReadState.Length:
					if (remain < 4) {
						return;
					}
					break;
				case ReadState.Data:
					if (remain < _packet.DataLength) {
						return;
					}
					break;
			}

			switch (_packet.State) {
				case ReadState.Type:
					_packet.DataType = (DataType)_packet.ReadUint8(offset);
					_packet.DataOffset += 1;
					_packet.State = ReadState.Sequence;
					break;
				case ReadState.Sequence:
					_packet.SequenceId = _packet.ReadUint16LE(offset);
					_packet.DataOffset += 2;
					_packet.State = ReadState.Length;
					break;
				case ReadState.Length:
					_packet.DataLength = _packet.ReadUint32LE(offset);
					_packet.DataOffset += 4;
					_packet.State = ReadState.Data;
					break;
				case ReadState.Data:
					Emit("data", _packet.Clone());
					//string text = _packet.GetStringData(Encoding);
					//Console.WriteLine("*** DEBUG Data: {0}, {1}, {2}", _packet.SequenceId, text.Length, text);
					_packet.SliceData(offset + _packet.DataLength);
					_packet.DataOffset = 0;
					_packet.State = ReadState.Type;
					break;
			}

			OnData(null, 0);
		}
	}
}
