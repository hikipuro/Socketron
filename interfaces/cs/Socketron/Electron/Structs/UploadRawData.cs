using System.Web.Script.Serialization;

namespace Socketron {
	public class UploadRawData {
		public string type;
		public Buffer bytes;

		public static UploadRawData Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<UploadRawData>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
