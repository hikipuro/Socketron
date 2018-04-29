using System.Web.Script.Serialization;

namespace Socketron {
	public class ScrubberItem {
		public string label;
		//public NativeImage icon;

		public static ScrubberItem Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<ScrubberItem>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
