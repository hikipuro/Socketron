namespace Socketron {
	public class WebSource {
		public string code;
		/// <summary>
		/// (optional)
		/// </summary>
		public string url;
		/// <summary>
		/// (optional) Default is 1.
		/// </summary>
		public int? startLine;

		public static WebSource Parse(string text) {
			return JSON.Parse<WebSource>(text);
		}

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <returns></returns>
		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
