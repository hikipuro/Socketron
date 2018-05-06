namespace Socketron.Electron {
	/// <summary>
	/// Handle responses to HTTP/HTTPS requests.
	/// <para>Process: Main</para>
	/// </summary>
	public class IncomingMessage {
		/// <summary>
		/// IncomingMessage instance events.
		/// </summary>
		public class Events {
			/// <summary>
			/// The data event is the usual method of transferring
			/// response data into applicative code.
			/// </summary>
			public const string Data = "data";
			/// <summary>
			/// Indicates that response body has ended.
			/// </summary>
			public const string End = "end";
			/// <summary>
			/// Emitted when a request has been canceled during an ongoing HTTP transaction.
			/// </summary>
			public const string Aborted = "aborted";
			/// <summary>
			/// Emitted when an error was encountered while streaming response data events.
			/// </summary>
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
