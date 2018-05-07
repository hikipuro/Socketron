using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	public partial class NodeModules {
		[type: SuppressMessage("Style", "IDE1006")]
		public class Console : JSModule {
			public bool LocalEcho = true;

			/// <summary>
			/// This constructor is used for internally by the library.
			/// </summary>
			public Console() {
				_client = SocketronClient.GetCurrent();
			}

			public void clear() {
				string script = ScriptBuilder.Build(
					"{0}.clear();",
					Script.GetObject(_id)
				);
				_ExecuteJavaScript(script);
			}

			public void count(string label = null) {
				string script = string.Empty;
				if (label == null) {
					script = ScriptBuilder.Build(
						"{0}.count();",
						Script.GetObject(_id)
					);
				} else {
					script = ScriptBuilder.Build(
						"{0}.count({1});",
						Script.GetObject(_id),
						label.Escape()
					);
				}
				_ExecuteJavaScript(script);
			}

			public void countReset(string label = null) {
				string script = string.Empty;
				if (label == null) {
					script = ScriptBuilder.Build(
						"{0}.countReset();",
						Script.GetObject(_id)
					);
				} else {
					script = ScriptBuilder.Build(
						"{0}.countReset({1});",
						Script.GetObject(_id),
						label.Escape()
					);
				}
				_ExecuteJavaScript(script);
			}

			public void log(params object[] args) {
				if (LocalEcho) {
					System.Console.WriteLine(JSON.Stringify(args));
				}
				string script = ScriptBuilder.Build(
					"{0}.log({1});",
					Script.GetObject(_id),
					CreateParams(args)
				);
				_ExecuteJavaScript(script);
			}

			public void table(params object[] args) {
				if (LocalEcho) {
					System.Console.WriteLine(JSON.Stringify(args));
				}
				string script = ScriptBuilder.Build(
					"{0}.table({1});",
					Script.GetObject(_id),
					CreateParams(args)
				);
				_ExecuteJavaScript(script);
			}

			public void trace(params object[] args) {
				string script = ScriptBuilder.Build(
					"{0}.trace({1});",
					Script.GetObject(_id),
					CreateParams(args)
				);
				_ExecuteJavaScript(script);
			}

			public void time(string label = null) {
				string script = string.Empty;
				if (label == null) {
					script = ScriptBuilder.Build(
						"{0}.time();",
						Script.GetObject(_id)
					);
				} else {
					script = ScriptBuilder.Build(
						"{0}.time({1});",
						Script.GetObject(_id),
						label.Escape()
					);
				}
				_ExecuteJavaScript(script);
			}

			public void timeEnd(string label = null) {
				string script = string.Empty;
				if (label == null) {
					script = ScriptBuilder.Build(
						"{0}.timeEnd();",
						Script.GetObject(_id)
					);
				} else {
					script = ScriptBuilder.Build(
						"{0}.timeEnd({1});",
						Script.GetObject(_id),
						label.Escape()
					);
				}
				_ExecuteJavaScript(script);
			}
		}
	}
}
