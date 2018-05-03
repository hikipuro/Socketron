using System.Collections.Generic;

namespace Socketron {
	/// <summary>
	/// Create OS desktop notifications.
	/// <para>Process: Main</para>
	/// </summary>
	public class Notification : ElectronBase {
		public const string Name = "Notification";
		public int ID;

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

		public Notification(Socketron socketron) {
			_socketron = socketron;
		}

		public static Callback GetCallbackFromId(ushort id) {
			if (!_callbackList.ContainsKey(id)) {
				return null;
			}
			return _callbackList[id];
		}

		public void Dispose() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"this._removeObjectReference({0});"
				),
				ID
			);
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public void On(string eventName, Callback callback) {
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
				Script.GetObject(ID),
				Name.Escape(),
				_callbackListId,
				eventName.Escape()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		public void Once(string eventName, Callback callback) {
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
				Script.GetObject(ID),
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
		public void Show() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var notification = {0};",
					"if (notification == null) {{",
						"return;",
					"}}",
					"notification.show();"
				),
				Script.GetObject(ID)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Dismisses the notification.
		/// </summary>
		public void Close() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var notification = {0};",
					"if (notification == null) {{",
						"return;",
					"}}",
					"notification.close();"
				),
				Script.GetObject(ID)
			);
			_ExecuteJavaScript(script);
		}
	}
}
