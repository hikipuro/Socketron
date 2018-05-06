namespace Socketron.Electron {
	public class FileFilter {
		public string name;
		public string[] extensions;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static FileFilter Parse(string text) {
			return JSON.Parse<FileFilter>(text);
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

