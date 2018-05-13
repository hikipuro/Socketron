namespace Socketron.Electron {
	/// <summary>
	/// Cookies.get() options.
	/// </summary>
	public class Filter {
		/// <summary>
		/// Retrieves cookies which are associated with url.
		/// Empty implies retrieving cookies of all urls.
		/// </summary>
		public string url;
		/// <summary>
		/// Filters cookies by name.
		/// </summary>
		public string name;
		/// <summary>
		/// Retrieves cookies whose domains match or are subdomains of domains
		/// </summary>
		public string domain;
		/// <summary>
		/// Retrieves cookies whose path matches path.
		/// </summary>
		public string path;
		/// <summary>
		/// Filters cookies by their Secure property.
		/// </summary>
		public bool? secure;
		/// <summary>
		/// Filters out session or persistent cookies.
		/// </summary>
		public bool? session;
	}

	/// <summary>
	/// Cookies.set() options.
	/// </summary>
	public class Details {
		/// <summary>
		/// The url to associate the cookie with.
		/// </summary>
		public string url;
		/// <summary>
		/// The name of the cookie. Empty by default if omitted.
		/// </summary>
		public string name;
		/// <summary>
		/// The value of the cookie. Empty by default if omitted.
		/// </summary>
		public string value;
		/// <summary>
		/// The domain of the cookie. Empty by default if omitted.
		/// </summary>
		public string domain;
		/// <summary>
		/// The path of the cookie. Empty by default if omitted.
		/// </summary>
		public string path;
		/// <summary>
		/// Whether the cookie should be marked as Secure. Defaults to false.
		/// </summary>
		public bool? secure;
		/// <summary>
		/// Whether the cookie should be marked as HTTP only. Defaults to false.
		/// </summary>
		public bool? httpOnly;
		/// <summary>
		/// The expiration date of the cookie as the number of seconds since the UNIX epoch.
		/// If omitted then the cookie becomes a session cookie and will not be retained
		/// between sessions.
		/// </summary>
		public long? expirationDate;
	}
}
