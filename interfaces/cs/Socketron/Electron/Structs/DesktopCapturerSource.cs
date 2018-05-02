namespace Socketron {
	public class DesktopCapturerSource {
		public string id;
		public string name;
		public NativeImage thumbnail;
		public string display_id;

		public static DesktopCapturerSource Parse(string text) {
			return JSON.Parse<DesktopCapturerSource>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

