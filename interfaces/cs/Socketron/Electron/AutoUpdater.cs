namespace Socketron {
	/// <summary>
	/// Enable apps to automatically update themselves.
	/// <para>Process: Main</para>
	/// </summary>
	public class AutoUpdater : ElectronBase {
		/// <summary>
		/// AutoUpdater object events.
		/// </summary>
		public class Events {
			public const string Error = "error";
			public const string CheckingForUpdate = "checking-for-update";
			public const string UpdateAvailable = "update-available";
			public const string UpdateNotAvailable = "update-not-available";
			public const string UpdateDownloaded = "update-downloaded";
			public const string BeforeQuitForUpdate = "before-quit-for-update";

		}
	}
}
