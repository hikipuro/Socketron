namespace Socketron.Electron {
	/// <summary>
	/// Notification constructor options.
	/// </summary>
	public class NotificationConstructorOptions {
		/// <summary>
		/// A title for the notification,
		/// which will be shown at the top of the notification window when it is shown.
		/// </summary>
		public string title;
		/// <summary>
		/// (optional) *macOS* 
		/// A subtitle for the notification, which will be displayed below the title.
		/// </summary>
		public string subtitle;
		/// <summary>
		/// The body text of the notification, which will be displayed below the title or subtitle.
		/// </summary>
		public string body;
		/// <summary>
		/// (optional) Whether or not to emit an OS notification noise when showing the notification.
		/// </summary>
		public bool? silent;
		/// <summary>
		/// (optional) An icon to use in the notification.
		/// </summary>
		public string icon;
		/// <summary>
		/// (optional) *macOS* Whether or not to add an inline reply option to the notification.
		/// </summary>
		public bool? hasReply;
		/// <summary>
		/// (optional) *macOS* The placeholder to write in the inline reply input field.
		/// </summary>
		public string replyPlaceholder;
		/// <summary>
		/// (optional) *macOS* The name of the sound file to play when the notification is shown.
		/// </summary>
		public string sound;
		/// <summary>
		/// (optional) *macOS* Actions to add to the notification.
		/// Please read the available actions and limitations in the NotificationAction documentation.
		/// </summary>
		public NotificationAction[] actions;
		/// <summary>
		/// (optional) *macOS* A custom title for the close button of an alert.
		/// An empty string will cause the default localized text to be used.
		/// </summary>
		public string closeButtonText;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static NotificationConstructorOptions Parse(string text) {
			return JSON.Parse<NotificationConstructorOptions>(text);
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
