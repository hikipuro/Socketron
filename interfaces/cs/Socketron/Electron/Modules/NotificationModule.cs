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
		public NotificationModule() {
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public NotificationModule(SocketronClient client, int id) {
			API.client = client;
			API.id = id;
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
					"var Notification = {0};",
					"var notification = new Notification({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				options.Stringify(),
				Script.AddObject("notification")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new Notification(API.client, result);
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
				Script.GetObject(API.id)
			);
			return API._ExecuteBlocking<bool>(script);
		}
	}
}
