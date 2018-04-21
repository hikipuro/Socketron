using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Socketron {
	[DataContract]
	public class Command {
		[DataMember(Name = "sequenceId", EmitDefaultValue = false)]
		public ushort? SequenceId = null;
		[DataMember(Name = "type")]
		public string Type = string.Empty;
		[DataMember(Name = "function")]
		public string Function = string.Empty;
		[DataMember(Name = "data")]
		public string Data = string.Empty;

		public Command() {
		}

		public static Command FromJson(string json, Encoding encoding = null) {
			if (encoding == null) {
				encoding = Encoding.UTF8;
			}
			Command command = null;
			using (var stream = new MemoryStream(encoding.GetBytes(json))) {
				var serializer = new DataContractJsonSerializer(typeof(Command));
				command = (Command)serializer.ReadObject(stream);
			}
			return command;
		}

		public string ToJson() {
			string json = string.Empty;
			using (var stream = new MemoryStream())
			using (var reader = new StreamReader(stream)) {
				var serializer = new DataContractJsonSerializer(typeof(Command));
				serializer.WriteObject(stream, this);
				stream.Position = 0;
				json = reader.ReadToEnd();
			}
			return json;
		}
	}

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

		public static Buffer CreateTextData(Command command, Encoding encoding = null) {
			if (encoding == null) {
				encoding = Encoding.UTF8;
			}
			string json = command.ToJson();
			int size = encoding.GetByteCount(json);
			if (size > ushort.MaxValue) {
				return null;
			}
			Buffer buffer = new Buffer();
			buffer.WriteUInt8(0);
			buffer.WriteUInt16LE((ushort)size);
			buffer.Write(json, encoding);
			return buffer;
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
	}
}
