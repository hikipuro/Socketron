using System.Web.Script.Serialization;

namespace Socketron {
	public class Cookie {
		public string name;
		public string value;
		public string domain;
		public bool? hostOnly;
		public string path;
		public bool? secure;
		public bool? httpOnly;
		public bool? session;
		public double expirationDate;

		public static Cookie Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<Cookie>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}

