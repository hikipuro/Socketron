namespace Socketron {
	public class NotificationAction {
		/// <summary>
		/// The type of action, can be button.
		/// </summary>
		public string type;
		/// <summary>
		/// (optional) The label for the given action.
		/// </summary>
		public string text;

		public static NotificationAction Parse(string text) {
			return JSON.Parse<NotificationAction>(text);
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
