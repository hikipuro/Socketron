using System.Web.Script.Serialization;

namespace Socketron {
	public class Size {
		public int width;
		public int height;

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
