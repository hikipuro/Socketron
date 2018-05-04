using System.Text;

namespace Socketron {
	public class SocketronData : JsonObject {
		public ushort? SequenceId {
			get { return this["sequenceId"] as ushort?; }
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
		public string Func {
			get { return this["func"] as string; }
			set { this["func"] = value; }
		}
		public object Params {
			get { return this["args"] as object; }
			set { this["args"] = value; }
		}

		public SocketronData() {
		}
		
		public static new SocketronData Parse(string json) {
			JsonObject jsonObject = JsonObject.Parse(json);
			SocketronData data = new SocketronData() {
				Status = jsonObject["status"] as string,
				Type = jsonObject["type"] as string,
				Func = jsonObject["func"] as string,
				Params = jsonObject["args"] as object
			};
			if (jsonObject["sequenceId"] != null) {
				int sequenceId = (int)jsonObject["sequenceId"];
				data.SequenceId = (ushort)sequenceId;
			}
			return data;
		}

		public new string Stringify() {
			if (this["args"] == null) {
				Remove("args");
			}
			if (this["sequenceId"] == null) {
				Remove("sequenceId");
			}
			return base.Stringify();
		}

		public LocalBuffer ToBuffer(DataType type, Encoding encoding = null) {
			if (encoding == null) {
				encoding = Encoding.UTF8;
			}
			string json = Stringify();
			int size = encoding.GetByteCount(json);
			if (size > ushort.MaxValue) {
				return null;
			}
			LocalBuffer buffer = new LocalBuffer();
			buffer.WriteUInt8((byte)type);
			buffer.WriteUInt16LE((ushort)size);
			buffer.Write(json, encoding);
			return buffer;
		}
	}
}
