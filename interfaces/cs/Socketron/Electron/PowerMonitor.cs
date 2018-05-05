using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
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
	public class PowerMonitor : NodeModule {
		public const string Name = "PowerMonitor";

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public PowerMonitor(SocketronClient client) {
			_client = client;
		}

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Callback GetCallbackFromId(ushort id) {
			if (!_callbackList.ContainsKey(id)) {
				return null;
			}
			return _callbackList[id];
		}

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
		/// Calculate the system idle state.
		/// idleThreshold is the amount of time (in seconds) before considered idle. 
		/// </summary>
		/// <param name="idleThreshold"></param>
		/// <param name="callback"></param>
		public void querySystemIdleState(int idleThreshold, Action<string> callback) {
			if (callback == null) {
				return;
			}
			ushort callbackId = _callbackListId;
			_callbackList.Add(_callbackListId, (object args) => {
				_callbackList.Remove(callbackId);
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				string idleState = argsList[0] as string;
				callback?.Invoke(idleState);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (idleState) => {{",
						"emit('__event',{0},{1},idleState);",
					"}};",
					"electron.powerMonitor.querySystemIdleState({2},callback);"
				),
				Name.Escape(),
				_callbackListId,
				idleThreshold
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Calculate system idle time in seconds.
		/// </summary>
		/// <param name="callback"></param>
		public void querySystemIdleTime(Action<int> callback) {
			if (callback == null) {
				return;
			}
			ushort callbackId = _callbackListId;
			_callbackList.Add(_callbackListId, (object args) => {
				_callbackList.Remove(callbackId);
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				int idleTime = (int)argsList[0];
				callback?.Invoke(idleTime);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (idleTime) => {{",
						"emit('__event',{0},{1},idleTime);",
					"}};",
					"electron.powerMonitor.querySystemIdleTime(callback);"
				),
				Name.Escape(),
				_callbackListId
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}
	}
}
