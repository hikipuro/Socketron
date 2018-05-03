using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Manage browser sessions, cookies, cache, proxy settings, etc.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class SessionClass : ElectronBase {
		public SessionClass(Socketron socketron) {
			_socketron = socketron;
		}

		public Session defaultSession {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = electron.session.defaultSession;",
						"return {1};"
					),
					Script.AddObject("session")
				);
				int result = _ExecuteJavaScriptBlocking<int>(script);
				return new Session(_socketron) {
					id = result
				};
			}
		}

		public Session fromPartition(string partition, JsonObject options = null) {
			string script = string.Empty;
			if (options == null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = electron.session.fromPartition({0});",
						"return {1};"
					),
					partition.Escape(),
					Script.AddObject("session")
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = electron.session.fromPartition({0},{1});",
						"return {2};"
					),
					partition.Escape(),
					options.Stringify(),
					Script.AddObject("session")
				);
			}
			int result = _ExecuteJavaScriptBlocking<int>(script);
			return new Session(_socketron) {
				id = result
			};
		}
	}
}
