namespace Socketron {
	public class MimeTypedBuffer {
		public string mimeType;
		public Buffer data;

		public static MimeTypedBuffer Parse(string text) {
			return JSON.Parse<MimeTypedBuffer>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
