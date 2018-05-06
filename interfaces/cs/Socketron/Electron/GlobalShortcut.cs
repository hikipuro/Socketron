using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Detect keyboard events when the application does not have keyboard focus.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class GlobalShortcut : NodeModule {
		public const string Name = "GlobalShortcut";

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public GlobalShortcut(SocketronClient client, int id) {
			_client = client;
			_id = id;
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
		/// Registers a global shortcut of accelerator.
		/// The callback is called when the registered shortcut is pressed by the user.
		/// </summary>
		/// <param name="accelerator"></param>
		/// <param name="callback"></param>
		public void register(string accelerator, Callback callback) {
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
					"{2}.register({3}, listener);"
				),
				Name.Escape(),
				_callbackListId,
				Script.GetObject(_id),
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
		public bool isRegistered(string accelerator) {
			string script = ScriptBuilder.Build(
				"return {0}.isRegistered({1});",
				Script.GetObject(_id),
				accelerator.Escape()
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Unregisters the global shortcut of accelerator.
		/// </summary>
		/// <param name="accelerator"></param>
		public void unregister(string accelerator) {
			string script = ScriptBuilder.Build(
				"{0}.unregister({1});",
				Script.GetObject(_id),
				accelerator.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Unregisters all of the global shortcuts.
		/// </summary>
		public void unregisterAll() {
			string script = ScriptBuilder.Build(
				"{0}.unregisterAll();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}
	}
}
