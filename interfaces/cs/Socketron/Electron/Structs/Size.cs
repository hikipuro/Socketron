using System.Web.Script.Serialization;

namespace Socketron {
	public class Size {
		public int width;
		public int height;

		public static Size FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new Size() {
				width = json.Int32("width"),
				height = json.Int32("height")
			};
		}

		public static Size Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<Size>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
