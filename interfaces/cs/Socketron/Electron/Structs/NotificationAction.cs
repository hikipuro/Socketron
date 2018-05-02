namespace Socketron {
	public class NotificationAction {
		public string type;
		public string text;

		public static NotificationAction Parse(string text) {
			return JSON.Parse<NotificationAction>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
