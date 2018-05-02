namespace Socketron {
	public class UploadData {
		public Buffer bytes;
		public string file;
		public string blobUUID;

		public static UploadData Parse(string text) {
			return JSON.Parse<UploadData>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

