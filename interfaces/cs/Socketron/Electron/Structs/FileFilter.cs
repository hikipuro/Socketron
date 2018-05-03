namespace Socketron {
	public class FileFilter {
		public string name;
		public string[] extensions;

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

