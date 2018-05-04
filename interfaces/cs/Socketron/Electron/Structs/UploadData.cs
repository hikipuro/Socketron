namespace Socketron {
	public class UploadData {
		/// <summary>
		/// Content being sent.
		/// </summary>
		public LocalBuffer bytes;
		/// <summary>
		/// Path of file being uploaded.
		/// </summary>
		public string file;
		/// <summary>
		/// UUID of blob data.
		/// Use ses.getBlobData method to retrieve the data.
		/// </summary>
		public string blobUUID;

		public static UploadData Parse(string text) {
			return JSON.Parse<UploadData>(text);
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

