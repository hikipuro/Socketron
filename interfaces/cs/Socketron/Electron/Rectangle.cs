using System.Web.Script.Serialization;

namespace Socketron {
	public class Rectangle {
		public int x;
		public int y;
		public int width;
		public int height;

		public static Rectangle Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<Rectangle>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
