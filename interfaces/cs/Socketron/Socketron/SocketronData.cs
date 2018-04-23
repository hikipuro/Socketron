using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace Socketron {
	[DataContract]
	public class Command {
		[DataMember(Name = "text")]
		public string Text;
	}

	[DataContract]
	public class SocketronData : JsonObject {
		/*
		[DataMember(Name = "sequenceId", Order = 0, EmitDefaultValue = false)]
		public ushort? SequenceId = null;
		[DataMember(Name = "status", Order = 1, EmitDefaultValue = false)]
		public string Status;
		[DataMember(Name = "type", Order = 2)]
		public string Type;
		[DataMember(Name = "func", Order = 3)]
		public string Function;
		[DataMember(Name = "args", Order = 4, EmitDefaultValue = false)]
		public object[] Arguments;
		*/

		public ushort SequenceId {
			get { return (ushort)this["sequenceId"]; }
			set { this["sequenceId"] = value; }
		}
		public string Status {
			get { return this["status"] as string; }
			set { this["status"] = value; }
		}
		public string Type {
			get { return this["type"] as string; }
			set { this["type"] = value; }
		}
		public string Function {
			get { return this["func"] as string; }
			set { this["func"] = value; }
		}
		public object[] Arguments {
			get { return this["args"] as object[]; }
			set { this["args"] = value; }
		}

		public SocketronData() {
		}
		
		public static new SocketronData Parse(string json) {
			/*
			if (encoding == null) {
				encoding = Encoding.UTF8;
			}
			SocketronData data = null;
			using (var stream = new MemoryStream(encoding.GetBytes(json))) {
				var serializer = new DataContractJsonSerializer(typeof(SocketronData));
				data = (SocketronData)serializer.ReadObject(stream);
			}
			//*/
			//JavaScriptSerializer serializer = new JavaScriptSerializer();
			//SocketronData data = serializer.Deserialize<SocketronData>(json);
			JsonObject jsonObject = JsonObject.Parse(json);
			SocketronData data = new SocketronData() {
				Status = jsonObject["status"] as string,
				Type = jsonObject["type"] as string,
				Function = jsonObject["func"] as string,
				Arguments = jsonObject["args"] as object[]
			};
			if (jsonObject["sequenceId"] != null) {
				int sequenceId = (int)jsonObject["sequenceId"];
				data.SequenceId = (ushort)sequenceId;
			}
			return data;
		}

		public Buffer ToBuffer(DataType type, Encoding encoding = null) {
			if (encoding == null) {
				encoding = Encoding.UTF8;
			}
			string json = Stringify();
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

		/*
		public string Stringify() {
			//JavaScriptSerializer serializer = new JavaScriptSerializer();
			//return serializer.Serialize(this);
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
		*/
	}
}
