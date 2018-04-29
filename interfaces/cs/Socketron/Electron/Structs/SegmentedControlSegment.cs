using System.Web.Script.Serialization;

namespace Socketron {
	public class SegmentedControlSegment {
		public string label;
		//public NativeImage icon;
		public bool? enabled;

		public static SegmentedControlSegment Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<SegmentedControlSegment>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
