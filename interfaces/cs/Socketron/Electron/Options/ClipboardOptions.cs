namespace Socketron.Electron {
	/// <summary>
	/// Clipboard.readBookmark() return value.
	/// </summary>
	public class ReadBookmark {
		public string title;
		public string url;

		public static ReadBookmark FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new ReadBookmark() {
				title = json.String("title"),
				url = json.String("url")
			};
		}
	}

	/// <summary>
	/// Clipboard.write() options.
	/// </summary>
	public class Data {
		public string text;
		public string html;
		public NativeImage image;
		public string rtf;
		/// <summary>
		/// The title of the url at text.
		/// </summary>
		public string bookmark;
	}
}
