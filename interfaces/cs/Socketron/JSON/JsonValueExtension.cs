using System.Collections.Generic;

namespace Socketron {
	public static class JsonValueExtension {
		/// <summary>
		/// Escape JSON value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Escape(this string value) {
			if (value == null) {
				return "null";
			}
			if (value.Contains("\"")) {
				return "\"" + value.Replace("\"", "\\\"") + "\"";
			}
			return "\"" + value + "\"";
		}

		/// <summary>
		/// Escape JSON value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Escape(this bool value) {
			return value.ToString().ToLower();
		}

		/// <summary>
		/// Escape JSON value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Escape(this bool? value) {
			return value.ToString().ToLower();
		}

		/// <summary>
		/// Escape JSON value.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Stringify<T>(this List<T> value) {
			return JSON.Stringify(value);
		}

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Stringify<T>(this T[] value) {
			return JSON.Stringify(value);
		}
	}
}
