using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// An alternate transport for Chrome's remote debugging protocol.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Debugger : NodeBase {
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

		public Debugger(Socketron socketron) {
			_socketron = socketron;
		}

		/// <summary>
		/// Attaches the debugger to the webContents.
		/// </summary>
		/// <param name="protocolVersion">
		/// (optional) Requested debugging protocol version.
		/// </param>
		public void attach(string protocolVersion = null) {
			string script = string.Empty;
			if (protocolVersion == null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var debugger = {0};",
						"debugger.attach();"
					),
					Script.GetObject(id)
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var debugger = {0};",
						"debugger.attach({1});"
					),
					Script.GetObject(id),
					protocolVersion.Escape()
				);
			}
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether a debugger is attached to the webContents.
		/// </summary>
		/// <returns></returns>
		public bool isAttached() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var debugger = {0};",
					"return debugger.isAttached();"
				),
				Script.GetObject(id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Detaches the debugger from the webContents.
		/// </summary>
		public void detach() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var debugger = {0};",
					"debugger.detach();"
				),
				Script.GetObject(id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Send given command to the debugging target.
		/// </summary>
		/// <param name="method"></param>
		public void sendCommand(string method) {
			// TODO: add params
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var debugger = {0};",
					"debugger.sendCommand({1});"
				),
				Script.GetObject(id),
				method.Escape()
			);
			_ExecuteJavaScript(script);
		}
	}
}
