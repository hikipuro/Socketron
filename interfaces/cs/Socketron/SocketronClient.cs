using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Socketron {
	public enum ReadState {
		Type = 0,
		CommandLength,
		Command
	}

	internal class SocketronClient: EventEmitter {
		public const int ReadBufferSize = 1024 * 8;
		public bool IsDebug = true;

		protected TcpClient _tcpClient;
		protected NetworkStream _stream;
		protected int _timeout = 10000;
		protected Encoding _encoding = Encoding.UTF8;
		protected Packet _packet = new Packet();
		protected byte[] _buffer = new byte[ReadBufferSize];
		protected Thread _readThread;
		protected ManualResetEvent _readDone = new ManualResetEvent(false);
		protected AsyncCallback _writeCallback;

		public SocketronClient() {
			_writeCallback = new AsyncCallback(_OnWrite);
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

		public void Connect(string hostname, int port) {
			Close();
			_tcpClient = new TcpClient();
			_tcpClient.NoDelay = true;
			_tcpClient.BeginConnect(
				hostname, port,
				new AsyncCallback(_OnConnect),
				_tcpClient
			);
		}

		public void Close() {
			Console.WriteLine("Socketron close");
			if (_stream != null) {
				_stream.Close();
				_stream = null;
			}
			if (_tcpClient != null) {
				_tcpClient.Close();
				//_tcpClient = null;
			}
			if (_readThread != null) {
				//_readDone.Set();
				_readThread.Abort();
				_readThread = null;
			}
		}

		public void Write(byte[] bytes) {
			try {
				//*
				_stream.BeginWrite(
					bytes, 0, bytes.Length,
					_writeCallback, _stream
				);
				//*/
				//_stream.Write(bytes, 0, bytes.Length);
			} catch (IOException) {
				Close();
			} catch (NullReferenceException) {
				Close();
			}
		}

		protected void _Read() {
			AsyncCallback callback = new AsyncCallback(_OnRead);
			TcpClient tcpClient = _tcpClient;
			NetworkStream stream = _stream;
			while (tcpClient.Connected && stream.CanRead) {
				do {
					//*
					stream.BeginRead(
						_buffer, 0, _buffer.Length,
						callback, stream
					);
					_readDone.WaitOne();
					_readDone.Reset();
					//*/
					/*
					int bytesReaded = _stream.Read(
						_buffer, 0, _buffer.Length
					);
					if (bytesReaded == 0) {
						Close();
						return;
					}
					_OnData(_buffer, bytesReaded);
					//*/
				} while (stream.DataAvailable);
				Thread.Sleep(TimeSpan.FromTicks(1));
			}
		}

		protected void _OnConnect(IAsyncResult result) {
			TcpClient client = (TcpClient)result.AsyncState;
			client.EndConnect(result);

			_DebugLog("Connected (Remote: {0}:{1} Local: {2}:{3})",
				((IPEndPoint)_tcpClient.Client.RemoteEndPoint).Address,
				((IPEndPoint)_tcpClient.Client.RemoteEndPoint).Port,
				((IPEndPoint)_tcpClient.Client.LocalEndPoint).Address,
				((IPEndPoint)_tcpClient.Client.LocalEndPoint).Port
			);

			_stream = _tcpClient.GetStream();
			_stream.ReadTimeout = _timeout;
			_stream.WriteTimeout = _timeout;

			_readThread = new Thread(_Read);
			_readThread.Name = "Socketron read thread";
			_readThread.Start();
			Emit("connect");
		}

		protected void _OnRead(IAsyncResult result) {
			NetworkStream stream = (NetworkStream)result.AsyncState;
			int bytesReaded = 0;
			try {
				bytesReaded = stream.EndRead(result);
			} catch (IOException) {
				_readDone.Set();
				return;
			} catch (ObjectDisposedException) {
				_readDone.Set();
				return;
			}
			if (bytesReaded == 0) {
				Close();
				_readDone.Set();
				return;
			}
			_OnData(_buffer, bytesReaded);
			_readDone.Set();
		}

		protected void _OnWrite(IAsyncResult result) {
			//NetworkStream stream = (NetworkStream)result.AsyncState;
			_stream.EndWrite(result);
		}

		protected void _OnData(byte[] data, int bytesReaded) {
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
						_DebugLog("receive: {0}", text);
						EmitNewThread("data", SocketronData.Parse(text));
					}
					_packet.Data = _packet.Data.Slice(offset + _packet.DataLength);
					_packet.DataOffset = 0;
					_packet.State = ReadState.Type;
					break;
			}

			_OnData(null, 0);
		}

		protected void _DebugLog(string format, params object[] args) {
			if (!IsDebug) {
				return;
			}
			format = string.Format("[{0}] {1}", typeof(Socketron).Name, format);
			EmitNewThread("debug", string.Format(format, args));
		}
	}
}
