namespace Socketron.Electron {
	public class UploadFileSystem {
		/// <summary>
		/// fileSystem.
		/// </summary>
		public string type;
		/// <summary>
		/// FileSystem url to read data for upload.
		/// </summary>
		public string filsSystemURL;
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
		public static UploadFileSystem Parse(string text) {
			return JSON.Parse<UploadFileSystem>(text);
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

