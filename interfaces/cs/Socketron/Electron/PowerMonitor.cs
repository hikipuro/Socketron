using System;

namespace Socketron {
	/// <summary>
	/// Monitor power state changes.
	/// <para>Process: Main</para>
	/// </summary>
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
			/// *Windows* Emitted when the system changes to AC power.
			/// </summary>
			public const string OnAC = "on-ac";
			/// <summary>
			/// *Windows* Emitted when system changes to battery power.
			/// </summary>
			public const string OnBattery = "on-battery";
			/// <summary>
			/// *Linux macOS* Emitted when the system is about to reboot or shut down.
			/// If the event handler invokes e.preventDefault(),
			/// Electron will attempt to delay system shutdown in order for the app to exit cleanly.
			/// If e.preventDefault() is called, the app should exit as soon as possible
			/// by calling something like app.quit().
			/// </summary>
			public const string Shutdown = "shutdown";
			/// <summary>
			/// *macOS Windows* Emitted when the system is about to lock the screen.
			/// </summary>
			public const string LockScreen = "lock-screen";
			/// <summary>
			/// *macOS Windows* Emitted as soon as the systems screen is unlocked.
			/// </summary>
			public const string UnlockScreen = "unlock-screen";
		}

		public void QuerySystemIdleState(int idleThreshold, Callback callback) {
			throw new NotImplementedException();
		}

		public void QuerySystemIdleTime(Callback callback) {
			throw new NotImplementedException();
		}
	}
}
