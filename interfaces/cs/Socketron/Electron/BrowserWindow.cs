using System;
using System.Collections.Generic;
using System.Threading;

namespace Socketron {
	/// <summary>
	/// Create and control browser windows.
	/// <para>Process: Main</para>
	/// </summary>
	public partial class BrowserWindow {
		public const string Name = "BrowserWindow";
		public int ID = 0;
		public WebContents WebContents;
		protected Socketron _socketron;

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		public class Events {
			public const string PageTitleUpdated = "page-title-updated";
			public const string Close = "close";
			public const string Closed = "closed";
			/// <summary>*Windows*</summary>
			public const string SessionEnd = "session-end";
			public const string Unresponsive = "unresponsive";
			public const string Responsive = "responsive";
			public const string Blur = "blur";
			public const string Focus = "focus";
			public const string Show = "show";
			public const string Hide = "hide";
			public const string ReadyToShow = "ready-to-show";
			public const string Maximize = "maximize";
			public const string Unmaximize = "unmaximize";
			public const string Minimize = "minimize";
			public const string Restore = "restore";
			public const string Resize = "resize";
			public const string Move = "move";
			/// <summary>*macOS*</summary>
			public const string Moved = "moved";
			public const string EnterFullScreen = "enter-full-screen";
			public const string LeaveFullScreen = "leave-full-screen";
			public const string EnterHtmlFullScreen = "enter-html-full-screen";
			public const string LeaveHtmlFullScreen = "leave-html-full-screen";
			/// <summary>*Windows*</summary>
			public const string AppCommand = "app-command";
			/// <summary>*macOS*</summary>
			public const string ScrollTouchBegin = "scroll-touch-begin";
			/// <summary>*macOS*</summary>
			public const string ScrollTouchEnd = "scroll-touch-end";
			/// <summary>*macOS*</summary>
			public const string ScrollTouchEdge = "scroll-touch-edge";
			/// <summary>*macOS*</summary>
			public const string Swipe = "swipe";
			/// <summary>*macOS*</summary>
			public const string SheetBegin = "sheet-begin";
			/// <summary>*macOS*</summary>
			public const string SheetEnd = "sheet-end";
			/// <summary>*macOS*</summary>
			public const string NewWindowForTab = "new-window-for-tab";
		}

		public BrowserWindow() {
		}

		public static Callback GetCallbackFromId(ushort id) {
			if (!_callbackList.ContainsKey(id)) {
				return null;
			}
			return _callbackList[id];
		}
		
		public static BrowserWindow Create(Socketron socketron, Options options = null) {
			if (options == null) {
				options = new Options();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = new electron.BrowserWindow({0});",
					"return [window.id, window.webContents.id];"
				),
				options.Stringify()
			);
			BrowserWindow window = null;
			object[] result = _ExecuteJavaScriptBlocking<object[]>(socketron, script);
			int? windowId = result[0] as int?;
			int? contentsId = result[1] as int?;
			if (windowId != null && contentsId != null) {
				window = new BrowserWindow() {
					ID = (int)windowId,
					_socketron = socketron
				};
				window.WebContents = new WebContents(window) {
					ID = (int)contentsId
				};
			} else {
				Console.Error.WriteLine("error");
			}
			return window;
		}

		public static List<BrowserWindow> GetAllWindows(Socketron socketron) {
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
			object[] result = _ExecuteJavaScriptBlocking<object[]>(socketron, script);
			List<BrowserWindow> windows = new List<BrowserWindow>();
			foreach (object[] item in result) {
				int windowId = (int)item[0];
				int contentsId = (int)item[1];
				BrowserWindow window = new BrowserWindow() {
					ID = windowId,
					_socketron = socketron
				};
				window.WebContents = new WebContents(window) {
					ID = contentsId
				};
				windows.Add(window);
			}
			return windows;
		}

		public static BrowserWindow GetFocusedWindow(Socketron socketron) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.getFocusedWindow();",
					"if (window == null) {{",
						"return null",
					"}}",
					"return [window.id,window.webContents.id];"
				)
			);
			object[] result = _ExecuteJavaScriptBlocking<object[]>(socketron, script);
			int windowId = (int)result[0];
			int contentsId = (int)result[1];
			BrowserWindow window = new BrowserWindow() {
				ID = windowId,
				_socketron = socketron
			};
			window.WebContents = new WebContents(window) {
				ID = contentsId
			};
			return window;
		}

		public static BrowserWindow FromWebContents(Socketron socketron, WebContents webContents) {
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
			object[] result = _ExecuteJavaScriptBlocking<object[]>(socketron, script);
			int windowId = (int)result[0];
			int contentsId = (int)result[1];
			BrowserWindow window = new BrowserWindow() {
				ID = windowId,
				_socketron = socketron
			};
			window.WebContents = new WebContents(window) {
				ID = contentsId
			};
			return window;
		}
		
		public static BrowserWindow FromBrowserView(Socketron socketron, BrowserView browserView) {
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
			object[] result = _ExecuteJavaScriptBlocking<object[]>(socketron, script);
			int windowId = (int)result[0];
			int contentsId = (int)result[1];
			BrowserWindow window = new BrowserWindow() {
				ID = windowId,
				_socketron = socketron
			};
			window.WebContents = new WebContents(window) {
				ID = contentsId
			};
			return window;
		}

		public static BrowserWindow FromId(Socketron socketron, int id) {
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
			object[] result = _ExecuteJavaScriptBlocking<object[]>(socketron, script);
			int windowId = (int)result[0];
			int contentsId = (int)result[1];
			BrowserWindow window = new BrowserWindow() {
				ID = windowId,
				_socketron = socketron
			};
			window.WebContents = new WebContents(window) {
				ID = contentsId
			};
			return window;
		}

		public static void AddExtension(Socketron socketron, string path) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.BrowserWindow.addExtension({0});"
				),
				path.Escape()
			);
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static void RemoveExtension(Socketron socketron, string name) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.BrowserWindow.removeExtension({0});"
				),
				name.Escape()
			);
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static JsonObject GetExtensions(Socketron socketron) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.BrowserWindow.getExtensions();"
				)
			);
			object result = _ExecuteJavaScriptBlocking<object>(socketron, script);
			return new JsonObject(result);
		}

		public static void AddDevToolsExtension(Socketron socketron, string path) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.BrowserWindow.addDevToolsExtension({0});"
				),
				path.Escape()
			);
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static void RemoveDevToolsExtension(Socketron socketron, string name) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.BrowserWindow.removeDevToolsExtension({0});"
				),
				name.Escape()
			);
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static JsonObject GetDevToolsExtensions(Socketron socketron) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.BrowserWindow.getDevToolsExtensions();"
				)
			);
			object result = _ExecuteJavaScriptBlocking<object>(socketron, script);
			return new JsonObject(result);
		}

		public void ExecuteJavaScript(string script, Callback success = null, Callback error = null) {
			_socketron.Main.ExecuteJavaScript(script, success, error);
		}

		public void ExecuteJavaScript(string[] script, Callback success = null, Callback error = null) {
			_socketron.Main.ExecuteJavaScript(script, success, error);
		}

		public T ExecuteJavaScriptBlocking<T>(string[] script) {
			return _ExecuteJavaScriptBlocking<T>(string.Join("\n", script));
		}

		public void On(string eventName, Callback callback) {
			if (callback == null) {
				return;
			}
			_callbackList.Add(_callbackListId, callback);
			string[] script = new[] {
				"var window = electron." + Name + ".fromId(" + ID + ");",
				"var listener = () => {",
					"emit('__event'," + Name.Escape() + "," + _callbackListId + ");",
				"};",
				"this._addClientEventListener(" + Name.Escape() + "," + _callbackListId + ",listener);",
				"window.on(" + eventName.Escape() + ", listener);"
			};
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		public void Once(string eventName, Callback callback) {
			if (callback == null) {
				return;
			}
			_callbackList.Add(_callbackListId, callback);
			string[] script = new[] {
				"var window = electron." + Name + ".fromId(" + ID + ");",
				"var listener = () => {",
					"this._removeClientEventListener(" + Name.Escape() + "," + _callbackListId + ");",
					"emit('__event'," + Name.Escape() + "," + _callbackListId + ");",
				"};",
				"this._addClientEventListener(" + Name.Escape() + "," + _callbackListId + ",listener);",
				"window.once(" + eventName.Escape() + ", listener);"
			};
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Force closing the window.
		/// <para>
		/// Force closing the window, the unload and beforeunload event won't be emitted for the web page,
		/// and close event will also not be emitted for this window,
		/// but it guarantees the closed event will be emitted.
		/// </para>
		/// </summary>
		public void Destroy() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.destroy();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Try to close the window.
		/// <para>
		/// This has the same effect as a user manually clicking the close button of the window.
		/// The web page may cancel the close though. See the close event.
		/// </para>
		/// </summary>
		public void Close() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.close();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Focuses on the window.
		/// </summary>
		public void Focus() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.focus();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Removes focus from the window.
		/// </summary>
		public void Blur() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.blur();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is focused.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsFocused() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isFocused();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Whether the window is destroyed.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsDestroyed() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isDestroyed();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Shows and gives focus to the window.
		/// </summary>
		public void Show() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.show();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Shows the window but doesn't focus on it.
		/// </summary>
		public void ShowInactive() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.showInactive();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Hides the window.
		/// </summary>
		public void Hide() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.hide();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is visible to the user.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsVisible() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isVisible();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Whether current window is a modal window.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsModal() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isModal();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Maximizes the window.
		/// <para>
		/// This will also show (but not focus) the window if it isn't being displayed already.
		/// </para>
		/// </summary>
		public void Maximize() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.maximize();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Unmaximizes the window.
		/// </summary>
		public void Unmaximize() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.unmaximize();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is maximized.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsMaximized() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isMaximized();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Minimizes the window.
		/// <para>
		/// On some platforms the minimized window will be shown in the Dock.
		/// </para>
		/// </summary>
		public void Minimize() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.minimize();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Restores the window from minimized state to its previous state.
		/// </summary>
		public void Restore() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.restore();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is minimized.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsMinimized() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isMinimized();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Sets whether the window should be in fullscreen mode.
		/// </summary>
		/// <param name="flag">bool</param>
		public void SetFullScreen(bool flag) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setFullScreen({1});"
				),
				ID,
				flag.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is in fullscreen mode.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsFullScreen() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isFullScreen();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS* Enters or leaves simple fullscreen mode.
		/// <para>
		/// Simple fullscreen mode emulates the native fullscreen behavior
		/// found in versions of Mac OS X prior to Lion (10.7).
		/// </para>
		/// </summary>
		/// <param name="flag">bool</param>
		public void SetSimpleFullScreen(bool flag) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setSimpleFullScreen({1});"
				),
				ID,
				flag.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Whether the window is in simple (pre-Lion) fullscreen mode.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsSimpleFullScreen() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isSimpleFullScreen();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS* This will make a window maintain an aspect ratio.
		/// <para>
		/// The extra size allows a developer to have space, specified in pixels,
		/// not included within the aspect ratio calculations.
		/// This API already takes into account the difference between a window's size and its content size.
		/// </para>
		/// </summary>
		/// <param name="aspectRatio">The aspect ratio to maintain for some portion of the content view.</param>
		/// <param name="size">The extra size not to be included while maintaining the aspect ratio.</param>
		public void SetAspectRatio(double aspectRatio, Size size = null) {
			string option = string.Empty;
			if (size == null) {
				option = ScriptBuilder.Params(
					aspectRatio.ToString()
				);
			} else {
				option = ScriptBuilder.Params(
					aspectRatio.ToString(),
					size.Stringify()
				);
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setAspectRatio({1});"
				),
				ID,
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Uses Quick Look to preview a file at a given path.
		/// </summary>
		/// <param name="path">
		/// The absolute path to the file to preview with QuickLook.
		/// This is important as Quick Look uses the file name and file extension
		/// on the path to determine the content type of the file to open.
		/// </param>
		/// <param name="displayName">
		/// The name of the file to display on the Quick Look modal view.
		/// This is purely visual and does not affect the content type of the file.
		/// Defaults to path.
		/// </param>
		public void PreviewFile(string path, string displayName = null) {
			string option = string.Empty;
			if (displayName == null) {
				option = ScriptBuilder.Params(
					path.Escape()
				);
			} else {
				option = ScriptBuilder.Params(
					path.Escape(),
					displayName.Escape()
				);
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.previewFile({1});"
				),
				ID,
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Closes the currently open Quick Look panel.
		/// </summary>
		public void CloseFilePreview() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.closeFilePreview();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Resizes and moves the window to the supplied bounds.
		/// </summary>
		/// <param name="bounds">Rectangle.</param>
		public void SetBounds(Rectangle bounds) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setBounds({1});"
				),
				ID,
				bounds.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Resizes and moves the window to the supplied bounds.
		/// </summary>
		/// <param name="bounds">Rectangle.</param>
		/// <param name="animate">*macOS*</param>
		public void SetBounds(Rectangle bounds, bool animate) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setBounds({1},{2});"
				),
				ID,
				bounds.Stringify(),
				animate.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>Rectangle.</returns>
		public Rectangle GetBounds() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.getBounds();"
				),
				ID
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return Rectangle.FromObject(result);
		}

		/// <summary>
		/// Resizes and moves the window's client area (e.g. the web page) to the supplied bounds.
		/// </summary>
		/// <param name="bounds">Rectangle.</param>
		public void SetContentBounds(Rectangle bounds) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setContentBounds({1});"
				),
				ID,
				bounds.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Resizes and moves the window's client area (e.g. the web page) to the supplied bounds.
		/// </summary>
		/// <param name="bounds">Rectangle.</param>
		/// <param name="animate">*macOS*</param>
		public void SetContentBounds(Rectangle bounds, bool animate) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setContentBounds({1},{2});"
				),
				ID,
				bounds.Stringify(),
				animate.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>Rectangle.</returns>
		public Rectangle GetContentBounds() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.getContentBounds();"
				),
				ID
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return Rectangle.FromObject(result);
		}

		/// <summary>
		/// Disable or enable the window.
		/// </summary>
		/// <param name="enable">bool</param>
		public void SetEnabled(bool enable) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setEnabled({1});"
				),
				ID,
				enable.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Resizes the window to width and height.
		/// </summary>
		/// <param name="width">int</param>
		/// <param name="height">int</param>
		public void SetSize(int width, int height) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setSize({1},{2});"
				),
				ID,
				width,
				height
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Resizes the window to width and height.
		/// </summary>
		/// <param name="width">int</param>
		/// <param name="height">int</param>
		/// <param name="animate">*macOS*</param>
		public void SetSize(int width, int height, bool animate) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setSize({1},{2},{3});"
				),
				ID,
				width,
				height,
				animate.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Contains the window's width and height.
		/// </summary>
		/// <returns></returns>
		public Size GetSize() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.getSize();"
				),
				ID
			);
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			return new Size() {
				width = (int)result[0],
				height = (int)result[1]
			};
		}

		/// <summary>
		/// Resizes the window's client area (e.g. the web page) to width and height.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void SetContentSize(int width, int height) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setContentSize({1},{2});"
				),
				ID,
				width,
				height
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Resizes the window's client area (e.g. the web page) to width and height.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="animate">*macOS*</param>
		public void SetContentSize(int width, int height, bool animate) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setContentSize({1},{2},{3});"
				),
				ID,
				width,
				height,
				animate.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Contains the window's client area's width and height.
		/// </summary>
		/// <returns></returns>
		public Size GetContentSize() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.getContentSize();"
				),
				ID
			);
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			return new Size() {
				width = (int)result[0],
				height = (int)result[1]
			};
		}

		/// <summary>
		/// Sets the minimum size of window to width and height.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void SetMinimumSize(int width, int height) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setMinimumSize({1},{2});"
				),
				ID,
				width,
				height
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Contains the window's minimum width and height.
		/// </summary>
		/// <returns></returns>
		public Size GetMinimumSize() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.getMinimumSize();"
				),
				ID
			);
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			return new Size() {
				width = (int)result[0],
				height = (int)result[1]
			};
		}

		/// <summary>
		/// Sets the maximum size of window to width and height.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void SetMaximumSize(int width, int height) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setMaximumSize({1},{2});"
				),
				ID,
				width,
				height
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Contains the window's maximum width and height.
		/// </summary>
		/// <returns></returns>
		public Size GetMaximumSize() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.getMaximumSize();"
				),
				ID
			);
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			return new Size() {
				width = (int)result[0],
				height = (int)result[1]
			};
		}

		/// <summary>
		/// Sets whether the window can be manually resized by user.
		/// </summary>
		/// <param name="resizable"></param>
		public void SetResizable(bool resizable) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setResizable({1});"
				),
				ID,
				resizable.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window can be manually resized by user.
		/// </summary>
		/// <returns></returns>
		public bool IsResizable() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isResizable();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows* Sets whether the window can be moved by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="movable"></param>
		public void SetMovable(bool movable) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setMovable({1});"
				),
				ID,
				movable.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// *macOS Windows* Whether the window can be moved by user.
		///	On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool IsMovable() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isMovable();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows* Sets whether the window can be manually minimized by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="minimizable"></param>
		public void SetMinimizable(bool minimizable) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setMinimizable({1});"
				),
				ID,
				minimizable.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// *macOS Windows* Whether the window can be manually minimized by user.
		/// On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool IsMinimizable() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isMinimizable();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows* Sets whether the window can be manually maximized by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="maximizable"></param>
		public void SetMaximizable(bool maximizable) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setMaximizable({1});"
				),
				ID,
				maximizable.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows* Whether the window can be manually maximized by user.
		/// On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool IsMaximizable() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isMaximizable();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Sets whether the maximize/zoom window button toggles fullscreen mode or maximizes the window.
		/// </summary>
		/// <param name="fullscreenable"></param>
		public void SetFullScreenable(bool fullscreenable) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setFullScreenable({1});"
				),
				ID,
				fullscreenable.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the maximize/zoom window button toggles fullscreen mode or maximizes the window.
		/// </summary>
		/// <returns></returns>
		public bool IsFullScreenable() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isFullScreenable();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows* Sets whether the window can be manually closed by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="closable"></param>
		public void SetClosable(bool closable) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setClosable({1});"
				),
				ID,
				closable.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows* Whether the window can be manually closed by user.
		/// On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool IsClosable() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isClosable();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Sets whether the window should show always on top of other windows.
		/// <para>
		/// After setting this, the window is still a normal window,
		/// not a toolbox window which can not be focused on.
		/// </para>
		/// </summary>
		/// <param name="flag"></param>
		public void SetAlwaysOnTop(bool flag) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setAlwaysOnTop({1});"
				),
				ID,
				flag.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets whether the window should show always on top of other windows.
		/// <para>
		/// After setting this, the window is still a normal window,
		/// not a toolbox window which can not be focused on.
		/// </para>
		/// </summary>
		/// <param name="flag"></param>
		/// <param name="level">
		/// *macOS* Values include normal, floating, torn-off-menu, modal-panel,
		/// main-menu, status, pop-up-menu, screen-saver, and dock (Deprecated).
		/// The default is floating. See the macOS docs for more details.
		/// </param>
		/// <param name="relativeLevel">
		/// *macOS* The number of layers higher to set this window relative to the given level.
		/// The default is 0. Note that Apple discourages setting levels higher than 1 above screen-saver.
		/// </param>
		public void SetAlwaysOnTop(bool flag, string level, int relativeLevel = 0) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setAlwaysOnTop({1},{2},{3});"
				),
				ID,
				flag.Escape(),
				level,
				relativeLevel
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is always on top of other windows.
		/// </summary>
		/// <returns></returns>
		public bool IsAlwaysOnTop() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isAlwaysOnTop();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows* Moves window to top(z-order) regardless of focus.
		/// </summary>
		public void MoveTop() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.moveTop();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Moves window to the center of the screen.
		/// </summary>
		public void Center() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.center();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Moves window to x and y.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void SetPosition(int x, int y) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setPosition({1},{2});"
				),
				ID, x, y
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Moves window to x and y.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="animate">*macOS*</param>
		public void SetPosition(int x, int y, bool animate) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setPosition({1},{2},{3});"
				),
				ID, x, y, animate.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Contains the window's current position.
		/// </summary>
		/// <returns></returns>
		public Point GetPosition() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.getPosition();"
				),
				ID
			);
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			return new Point() {
				x = (int)result[0],
				y = (int)result[1]
			};
		}

		/// <summary>
		/// Changes the title of native window to title.
		/// </summary>
		/// <param name="title"></param>
		public void SetTitle(string title) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setTitle({1});"
				),
				ID,
				title.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// The title of the native window.
		/// <para>
		/// Note: The title of web page can be different from the title of the native window.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public string GetTitle() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.getTitle();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// *macOS* Changes the attachment point for sheets on macOS. 
		/// <para>
		/// By default, sheets are attached just below the window frame,
		/// but you may want to display them beneath a HTML-rendered toolbar.
		/// </para>
		/// </summary>
		/// <example>
		/// For example:
		/// <code>
		/// const {BrowserWindow} = require('electron')
		/// let win = new BrowserWindow()
		/// 
		/// let toolbarRect = document.getElementById('toolbar').getBoundingClientRect()
		/// win.setSheetOffset(toolbarRect.height)
		/// </code>
		/// </example>
		/// <param name="offsetY"></param>
		public void SetSheetOffset(double offsetY) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setSheetOffset({1});"
				),
				ID,
				offsetY
			);
			_ExecuteJavaScript(script);
		}

		public void SetSheetOffset(double offsetY, double offsetX) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setSheetOffset({1},{2});"
				),
				ID,
				offsetY,
				offsetX
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Starts or stops flashing the window to attract user's attention.
		/// </summary>
		/// <param name="flag"></param>
		public void FlashFrame(bool flag) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.flashFrame({1});"
				),
				ID,
				flag.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Makes the window not show in the taskbar.
		/// </summary>
		/// <param name="skip"></param>
		public void SetSkipTaskbar(bool skip) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setSkipTaskbar({1});"
				),
				ID,
				skip.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Enters or leaves the kiosk mode.
		/// </summary>
		/// <param name="flag"></param>
		public void SetKiosk(bool flag) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setSkipTaskbar({1});"
				),
				ID,
				flag.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is in kiosk mode.
		/// </summary>
		/// <returns></returns>
		public bool IsKiosk() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isKiosk();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// The native type of the handle is HWND on Windows, NSView* on macOS,
		/// and Window (unsigned long) on Linux.
		/// </summary>
		/// <returns>The platform-specific handle of the window.</returns>
		public ulong GetNativeWindowHandle() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.getNativeWindowHandle();"
				),
				ID
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			JsonObject json = new JsonObject(result);
			object[] data = json["data"] as object[];
			byte[] bytes = new byte[8];
			for (int i = 0; i < data.Length; i++) {
				object item = data[i];
				bytes[i] = (byte)(int)item;
			}
			return BitConverter.ToUInt64(bytes, 0);
		}

		/// <summary>
		/// *Windows* Hooks a windows message.
		/// The callback is called when the message is received in the WndProc.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="callback">JavaScript string</param>
		public void HookWindowMessage(int message, string callback) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.hookWindowMessage({1},{2});"
				),
				ID,
				message,
				callback.Escape()
			);
			ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows* true or false depending on whether the message is hooked.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public bool IsWindowMessageHooked(int message) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isWindowMessageHooked({1});"
				),
				ID,
				message
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows* Unhook the window message.
		/// </summary>
		/// <param name="message"></param>
		public void UnhookWindowMessage(int message) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.unhookWindowMessage({1});"
				),
				ID,
				message
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows* Unhooks all of the window messages.
		/// </summary>
		public void UnhookAllWindowMessages() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.unhookAllWindowMessages();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Sets the pathname of the file the window represents,
		/// and the icon of the file will show in window's title bar.
		/// </summary>
		/// <param name="filename"></param>
		public void SetRepresentedFilename(string filename) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setRepresentedFilename({1});"
				),
				ID,
				filename.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*  The pathname of the file the window represents.
		/// </summary>
		/// <returns></returns>
		public string GetRepresentedFilename() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.getRepresentedFilename();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// *macOS* Specifies whether the window’s document has been edited,
		/// and the icon in title bar will become gray when set to true.
		/// </summary>
		/// <param name="edited"></param>
		public void SetDocumentEdited(bool edited) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setDocumentEdited({1});"
				),
				ID,
				edited.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// *macOS* Whether the window's document has been edited.
		/// </summary>
		/// <returns></returns>
		public bool IsDocumentEdited() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isDocumentEdited();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// 
		/// </summary>
		public void FocusOnWebView() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.focusOnWebView();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// 
		/// </summary>
		public void BlurWebView() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.blurWebView();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Same as webContents.capturePage([rect, ]callback).
		/// </summary>
		public void CapturePage(Rectangle rect, Action callback) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Same as webContents.loadURL(url[, options]).
		/// <para>
		/// The url can be a remote address (e.g. http://)
		/// or a path to a local HTML file using the file:// protocol.
		/// </para>
		/// </summary>
		/// <param name="url"></param>
		public void LoadURL(string url) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.loadURL({1});"
				),
				ID,
				url.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <param name="options"></param>
		public void LoadURL(string url, JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.loadURL({1},{2});"
				),
				ID,
				url.Escape(),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Same as webContents.loadFile.
		/// <para>
		/// filePath should be a path to an HTML file relative to the root of your application.
		/// See the webContents docs for more information.
		/// </para>
		/// </summary>
		/// <param name="filePath"></param>
		public void LoadFile(string filePath) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.loadFile({1});"
				),
				ID,
				filePath.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Same as webContents.reload.
		/// </summary>
		public void Reload() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.reload();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}


		/// <summary>
		/// *Linux Windows* Sets the menu as the window's menu bar,
		/// setting it to null will remove the menu bar.
		/// </summary>
		/// <param name="menu"></param>
		public void SetMenu(Menu menu) {
			string script = string.Empty;
			if (menu == null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"window.setMenu(null);"
					),
					ID
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"var menu = this._objRefs[{1}];",
						"window.setMenu(menu);"
					),
					ID,
					menu.ID
				);
			}
			ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets progress value in progress bar. Valid range is [0, 1.0].
		/// </summary>
		/// <param name="progress"></param>
		public void SetProgressBar(double progress) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setProgressBar({1});"
				),
				ID,
				progress
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets progress value in progress bar. Valid range is [0, 1.0].
		/// </summary>
		/// <param name="progress"></param>
		/// <param name="options"></param>
		public void SetProgressBar(double progress, JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setProgressBar({1},{2});"
				),
				ID,
				progress,
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows* Sets a 16 x 16 pixel overlay onto the current taskbar icon,
		/// usually used to convey some sort of application status or to passively notify the user.
		/// </summary>
		/// <param name="overlay">
		/// the icon to display on the bottom right corner of the taskbar icon.
		/// If this parameter is null, the overlay is cleared
		/// </param>
		/// <param name="description">
		/// a description that will be provided to Accessibility screen readers
		/// </param>
		public void SetOverlayIcon(NativeImage overlay, string description) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"var image = this._objRefs[{1}];",
					"window.setOverlayIcon(image,{2});"
				),
				ID,
				overlay.ID,
				description.Escape()
			);
			ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Sets whether the window should have a shadow.
		/// On Windows and Linux does nothing.
		/// </summary>
		/// <param name="hasShadow"></param>
		public void SetHasShadow(bool hasShadow) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setHasShadow({1});"
				),
				ID,
				hasShadow.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// *macOS* Whether the window has a shadow.
		/// On Windows and Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool HasShadow() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.hasShadow();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows macOS* Sets the opacity of the window.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="opacity">between 0.0 (fully transparent) and 1.0 (fully opaque)</param>
		public void SetOpacity(double opacity) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setHasShadow({1});"
				),
				ID,
				opacity
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows macOS* between 0.0 (fully transparent) and 1.0 (fully opaque)
		/// </summary>
		/// <returns></returns>
		public double GetOpacity() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.getOpacity();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<double>(script);
		}

		/// <summary>
		/// *Windows* Add a thumbnail toolbar with a specified set of buttons
		/// to the thumbnail image of a window in a taskbar button layout.
		/// Returns a Boolean object indicates whether the thumbnail has been added successfully.
		/// </summary>
		/// <param name="buttons"></param>
		public void SetThumbarButtons(ThumbarButton[] buttons) {
			throw new NotImplementedException();
			/*
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.setThumbarButtons();"
				),
				ID
			);
			ExecuteJavaScript(script);
			//*/
		}

		/// <summary>
		/// *Windows* Sets the region of the window to show as the thumbnail image displayed
		/// when hovering over the window in the taskbar.
		/// You can reset the thumbnail to be the entire window by specifying an empty region:
		/// {x: 0, y: 0, width: 0, height: 0}.
		/// </summary>
		/// <param name="region">Region of the window</param>
		public void SetThumbnailClip(Rectangle region) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setThumbnailClip({1});"
				),
				ID,
				region.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows* Sets the toolTip that is displayed
		/// when hovering over the window thumbnail in the taskbar.
		/// </summary>
		/// <param name="toolTip"></param>
		public void SetThumbnailToolTip(string toolTip) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setThumbnailToolTip({1});"
				),
				ID,
				toolTip.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows* Sets the properties for the window's taskbar button.
		/// </summary>
		/// <param name="options"></param>
		public void SetAppDetails(JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setAppDetails({1});"
				),
				ID,
				options.Stringify()
			);
			ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Same as webContents.showDefinitionForSelection().
		/// </summary>
		public void ShowDefinitionForSelection() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.showDefinitionForSelection();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows Linux* Changes window icon.
		/// </summary>
		/// <param name="icon"></param>
		public void SetIcon(NativeImage icon) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"var icon = this._objRefs[{1}];",
					"window.setIcon(icon);"
				),
				ID,
				icon.ID
			);
			ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets whether the window menu bar should hide itself automatically.
		/// Once set the menu bar will only show when users press the single Alt key.
		/// </summary>
		/// <param name="hide"></param>
		public void SetAutoHideMenuBar(bool hide) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setAutoHideMenuBar({1});"
				),
				ID,
				hide.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// Whether menu bar automatically hides itself.
		/// </summary>
		/// <returns></returns>
		public bool IsMenuBarAutoHide() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isMenuBarAutoHide();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows Linux* Sets whether the menu bar should be visible.
		/// If the menu bar is auto-hide, users can still bring up the menu bar by pressing the single Alt key.
		/// </summary>
		/// <param name="visible"></param>
		public void SetMenuBarVisibility(bool visible) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setMenuBarVisibility({1});"
				),
				ID,
				visible.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// Whether the menu bar is visible.
		/// </summary>
		/// <returns></returns>
		public bool IsMenuBarVisible() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isMenuBarVisible();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Sets whether the window should be visible on all workspaces.
		/// <para>Note: This API does nothing on Windows.</para>
		/// </summary>
		/// <param name="visible"></param>
		public void SetVisibleOnAllWorkspaces(bool visible) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setVisibleOnAllWorkspaces({1});"
				),
				ID,
				visible.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// Whether the window is visible on all workspaces.
		/// <para>Note: This API always returns false on Windows.</para>
		/// </summary>
		/// <returns></returns>
		public bool IsVisibleOnAllWorkspaces() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"return window.isVisibleOnAllWorkspaces();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Makes the window ignore all mouse events.
		/// <para>
		/// All mouse events happened in this window will be passed to the window below this window,
		/// but if this window has focus, it will still receive keyboard events.
		/// </para>
		/// </summary>
		/// <param name="ignore"></param>
		public void SetIgnoreMouseEvents(bool ignore) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setIgnoreMouseEvents({1});"
				),
				ID,
				ignore.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ignore"></param>
		/// <param name="options"></param>
		public void SetIgnoreMouseEvents(bool ignore, JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setIgnoreMouseEvents({1},{2});"
				),
				ID,
				ignore.Escape(),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows* Prevents the window contents from being captured by other apps.
		/// <para>
		/// On macOS it sets the NSWindow's sharingType to NSWindowSharingNone.
		/// On Windows it calls SetWindowDisplayAffinity with WDA_MONITOR.
		/// </para>
		/// </summary>
		/// <param name="enable"></param>
		public void SetContentProtection(bool enable) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setContentProtection({1});"
				),
				ID,
				enable.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows* Changes whether the window can be focused.
		/// </summary>
		/// <param name="focusable"></param>
		public void SetFocusable(bool focusable) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setFocusable({1});"
				),
				ID,
				focusable.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Linux macOS* Sets parent as current window's parent window,
		/// passing null will turn current window into a top-level window.
		/// </summary>
		/// <param name="parent"></param>
		public void SetParentWindow(BrowserWindow parent) {
			string script = string.Empty;
			if (parent == null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"window.setParentWindow(null);"
					),
					ID
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"var parent = electron.BrowserWindow.fromId({1});",
						"window.setParentWindow(parent);"
					),
					ID,
					parent.ID
				);
			}
			ExecuteJavaScript(script);
		}

		/// <summary>
		/// The parent window.
		/// </summary>
		public BrowserWindow GetParentWindow() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"var parent = window.getParentWindow();",
					"if (parent == null) {{",
						"return null;",
					"}}",
					"return [parent.id,parent.webContents.id];"
				),
				ID
			);
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			int windowId = (int)result[0];
			int contentsId = (int)result[1];
			BrowserWindow window = new BrowserWindow() {
				ID = windowId,
				_socketron = _socketron
			};
			window.WebContents = new WebContents(window) {
				ID = contentsId
			};
			return window;
		}

		/// <summary>
		/// All child windows.
		/// </summary>
		/// <param name="callback"></param>
		/// <returns></returns>
		public List<BrowserWindow> GetChildWindows(Action<bool> callback) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var result = [];",
					"var window = electron.BrowserWindow.fromId({0});",
					"var windows = window.getChildWindows();",
					"for (var win of windows) {{",
						"result.push([win.id,win.webContents.id]);",
					"}}",
					"return result;"
				),
				ID
			);
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			List<BrowserWindow> windows = new List<BrowserWindow>();
			foreach (object[] item in result) {
				int windowId = (int)item[0];
				int contentsId = (int)item[1];
				BrowserWindow window = new BrowserWindow() {
					ID = windowId,
					_socketron = _socketron
				};
				window.WebContents = new WebContents(window) {
					ID = contentsId
				};
				windows.Add(window);
			}
			return windows;
		}

		/// <summary>
		/// *macOS* Controls whether to hide cursor when typing.
		/// </summary>
		/// <param name="autoHide"></param>
		public void SetAutoHideCursor(bool autoHide) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setAutoHideCursor({1});"
				),
				ID,
				autoHide.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Selects the previous tab when native tabs
		/// are enabled and there are other tabs in the window.
		/// </summary>
		public void SelectPreviousTab() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.selectPreviousTab();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Selects the next tab when native tabs
		/// are enabled and there are other tabs in the window.
		/// </summary>
		public void SelectNextTab() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.selectNextTab();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Merges all windows into one window with multiple tabs
		/// when native tabs are enabled and there is more than one open window.
		/// </summary>
		public void MergeAllWindows() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.mergeAllWindows();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Moves the current tab into a new window if native tabs are enabled
		/// and there is more than one tab in the current window.
		/// </summary>
		public void MoveTabToNewWindow() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.moveTabToNewWindow();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Toggles the visibility of the tab bar if native tabs
		/// are enabled and there is only one tab in the current window.
		/// </summary>
		public void ToggleTabBar() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.toggleTabBar();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Adds a window as a tab on this window,
		/// after the tab for the window instance.
		/// </summary>
		/// <param name="browserWindow"></param>
		public void AddTabbedWindow(BrowserWindow browserWindow) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"var window2 = electron.BrowserWindow.fromId({1});",
					"window.addTabbedWindow(window2);"
				),
				ID,
				browserWindow.ID
			);
			ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Adds a vibrancy effect to the browser window.
		/// <para>Passing null or an empty string will remove the vibrancy effect on the window.</para>
		/// </summary>
		/// <param name="type"></param>
		public void SetVibrancy(string type) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"window.setVibrancy({1});"
				),
				ID,
				type.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Experimental* Sets the touchBar layout for the current window.
		/// <para>
		/// Specifying null or undefined clears the touch bar.
		/// This method only has an effect if the machine has a touch bar and is running on macOS 10.12.1+.
		/// </para>
		/// </summary>
		public void SetTouchBar(TouchBar touchBar) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		public void SetBrowserView(BrowserView browserView) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"var view = electron.BrowserView.fromId({1});",
					"window.setBrowserView(view);"
				),
				ID,
				browserView.ID
			);
			ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Experimental* an attached BrowserView. Returns null if none is attached.
		/// <para>
		/// Note: The BrowserView API is currently experimental
		/// and may change or be removed in future Electron releases.
		/// </para>
		/// </summary>
		public BrowserView GetBrowserView() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = electron.BrowserWindow.fromId({0});",
					"var view = window.getBrowserView();",
					"return view.id;"
				),
				ID
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			BrowserView view = new BrowserView(_socketron) {
				ID = result
			};
			return view;
		}

		protected void _ExecuteJavaScript(string script) {
			_socketron.Main.ExecuteJavaScript(script);
		}

		protected void _ExecuteJavaScript(string script, Callback success, Callback error) {
			_socketron.Main.ExecuteJavaScript(script, success, error);
		}

		protected void _ExecuteJavaScript(string[] script) {
			_socketron.Main.ExecuteJavaScript(script);
		}

		protected void _ExecuteJavaScript(string[] script, Callback callback) {
			_socketron.Main.ExecuteJavaScript(script, callback);
		}

		protected void _ExecuteJavaScript(string[] script, Callback success, Callback error) {
			_socketron.Main.ExecuteJavaScript(script, success, error);
		}

		protected T _ExecuteJavaScriptBlocking<T>(string script) {
			bool done = false;
			T value = default(T);

			_ExecuteJavaScript(script, (result) => {
				if (result == null) {
					done = true;
					return;
				}
				if (typeof(T) == typeof(double)) {
					//Console.WriteLine(result.GetType());
					if (result.GetType() == typeof(int)) {
						result = (double)(int)result;
					} else if (result.GetType() == typeof(Decimal)) {
						result = (double)(Decimal)result;
					}
				}
				value = (T)result;
				done = true;
			}, (result) => {
				Console.Error.WriteLine("error: BrowserWindow._ExecuteJavaScriptBlocking");
				throw new InvalidOperationException(result as string);
				//done = true;
			});

			while (!done) {
				Thread.Sleep(TimeSpan.FromTicks(1));
			}
			return value;
		}

		protected static void _ExecuteJavaScript(Socketron socketron, string script, Callback success, Callback error) {
			socketron.Main.ExecuteJavaScript(script, success, error);
		}

		protected static T _ExecuteJavaScriptBlocking<T>(Socketron socketron, string script) {
			bool done = false;
			T value = default(T);

			_ExecuteJavaScript(socketron, script, (result) => {
				if (result == null) {
					done = true;
					return;
				}
				if (typeof(T) == typeof(double)) {
					//Console.WriteLine(result.GetType());
					if (result.GetType() == typeof(int)) {
						result = (double)(int)result;
					} else if (result.GetType() == typeof(Decimal)) {
						result = (double)(Decimal)result;
					}
				}
				value = (T)result;
				done = true;
			}, (result) => {
				Console.Error.WriteLine("error: BrowserWindow._ExecuteJavaScriptBlocking");
				throw new InvalidOperationException(result as string);
				//done = true;
			});

			while (!done) {
				Thread.Sleep(TimeSpan.FromTicks(1));
			}
			return value;
		}
	}
}
