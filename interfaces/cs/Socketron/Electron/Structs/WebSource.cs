namespace Socketron {
	public class WebSource {
		public string code;
		public string url;
		public int? startLine;

		public static WebSource Parse(string text) {
			return JSON.Parse<WebSource>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
