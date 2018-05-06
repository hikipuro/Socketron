﻿namespace Socketron.Electron {
	/// <summary>
	/// Enable apps to automatically update themselves.
	/// <para>Process: Main</para>
	/// </summary>
	public class AutoUpdater {
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
	}
}
