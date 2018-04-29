namespace Socketron {
	static class ValueExtension {
		public static string Escape(this string value) {
			if (value == null) {
				return "null";
			}
			if (value.Contains("\"")) {
				return "\"" + value.Replace("\"", "\\\"") + "\"";
			}
			return "\"" + value + "\"";
		}

		public static string Escape(this bool value) {
			return value.ToString().ToLower();
		}
	}
}
