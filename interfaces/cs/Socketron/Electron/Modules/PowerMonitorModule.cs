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
	public class PowerMonitorModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public PowerMonitorModule() {
		}

		public EventEmitter on(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.on(eventName, listener);
		}

		public EventEmitter once(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.once(eventName, listener);
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
