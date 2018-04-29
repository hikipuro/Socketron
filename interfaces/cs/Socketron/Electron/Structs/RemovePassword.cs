using System.Web.Script.Serialization;

namespace Socketron {
	public class RemovePassword {
		public string type;
		public string origin;
		public string scheme;
		public string realm;
		public string username;
		public string password;

		public static RemovePassword Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<RemovePassword>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
