namespace Socketron {
	public class UploadBlob {
		public string type;
		public string blobUUID;

		public static UploadBlob Parse(string text) {
			return JSON.Parse<UploadBlob>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

