using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create and control browser windows.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class BrowserWindowModule : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public BrowserWindowModule() {
		}

		/// <summary>
		/// Create a new BrowserWindow instance.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public BrowserWindow Create(BrowserWindow.Options options) {
			if (options == null) {
				options = new BrowserWindow.Options();
			}
			return API.ApplyConstructor<BrowserWindow>(options);
		}

		/// <summary>
		/// Create a new BrowserWindow instance.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public BrowserWindow Create(string options) {
			return Create(BrowserWindow.Options.Parse(options));
		}

		/// <summary>
		/// Returns BrowserWindow[] - An array of all opened browser windows.
		/// </summary>
		/// <returns></returns>
		public BrowserWindow[] getAllWindows() {
			return API.ApplyAndGetObjectList<BrowserWindow>("getAllWindows");
		}

		/// <summary>
		/// Returns BrowserWindow | null - The window that is focused in this application,
		/// otherwise returns null.
		/// </summary>
		/// <returns></returns>
		public BrowserWindow getFocusedWindow() {
			return API.ApplyAndGetObject<BrowserWindow>("getFocusedWindow");
		}

		/// <summary>
		/// Returns BrowserWindow - The window that owns the given webContents.
		/// </summary>
		/// <param name="webContents"></param>
		/// <returns></returns>
		public BrowserWindow fromWebContents(WebContents webContents) {
			return API.ApplyAndGetObject<BrowserWindow>("fromWebContents", webContents);
		}

		/// <summary>
		/// Returns BrowserWindow | null - The window that owns the given browserView.
		/// If the given view is not attached to any window, returns null.
		/// </summary>
		/// <param name="browserView"></param>
		/// <returns></returns>
		public BrowserWindow fromBrowserView(BrowserView browserView) {
			return API.ApplyAndGetObject<BrowserWindow>("fromBrowserView", browserView);
		}

		/// <summary>
		/// Returns BrowserWindow - The window with the given id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public BrowserWindow fromId(int id) {
			return API.ApplyAndGetObject<BrowserWindow>("fromId", id);
		}

		/// <summary>
		/// Adds Chrome extension located at path, and returns extension's name.
		/// <para>
		/// The method will also not return if the extension's manifest is missing or incomplete.
		/// </para>
		/// <para>
		/// Note: This API cannot be called before the ready event of the app module is emitted.
		/// </para>
		/// </summary>
		/// <param name="path"></param>
		public void addExtension(string path) {
			API.Apply("addExtension", path);
		}

		/// <summary>
		/// Remove a Chrome extension by name.
		/// <para>
		/// Note: This API cannot be called before the ready event of the app module is emitted.
		/// </para>
		/// </summary>
		/// <param name="name"></param>
		public void removeExtension(string name) {
			API.Apply("removeExtension", name);
		}

		/// <summary>
		/// Returns Object - The keys are the extension names
		/// and each value is an Object containing name and version properties.
		/// <para>
		/// Note: This API cannot be called before the ready event of the app module is emitted.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public JsonObject getExtensions() {
			object result = API.Apply("getExtensions");
			return new JsonObject(result);
		}

		/// <summary>
		/// Adds DevTools extension located at path, and returns extension's name.
		/// </summary>
		/// <param name="path"></param>
		public void addDevToolsExtension(string path) {
			API.Apply("addDevToolsExtension", path);
		}

		/// <summary>
		/// Remove a DevTools extension by name.
		/// </summary>
		/// <param name="name"></param>
		public void removeDevToolsExtension(string name) {
			API.Apply("removeDevToolsExtension", name);
		}

		/// <summary>
		/// Returns Object - The keys are the extension names
		/// and each value is an Object containing name and version properties.
		/// </summary>
		/// <returns></returns>
		public JsonObject getDevToolsExtensions() {
			object result = API.Apply("getDevToolsExtensions");
			return new JsonObject(result);
		}
	}
}
