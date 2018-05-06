using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
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
	public class PowerSaveBlocker : NodeModule {
		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public PowerSaveBlocker(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

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
			string script = ScriptBuilder.Build(
				"return {0}.start({1});",
				Script.GetObject(_id),
				type.Escape()
			);
			return _ExecuteBlocking<int>(script);
		}

		/// <summary>
		/// Stops the specified power save blocker.
		/// </summary>
		/// <param name="id">
		/// The power save blocker id returned by powerSaveBlocker.start.
		/// </param>
		public void stop(int id) {
			string script = ScriptBuilder.Build(
				"{0}.stop({1});",
				Script.GetObject(_id),
				id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the corresponding powerSaveBlocker has started.
		/// </summary>
		/// <param name="id">
		/// The power save blocker id returned by powerSaveBlocker.start.
		/// </param>
		/// <returns></returns>
		public bool isStarted(int id) {
			string script = ScriptBuilder.Build(
				"return {0}.isStarted({1});",
				Script.GetObject(_id),
				id
			);
			return _ExecuteBlocking<bool>(script);
		}
	}
}
