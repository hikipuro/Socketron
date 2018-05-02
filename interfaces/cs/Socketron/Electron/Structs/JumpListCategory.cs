namespace Socketron {
	public class JumpListCategory {
		public string type;
		public string name;
		public JumpListItem[] items;

		public static JumpListCategory Parse(string text) {
			return JSON.Parse<JumpListCategory>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

