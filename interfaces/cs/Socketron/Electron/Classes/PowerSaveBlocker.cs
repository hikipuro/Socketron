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
	public class PowerSaveBlocker {
		/// <summary>
		/// Power save blocker type values.
		/// </summary>
		public class Types {
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
	}
}
