using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Socketron {
	internal class SocketClient: LocalEventEmitter {
		public Config Config = new Config();
		protected TcpClient _tcpClient;
		protected NetworkStream _stream;
		protected Payload _payload = new Payload();
		protected byte[] _buffer;
		protected Thread _readThread;
		//protected ManualResetEvent _readDone = new ManualResetEvent(false);
		protected AsyncCallback _writeCallback;

		public SocketClient(Config config = null) {
			if (config != null) {
				Config = config;
			}
			_buffer = new byte[Config.ReadBufferSize];
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

		public void Connect(string hostname, int port) {
			Close();
			_tcpClient = new TcpClient() {
				NoDelay = true
			};
			IAsyncResult result = _tcpClient.BeginConnect(
				hostname, port, null, null
			);
			bool success = result.AsyncWaitHandle.WaitOne(
				Config.Timeout, true
			);
			if (success) {
				_tcpClient.EndConnect(result);
			} else {
				_tcpClient.Close();
				throw new TimeoutException();
			}

			_DebugLog("Connected (Remote: {0}:{1} Local: {2}:{3})",
				((IPEndPoint)_tcpClient.Client.RemoteEndPoint).Address,
				((IPEndPoint)_tcpClient.Client.RemoteEndPoint).Port,
				((IPEndPoint)_tcpClient.Client.LocalEndPoint).Address,
				((IPEndPoint)_tcpClient.Client.LocalEndPoint).Port
			);

			_stream = _tcpClient.GetStream();
			//_stream.ReadTimeout = Config.Timeout;
			//_stream.WriteTimeout = Config.Timeout;

			_readThread = new Thread(_Read) {
				Name = typeof(SocketronClient).Name + " read thread"
			};
			_readThread.Start();
			Emit("connect");
		}

		public void Close() {
			_DebugLog("SocketClient close");
			if (_stream != null) {
				_stream.Close();
				_stream = null;
			}
			if (_tcpClient != null) {
				_tcpClient.Close();
				_tcpClient = null;
			}
			/*
			if (_readThread != null) {
				//_readDone.Set();
				_readThread.Abort();
				_readThread = null;
			}
			//*/
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
				_DebugLog("Write IOException");
				Close();
			} catch (InvalidOperationException) {
				_DebugLog("Write InvalidOperationException");
				Close();
			} catch (SocketException) {
				_DebugLog("Write SocketException");
				Close();
			} catch (NullReferenceException) {
				_DebugLog("Write NullReferenceException");
				Close();
			}
		}

		protected void _Read() {
			AsyncCallback callback = new AsyncCallback(_OnRead);
			TcpClient tcpClient = _tcpClient;
			NetworkStream stream = _stream;
			while (tcpClient.Connected && stream.CanRead) {
				do {
					/*
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
					IAsyncResult result = stream.BeginRead(
						_buffer, 0, _buffer.Length, null, null
					);
					bool success = result.AsyncWaitHandle.WaitOne();
					if (success) {
						int bytesReaded = 0;
						try {
							bytesReaded = stream.EndRead(result);
						} catch (IOException) {
							_DebugLog("_OnRead IOException");
							break;
						} catch (ObjectDisposedException) {
							_DebugLog("_OnRead ObjectDisposedException");
							break;
						}
						if (bytesReaded == 0) {
							break;
						}
						_OnData(_buffer, bytesReaded);
					} else {
						break;
					}
				} while (stream.DataAvailable);
				//Thread.Sleep(TimeSpan.FromTicks(1));
			}
		}

		protected void _OnRead(IAsyncResult result) {
			/*
			NetworkStream stream = (NetworkStream)result.AsyncState;
			int bytesReaded = 0;
			try {
				bytesReaded = stream.EndRead(result);
			} catch (IOException) {
				_DebugLog("_OnRead IOException");
				_readDone.Set();
				return;
			} catch (ObjectDisposedException) {
				_DebugLog("_OnRead ObjectDisposedException");
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
			*/
		}

		protected void _OnWrite(IAsyncResult result) {
			//NetworkStream stream = (NetworkStream)result.AsyncState;
			_stream.EndWrite(result);
		}

		protected void _OnData(byte[] data, int bytesReaded) {
			if (data != null) {
				_payload.Data.Write(data, 0, bytesReaded);
			}

			uint offset = _payload.DataOffset;
			uint remain = (uint)_payload.Data.Length - offset;

			switch (_payload.State) {
				case ReadState.Type:
					if (remain < 1) {
						return;
					}
					break;
				case ReadState.CommandLength:
					switch (_payload.DataType) {
						case DataType.Text16:
							if (remain < 2) {
								return;
							}
							break;
						case DataType.Text32:
							if (remain < 4) {
								return;
							}
							break;
					}
					break;
				case ReadState.Command:
					if (remain < _payload.DataLength) {
						return;
					}
					break;
			}

			switch (_payload.State) {
				case ReadState.Type:
					_payload.DataType = (DataType)_payload.Data[offset];
					_payload.DataOffset += 1;
					_payload.State = ReadState.CommandLength;
					//Debug.WriteLine("_payload.DataType: " + _payload.DataType);
					break;
				case ReadState.CommandLength:
					switch (_payload.DataType) {
						case DataType.Text16:
							_payload.DataLength = _payload.Data.ReadUInt16LE(offset);
							_payload.DataOffset += 2;
							break;
						case DataType.Text32:
							_payload.DataLength = _payload.Data.ReadUInt32LE(offset);
							_payload.DataOffset += 4;
							break;
					}
					//Debug.WriteLine("_payload.DataLength: " + _payload.DataLength);
					_payload.State = ReadState.Command;
					break;
				case ReadState.Command:
					switch (_payload.DataType) {
						case DataType.Text16:
						case DataType.Text32:
							string text = _payload.GetStringData();
							if (Config.IsDebug && Config.EnableDebugPayloads) {
								_DebugLog("receive: {0}", text);
							}
							EmitNewThread("data", SocketronData.Parse(text));
							break;
					}
					_payload.Data = _payload.Data.Slice(offset + _payload.DataLength);
					_payload.DataOffset = 0;
					_payload.State = ReadState.Type;
					break;
			}

			_OnData(null, 0);
		}

		protected void _DebugLog(string format, params object[] args) {
			Emit("debug", string.Format(format, args));
		}
	}
}
