using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Error object of the Node API.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Error : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public Error() {
		}

		public static void captureStackTrace(JsonObject targetObject) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public static int stackTraceLimit {
			get {
				// TODO: implement this
				throw new NotImplementedException();
			}
		}

		public string code {
			get { return API.GetProperty<string>("code"); }
		}

		public string message {
			get { return API.GetProperty<string>("message"); }
		}

		public string stack {
			get { return API.GetProperty<string>("stack"); }
		}

		public string toString() {
			return API.Apply<string>("toString");
		}
	}
}
