using System.Text;

namespace Socketron {
	public class ScriptBuilder {
		public static string Delimiter = "\n";
		protected static StringBuilder builder;

		static ScriptBuilder() {
			builder = new StringBuilder(1024);
		}

		public static string Build(string script, params object[] args) {
			if (args == null) {
				return script;
			}
			builder.Length = 0;
			builder.AppendFormat(script, args);
			return builder.ToString();
			//return string.Format(script, args);
		}

		public static string Script(params string[] script) {
			if (script == null) {
				return string.Empty;
			}
			return string.Join(Delimiter, script);
		}

		public static string Params(params string[] args) {
			if (args == null) {
				return string.Empty;
			}
			return string.Join(",", args);
		}
	}
}
