using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Error object of the Node API.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Error : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public Error(SocketronClient client, int id) {
			API.client = client;
			API.id = id;
		}

		public static void captureStackTrace() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public static int stackTraceLimit() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public string code {
			get {
				string script = ScriptBuilder.Build(
					"return {0}.code;",
					Script.GetObject(API.id)
				);
				return API._ExecuteBlocking<string>(script);
			}
		}

		public string message {
			get {
				string script = ScriptBuilder.Build(
					"return {0}.message;",
					Script.GetObject(API.id)
				);
				return API._ExecuteBlocking<string>(script);
			}
		}

		public string stack {
			get {
				string script = ScriptBuilder.Build(
					"return {0}.stack;",
					Script.GetObject(API.id)
				);
				return API._ExecuteBlocking<string>(script);
			}
		}

		public string toString() {
			string script = ScriptBuilder.Build(
				"return {0}.toString();",
				Script.GetObject(API.id)
			);
			return API._ExecuteBlocking<string>(script);
		}
	}
}
