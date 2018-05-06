namespace Socketron.Electron {
	public class CertificatePrincipal {
		/// <summary>
		/// Common Name.
		/// </summary>
		public string commonName;
		/// <summary>
		/// Organization names.
		/// </summary>
		public string[] organizations;
		/// <summary>
		/// Organization Unit names.
		/// </summary>
		public string[] organizationUnits;
		/// <summary>
		/// Locality.
		/// </summary>
		public string locality;
		/// <summary>
		/// State or province.
		/// </summary>
		public string state;
		/// <summary>
		/// Country or region.
		/// </summary>
		public string country;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static CertificatePrincipal Parse(string text) {
			return JSON.Parse<CertificatePrincipal>(text);
		}

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <returns></returns>
		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
