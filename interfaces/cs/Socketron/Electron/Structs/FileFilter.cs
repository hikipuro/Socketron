namespace Socketron {
	public class FileFilter {
		public string name;
		public string[] extensions;

		public static FileFilter Parse(string text) {
			return JSON.Parse<FileFilter>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

