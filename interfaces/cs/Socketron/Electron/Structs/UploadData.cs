using System.Web.Script.Serialization;

namespace Socketron {
	public class UploadData {
		public Buffer bytes;
		public string file;
		public string blobUUID;

		public static UploadData Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<UploadData>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}

