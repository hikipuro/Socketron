
using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class NodeConsole : ElectronBase {
		public NodeConsole(Socketron socketron) {
			_socketron = socketron;
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
				_CreateParams(args)
			);
			_ExecuteJavaScript(script);
		}

		public void trace(params object[] args) {
			if (args == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				"console.trace({0});",
				_CreateParams(args)
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

		protected string _CreateParams(object[] args) {
			string[] strings = new string[args.Length];
			for (int i = 0; i < args.Length; i++) {
				object arg = args[i];
				if (arg == null) {
					strings[i] = "null";
					continue;
				}
				Type type = arg.GetType();
				if (type == typeof(string)) {
					strings[i] = ((string)arg).Escape();
					continue;
				}
				if (type == typeof(bool)) {
					strings[i] = ((bool)arg).Escape();
					continue;
				}
				strings[i] = arg.ToString();
			}
			return string.Join(",", strings);
		}
	}
}
