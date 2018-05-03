using System;

namespace Socketron {
	public class CrashReport {
		public DateTime date;
		public string id;

		public static CrashReport Parse(string text) {
			return JSON.Parse<CrashReport>(text);
		}

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <returns></returns>
		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

