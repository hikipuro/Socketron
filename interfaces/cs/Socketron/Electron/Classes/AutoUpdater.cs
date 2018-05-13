using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Enable apps to automatically update themselves.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class AutoUpdater : JSObject {
		/// <summary>
		/// AutoUpdater module events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when there is an error while updating.
			/// </summary>
			public const string Error = "error";
			/// <summary>
			/// Emitted when checking if an update has started.
			/// </summary>
			public const string CheckingForUpdate = "checking-for-update";
			/// <summary>
			/// Emitted when there is an available update.
			/// The update is downloaded automatically.
			/// </summary>
			public const string UpdateAvailable = "update-available";
			/// <summary>
			/// Emitted when there is no available update.
			/// </summary>
			public const string UpdateNotAvailable = "update-not-available";
			/// <summary>
			/// Emitted when an update has been downloaded.
			/// On Windows only releaseName is available.
			/// </summary>
			public const string UpdateDownloaded = "update-downloaded";
			/// <summary>
			/// This event is emitted after a user calls quitAndInstall().
			/// </summary>
			public const string BeforeQuitForUpdate = "before-quit-for-update";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public AutoUpdater() {
		}

		/// <summary>
		/// Sets the url and initialize the auto updater.
		/// </summary>
		/// <param name="options"></param>
		public void setFeedURL(JsonObject options) {
			API.Apply("setFeedURL", options);
		}

		/// <summary>
		/// Returns String - The current update feed URL.
		/// </summary>
		/// <returns></returns>
		public string getFeedURL() {
			return API.Apply<string>("getFeedURL");
		}

		/// <summary>
		/// Asks the server whether there is an update.
		/// You must call setFeedURL before using this API.
		/// </summary>
		public void checkForUpdates() {
			API.Apply("checkForUpdates");
		}

		/// <summary>
		/// Restarts the app and installs the update after it has been downloaded.
		/// It should only be called after update-downloaded has been emitted.
		/// <para>
		/// Under the hood calling autoUpdater.quitAndInstall()
		/// will close all application windows first,
		/// and automatically call app.quit() after all windows have been closed.
		/// </para>
		/// <para>
		/// Note: If the application is quit without calling this API
		/// after the update-downloaded event has been emitted,
		/// the application will still be replaced by the updated one on the next run.
		/// </para>
		/// </summary>
		public void quitAndInstall() {
			API.Apply("quitAndInstall");
		}
	}
}
