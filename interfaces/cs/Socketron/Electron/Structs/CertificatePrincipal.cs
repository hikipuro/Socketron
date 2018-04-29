using System.Web.Script.Serialization;

namespace Socketron {
	public class CertificatePrincipal {
		public string commonName;
		public string[] organizations;
		public string[] organizationUnits;
		public string locality;
		public string state;
		public string country;

		public static CertificatePrincipal Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<CertificatePrincipal>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
