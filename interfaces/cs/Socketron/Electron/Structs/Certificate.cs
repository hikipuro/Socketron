namespace Socketron.Electron {
	public class Certificate {
		/// <summary>
		/// PEM encoded data.
		/// </summary>
		public string data;
		/// <summary>
		/// Issuer principal.
		/// </summary>
		public CertificatePrincipal issuer;
		/// <summary>
		/// Issuer's Common Name.
		/// </summary>
		public string issuerName;
		/// <summary>
		/// Issuer certificate (if not self-signed).
		/// </summary>
		public Certificate issuerCert;
		/// <summary>
		/// Subject principal.
		/// </summary>
		public CertificatePrincipal subject;
		/// <summary>
		/// Subject's Common Name.
		/// </summary>
		public string subjectName;
		/// <summary>
		/// Hex value represented string.
		/// </summary>
		public string serialNumber;
		/// <summary>
		/// Start date of the certificate being valid in seconds.
		/// </summary>
		public ulong validStart;
		/// <summary>
		/// End date of the certificate being valid in seconds.
		/// </summary>
		public ulong validExpiry;
		/// <summary>
		/// Fingerprint of the certificate.
		/// </summary>
		public string fingerprint;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static Certificate Parse(string text) {
			return JSON.Parse<Certificate>(text);
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
