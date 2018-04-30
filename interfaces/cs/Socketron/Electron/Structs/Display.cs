using System.Web.Script.Serialization;

namespace Socketron {
	public class Display {
		public long? id;
		public double? rotation;
		public double? scaleFactor;
		public string touchSupport;
		public Rectangle bounds;
		public Size size;
		public Rectangle workArea;
		public Size workAreaSize;

		public static Display Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<Display>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}

