using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Socketron {
	[DataContract]
	public class SocketronData {
		[DataMember(Name = "sequenceId", EmitDefaultValue = false)]
		public ushort? SequenceId = null;
		[DataMember(Name = "status")]
		public string Status = string.Empty;
		[DataMember(Name = "type")]
		public string Type = string.Empty;
		[DataMember(Name = "function")]
		public string Function = string.Empty;
		[DataMember(Name = "command")]
		public string Command = string.Empty;

		public SocketronData() {
		}

		public static SocketronData FromJson(string json, Encoding encoding = null) {
			if (encoding == null) {
				encoding = Encoding.UTF8;
			}
			SocketronData data = null;
			using (var stream = new MemoryStream(encoding.GetBytes(json))) {
				var serializer = new DataContractJsonSerializer(typeof(SocketronData));
				data = (SocketronData)serializer.ReadObject(stream);
			}
			return data;
		}

		public Buffer ToBuffer(DataType type, Encoding encoding = null) {
			if (encoding == null) {
				encoding = Encoding.UTF8;
			}
			string json = ToJson();
			int size = encoding.GetByteCount(json);
			if (size > ushort.MaxValue) {
				return null;
			}
			Buffer buffer = new Buffer();
			buffer.WriteUInt8((byte)type);
			buffer.WriteUInt16LE((ushort)size);
			buffer.Write(json, encoding);
			return buffer;
		}

		public string ToJson() {
			string json = string.Empty;
			using (var stream = new MemoryStream())
			using (var reader = new StreamReader(stream)) {
				var serializer = new DataContractJsonSerializer(typeof(SocketronData));
				serializer.WriteObject(stream, this);
				stream.Position = 0;
				json = reader.ReadToEnd();
			}
			return json;
		}
	}
}
