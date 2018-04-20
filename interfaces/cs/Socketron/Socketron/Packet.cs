using System.Text;

namespace Socketron {
	public class Packet {
		public Buffer Data = null;
		public DataType DataType = DataType.Null;
		public ushort SequenceId = 0;
		public uint DataLength = 0;
		public uint DataOffset = 0;
		public ReadState State = ReadState.Type;
		public Encoding Encoding = Encoding.UTF8;

		public Packet() {
			Data = new Buffer();
		}

		public static Buffer CreateData(DataType dataType, ushort sequenceId, string data, Encoding encoding = null) {
			Buffer buffer = new Buffer();
			buffer.WriteUInt8((byte)dataType);
			buffer.WriteUInt16LE(sequenceId);
			buffer.WriteUInt32LE((uint)data.Length);
			buffer.Write(data, encoding);
			return buffer;
		}

		public static Buffer CreateData(DataType dataType, ushort sequenceId, Buffer data) {
			Buffer buffer = new Buffer();
			buffer.WriteUInt8((byte)dataType);
			buffer.WriteUInt16LE(sequenceId);
			buffer.WriteUInt32LE((uint)data.Length);
			buffer.Write(data);
			return buffer;
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

		public string GetStringData() {
			return Data.ToString(
				Encoding,
				(int)DataOffset,
				(int)DataOffset + (int)DataLength
			);
		}
	}
}
