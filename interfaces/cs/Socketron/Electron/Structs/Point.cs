using System.Web.Script.Serialization;

namespace Socketron {
	public class Point {
		public int x;
		public int y;

		public static Point Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<Point>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
