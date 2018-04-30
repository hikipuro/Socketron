namespace Socketron {
	public class ScriptBuilder {
		public static string Delimiter = "\n";

		public static string Build(string script, params object[] args) {
			if (args == null) {
				return script;
			}
			return string.Format(script, args);
		}

		public static string Script(params string[] script) {
			if (script == null) {
				return string.Empty;
			}
			return string.Join(Delimiter, script);
		}

		public static string Params(params string[] args) {
			return string.Join(",", args);
		}
	}
}
