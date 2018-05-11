using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	public partial class NodeModules {
		/// <summary>
		/// Console module of the Node API.
		/// </summary>
		[type: SuppressMessage("Style", "IDE1006")]
		public class Console : JSModule {
			/// <summary>
			/// Whether the console messages write to the local console too.
			/// </summary>
			public bool LocalEcho = true;

			/// <summary>
			/// This constructor is used for internally by the library.
			/// </summary>
			public Console() {
			}

			public void clear() {
				string script = ScriptBuilder.Build(
					"{0}.clear();",
					Script.GetObject(API.id)
				);
				API.ExecuteJavaScript(script);
			}

			public void count(string label = null) {
				string script = string.Empty;
				if (label == null) {
					script = ScriptBuilder.Build(
						"{0}.count();",
						Script.GetObject(API.id)
					);
				} else {
					script = ScriptBuilder.Build(
						"{0}.count({1});",
						Script.GetObject(API.id),
						label.Escape()
					);
				}
				API.ExecuteJavaScript(script);
			}

			public void countReset(string label = null) {
				string script = string.Empty;
				if (label == null) {
					script = ScriptBuilder.Build(
						"{0}.countReset();",
						Script.GetObject(API.id)
					);
				} else {
					script = ScriptBuilder.Build(
						"{0}.countReset({1});",
						Script.GetObject(API.id),
						label.Escape()
					);
				}
				API.ExecuteJavaScript(script);
			}

			public void log(params object[] args) {
				if (LocalEcho) {
					System.Diagnostics.Debug.WriteLine(JSON.Stringify(args));
				}
				string script = ScriptBuilder.Build(
					"{0}.log({1});",
					Script.GetObject(API.id),
					API.CreateParams(args)
				);
				API.ExecuteJavaScript(script);
			}

			public void table(params object[] args) {
				if (LocalEcho) {
					System.Diagnostics.Debug.WriteLine(JSON.Stringify(args));
				}
				string script = ScriptBuilder.Build(
					"{0}.table({1});",
					Script.GetObject(API.id),
					API.CreateParams(args)
				);
				API.ExecuteJavaScript(script);
			}

			public void trace(params object[] args) {
				string script = ScriptBuilder.Build(
					"{0}.trace({1});",
					Script.GetObject(API.id),
					API.CreateParams(args)
				);
				API.ExecuteJavaScript(script);
			}

			public void time(string label = null) {
				string script = string.Empty;
				if (label == null) {
					script = ScriptBuilder.Build(
						"{0}.time();",
						Script.GetObject(API.id)
					);
				} else {
					script = ScriptBuilder.Build(
						"{0}.time({1});",
						Script.GetObject(API.id),
						label.Escape()
					);
				}
				API.ExecuteJavaScript(script);
			}

			public void timeEnd(string label = null) {
				string script = string.Empty;
				if (label == null) {
					script = ScriptBuilder.Build(
						"{0}.timeEnd();",
						Script.GetObject(API.id)
					);
				} else {
					script = ScriptBuilder.Build(
						"{0}.timeEnd({1});",
						Script.GetObject(API.id),
						label.Escape()
					);
				}
				API.ExecuteJavaScript(script);
			}
		}
	}
}
