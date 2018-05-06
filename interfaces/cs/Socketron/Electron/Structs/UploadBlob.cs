namespace Socketron.Electron {
	public class UploadBlob {
		/// <summary>
		/// blob.
		/// </summary>
		public string type;
		/// <summary>
		/// UUID of blob data to upload.
		/// </summary>
		public string blobUUID;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
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

