using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// An alternate transport for Chrome's remote debugging protocol.
	/// <para>Process: Main</para>
	/// <para>
	/// Chrome Developer Tools has a special binding available at JavaScript runtime
	/// that allows interacting with pages and instrumenting them.
	/// </para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Debugger : EventEmitter {
		/// <summary>
		/// Debugger instance events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when debugging session is terminated.
			/// This happens either when webContents is closed
			/// or devtools is invoked for the attached webContents.
			/// </summary>
			public const string Detach = "detach";
			/// <summary>
			/// Emitted whenever debugging target issues instrumentation event.
			/// </summary>
			public const string Message = "message";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public Debugger() {
		}

		/// <summary>
		/// Attaches the debugger to the webContents.
		/// </summary>
		/// <param name="protocolVersion">
		/// (optional) Requested debugging protocol version.
		/// </param>
		public void attach(string protocolVersion = null) {
			if (protocolVersion == null) {
				API.Apply("attach");
			} else {
				API.Apply("attach", protocolVersion);
			}
		}

		/// <summary>
		/// Returns Boolean - Whether a debugger is attached to the webContents.
		/// </summary>
		/// <returns></returns>
		public bool isAttached() {
			return API.Apply<bool>("isAttached");
		}

		/// <summary>
		/// Detaches the debugger from the webContents.
		/// </summary>
		public void detach() {
			API.Apply("detach");
		}

		/// <summary>
		/// Send given command to the debugging target.
		/// </summary>
		/// <param name="method"></param>
		public void sendCommand(string method) {
			API.Apply("sendCommand", method);
		}

		public void sendCommand(string method, JsonObject commandParams) {
			API.Apply("sendCommand", method, commandParams);
		}

		public void sendCommand(string method, JsonObject commandParams, Action<Error, JsonObject> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_sendCommand";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				Error error = API.CreateObject<Error>(args[0]);
				JsonObject json = new JsonObject(args[1]);
				callback?.Invoke(error, json);
			});
			API.Apply("sendCommand", method, commandParams, item);
		}
	}
}
