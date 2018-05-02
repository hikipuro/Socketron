namespace Socketron {
	public class Referrer {
		public string url;
		public string policy;

		public static Referrer Parse(string text) {
			return JSON.Parse<Referrer>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
