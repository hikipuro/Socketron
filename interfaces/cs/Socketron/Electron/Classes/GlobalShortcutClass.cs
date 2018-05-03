using System.Collections.Generic;

namespace Socketron {
	/// <summary>
	/// Detect keyboard events when the application does not have keyboard focus.
	/// <para>Process: Main</para>
	/// </summary>
	public class GlobalShortcutClass : ElectronBase {
		public const string Name = "GlobalShortcut";

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		public GlobalShortcutClass(Socketron socketron) {
			_socketron = socketron;
		}

		public static Callback GetCallbackFromId(ushort id) {
			if (!_callbackList.ContainsKey(id)) {
				return null;
			}
			return _callbackList[id];
		}

		/// <summary>
		/// Registers a global shortcut of accelerator.
		/// The callback is called when the registered shortcut is pressed by the user.
		/// </summary>
		/// <param name="accelerator"></param>
		/// <param name="callback"></param>
		public void Register(string accelerator, Callback callback) {
			if (callback == null) {
				return;
			}
			_callbackList.Add(_callbackListId, callback);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var listener = () => {{",
						"emit('__event',{0},{1});",
					"}};",
					"this._addClientEventListener({0},{1},listener);",
					"electron.globalShortcut.register({2}, listener);"
				),
				Name.Escape(),
				_callbackListId,
				accelerator.Escape()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether this application has registered accelerator.
		/// </summary>
		/// <param name="accelerator"></param>
		/// <returns></returns>
		public bool IsRegistered(string accelerator) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.globalShortcut.isRegistered({0});"
				),
				accelerator.Escape()
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Unregisters the global shortcut of accelerator.
		/// </summary>
		/// <param name="accelerator"></param>
		public void Unregister(string accelerator) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.globalShortcut.unregister({0});"
				),
				accelerator.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Unregisters all of the global shortcuts.
		/// </summary>
		public void UnregisterAll() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.globalShortcut.unregisterAll();"
				)
			);
			_ExecuteJavaScript(script);
		}
	}
}
