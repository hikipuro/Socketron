namespace Socketron {
	/// <summary>
	/// Handle responses to HTTP/HTTPS requests.
	/// <para>Process: Main</para>
	/// </summary>
	public class IncomingMessage {
		public class Events {
			public const string Data = "data";
			public const string End = "end";
			public const string Aborted = "aborted";
			public const string Error = "error";
		}

		public int statusCode;
		public string statusMessage;
		public object headers;
		public string httpVersion;
		public int httpVersionMajor;
		public int httpVersionMinor;
	}
}
