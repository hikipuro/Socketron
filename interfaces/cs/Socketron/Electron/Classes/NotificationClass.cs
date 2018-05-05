﻿using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Create OS desktop notifications.
	/// <para>Process: Main</para>
	/// <para>
	/// If you want to show Notifications from a renderer process you should use the HTML5 Notification API.
	/// </para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class NotificationClass : NodeModule {
		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public NotificationClass(SocketronClient client) {
			_client = client;
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
