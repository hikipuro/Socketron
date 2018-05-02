namespace Socketron {
	public class ShortcutDetails {
		public string target;
		public string cwd;
		public string args;
		public string description;
		public string icon;
		public int? iconIndex;
		public string appUserModelId;

		public static ShortcutDetails Parse(string text) {
			return JSON.Parse<ShortcutDetails>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
