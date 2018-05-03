namespace Socketron {
	public class Referrer {
		/// <summary>
		/// HTTP Referrer URL.
		/// </summary>
		public string url;
		/// <summary>
		/// Can be default, unsafe-url, no-referrer-when-downgrade, no-referrer,
		/// origin, strict-origin-when-cross-origin, same-origin, strict-origin,
		/// or no-referrer.
		/// See the Referrer-Policy spec for more details on the meaning of these values.
		/// </summary>
		public string policy;

		public static Referrer Parse(string text) {
			return JSON.Parse<Referrer>(text);
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
