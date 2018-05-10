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
	public class PowerMonitorModule : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public PowerMonitorModule() {
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public PowerMonitorModule(SocketronClient client, int id) {
			API.client = client;
			API.id = id;
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
			string eventName = "querySystemIdleState";
			CallbackItem item = null;
			item = API.client.Callbacks.Add(API.id, eventName, (object[] args) => {
				API.client.Callbacks.RemoveItem(API.id, eventName, item.CallbackId);
				string idleState = args[0] as string;
				callback?.Invoke(idleState);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (idleState) => {{",
						"this.emit('__event',{0},{1},{2},idleState);",
					"}};",
					"return {3};"
				),
				API.id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			int objectId = API._ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.querySystemIdleState({1},{2});",
				Script.GetObject(API.id),
				idleThreshold,
				Script.GetObject(objectId)
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Calculate system idle time in seconds.
		/// </summary>
		/// <param name="callback"></param>
		public void querySystemIdleTime(Action<int> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "querySystemIdleTime";
			CallbackItem item = null;
			item = API.client.Callbacks.Add(API.id, eventName, (object[] args) => {
				API.client.Callbacks.RemoveItem(API.id, eventName, item.CallbackId);
				int idleTime = (int)args[0];
				callback?.Invoke(idleTime);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (idleTime) => {{",
						"this.emit('__event',{0},{1},{2},idleTime);",
					"}};",
					"return {3};"
				),
				API.id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			int objectId = API._ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.querySystemIdleTime({1});",
				Script.GetObject(API.id),
				Script.GetObject(objectId)
			);
			API.ExecuteJavaScript(script);
		}
	}
}
