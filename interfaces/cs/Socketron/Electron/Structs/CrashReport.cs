using System;
using System.Web.Script.Serialization;

namespace Socketron {
	public class CrashReport {
		public DateTime date;
		public string id;

		public static CrashReport Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<CrashReport>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}

