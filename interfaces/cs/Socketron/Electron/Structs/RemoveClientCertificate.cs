namespace Socketron {
	public class RemoveClientCertificate {
		public string type;
		public string origin;

		public static RemoveClientCertificate Parse(string text) {
			return JSON.Parse<RemoveClientCertificate>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
