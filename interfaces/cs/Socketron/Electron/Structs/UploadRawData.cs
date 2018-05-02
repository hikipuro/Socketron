namespace Socketron {
	public class UploadRawData {
		public string type;
		public Buffer bytes;

		public static UploadRawData Parse(string text) {
			return JSON.Parse<UploadRawData>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
