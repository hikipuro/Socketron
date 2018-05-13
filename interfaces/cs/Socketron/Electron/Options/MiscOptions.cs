namespace Socketron.Electron {
	public class AuthInfo {
		public bool isProxy;
		public string scheme;
		public string host;
		public int port;
		public string realm;
	}

	public class Request {
		public string method;
		public string url;
		public string referrer;
	}

	public class Result {
		public int requestId;
		/// <summary>
		/// Position of the active match.
		/// </summary>
		public int activeMatchOrdinal;
		/// <summary>
		/// Number of Matches.
		/// </summary>
		public int matches;
		/// <summary>
		/// Coordinates of first match region.
		/// </summary>
		public JsonObject selectionArea;
		public bool finalUpdate;
	}
}
