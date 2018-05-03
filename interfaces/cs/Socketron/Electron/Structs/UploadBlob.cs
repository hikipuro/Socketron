namespace Socketron {
	public class UploadBlob {
		/// <summary>
		/// blob.
		/// </summary>
		public string type;
		/// <summary>
		/// UUID of blob data to upload.
		/// </summary>
		public string blobUUID;

		public static UploadBlob Parse(string text) {
			return JSON.Parse<UploadBlob>(text);
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

