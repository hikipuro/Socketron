using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Create OS desktop notifications.
	/// <para>Process: Main</para>
	/// <para>
	/// If you want to show Notifications from a renderer process you should use the HTML5 Notification API.
	/// </para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class NotificationClass : NodeBase {
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
			int result = _ExecuteBlocking<int>(script);
			Notification notification = new Notification(_socketron) {
				id = result
			};
			return notification;
		}

		/// <summary>
		/// Returns Boolean - Whether or not desktop notifications are supported on the current system.
		/// </summary>
		/// <returns></returns>
		public bool isSupported() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.Notification.isSupported();"
				)
			);
			return _ExecuteBlocking<bool>(script);
		}
	}
}
