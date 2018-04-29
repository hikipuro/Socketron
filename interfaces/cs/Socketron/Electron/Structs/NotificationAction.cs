using System.Web.Script.Serialization;

namespace Socketron {
	public class NotificationAction {
		public string type;
		public string text;

		public static NotificationAction Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<NotificationAction>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
