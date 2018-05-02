namespace Socketron {
	public class ScrubberItem {
		public string label;
		public NativeImage icon;

		public static ScrubberItem Parse(string text) {
			return JSON.Parse<ScrubberItem>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
