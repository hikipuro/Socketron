using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socketron {
	internal class SocketronClient: EventEmitter {
		public event Action<string> Callback;

		public const int ReadBufferSize = 1024;
		public Encoding Encoding = Encoding.UTF8;

		protected TcpClient _tcpClient;
		protected NetworkStream _stream;
		protected int _timeout = 10000;

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
			while (_tcpClient.Connected && _stream.CanRead) {
				MemoryStream memoryStream = new MemoryStream();
				byte[] bytes = new byte[ReadBufferSize];
				int bytesReaded = 0;
				do {
					bytesReaded = await _stream.ReadAsync(bytes, 0, bytes.Length);
					if (bytesReaded == 0) {
						Close();
						return;
					}
					memoryStream.Write(bytes, 0, bytesReaded);
				} while (_stream.DataAvailable || bytes[bytesReaded - 1] != '\n');

				string message = Encoding.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
				memoryStream.Close();
				message = message.TrimEnd('\n');
				Emit("debug", "Read: " + message);

				if (message.IndexOf("jquery") >= 0) {
					Callback?.Invoke(message);
				}
			}
			Console.WriteLine("[Close]");
		}
	}
}
