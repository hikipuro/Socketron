namespace Socketron {
	public class CertificatePrincipal {
		public string commonName;
		public string[] organizations;
		public string[] organizationUnits;
		public string locality;
		public string state;
		public string country;

		public static CertificatePrincipal Parse(string text) {
			return JSON.Parse<CertificatePrincipal>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
