using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Manipulate the child browser window.
	/// <para>Process: Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class BrowserWindowProxy : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public BrowserWindowProxy() {
		}

		/// <summary>
		/// A Boolean that is set to true after the child window gets closed.
		/// </summary>
		public bool closed {
			get { return API.GetProperty<bool>("closed"); }
		}

		/// <summary>
		/// Removes focus from the child window.
		/// </summary>
		public void blur() {
			API.Apply("blur");
		}

		/// <summary>
		/// Forcefully closes the child window without calling its unload event.
		/// </summary>
		public void close() {
			API.Apply("close");
		}

		/// <summary>
		/// Evaluates the code in the child window.
		/// </summary>
		/// <param name="code"></param>
		public void eval(string code) {
			API.Apply("eval", code);
		}

		/// <summary>
		/// Focuses the child window (brings the window to front).
		/// </summary>
		public void focus() {
			API.Apply("focus");
		}

		/// <summary>
		/// Invokes the print dialog on the child window.
		/// </summary>
		public void print() {
			API.Apply("print");
		}

		/// <summary>
		/// Sends a message to the child window with the
		/// specified origin or * for no origin preference.
		/// <para>
		/// In addition to these methods, the child window implements
		/// window.opener object with no properties and a single method.
		/// </para>
		/// </summary>
		/// <param name="message"></param>
		/// <param name="targetOrigin"></param>
		public void postMessage(string message, string targetOrigin) {
			API.Apply("postMessage", message, targetOrigin);
		}
	}
}
