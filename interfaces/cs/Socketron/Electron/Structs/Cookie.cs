namespace Socketron {
	public class Cookie {
		public string name;
		public string value;
		public string domain;
		public bool? hostOnly;
		public string path;
		public bool? secure;
		public bool? httpOnly;
		public bool? session;
		public double expirationDate;

		public static Cookie Parse(string text) {
			return JSON.Parse<Cookie>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

