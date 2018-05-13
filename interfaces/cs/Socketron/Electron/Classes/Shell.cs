using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Manage files and URLs using their default applications.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Shell : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public Shell() {
		}
		
		/// <summary>
		/// Show the given file in a file manager. If possible, select the file.
		/// </summary>
		/// <param name="fullPath"></param>
		/// <returns>Whether the item was successfully shown.</returns>
		public bool showItemInFolder(string fullPath) {
			return API.Apply<bool>("showItemInFolder", fullPath);
		}

		/// <summary>
		/// Open the given file in the desktop's default manner.
		/// </summary>
		/// <param name="fullPath"></param>
		/// <returns>Whether the item was successfully opened.</returns>
		public bool openItem(string fullPath) {
			return API.Apply<bool>("openItem", fullPath);
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
			return API.Apply<bool>("openExternal", url);
		}

		public bool openExternal(string url, JsonObject options, Action<Error> callback) {
			if (options == null) {
				options = new JsonObject();
			}
			string eventName = "_openExternal";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Error error = API.CreateObject<Error>(args[0]);
				callback?.Invoke(error);
			});
			return API.Apply<bool>("openExternal", url, options, item);
		}

		/// <summary>
		/// Move the given file to trash and returns a boolean status for the operation.
		/// </summary>
		/// <param name="fullPath"></param>
		/// <returns>Whether the item was successfully moved to the trash.</returns>
		public bool moveItemToTrash(string fullPath) {
			return API.Apply<bool>("moveItemToTrash", fullPath);
		}

		/// <summary>
		/// Play the beep sound.
		/// </summary>
		public void beep() {
			API.Apply("beep");
		}

		/// <summary>
		/// *Windows*
		/// Creates or updates a shortcut link at shortcutPath.
		/// </summary>
		/// <param name="shortcutPath"></param>
		/// <param name="options"></param>
		/// <returns>Whether the shortcut was created successfully.</returns>
		public bool writeShortcutLink(string shortcutPath, ShortcutDetails options) {
			return API.Apply<bool>("writeShortcutLink", shortcutPath, options);
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
			return API.Apply<bool>("writeShortcutLink", shortcutPath, operation, options);
		}

		/// <summary>
		/// *Windows*
		/// Resolves the shortcut link at shortcutPath.
		/// An exception will be thrown when any error happens.
		/// </summary>
		/// <param name="shortcutPath"></param>
		/// <returns></returns>
		public ShortcutDetails readShortcutLink(string shortcutPath) {
			object result = API.Apply("readShortcutLink", shortcutPath);
			return ShortcutDetails.FromObject(result);
		}
	}
}
