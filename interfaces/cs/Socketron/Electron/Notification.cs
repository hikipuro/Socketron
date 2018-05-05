using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Create OS desktop notifications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Notification : NodeModule {
		public const string Name = "Notification";

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		/// <summary>
		/// Notification instance events.
		/// </summary>
		public class Events {
			public const string Show = "show";
			public const string Click = "click";
			public const string Close = "close";
			/// <summary>*macOS*</summary>
			public const string Reply = "reply";
			/// <summary>*macOS*</summary>
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
			/// Create JSON text.
			/// </summary>
			/// <returns></returns>
			public string Stringify() {
				return JSON.Stringify(this);
			}
		}

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public Notification(SocketronClient client) {
			_client = client;
		}

		/// <summary>
		/// *Experimental* 
		/// </summary>
		/// <param name="options"></param>
		public Notification(Options options) {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
				"var notification = new electron.Notification({0});",
				"return {1};"
				),
				options.Stringify(),
				Script.AddObject("notification")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			_client = client;
			_id = result;
		}

		public static Callback GetCallbackFromId(ushort id) {
			if (!_callbackList.ContainsKey(id)) {
				return null;
			}
			return _callbackList[id];
		}

		public void on(string eventName, Callback callback) {
			if (callback == null) {
				return;
			}
			_callbackList.Add(_callbackListId, callback);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var notification = {0};",
					"if (notification == null) {{",
						"return;",
					"}}",
					"var listener = () => {{",
						"emit('__event',{1},{2});",
					"}};",
					"this._addClientEventListener({1},{2},listener);",
					"notification.on({3}, listener);"
				),
				Script.GetObject(_id),
				Name.Escape(),
				_callbackListId,
				eventName.Escape()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		public void once(string eventName, Callback callback) {
			if (callback == null) {
				return;
			}
			_callbackList.Add(_callbackListId, callback);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var notification = {0};",
					"if (notification == null) {{",
						"return;",
					"}}",
					"var listener = () => {{",
						"this._removeClientEventListener({1},{2});",
						"emit('__event',{1},{2});",
					"}};",
					"this._addClientEventListener({1},{2},listener);",
					"notification.once({3}, listener);"
				),
				Script.GetObject(_id),
				Name.Escape(),
				_callbackListId,
				eventName.Escape()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
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
