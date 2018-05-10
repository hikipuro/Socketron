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
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public BrowserWindowModule(SocketronClient client, int id) {
			API.client = client;
			API.id = id;
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
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var BrowserWindow = {0};",
					"var window = new BrowserWindow({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				options.Stringify(),
				Script.AddObject("window")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new BrowserWindow(API.client, result);
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
		public List<BrowserWindow> getAllWindows() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var result = [];",
					"var windows = {0}.getAllWindows();",
					"for (var window of windows) {{",
						"result.push({1});",
					"}}",
					"return result;"
				),
				Script.GetObject(API.id),
				Script.AddObject("window")
			);
			object[] result = API._ExecuteBlocking<object[]>(script);
			return API.CreateObjectList<BrowserWindow>(result);
		}

		/// <summary>
		/// Returns BrowserWindow | null - The window that is focused in this application,
		/// otherwise returns null.
		/// </summary>
		/// <returns></returns>
		public BrowserWindow getFocusedWindow() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = {0}.getFocusedWindow();",
					"if (window == null) {{",
						"return null",
					"}}",
					"return {1};"
				),
				Script.GetObject(API.id),
				Script.AddObject("window")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new BrowserWindow(API.client, result);
		}

		/// <summary>
		/// Returns BrowserWindow - The window that owns the given webContents.
		/// </summary>
		/// <param name="webContents"></param>
		/// <returns></returns>
		public BrowserWindow fromWebContents(WebContents webContents) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = {0}.fromWebContents({1});",
					"if (window == null) {{",
						"return null",
					"}}",
					"return {2};"
				),
				Script.GetObject(API.id),
				Script.GetObject(webContents.API.id),
				Script.AddObject("window")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new BrowserWindow(API.client, result);
		}

		/// <summary>
		/// Returns BrowserWindow | null - The window that owns the given browserView.
		/// If the given view is not attached to any window, returns null.
		/// </summary>
		/// <param name="browserView"></param>
		/// <returns></returns>
		public BrowserWindow fromBrowserView(BrowserView browserView) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = {0}.fromBrowserView({1});",
					"if (window == null) {{",
						"return null",
					"}}",
					"return {2};"
				),
				Script.GetObject(API.id),
				Script.GetObject(browserView.API.id),
				Script.AddObject("window")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new BrowserWindow(API.client, result);
		}

		/// <summary>
		/// Returns BrowserWindow - The window with the given id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public BrowserWindow fromId(int id) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = {0}.fromId({1});",
					"if (window == null) {{",
						"return null;",
					"}}",
					"return {2};"
				),
				Script.GetObject(API.id),
				id,
				Script.AddObject("window")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new BrowserWindow(API.client, result);
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
			string script = ScriptBuilder.Build(
				"return {0}.getExtensions();",
				Script.GetObject(API.id)
			);
			object result = API._ExecuteBlocking<object>(script);
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
			string script = ScriptBuilder.Build(
				"return {0}.getDevToolsExtensions();",
				Script.GetObject(API.id)
			);
			object result = API._ExecuteBlocking<object>(script);
			return new JsonObject(result);
		}
	}
}
