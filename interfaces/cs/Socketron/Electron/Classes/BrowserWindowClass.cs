using System;
using System.Collections.Generic;

namespace Socketron {
	/// <summary>
	/// Create and control browser windows.
	/// <para>Process: Main</para>
	/// </summary>
	public class BrowserWindowClass : ElectronBase {
		public BrowserWindowClass(Socketron socketron) {
			_socketron = socketron;
		}

		public BrowserWindow Create(BrowserWindow.Options options = null) {
			if (options == null) {
				options = new BrowserWindow.Options();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = new electron.BrowserWindow({0});",
					"return [window.id, window.webContents.id];"
				),
				options.Stringify()
			);
			BrowserWindow window = null;
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			int? windowId = result[0] as int?;
			int? contentsId = result[1] as int?;
			if (windowId != null && contentsId != null) {
				window = new BrowserWindow(_socketron) {
					ID = (int)windowId
				};
				window.webContents = new WebContents(_socketron, window) {
					ID = (int)contentsId
				};
			} else {
				Console.Error.WriteLine("error");
			}
			return window;
		}

		public List<BrowserWindow> GetAllWindows() {
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
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			List<BrowserWindow> windows = new List<BrowserWindow>();
			foreach (object[] item in result) {
				int windowId = (int)item[0];
				int contentsId = (int)item[1];
				BrowserWindow window = new BrowserWindow(_socketron) {
					ID = windowId
				};
				window.webContents = new WebContents(_socketron, window) {
					ID = contentsId
				};
				windows.Add(window);
			}
			return windows;
		}

		public BrowserWindow GetFocusedWindow() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.getFocusedWindow();",
					"if (window == null) {{",
						"return null",
					"}}",
					"return [window.id,window.webContents.id];"
				)
			);
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			int windowId = (int)result[0];
			int contentsId = (int)result[1];
			BrowserWindow window = new BrowserWindow(_socketron) {
				ID = windowId,
			};
			window.webContents = new WebContents(_socketron, window) {
				ID = contentsId
			};
			return window;
		}

		public BrowserWindow FromWebContents(WebContents webContents) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"var window = electron.BrowserWindow.fromWebContents(contents);",
					"if (window == null) {{",
						"return null",
					"}}",
					"return [window.id,window.webContents.id];"
				),
				webContents.ID
			);
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			int windowId = (int)result[0];
			int contentsId = (int)result[1];
			BrowserWindow window = new BrowserWindow(_socketron) {
				ID = windowId
			};
			window.webContents = new WebContents(_socketron, window) {
				ID = contentsId
			};
			return window;
		}

		public BrowserWindow FromBrowserView(BrowserView browserView) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"var window = electron.BrowserWindow.fromBrowserView(view);",
					"if (window == null) {{",
						"return null",
					"}}",
					"return [window.id,window.webContents.id];"
				),
				browserView.ID
			);
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			int windowId = (int)result[0];
			int contentsId = (int)result[1];
			BrowserWindow window = new BrowserWindow(_socketron) {
				ID = windowId
			};
			window.webContents = new WebContents(_socketron, window) {
				ID = contentsId
			};
			return window;
		}

		public BrowserWindow FromId(int id) {
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
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			int windowId = (int)result[0];
			int contentsId = (int)result[1];
			BrowserWindow window = new BrowserWindow(_socketron) {
				ID = windowId
			};
			window.webContents = new WebContents(_socketron, window) {
				ID = contentsId
			};
			return window;
		}

		public void AddExtension(string path) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.BrowserWindow.addExtension({0});"
				),
				path.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public void RemoveExtension(string name) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.BrowserWindow.removeExtension({0});"
				),
				name.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public JsonObject GetExtensions() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.BrowserWindow.getExtensions();"
				)
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return new JsonObject(result);
		}

		public void AddDevToolsExtension(string path) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.BrowserWindow.addDevToolsExtension({0});"
				),
				path.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public void RemoveDevToolsExtension(string name) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.BrowserWindow.removeDevToolsExtension({0});"
				),
				name.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public JsonObject GetDevToolsExtensions() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.BrowserWindow.getDevToolsExtensions();"
				)
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return new JsonObject(result);
		}
	}
}
