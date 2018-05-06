using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Create and control browser windows.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class BrowserWindowClass : NodeModule {
		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public BrowserWindowClass(SocketronClient client, int id) {
			_client = client;
			_id = id;
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
				Script.GetObject(_id),
				Script.AddObject("window")
			);
			object[] result = _ExecuteBlocking<object[]>(script);
			List<BrowserWindow> windows = new List<BrowserWindow>();
			foreach (object item in result) {
				BrowserWindow window = new BrowserWindow(_client, (int)item);
				windows.Add(window);
			}
			return windows;
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
				Script.GetObject(_id),
				Script.AddObject("window")
			);
			int result = _ExecuteBlocking<int>(script);
			return new BrowserWindow(_client, result);
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
				Script.GetObject(_id),
				Script.GetObject(webContents._id),
				Script.AddObject("window")
			);
			int result = _ExecuteBlocking<int>(script);
			return new BrowserWindow(_client, result);
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
				Script.GetObject(_id),
				Script.GetObject(browserView._id),
				Script.AddObject("window")
			);
			int result = _ExecuteBlocking<int>(script);
			return new BrowserWindow(_client, result);
		}

		/// <summary>
		/// Returns BrowserWindow - The window with the given id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public BrowserWindow fromId(int id) {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = {0}.fromId({1});",
					"if (window == null) {{",
						"return null;",
					"}}",
					"return {2};"
				),
				Script.GetObject(_id),
				id,
				Script.AddObject("window")
			);
			int result = _ExecuteBlocking<int>(script);
			return new BrowserWindow(_client, result);
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
			string script = ScriptBuilder.Build(
				"{0}.addExtension({1});",
				Script.GetObject(_id),
				path.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Remove a Chrome extension by name.
		/// <para>
		/// Note: This API cannot be called before the ready event of the app module is emitted.
		/// </para>
		/// </summary>
		/// <param name="name"></param>
		public void removeExtension(string name) {
			string script = ScriptBuilder.Build(
				"{0}.removeExtension({1});",
				Script.GetObject(_id),
				name.Escape()
			);
			_ExecuteJavaScript(script);
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
				Script.GetObject(_id)
			);
			object result = _ExecuteBlocking<object>(script);
			return new JsonObject(result);
		}

		/// <summary>
		/// Adds DevTools extension located at path, and returns extension's name.
		/// </summary>
		/// <param name="path"></param>
		public void addDevToolsExtension(string path) {
			string script = ScriptBuilder.Build(
				"{0}.addDevToolsExtension({1});",
				Script.GetObject(_id),
				path.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Remove a DevTools extension by name.
		/// </summary>
		/// <param name="name"></param>
		public void removeDevToolsExtension(string name) {
			string script = ScriptBuilder.Build(
				"{0}.removeDevToolsExtension({1});",
				Script.GetObject(_id),
				name.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Object - The keys are the extension names
		/// and each value is an Object containing name and version properties.
		/// </summary>
		/// <returns></returns>
		public JsonObject getDevToolsExtensions() {
			string script = ScriptBuilder.Build(
				"return {0}.getDevToolsExtensions();",
				Script.GetObject(_id)
			);
			object result = _ExecuteBlocking<object>(script);
			return new JsonObject(result);
		}
	}
}
