namespace Socketron.Electron {
	public class UploadRawData {
		/// <summary>
		/// rawData.
		/// </summary>
		public string type;
		/// <summary>
		/// Data to be uploaded.
		/// </summary>
		public Buffer bytes;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
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
