using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create OS desktop notifications.
	/// <para>Process: Main</para>
	/// <para>
	/// If you want to show Notifications from a renderer process you should use the HTML5 Notification API.
	/// </para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class NotificationModule : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public NotificationModule(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// *Experimental* 
		/// Create a new Notification instance.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public Notification Create(Notification.Options options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var notification = new {0}({1});",
					"return {2};"
				),
				Script.GetObject(_id),
				options.Stringify(),
				Script.AddObject("notification")
			);
			int result = _ExecuteBlocking<int>(script);
			return new Notification(_client, result);
		}

		/// <summary>
		/// *Experimental* 
		/// Create a new Notification instance.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public Notification Create(string options) {
			return Create(Notification.Options.Parse(options));
		}

		/// <summary>
		/// Returns Boolean - Whether or not desktop notifications are supported on the current system.
		/// </summary>
		/// <returns></returns>
		public bool isSupported() {
			string script = ScriptBuilder.Build(
				"return {0}.isSupported();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}
	}
}
