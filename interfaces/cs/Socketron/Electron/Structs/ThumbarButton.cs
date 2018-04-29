using System.Web.Script.Serialization;

namespace Socketron {
	public class ThumbarButton {
		//public NativeImage icon;
		//public Function click;
		public string tooltip;
		public string[] flags;

		public static ThumbarButton Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<ThumbarButton>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
