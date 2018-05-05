using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Manage files and URLs using their default applications.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class ShellClass : NodeModule {
		public const string Name = "ShellClass";

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public ShellClass(SocketronClient client) {
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
		/// Show the given file in a file manager. If possible, select the file.
		/// </summary>
		/// <param name="fullPath"></param>
		/// <returns>Whether the item was successfully shown.</returns>
		public bool showItemInFolder(string fullPath) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.showItemInFolder({0});"
				),
				fullPath.Escape()
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Open the given file in the desktop's default manner.
		/// </summary>
		/// <param name="fullPath"></param>
		/// <returns>Whether the item was successfully opened.</returns>
		public bool openItem(string fullPath) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.openItem({0});"
				),
				fullPath.Escape()
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Open the given external protocol URL in the desktop's default manner.
		/// (For example, mailto: URLs in the user's default mail agent).
		/// </summary>
		/// <param name="url">Max 2081 characters on windows, or the function returns false.</param>
		/// <returns>
		/// Whether an application was available to open the URL.
		/// If callback is specified, always returns true.
		/// </returns>
		public bool openExternal(string url) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.openExternal({0});"
				),
				url.Escape()
			);
			return _ExecuteBlocking<bool>(script);
		}

		public bool openExternal(string url, JsonObject options, Action<Error> callback) {
			if (options == null) {
				options = new JsonObject();
			}
			ushort callbackId = _callbackListId;
			_callbackList.Add(_callbackListId, (object args) => {
				_callbackList.Remove(callbackId);
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				Error error = new Error(_client, (int)argsList[0]);
				callback?.Invoke(error);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.openExternal({0},{1},(err) => {{",
						"var errId = {2};",
						"emit('__event',{3},{4},errId);",
					"}});"
				),
				url.Escape(),
				options.Stringify(),
				Script.AddObject("err"),
				Name.Escape(),
				_callbackListId
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Move the given file to trash and returns a boolean status for the operation.
		/// </summary>
		/// <param name="fullPath"></param>
		/// <returns>Whether the item was successfully moved to the trash.</returns>
		public bool moveItemToTrash(string fullPath) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.moveItemToTrash({0});"
				),
				fullPath.Escape()
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Play the beep sound.
		/// </summary>
		public void beep() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.beep();"
				)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows*
		/// Creates or updates a shortcut link at shortcutPath.
		/// </summary>
		/// <param name="shortcutPath"></param>
		/// <param name="options"></param>
		/// <returns>Whether the shortcut was created successfully.</returns>
		public bool writeShortcutLink(string shortcutPath, ShortcutDetails options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.writeShortcutLink({0},{1});"
				),
				shortcutPath.Escape(),
				options.Stringify()
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows*
		/// Creates or updates a shortcut link at shortcutPath.
		/// </summary>
		/// <param name="shortcutPath"></param>
		/// <param name="operation"></param>
		/// <param name="options"></param>
		/// <returns>Whether the shortcut was created successfully.</returns>
		public bool writeShortcutLink(string shortcutPath, string operation, ShortcutDetails options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.writeShortcutLink({0},{1});"
				),
				shortcutPath.Escape(),
				operation.Escape(),
				options.Stringify()
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows*
		/// Resolves the shortcut link at shortcutPath.
		/// An exception will be thrown when any error happens.
		/// </summary>
		/// <param name="shortcutPath"></param>
		/// <returns></returns>
		public ShortcutDetails readShortcutLink(string shortcutPath) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.readShortcutLink({0});"
				),
				shortcutPath.Escape()
			);
			object result = _ExecuteBlocking<object>(script);
			return ShortcutDetails.FromObject(result);
		}
	}
}
