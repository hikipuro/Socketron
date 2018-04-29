using System.Web.Script.Serialization;

namespace Socketron {
	public class UploadFile {
		public string type;
		public string filePath;
		public int? offset;
		public int? length;
		public double? modificationTime;

		public static UploadFile Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<UploadFile>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
