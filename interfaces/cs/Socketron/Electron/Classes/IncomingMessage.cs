namespace Socketron.Electron {
	/// <summary>
	/// Handle responses to HTTP/HTTPS requests.
	/// <para>Process: Main</para>
	/// </summary>
	public class IncomingMessage : JSObject {
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

		public int statusCode {
			get { return API.GetProperty<int>("statusCode"); }
		}

		public string statusMessage {
			get { return API.GetProperty<string>("statusMessage"); }
		}

		public JsonObject headers {
			get {
				object result = API.GetProperty<object>("headers");
				return new JsonObject(result);
			}
		}

		public string httpVersion {
			get { return API.GetProperty<string>("httpVersion"); }
		}

		public int httpVersionMajor {
			get { return API.GetProperty<int>("httpVersionMajor"); }
		}

		public int httpVersionMinor {
			get { return API.GetProperty<int>("httpVersionMinor"); }
		}

		public EventEmitter on(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.on(eventName, listener);
		}

		public EventEmitter once(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.once(eventName, listener);
		}

		public EventEmitter removeListener(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.removeListener(eventName, listener);
		}

		public EventEmitter removeAllListeners(string eventName) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.removeAllListeners(eventName);
		}
	}
}
