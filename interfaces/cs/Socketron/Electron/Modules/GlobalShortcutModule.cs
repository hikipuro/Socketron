using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Detect keyboard events when the application does not have keyboard focus.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class GlobalShortcutModule : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public GlobalShortcutModule() {
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public GlobalShortcutModule(SocketronClient client, int id) {
			API.client = client;
			API.id = id;
		}

		/// <summary>
		/// Registers a global shortcut of accelerator.
		/// The callback is called when the registered shortcut is pressed by the user.
		/// </summary>
		/// <param name="accelerator"></param>
		/// <param name="callback"></param>
		public void register(string accelerator, Action callback) {
			if (callback == null) {
				return;
			}
			string eventName = "register";
			CallbackItem item = null;
			item = API.client.Callbacks.Add(API.id, eventName, (object[] args) => {
				callback?.Invoke();
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"this.emit('__event',{0},{1},{2});",
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
				"{0}.register({1},{2});",
				Script.GetObject(API.id),
				accelerator.Escape(),
				Script.GetObject(objectId)
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether this application has registered accelerator.
		/// </summary>
		/// <param name="accelerator"></param>
		/// <returns></returns>
		public bool isRegistered(string accelerator) {
			string script = ScriptBuilder.Build(
				"return {0}.isRegistered({1});",
				Script.GetObject(API.id),
				accelerator.Escape()
			);
			return API._ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Unregisters the global shortcut of accelerator.
		/// </summary>
		/// <param name="accelerator"></param>
		public void unregister(string accelerator) {
			string script = ScriptBuilder.Build(
				"{0}.unregister({1});",
				Script.GetObject(API.id),
				accelerator.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Unregisters all of the global shortcuts.
		/// </summary>
		public void unregisterAll() {
			string script = ScriptBuilder.Build(
				"{0}.unregisterAll();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}
	}
}
