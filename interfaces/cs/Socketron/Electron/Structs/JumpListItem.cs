namespace Socketron {
	public class JumpListItem {
		public string type;
		public string path;
		public string program;
		public string args;
		public string title;
		public string description;
		public string iconPath;
		public int? iconIndex;

		public static JumpListItem Parse(string text) {
			return JSON.Parse<JumpListItem>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

