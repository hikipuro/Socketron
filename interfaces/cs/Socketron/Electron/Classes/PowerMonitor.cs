namespace Socketron.Electron {
	/// <summary>
	/// Monitor power state changes.
	/// <para>Process: Main</para>
	/// <para>
	/// You cannot require or use this module until the ready event of the app module is emitted.
	/// </para>
	/// </summary>
	/// <example>
	/// For example:
	/// <code>
	/// const electron = require('electron')
	/// const {app} = electron
	/// 
	/// app.on('ready', () => {
	///		electron.powerMonitor.on('suspend', () => {
	///			console.log('The system is going to sleep')
	///		})
	/// })
	/// </code>
	/// </example>
	public class PowerMonitor {
		/// <summary>
		/// PowerMonitor module events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when the system is suspending.
			/// </summary>
			public const string Suspend = "suspend";
			/// <summary>
			/// Emitted when system is resuming.
			/// </summary>
			public const string Resume = "resume";
			/// <summary>
			/// *Windows*
			/// Emitted when the system changes to AC power.
			/// </summary>
			public const string OnAC = "on-ac";
			/// <summary>
			/// *Windows*
			/// Emitted when system changes to battery power.
			/// </summary>
			public const string OnBattery = "on-battery";
			/// <summary>
			/// *Linux macOS*
			/// Emitted when the system is about to reboot or shut down.
			/// If the event handler invokes e.preventDefault(),
			/// Electron will attempt to delay system shutdown in order for the app to exit cleanly.
			/// If e.preventDefault() is called, the app should exit as soon as possible
			/// by calling something like app.quit().
			/// </summary>
			public const string Shutdown = "shutdown";
			/// <summary>
			/// *macOS Windows*
			/// Emitted when the system is about to lock the screen.
			/// </summary>
			public const string LockScreen = "lock-screen";
			/// <summary>
			/// *macOS Windows*
			/// Emitted as soon as the systems screen is unlocked.
			/// </summary>
			public const string UnlockScreen = "unlock-screen";
		}
	}
}
