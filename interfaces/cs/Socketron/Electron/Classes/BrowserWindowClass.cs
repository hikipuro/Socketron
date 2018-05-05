using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Create and control browser windows.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class BrowserWindowClass {
		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		public BrowserWindowClass() {
		}

		/// <summary>
		/// Returns BrowserWindow[] - An array of all opened browser windows.
		/// </summary>
		/// <returns></returns>
		public List<BrowserWindow> getAllWindows() {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var result = [];",
					"var windows = electron.BrowserWindow.getAllWindows();",
					"for (var window of windows) {{",
						"result.push([window.id,window.webContents.id]);",
					"}}",
					"return result;"
				)
			);
			object[] result = client.ExecuteJavaScriptBlocking<object[]>(script);
			List<BrowserWindow> windows = new List<BrowserWindow>();
			foreach (object[] item in result) {
				int windowId = (int)item[0];
				int contentsId = (int)item[1];
				BrowserWindow window = new BrowserWindow(client) {
					_id = windowId
				};
				window.webContents = new WebContents(client, window) {
					_id = contentsId
				};
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
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.getFocusedWindow();",
					"if (window == null) {{",
						"return null",
					"}}",
					"return [window.id,window.webContents.id];"
				)
			);
			object[] result = client.ExecuteJavaScriptBlocking<object[]>(script);
			int windowId = (int)result[0];
			int contentsId = (int)result[1];
			BrowserWindow window = new BrowserWindow(client) {
				_id = windowId,
			};
			window.webContents = new WebContents(client, window) {
				_id = contentsId
			};
			return window;
		}

		/// <summary>
		/// Returns BrowserWindow - The window that owns the given webContents.
		/// </summary>
		/// <param name="webContents"></param>
		/// <returns></returns>
		public BrowserWindow fromWebContents(WebContents webContents) {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"var window = electron.BrowserWindow.fromWebContents(contents);",
					"if (window == null) {{",
						"return null",
					"}}",
					"return [window.id,window.webContents.id];"
				),
				webContents._id
			);
			object[] result = client.ExecuteJavaScriptBlocking<object[]>(script);
			int windowId = (int)result[0];
			int contentsId = (int)result[1];
			BrowserWindow window = new BrowserWindow(client) {
				_id = windowId
			};
			window.webContents = new WebContents(client, window) {
				_id = contentsId
			};
			return window;
		}

		/// <summary>
		/// Returns BrowserWindow | null - The window that owns the given browserView.
		/// If the given view is not attached to any window, returns null.
		/// </summary>
		/// <param name="browserView"></param>
		/// <returns></returns>
		public BrowserWindow fromBrowserView(BrowserView browserView) {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"var window = electron.BrowserWindow.fromBrowserView(view);",
					"if (window == null) {{",
						"return null",
					"}}",
					"return [window.id,window.webContents.id];"
				),
				browserView._id
			);
			object[] result = client.ExecuteJavaScriptBlocking<object[]>(script);
			int windowId = (int)result[0];
			int contentsId = (int)result[1];
			BrowserWindow window = new BrowserWindow(client) {
				_id = windowId
			};
			window.webContents = new WebContents(client, window) {
				_id = contentsId
			};
			return window;
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
					"var window = electron.BrowserWindow.fromId({0});",
					"if (window == null) {{",
						"return null;",
					"}}",
					"return [window.id,window.webContents.id];"
				),
				id
			);
			object[] result = client.ExecuteJavaScriptBlocking<object[]>(script);
			int windowId = (int)result[0];
			int contentsId = (int)result[1];
			BrowserWindow window = new BrowserWindow(client) {
				_id = windowId
			};
			window.webContents = new WebContents(client, window) {
				_id = contentsId
			};
			return window;
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
				ScriptBuilder.Script(
					"electron.BrowserWindow.addExtension({0});"
				),
				path.Escape()
			);
			SocketronClient.Execute(script);
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
				ScriptBuilder.Script(
					"electron.BrowserWindow.removeExtension({0});"
				),
				name.Escape()
			);
			SocketronClient.Execute(script);
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
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.BrowserWindow.getExtensions();"
				)
			);
			object result = client.ExecuteJavaScriptBlocking<object>(script);
			return new JsonObject(result);
		}

		/// <summary>
		/// Adds DevTools extension located at path, and returns extension's name.
		/// </summary>
		/// <param name="path"></param>
		public void addDevToolsExtension(string path) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.BrowserWindow.addDevToolsExtension({0});"
				),
				path.Escape()
			);
			SocketronClient.Execute(script);
		}

		/// <summary>
		/// Remove a DevTools extension by name.
		/// </summary>
		/// <param name="name"></param>
		public void removeDevToolsExtension(string name) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.BrowserWindow.removeDevToolsExtension({0});"
				),
				name.Escape()
			);
			SocketronClient.Execute(script);
		}

		/// <summary>
		/// Returns Object - The keys are the extension names
		/// and each value is an Object containing name and version properties.
		/// </summary>
		/// <returns></returns>
		public JsonObject getDevToolsExtensions() {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.BrowserWindow.getDevToolsExtensions();"
				)
			);
			object result = client.ExecuteJavaScriptBlocking<object>(script);
			return new JsonObject(result);
		}
	}
}
