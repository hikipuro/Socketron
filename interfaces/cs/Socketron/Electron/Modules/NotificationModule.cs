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
		/// *Experimental* 
		/// Create a new Notification instance.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public Notification Create(Notification.Options options) {
			if (options == null) {
				options = new Notification.Options();
			}
			return API.ApplyConstructor<Notification>(options);
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
			return API.Apply<bool>("isSupported");
		}
	}
}
