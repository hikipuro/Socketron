using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class ConsoleModule : NodeModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public ConsoleModule(SocketronClient client) {
			_disposeManually = true;
			_client = client;
		}

		public void clear() {
			_ExecuteJavaScript("console.clear();");
		}

		public void count(string label = null) {
			string script = string.Empty;
			if (label == null) {
				script = "console.count();";
			} else {
				script = ScriptBuilder.Build(
					"console.count({0});",
					label.Escape()
				);
			}
			_ExecuteJavaScript(script);
		}

		public void countReset(string label = null) {
			string script = string.Empty;
			if (label == null) {
				script = "console.countReset();";
			} else {
				script = ScriptBuilder.Build(
					"console.countReset({0});",
					label.Escape()
				);
			}
			_ExecuteJavaScript(script);
		}

		public void log(params object[] args) {
			if (args == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				"console.log({0});",
				CreateParams(args)
			);
			_ExecuteJavaScript(script);
		}

		public void table(params object[] args) {
			if (args == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				"console.table({0});",
				CreateParams(args)
			);
			_ExecuteJavaScript(script);
		}

		public void trace(params object[] args) {
			if (args == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				"console.trace({0});",
				CreateParams(args)
			);
			_ExecuteJavaScript(script);
		}

		public void time(string label = null) {
			string script = string.Empty;
			if (label == null) {
				script = "console.time();";
			} else {
				script = ScriptBuilder.Build(
					"console.time({0});",
					label.Escape()
				);
			}
			_ExecuteJavaScript(script);
		}

		public void timeEnd(string label = null) {
			string script = string.Empty;
			if (label == null) {
				script = "console.timeEnd();";
			} else {
				script = ScriptBuilder.Build(
					"console.timeEnd({0});",
					label.Escape()
				);
			}
			_ExecuteJavaScript(script);
		}

		public static string CreateParams(object[] args) {
			string[] strings = new string[args.Length];
			for (int i = 0; i < args.Length; i++) {
				object arg = args[i];
				if (arg == null) {
					strings[i] = "null";
					continue;
				}
				if (arg is string) {
					strings[i] = ((string)arg).Escape();
					continue;
				}
				if (arg is bool) {
					strings[i] = ((bool)arg).Escape();
					continue;
				}
				if (arg is NodeModule) {
					NodeModule obj = arg as NodeModule;
					strings[i] = string.Format("this._objRefs[{0}]", obj._id);
					continue;
				}
				strings[i] = arg.ToString();
			}
			return string.Join(",", strings);
		}
	}
}
