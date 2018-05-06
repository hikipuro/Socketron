namespace Socketron.Electron {
	public class Cookie {
		/// <summary>
		/// The name of the cookie.
		/// </summary>
		public string name;
		/// <summary>
		/// The value of the cookie.
		/// </summary>
		public string value;
		/// <summary>
		/// (optional) The domain of the cookie.
		/// </summary>
		public string domain;
		/// <summary>
		/// (optional) Whether the cookie is a host-only cookie.
		/// </summary>
		public bool? hostOnly;
		/// <summary>
		/// (optional) The path of the cookie.
		/// </summary>
		public string path;
		/// <summary>
		/// (optional) Whether the cookie is marked as secure.
		/// </summary>
		public bool? secure;
		/// <summary>
		/// (optional) Whether the cookie is marked as HTTP only.
		/// </summary>
		public bool? httpOnly;
		/// <summary>
		/// (optional) Whether the cookie is a session cookie or a persistent cookie with an expiration date.
		/// </summary>
		public bool? session;
		/// <summary>
		/// (optional) The expiration date of the cookie as the number of seconds since the UNIX epoch.
		/// Not provided for session cookies.
		/// </summary>
		public double? expirationDate;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static Cookie Parse(string text) {
			return JSON.Parse<Cookie>(text);
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

