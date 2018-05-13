using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create OS desktop notifications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Notification : EventEmitter {
		/// <summary>
		/// Notification instance events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when the notification is shown to the user,
			/// note this could be fired multiple times as a notification
			/// can be shown multiple times through the show() method.
			/// </summary>
			public const string Show = "show";
			/// <summary>
			/// Emitted when the notification is clicked by the user.
			/// </summary>
			public const string Click = "click";
			/// <summary>
			/// Emitted when the notification is closed by manual intervention from the user.
			/// </summary>
			public const string Close = "close";
			/// <summary>
			/// *macOS*
			/// Emitted when the user clicks the "Reply" button on a notification with hasReply: true.
			/// </summary>
			public const string Reply = "reply";
			/// <summary>
			/// *macOS*
			/// </summary>
			public const string Action = "action";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the Notification constructors,
		/// please use electron.Notification.Create() method instead.
		/// </para>
		/// </summary>
		public Notification() {
		}

		/// <summary>
		/// Immediately shows the notification to the user,
		/// please note this means unlike the HTML5 Notification implementation,
		/// simply instantiating a new Notification does not immediately show it to the user,
		/// you need to call this method before the OS will display it.
		/// <para>
		/// If the notification has been shown before,
		/// this method will dismiss the previously shown notification
		/// and create a new one with identical properties.
		/// </para>
		/// </summary>
		public void show() {
			API.Apply("show");
		}

		/// <summary>
		/// Dismisses the notification.
		/// </summary>
		public void close() {
			API.Apply("close");
		}
	}
}
