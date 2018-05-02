namespace Socketron {
	public class RemovePassword {
		public string type;
		public string origin;
		public string scheme;
		public string realm;
		public string username;
		public string password;

		public static RemovePassword Parse(string text) {
			return JSON.Parse<RemovePassword>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
