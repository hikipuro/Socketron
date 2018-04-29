using System.Web.Script.Serialization;

namespace Socketron {
	public class Certificate {
		public string data;
		public CertificatePrincipal issuer;
		public string issuerName;
		public Certificate issuerCert;
		public CertificatePrincipal subject;
		public string subjectName;
		public string serialNumber;
		public ulong validStart;
		public ulong validExpiry;
		public string fingerprint;

		public static Certificate Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<Certificate>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
