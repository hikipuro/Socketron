using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create OS desktop notifications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Notification : NodeModule {
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
		/// Notification create options.
		/// </summary>
		public class Options {
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
			//public NotificationAction[] actions;
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
			public static Options Parse(string text) {
				return JSON.Parse<Options>(text);
			}

			/// <summary>
			/// Create JSON text.
			/// </summary>
			/// <returns></returns>
			public string Stringify() {
				return JSON.Stringify(this);
			}
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the Notification constructors,
		/// please use electron.Notification.Create() method instead.
		/// </para>
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public Notification(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/*
		/// <summary>
		/// *Experimental* 
		/// </summary>
		/// <param name="options"></param>
		public Notification(Options options) {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var notification = new {0}({1});",
					"return {2};"
				),
				Script.GetObject(_Class._id),
				options.Stringify(),
				Script.AddObject("notification")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			_client = client;
			_id = result;
		}

		/// <summary>
		/// *Experimental* 
		/// </summary>
		/// <param name="options"></param>
		public Notification(string options) : this(Options.Parse(options)) {
		}
		*/

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
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var notification = {0};",
					"if (notification == null) {{",
						"return;",
					"}}",
					"notification.show();"
				),
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Dismisses the notification.
		/// </summary>
		public void close() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var notification = {0};",
					"if (notification == null) {{",
						"return;",
					"}}",
					"notification.close();"
				),
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}
	}
}
