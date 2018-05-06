namespace Socketron.Electron {
	public class MimeTypedBuffer {
		/// <summary>
		/// The mimeType of the Buffer that you are sending.
		/// </summary>
		public string mimeType;
		/// <summary>
		/// The actual Buffer content.
		/// </summary>
		public LocalBuffer data;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static MimeTypedBuffer Parse(string text) {
			return JSON.Parse<MimeTypedBuffer>(text);
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
