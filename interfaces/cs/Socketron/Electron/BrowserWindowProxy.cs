using System;

namespace Socketron {
	/// <summary>
	/// Manipulate the child browser window.
	/// <para>Process: Renderer</para>
	/// </summary>
	public class BrowserWindowProxy {
		/// <summary>
		/// A Boolean that is set to true after the child window gets closed.
		/// </summary>
		public bool closed {
			get {
				return false;
			}
		}

		/// <summary>
		/// Removes focus from the child window.
		/// </summary>
		public void blur() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Forcefully closes the child window without calling its unload event.
		/// </summary>
		public void close() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Evaluates the code in the child window.
		/// </summary>
		/// <param name="code"></param>
		public void eval(string code) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Focuses the child window (brings the window to front).
		/// </summary>
		public void focus() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Invokes the print dialog on the child window.
		/// </summary>
		public void print() {
			// TODO: implement this
			throw new NotImplementedException();
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
			// TODO: implement this
			throw new NotImplementedException();
		}
	}
}
