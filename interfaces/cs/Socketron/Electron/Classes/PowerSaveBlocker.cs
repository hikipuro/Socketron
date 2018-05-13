using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Block the system from entering low-power (sleep) mode.
	/// <para>Process: Main</para>
	/// </summary>
	/// <example>
	/// For example:
	/// <code>
	/// const {powerSaveBlocker} = require('electron')
	/// 
	/// const id = powerSaveBlocker.start('prevent-display-sleep')
	/// console.log(powerSaveBlocker.isStarted(id))
	/// 
	/// powerSaveBlocker.stop(id)
	/// </code>
	/// </example>
	[type: SuppressMessage("Style", "IDE1006")]
	public class PowerSaveBlocker : EventEmitter {
		/// <summary>
		/// Power save blocker type values.
		/// </summary>
		public class Type {
			/// <summary>
			/// Prevent the application from being suspended.
			/// <para>
			/// Keeps system active but allows screen to be turned off.
			/// Example use cases: downloading a file or playing audio.
			/// </para>
			/// </summary>
			public const string PreventAppSuspension = "prevent-app-suspension";
			/// <summary>
			/// Prevent the display from going to sleep.
			/// <para>
			/// Keeps system and screen active.
			/// Example use case: playing video.
			/// </para>
			/// </summary>
			public const string PreventDisplaySleep = "prevent-display-sleep";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public PowerSaveBlocker() {
		}

		/// <summary>
		/// Returns Integer - The blocker ID that is assigned to this power blocker.
		/// <para>
		/// Starts preventing the system from entering lower-power mode.
		/// Returns an integer identifying the power save blocker.
		/// </para>
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public int start(string type) {
			return API.Apply<int>("start", type);
		}

		/// <summary>
		/// Stops the specified power save blocker.
		/// </summary>
		/// <param name="id">
		/// The power save blocker id returned by powerSaveBlocker.start.
		/// </param>
		public void stop(int id) {
			API.Apply("stop", id);
		}

		/// <summary>
		/// Returns Boolean - Whether the corresponding powerSaveBlocker has started.
		/// </summary>
		/// <param name="id">
		/// The power save blocker id returned by powerSaveBlocker.start.
		/// </param>
		/// <returns></returns>
		public bool isStarted(int id) {
			return API.Apply<bool>("isStarted", id);
		}
	}
}
