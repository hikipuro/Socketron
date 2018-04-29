using System.Web.Script.Serialization;

namespace Socketron {
	public class MimeTypedBuffer {
		public string mimeType;
		public Buffer data;

		public static MimeTypedBuffer Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<MimeTypedBuffer>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
