using System;
using System.Diagnostics.CodeAnalysis;

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
	[type: SuppressMessage("Style", "IDE1006")]
	public class PowerMonitor : EventEmitter {
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

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public PowerMonitor() {
		}

		/// <summary>
		/// Calculate the system idle state.
		/// idleThreshold is the amount of time (in seconds) before considered idle. 
		/// </summary>
		/// <param name="idleThreshold"></param>
		/// <param name="callback"></param>
		public void querySystemIdleState(int idleThreshold, Action<string> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_querySystemIdleState";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				string idleState = Convert.ToString(args[0]);
				callback?.Invoke(idleState);
			});
			API.Apply("querySystemIdleState", idleThreshold, item);
		}

		/// <summary>
		/// Calculate system idle time in seconds.
		/// </summary>
		/// <param name="callback"></param>
		public void querySystemIdleTime(Action<int> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_querySystemIdleTime";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				int idleTime = Convert.ToInt32(args[0]);
				callback?.Invoke(idleTime);
			});
			API.Apply("querySystemIdleTime", item);
		}
	}
}
