namespace Socketron {
	/// <summary>
	/// Manage files and URLs using their default applications.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	public class ShellClass : ElectronBase {
		public ShellClass(Socketron socketron) {
			_socketron = socketron;
		}

		/// <summary>
		/// Show the given file in a file manager. If possible, select the file.
		/// </summary>
		/// <param name="fullPath"></param>
		/// <returns>Whether the item was successfully shown.</returns>
		public bool ShowItemInFolder(string fullPath) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.showItemInFolder({0});"
				),
				fullPath.Escape()
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Open the given file in the desktop's default manner.
		/// </summary>
		/// <param name="fullPath"></param>
		/// <returns>Whether the item was successfully opened.</returns>
		public bool OpenItem(string fullPath) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.openItem({0});"
				),
				fullPath.Escape()
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
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
		public bool OpenExternal(string url) {
			// TODO: add options, callback
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.openExternal({0});"
				),
				url.Escape()
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Move the given file to trash and returns a boolean status for the operation.
		/// </summary>
		/// <param name="fullPath"></param>
		/// <returns>Whether the item was successfully moved to the trash.</returns>
		public bool MoveItemToTrash(string fullPath) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.moveItemToTrash({0});"
				),
				fullPath.Escape()
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Play the beep sound.
		/// </summary>
		public void Beep() {
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
		/// <returns>Whether the shortcut was created successfully.</returns>
		public bool WriteShortcutLink(string shortcutPath) {
			// TODO: add operation, options
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.writeShortcutLink({0});"
				),
				shortcutPath.Escape()
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows*
		/// Resolves the shortcut link at shortcutPath.
		/// An exception will be thrown when any error happens.
		/// </summary>
		/// <param name="shortcutPath"></param>
		/// <returns></returns>
		public ShortcutDetails ReadShortcutLink(string shortcutPath) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.shell.readShortcutLink({0});"
				),
				shortcutPath.Escape()
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return ShortcutDetails.FromObject(result);
		}
	}
}
