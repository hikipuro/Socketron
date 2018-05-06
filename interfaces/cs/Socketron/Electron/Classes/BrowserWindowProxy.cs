using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Manipulate the child browser window.
	/// <para>Process: Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class BrowserWindowProxy : NodeModule {
		/// <summary>
		/// A Boolean that is set to true after the child window gets closed.
		/// </summary>
		public bool closed {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return {0}.closed;"
					),
					Script.GetObject(_id)
				);
				return _ExecuteBlocking<bool>(script);
			}
		}

		/// <summary>
		/// Removes focus from the child window.
		/// </summary>
		public void blur() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.blur();"
				),
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Forcefully closes the child window without calling its unload event.
		/// </summary>
		public void close() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.close();"
				),
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Evaluates the code in the child window.
		/// </summary>
		/// <param name="code"></param>
		public void eval(string code) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.eval({1});"
				),
				Script.GetObject(_id),
				code.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Focuses the child window (brings the window to front).
		/// </summary>
		public void focus() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.focus();"
				),
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Invokes the print dialog on the child window.
		/// </summary>
		public void print() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.print();"
				),
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
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
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.postMessage({1},{2});"
				),
				Script.GetObject(_id),
				message.Escape(),
				targetOrigin.Escape()
			);
			_ExecuteJavaScript(script);
		}
	}
}
