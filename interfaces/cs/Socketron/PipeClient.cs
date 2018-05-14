using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace Socketron {
	public class PipeClient : Client {
		public LocalConfig Config = new LocalConfig();
		protected Thread _readThread;
		protected NamedPipeClientStream _stream;
		protected Payload _payload = new Payload();
		protected AsyncCallback _writeCallback;

		public PipeClient(LocalConfig config = null) {
			if (config != null) {
				Config = config;
			}
			_writeCallback = new AsyncCallback(_OnWrite);
		}

		public override bool IsConnected {
			get {
				if (_stream != null && _stream.IsConnected) {
					return true;
				}
				return false;
			}
		}

		public override void Connect(string hostname, string pipename) {
			_stream = new NamedPipeClientStream(
				hostname, pipename,
				PipeDirection.InOut,
				PipeOptions.WriteThrough | PipeOptions.Asynchronous
			);
			_stream.Connect(Config.Timeout);

			_readThread = new Thread(_Read) {
				Name = typeof(SocketronClient).Name + " read thread"
			};
			_readThread.Start();
			Emit("connect");
		}

		public override void Close() {
			_DebugLog("PipeClient close");
			if (_stream != null) {
				_stream.Close();
				_stream = null;
			}
		}

		public override void Write(byte[] bytes) {
			try {
				//*
				_stream.BeginWrite(
					bytes, 0, bytes.Length,
					_writeCallback, _stream
				);
				//*/
				//_stream.Write(bytes, 0, bytes.Length);
				//_stream.Flush();
			} catch (IOException) {
				_DebugLog("Write IOException");
				Close();
			} catch (InvalidOperationException) {
				_DebugLog("Write InvalidOperationException");
				Close();
			} catch (NullReferenceException) {
				_DebugLog("Write NullReferenceException");
				Close();
			}
		}

		protected void _Read() {
			NamedPipeClientStream stream = _stream;
			byte[] _buffer = new byte[LocalConfig.ReadBufferSize];
			while (stream.IsConnected && stream.CanRead) {
				do {
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
				} while (stream.IsConnected);
				//Thread.Sleep(TimeSpan.FromTicks(1));
			}
		}

		protected void _OnWrite(IAsyncResult result) {
			//NetworkStream stream = (NetworkStream)result.AsyncState;
			try {
				_stream.EndWrite(result);
			} catch (IOException) {
				_DebugLog("_OnWrite IOException");
			} catch (NullReferenceException) {
				_DebugLog("_OnWrite NullReferenceException");
			}
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
					var newData = _payload.Data.Slice(offset + _payload.DataLength);
					_payload.Data.Dispose();
					_payload.Data = newData;
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
