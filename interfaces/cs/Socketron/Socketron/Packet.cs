using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Socketron {
	public class Packet {
		public Buffer Data = null;
		public DataType DataType = DataType.Text;
		public uint DataLength = 0;
		public uint DataOffset = 0;
		public ReadState State = ReadState.Type;
		public Encoding Encoding = Encoding.UTF8;

		public Packet() {
			Data = new Buffer();
		}

		public Packet Clone() {
			Packet packet = new Packet();
			;
			packet.Data = Data;
			packet.DataType = DataType;
			packet.DataLength = DataLength;
			packet.DataOffset = DataOffset;
			packet.State = State;
			return packet;
		}

		public string GetStringData() {
			return Data.ToString(
				Encoding,
				(int)DataOffset,
				(int)DataOffset + (int)DataLength
			);
		}

		protected static byte[] Compress(string text, Encoding encoding) {
			byte[] textBytes = encoding.GetBytes(text);
			MemoryStream stream = new MemoryStream();
			using (DeflateStream deflate = new DeflateStream(stream, CompressionMode.Compress, true)) {
				deflate.Write(textBytes, 0, textBytes.Length);
			}
			byte[] bytes = new byte[stream.Length];
			stream.Position = 0;
			stream.Read(bytes, 0, bytes.Length);
			stream.Close();
			return bytes;
		}
	}
}
