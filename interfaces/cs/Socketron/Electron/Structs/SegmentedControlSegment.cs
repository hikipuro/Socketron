namespace Socketron {
	public class SegmentedControlSegment {
		public string label;
		public NativeImage icon;
		public bool? enabled;

		public static SegmentedControlSegment Parse(string text) {
			return JSON.Parse<SegmentedControlSegment>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
