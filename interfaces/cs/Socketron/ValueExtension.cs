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

		public static string Escape(this string[] value) {
			if (value == null) {
				return "null";
			}
			string[] result = new string[value.Length];
			for (int i = 0; i < value.Length; i++) {
				string item = value[i];
				result[i] = item.Escape();
			}
			return string.Join(",", result);
		}
	}
}
