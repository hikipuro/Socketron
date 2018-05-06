namespace Socketron.Electron {
	public class RemovePassword {
		/// <summary>
		/// password.
		/// </summary>
		public string type;
		/// <summary>
		/// (optional) When provided, the authentication info related to the origin
		/// will only be removed otherwise the entire cache will be cleared.
		/// </summary>
		public string origin;
		/// <summary>
		/// (optional) Scheme of the authentication.
		/// Can be basic, digest, ntlm, negotiate.
		/// Must be provided if removing by origin.
		/// </summary>
		public string scheme;
		/// <summary>
		/// (optional) Realm of the authentication.
		/// Must be provided if removing by origin.
		/// </summary>
		public string realm;
		/// <summary>
		/// (optional) Credentials of the authentication.
		/// Must be provided if removing by origin.
		/// </summary>
		public string username;
		/// <summary>
		/// (optional) Credentials of the authentication.
		/// Must be provided if removing by origin.
		/// </summary>
		public string password;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static RemovePassword Parse(string text) {
			return JSON.Parse<RemovePassword>(text);
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
