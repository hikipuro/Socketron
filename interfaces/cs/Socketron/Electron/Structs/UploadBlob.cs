using System.Web.Script.Serialization;

namespace Socketron {
	public class UploadBlob {
		public string type;
		public string blobUUID;

		public static UploadBlob Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<UploadBlob>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}

