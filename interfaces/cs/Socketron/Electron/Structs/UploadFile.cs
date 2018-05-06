namespace Socketron.Electron {
	public class UploadFile {
		/// <summary>
		/// file.
		/// </summary>
		public string type;
		/// <summary>
		/// Path of file to be uploaded.
		/// </summary>
		public string filePath;
		/// <summary>
		/// Defaults to 0.
		/// </summary>
		public int? offset;
		/// <summary>
		/// Number of bytes to read from offset. Defaults to 0.
		/// </summary>
		public int? length;
		/// <summary>
		/// Last Modification time in number of seconds since the UNIX epoch.
		/// </summary>
		public double? modificationTime;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static UploadFile Parse(string text) {
			return JSON.Parse<UploadFile>(text);
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
