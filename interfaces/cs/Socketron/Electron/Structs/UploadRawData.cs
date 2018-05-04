namespace Socketron {
	public class UploadRawData {
		/// <summary>
		/// rawData.
		/// </summary>
		public string type;
		/// <summary>
		/// Data to be uploaded.
		/// </summary>
		public LocalBuffer bytes;

		public static UploadRawData Parse(string text) {
			return JSON.Parse<UploadRawData>(text);
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
