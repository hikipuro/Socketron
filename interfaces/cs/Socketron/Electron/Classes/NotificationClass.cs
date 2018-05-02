namespace Socketron {
	/// <summary>
	/// Create OS desktop notifications.
	/// <para>Process: Main</para>
	/// <para>
	/// If you want to show Notifications from a renderer process you should use the HTML5 Notification API.
	/// </para>
	/// </summary>
	public class NotificationClass : ElectronBase {
		public NotificationClass(Socketron socketron) {
			_socketron = socketron;
		}

		/// <summary>
		/// *Experimental* 
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public Notification Create(Notification.Options options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
				"var notification = new electron.Notification({0});",
				"return this._addObjectReference(notification);"
				),
				options.Stringify()
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			Notification notification = new Notification(_socketron) {
				ID = result
			};
			return notification;
		}

		/// <summary>
		/// Returns Boolean - Whether or not desktop notifications are supported on the current system.
		/// </summary>
		/// <returns></returns>
		public bool IsSupported() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.Notification.isSupported();"
				)
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}
	}
}
