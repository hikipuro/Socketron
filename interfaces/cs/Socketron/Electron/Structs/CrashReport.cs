using System;

namespace Socketron {
	public class CrashReport {
		public DateTime date;
		public string id;

		public static CrashReport Parse(string text) {
			return JSON.Parse<CrashReport>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

