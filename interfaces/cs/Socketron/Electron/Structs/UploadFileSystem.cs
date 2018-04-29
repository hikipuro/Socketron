using System.Web.Script.Serialization;

namespace Socketron {
	public class UploadFileSystem {
		public string type;
		public string filsSystemURL;
		public int? offset;
		public int? length;
		public double? modificationTime;

		public static UploadFileSystem Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<UploadFileSystem>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}

