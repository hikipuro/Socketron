using System.Web.Script.Serialization;

namespace Socketron {
	public class Referrer {
		public string url;
		public string policy;

		public static Referrer Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<Referrer>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
