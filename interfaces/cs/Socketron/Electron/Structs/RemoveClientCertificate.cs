using System.Web.Script.Serialization;

namespace Socketron {
	public class RemoveClientCertificate {
		public string type;
		public string origin;

		public static RemoveClientCertificate Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<RemoveClientCertificate>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
