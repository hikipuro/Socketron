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

		public static void Create(Socketron socketron, Action<BrowserWindow> callback) {
			string[] script = new[] {
				"var browserWindow = new electron.BrowserWindow({",
				"});",
				"return [browserWindow.id, browserWindow.webContents.id];"
			};
			socketron.Main.ExecuteJavaScript(
				script,
				(result) => {
					object[] list = result as object[];
					int? windowId = list[0] as int?;
					int? contentsId = list[1] as int?;
					if (windowId != null && contentsId != null) {
						BrowserWindow window = new BrowserWindow() {
							ID = (int)windowId,
							_socketron = socketron
						};
						window.WebContents = new WebContents(window) {
							ID = (int)contentsId
						};
						callback?.Invoke(window);
					} else {
						Console.Error.WriteLine("error");
					}
				}, (result) => {
					Console.Error.WriteLine("error");
				}
			);
		}

		public static BrowserWindow Create(Socketron socketron, Options options = null) {
			if (options == null) {
				options = new Options();
			}
			string[] script = new[] {
				"var browserWindow = new electron.BrowserWindow(",
					options.Stringify(),
				");",
				"return [browserWindow.id, browserWindow.webContents.id];"
			};
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
			string[] script = new[] {
				"var result = [];",
				"var windows = electron.BrowserWindow.getAllWindows();",
				"for (var window of windows) {",
					"result.push([window.id,window.webContents.id]);",
				"}",
				"return result;"
			};
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
			string[] script = new[] {
				"var window = electron.BrowserWindow.getFocusedWindow();",
				"if (window == null) {",
					"return null",
				"}",
				"return [window.id,window.webContents.id];"
			};
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
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + webContents.ID + ");",
				"var window = electron.BrowserWindow.fromWebContents(contents);",
				"if (window == null) {",
					"return null",
				"}",
				"return [window.id,window.webContents.id];"
			};
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

		/*
		public static BrowserWindow FromBrowserView(Socketron socketron, BrowserView browserView) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + webContents.ID + ");",
				"var window = electron.BrowserWindow.fromBrowserView(contents);",
				"if (window == null) {",
					"return null",
				"}",
				"return [window.id,window.webContents.id];"
			};
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
		//*/

		public static BrowserWindow FromId(Socketron socketron, int id) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"if (window == null) {",
					"return null",
				"}",
				"return [window.id,window.webContents.id];"
			};
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
			string[] script = new[] {
				"electron.BrowserWindow.addExtension(" + path.Escape() + ");"
			};
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static void RemoveExtension(Socketron socketron, string name) {
			string[] script = new[] {
				"electron.BrowserWindow.removeExtension(" + name.Escape() + ");"
			};
			_ExecuteJavaScript(socketron, script, null, null);
		}

		/*
		public static void GetExtensions(Socketron socketron, string name) {
			string[] script = new[] {
				"return electron.BrowserWindow.getExtensions();"
			};
			_ExecuteJavaScript(socketron, script, null, null);
		}
		//*/

		public static void AddDevToolsExtension(Socketron socketron, string path) {
			string[] script = new[] {
				"electron.BrowserWindow.addDevToolsExtension(" + path.Escape() + ");"
			};
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static void RemoveDevToolsExtension(Socketron socketron, string name) {
			string[] script = new[] {
				"electron.BrowserWindow.removeDevToolsExtension(" + name.Escape() + ");"
			};
			_ExecuteJavaScript(socketron, script, null, null);
		}

		/*
		public static void GetDevToolsExtensions(Socketron socketron, string name) {
			string[] script = new[] {
				"return electron.BrowserWindow.getDevToolsExtensions();"
			};
			_ExecuteJavaScript(socketron, script, null, null);
		}
		//*/

		public void ExecuteJavaScript(string script, Callback success = null, Callback error = null) {
			_socketron.Main.ExecuteJavaScript(script, success, error);
		}

		public void ExecuteJavaScript(string[] script, Callback success = null, Callback error = null) {
			_socketron.Main.ExecuteJavaScript(script, success, error);
		}

		public T ExecuteJavaScriptBlocking<T>(string[] script) {
			return _ExecuteJavaScriptBlocking<T>(script);
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
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.destroy();"
			};
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
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.close();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Focuses on the window.
		/// </summary>
		public void Focus() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.focus();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Removes focus from the window.
		/// </summary>
		public void Blur() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.blur();"
			};
			_ExecuteJavaScript(script);
		}

		public void IsFocused(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFocused();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// Whether the window is focused.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsFocused() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFocused();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void IsDestroyed(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isDestroyed();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// Whether the window is destroyed.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsDestroyed() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isDestroyed();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Shows and gives focus to the window.
		/// </summary>
		public void Show() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.show();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Shows the window but doesn't focus on it.
		/// </summary>
		public void ShowInactive() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.showInactive();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Hides the window.
		/// </summary>
		public void Hide() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.hide();"
			};
			_ExecuteJavaScript(script);
		}

		public void IsVisible(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isVisible();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// Whether the window is visible to the user.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsVisible() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isVisible();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void IsModal(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isModal();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// Whether current window is a modal window.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsModal() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isModal();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Maximizes the window.
		/// <para>
		/// This will also show (but not focus) the window if it isn't being displayed already.
		/// </para>
		/// </summary>
		public void Maximize() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.maximize();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Unmaximizes the window.
		/// </summary>
		public void Unmaximize() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.unmaximize();"
			};
			_ExecuteJavaScript(script);
		}

		public void IsMaximized(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMaximized();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// Whether the window is maximized.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsMaximized() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMaximized();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Minimizes the window.
		/// <para>
		/// On some platforms the minimized window will be shown in the Dock.
		/// </para>
		/// </summary>
		public void Minimize() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.minimize();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Restores the window from minimized state to its previous state.
		/// </summary>
		public void Restore() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.restore();"
			};
			_ExecuteJavaScript(script);
		}

		public void IsMinimized(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMinimized();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// Whether the window is minimized.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsMinimized() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMinimized();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Sets whether the window should be in fullscreen mode.
		/// </summary>
		/// <param name="flag">bool</param>
		public void SetFullScreen(bool flag) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setFullScreen(" + flag.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void IsFullScreen(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFullScreen();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// Whether the window is in fullscreen mode.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsFullScreen() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFullScreen();"
			};
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
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setSimpleFullScreen(" + flag.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Whether the window is in simple (pre-Lion) fullscreen mode.
		/// </summary>
		/// <returns>bool</returns>
		public bool IsSimpleFullScreen() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isSimpleFullScreen();"
			};
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
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setAspectRatio(" + aspectRatio + ");"
			};
			_ExecuteJavaScript(script);
			// TODO: add size
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
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.previewFile(" + path.Escape() + ");"
			};
			_ExecuteJavaScript(script);
			// TODO: add displayName
		}

		/// <summary>
		/// *macOS* Closes the currently open Quick Look panel.
		/// </summary>
		public void CloseFilePreview() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.closeFilePreview();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Resizes and moves the window to the supplied bounds.
		/// </summary>
		/// <param name="bounds">Rectangle.</param>
		public void SetBounds(Rectangle bounds) {
			// TODO: add animate option
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setBounds({",
					"x:" + bounds.x + ",",
					"y:" + bounds.y + ",",
					"width:" + bounds.width + ",",
					"height:" + bounds.height,
				"});"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>Rectangle.</returns>
		public Rectangle GetBounds() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getBounds();"
			};
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return Rectangle.FromObject(result);
		}

		/// <summary>
		/// Resizes and moves the window's client area (e.g. the web page) to the supplied bounds.
		/// </summary>
		/// <param name="bounds">Rectangle.</param>
		public void SetContentBounds(Rectangle bounds) {
			// TODO: add animate option
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setContentBounds(" + bounds.Stringify() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void GetContentBounds(Action<Rectangle> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getContentBounds();"
			};
			_ExecuteJavaScript(script, (result) => {
				JsonObject json = new JsonObject(result);
				Rectangle rect = new Rectangle() {
					x = (int)json["x"],
					y = (int)json["y"],
					width = (int)json["width"],
					height = (int)json["height"]
				};
				callback?.Invoke(rect);
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>Rectangle.</returns>
		public Rectangle GetContentBounds() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getContentBounds();"
			};
			object result = _ExecuteJavaScriptBlocking<object>(script);
			JsonObject json = new JsonObject(result);
			Rectangle rect = new Rectangle() {
				x = (int)json["x"],
				y = (int)json["y"],
				width = (int)json["width"],
				height = (int)json["height"]
			};
			return rect;
		}

		/// <summary>
		/// Disable or enable the window.
		/// </summary>
		/// <param name="enable">bool</param>
		public void SetEnabled(bool enable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setEnabled(" + enable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Resizes the window to width and height.
		/// </summary>
		/// <param name="width">int</param>
		/// <param name="height">int</param>
		public void SetSize(int width, int height) {
			// TODO: add animate option
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setSize(" + width + "," + height + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void GetSize(Action<int, int> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getSize();"
			};
			_ExecuteJavaScript(script, (result) => {
				object[] resultList = result as object[];
				int width = (int)resultList[0];
				int height = (int)resultList[1];
				callback?.Invoke(width, height);
			});
		}

		/// <summary>
		/// Contains the window's width and height.
		/// </summary>
		/// <returns></returns>
		public int[] GetSize() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getSize();"
			};
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			int width = (int)result[0];
			int height = (int)result[1];
			return new int[] { width, height };
		}

		/// <summary>
		/// Resizes the window's client area (e.g. the web page) to width and height.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void SetContentSize(int width, int height) {
			// TODO: add animate option
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setContentSize(" + width + "," + height + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void GetContentSize(Action<int, int> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getContentSize();"
			};
			_ExecuteJavaScript(script, (result) => {
				object[] resultList = result as object[];
				int width = (int)resultList[0];
				int height = (int)resultList[1];
				callback?.Invoke(width, height);
			});
		}

		/// <summary>
		/// Contains the window's client area's width and height.
		/// </summary>
		/// <returns></returns>
		public int[] GetContentSize() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getContentSize();"
			};
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			int width = (int)result[0];
			int height = (int)result[1];
			return new int[] { width, height };
		}

		/// <summary>
		/// Sets the minimum size of window to width and height.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void SetMinimumSize(int width, int height) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMinimumSize(" + width + "," + height + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void GetMinimumSize(Action<int, int> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getMinimumSize();"
			};
			_ExecuteJavaScript(script, (result) => {
				object[] resultList = result as object[];
				int width = (int)resultList[0];
				int height = (int)resultList[1];
				callback?.Invoke(width, height);
			});
		}

		/// <summary>
		/// Contains the window's minimum width and height.
		/// </summary>
		/// <returns></returns>
		public int[] GetMinimumSize() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getMinimumSize();"
			};
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			int width = (int)result[0];
			int height = (int)result[1];
			return new int[] { width, height };
		}

		/// <summary>
		/// Sets the maximum size of window to width and height.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void SetMaximumSize(int width, int height) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMaximumSize(" + width + "," + height + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void GetMaximumSize(Action<int, int> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getMaximumSize();"
			};
			_ExecuteJavaScript(script, (result) => {
				object[] resultList = result as object[];
				int width = (int)resultList[0];
				int height = (int)resultList[1];
				callback?.Invoke(width, height);
			});
		}

		/// <summary>
		/// Contains the window's maximum width and height.
		/// </summary>
		/// <returns></returns>
		public int[] GetMaximumSize() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getMaximumSize();"
			};
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			int width = (int)result[0];
			int height = (int)result[1];
			return new int[] { width, height };
		}

		/// <summary>
		/// Sets whether the window can be manually resized by user.
		/// </summary>
		/// <param name="resizable"></param>
		public void SetResizable(bool resizable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setResizable(" + resizable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void IsResizable(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isResizable();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// Whether the window can be manually resized by user.
		/// </summary>
		/// <returns></returns>
		public bool IsResizable() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isResizable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows* Sets whether the window can be moved by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="movable"></param>
		public void SetMovable(bool movable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMovable(" + movable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// macOS Windows
		/// </summary>
		/// <param name="callback"></param>
		public void IsMovable(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMovable();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// *macOS Windows* Whether the window can be moved by user.
		///	On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool IsMovable() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMovable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows* Sets whether the window can be manually minimized by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="minimizable"></param>
		public void SetMinimizable(bool minimizable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMinimizable(" + minimizable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// macOS Windows
		/// </summary>
		/// <param name="callback"></param>
		public void IsMinimizable(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMinimizable();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// *macOS Windows* Whether the window can be manually minimized by user.
		/// On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool IsMinimizable() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMinimizable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows* Sets whether the window can be manually maximized by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="maximizable"></param>
		public void SetMaximizable(bool maximizable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMaximizable(" + maximizable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows* Whether the window can be manually maximized by user.
		/// On Linux always returns true.
		/// </summary>
		/// <param name="callback"></param>
		public void IsMaximizable(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMaximizable();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// *macOS Windows* Whether the window can be manually maximized by user.
		/// On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool IsMaximizable() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMaximizable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Sets whether the maximize/zoom window button toggles fullscreen mode or maximizes the window.
		/// </summary>
		/// <param name="fullscreenable"></param>
		public void SetFullScreenable(bool fullscreenable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setFullScreenable(" + fullscreenable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the maximize/zoom window button toggles fullscreen mode or maximizes the window.
		/// </summary>
		/// <param name="callback"></param>
		public void IsFullScreenable(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFullScreenable();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// Whether the maximize/zoom window button toggles fullscreen mode or maximizes the window.
		/// </summary>
		/// <returns></returns>
		public bool IsFullScreenable() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFullScreenable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows* Sets whether the window can be manually closed by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="closable"></param>
		public void SetClosable(bool closable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setClosable(" + closable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// macOS Windows
		/// </summary>
		/// <param name="callback"></param>
		public void IsClosable(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isClosable();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// *macOS Windows* Whether the window can be manually closed by user.
		/// On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool IsClosable() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isClosable();"
			};
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
			// TODO: add level option, relativeLevel option
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setAlwaysOnTop(" + flag.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is always on top of other windows.
		/// </summary>
		/// <param name="callback"></param>
		public void IsAlwaysOnTop(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isAlwaysOnTop();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// Whether the window is always on top of other windows.
		/// </summary>
		/// <returns></returns>
		public bool IsAlwaysOnTop() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isAlwaysOnTop();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows* Moves window to top(z-order) regardless of focus.
		/// </summary>
		public void MoveTop() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.moveTop();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Moves window to the center of the screen.
		/// </summary>
		public void Center() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.center();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Moves window to x and y.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void SetPosition(int x, int y) {
			// TODO: add animate option
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setPosition(" + x + "," + y + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void GetPosition(Action<int, int> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getPosition();"
			};
			_ExecuteJavaScript(script, (result) => {
				object[] resultList = result as object[];
				int x = (int)resultList[0];
				int y = (int)resultList[1];
				callback?.Invoke(x, y);
			});
		}

		/// <summary>
		/// Contains the window's current position.
		/// </summary>
		/// <returns></returns>
		public int[] GetPosition() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getPosition();"
			};
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			int x = (int)result[0];
			int y = (int)result[1];
			return new int[] { x, y };
		}

		/// <summary>
		/// Changes the title of native window to title.
		/// </summary>
		/// <param name="title"></param>
		public void SetTitle(string title) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setTitle(" + title.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void GetTitle(Callback<string> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getTitle();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke(result as string);
			});
		}

		/// <summary>
		/// The title of the native window.
		/// <para>
		/// Note: The title of web page can be different from the title of the native window.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public string GetTitle() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getTitle();"
			};
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
			// TODO: add offsetX option
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setSheetOffset(" + offsetY + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Starts or stops flashing the window to attract user's attention.
		/// </summary>
		/// <param name="flag"></param>
		public void FlashFrame(bool flag) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.flashFrame(" + flag.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Makes the window not show in the taskbar.
		/// </summary>
		/// <param name="skip"></param>
		public void SetSkipTaskbar(bool skip) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setSkipTaskbar(" + skip.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Enters or leaves the kiosk mode.
		/// </summary>
		/// <param name="flag"></param>
		public void SetKiosk(bool flag) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setKiosk(" + flag.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void IsKiosk(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isKiosk();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// Whether the window is in kiosk mode.
		/// </summary>
		/// <returns></returns>
		public bool IsKiosk() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isKiosk();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void GetNativeWindowHandle(Action<ulong> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getNativeWindowHandle();"
			};
			_ExecuteJavaScript(script, (result) => {
				JsonObject json = new JsonObject(result);
				object[] data = json["data"] as object[];
				byte[] bytes = new byte[8];
				for (int i = 0; i < data.Length; i++) {
					object item = data[i];
					bytes[i] = (byte)(int)item;
				}
				ulong param = BitConverter.ToUInt64(bytes, 0);
				callback?.Invoke(param);
			});
		}

		/// <summary>
		/// The native type of the handle is HWND on Windows, NSView* on macOS,
		/// and Window (unsigned long) on Linux.
		/// </summary>
		/// <returns>The platform-specific handle of the window.</returns>
		public ulong GetNativeWindowHandle() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getNativeWindowHandle();"
			};
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
		/*
		public void HookWindowMessage() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.hookWindowMessage();"
			};
			ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// *Windows* 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="callback"></param>
		public void IsWindowMessageHooked(int message, Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isWindowMessageHooked(" + message + ");"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// *Windows* true or false depending on whether the message is hooked.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public bool IsWindowMessageHooked(int message) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isWindowMessageHooked(" + message + ");"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows* Unhook the window message.
		/// </summary>
		/// <param name="message"></param>
		public void UnhookWindowMessage(int message) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.unhookWindowMessage(" + message + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows* Unhooks all of the window messages.
		/// </summary>
		public void UnhookAllWindowMessages() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.unhookAllWindowMessages();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Sets the pathname of the file the window represents,
		/// and the icon of the file will show in window's title bar.
		/// </summary>
		/// <param name="filename"></param>
		public void SetRepresentedFilename(string filename) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setRepresentedFilename(" + filename.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* The pathname of the file the window represents.
		/// </summary>
		/// <param name="callback"></param>
		public void GetRepresentedFilename(Action<string> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getRepresentedFilename();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke(result as string);
			});
		}

		/// <summary>
		/// *macOS*  The pathname of the file the window represents.
		/// </summary>
		/// <returns></returns>
		public string GetRepresentedFilename() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getRepresentedFilename();"
			};
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// *macOS* Specifies whether the window’s document has been edited,
		/// and the icon in title bar will become gray when set to true.
		/// </summary>
		/// <param name="edited"></param>
		public void SetDocumentEdited(bool edited) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setDocumentEdited(" + edited.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Whether the window's document has been edited.
		/// </summary>
		/// <param name="callback"></param>
		public void IsDocumentEdited(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isDocumentEdited();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// *macOS* Whether the window's document has been edited.
		/// </summary>
		/// <returns></returns>
		public bool IsDocumentEdited() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isDocumentEdited();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// 
		/// </summary>
		public void FocusOnWebView() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.focusOnWebView();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// 
		/// </summary>
		public void BlurWebView() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.blurWebView();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Same as webContents.capturePage([rect, ]callback).
		/// </summary>
		/*
		public void CapturePage(Rectangle rect, Action callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.capturePage();"
			};
			ExecuteJavaScript(script, (result) => {
			});
		}
		//*/

		/// <summary>
		/// Same as webContents.loadURL(url[, options]).
		/// <para>
		/// The url can be a remote address (e.g. http://)
		/// or a path to a local HTML file using the file:// protocol.
		/// </para>
		/// </summary>
		/// <param name="url"></param>
		public void LoadURL(string url) {
			// TODO: add options param
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.loadURL(" + url.Escape() + ");"
			};
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
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.loadFile(" + filePath.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Same as webContents.reload.
		/// </summary>
		public void Reload() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.reload();"
			};
			_ExecuteJavaScript(script);
		}


		/// <summary>
		/// *Linux Windows* Sets the menu as the window's menu bar,
		/// setting it to null will remove the menu bar.
		/// </summary>
		/// <param name="menu"></param>
		/*
		public void SetMenu(Menu menu) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setMenu(" + menu + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// Sets progress value in progress bar. Valid range is [0, 1.0].
		/// </summary>
		/// <param name="progress"></param>
		public void SetProgressBar(double progress) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setProgressBar(" + progress + ");"
			};
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
		/*
		public void SetOverlayIcon(NativeImage overlay, string description) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setOverlayIcon(" + overlay + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// *macOS* Sets whether the window should have a shadow.
		/// On Windows and Linux does nothing.
		/// </summary>
		/// <param name="hasShadow"></param>
		public void SetHasShadow(bool hasShadow) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setHasShadow(" + hasShadow.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Whether the window has a shadow.
		/// On Windows and Linux always returns true.
		/// </summary>
		/// <param name="callback"></param>
		public void HasShadow(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.hasShadow();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// *macOS* Whether the window has a shadow.
		/// On Windows and Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool HasShadow() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.hasShadow();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows macOS* Sets the opacity of the window.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="opacity">between 0.0 (fully transparent) and 1.0 (fully opaque)</param>
		public void SetOpacity(double opacity) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setOpacity(" + opacity + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows macOS* between 0.0 (fully transparent) and 1.0 (fully opaque)
		/// </summary>
		/// <param name="callback"></param>
		public void GetOpacity(Action<double> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getOpacity();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((double)result);
			});
		}

		/// <summary>
		/// *Windows macOS* between 0.0 (fully transparent) and 1.0 (fully opaque)
		/// </summary>
		/// <returns></returns>
		public double GetOpacity() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getOpacity();"
			};
			return _ExecuteJavaScriptBlocking<double>(script);
		}

		/// <summary>
		/// *Windows* Add a thumbnail toolbar with a specified set of buttons
		/// to the thumbnail image of a window in a taskbar button layout.
		/// Returns a Boolean object indicates whether the thumbnail has been added successfully.
		/// </summary>
		/// <param name="buttons"></param>
		/*
		public void SetThumbarButtons(string buttons) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setThumbarButtons(" + buttons + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// *Windows* Sets the region of the window to show as the thumbnail image displayed
		/// when hovering over the window in the taskbar.
		/// You can reset the thumbnail to be the entire window by specifying an empty region:
		/// {x: 0, y: 0, width: 0, height: 0}.
		/// </summary>
		/// <param name="region">Region of the window</param>
		public void SetThumbnailClip(Rectangle region) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setThumbnailClip({",
					"x:" + region.x + ",",
					"y:" + region.y + ",",
					"width:" + region.width + ",",
					"height:" + region.height,
				");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows* Sets the toolTip that is displayed
		/// when hovering over the window thumbnail in the taskbar.
		/// </summary>
		/// <param name="toolTip"></param>
		public void SetThumbnailToolTip(string toolTip) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setThumbnailToolTip(" + toolTip.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows* Sets the properties for the window's taskbar button.
		/// </summary>
		/// <param name="options"></param>
		/*
		public void SetAppDetails(string options) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setAppDetails(" + options + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// *macOS* Same as webContents.showDefinitionForSelection().
		/// </summary>
		public void ShowDefinitionForSelection() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.showDefinitionForSelection();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows Linux* Changes window icon.
		/// </summary>
		/// <param name="icon"></param>
		/*
		public void SetIcon(NativeImage icon) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setIcon(" + icon + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// Sets whether the window menu bar should hide itself automatically.
		/// Once set the menu bar will only show when users press the single Alt key.
		/// </summary>
		/// <param name="hide"></param>
		public void SetAutoHideMenuBar(bool hide) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setAutoHideMenuBar(" + hide.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether menu bar automatically hides itself.
		/// </summary>
		/// <param name="callback"></param>
		public void IsMenuBarAutoHide(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMenuBarAutoHide();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// Whether menu bar automatically hides itself.
		/// </summary>
		/// <returns></returns>
		public bool IsMenuBarAutoHide() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMenuBarAutoHide();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows Linux* Sets whether the menu bar should be visible.
		/// If the menu bar is auto-hide, users can still bring up the menu bar by pressing the single Alt key.
		/// </summary>
		/// <param name="visible"></param>
		public void SetMenuBarVisibility(bool visible) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMenuBarVisibility(" + visible.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the menu bar is visible.
		/// </summary>
		/// <param name="callback"></param>
		public void IsMenuBarVisible(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMenuBarVisible();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// Whether the menu bar is visible.
		/// </summary>
		/// <returns></returns>
		public bool IsMenuBarVisible() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMenuBarVisible();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Sets whether the window should be visible on all workspaces.
		/// <para>Note: This API does nothing on Windows.</para>
		/// </summary>
		/// <param name="visible"></param>
		public void SetVisibleOnAllWorkspaces(bool visible) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setVisibleOnAllWorkspaces(" + visible.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is visible on all workspaces.
		/// <para>Note: This API always returns false on Windows.</para>
		/// </summary>
		/// <param name="callback"></param>
		public void IsVisibleOnAllWorkspaces(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isVisibleOnAllWorkspaces();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		/// <summary>
		/// Whether the window is visible on all workspaces.
		/// <para>Note: This API always returns false on Windows.</para>
		/// </summary>
		/// <returns></returns>
		public bool IsVisibleOnAllWorkspaces() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isVisibleOnAllWorkspaces();"
			};
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
			// TODO: add options param
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setIgnoreMouseEvents(" + ignore.Escape() + ");"
			};
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
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setContentProtection(" + enable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows* Changes whether the window can be focused.
		/// </summary>
		/// <param name="focusable"></param>
		public void SetFocusable(bool focusable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setFocusable(" + focusable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Linux macOS* Sets parent as current window's parent window,
		/// passing null will turn current window into a top-level window.
		/// </summary>
		/// <param name="parent"></param>
		/*
		public void SetParentWindow(BrowserWindow parent) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setParentWindow(" + parent + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// The parent window.
		/// </summary>
		/*
		public void GetParentWindow(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"return window.getParentWindow();"
			};
			ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}
		//*/

		/// <summary>
		/// All child windows.
		/// </summary>
		/*
		public void GetChildWindows(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"return window.getChildWindows();"
			};
			ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}
		//*/

		/// <summary>
		/// *macOS* Controls whether to hide cursor when typing.
		/// </summary>
		/// <param name="autoHide"></param>
		public void SetAutoHideCursor(bool autoHide) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setAutoHideCursor(" + autoHide.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Selects the previous tab when native tabs
		/// are enabled and there are other tabs in the window.
		/// </summary>
		public void SelectPreviousTab() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.selectPreviousTab();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Selects the next tab when native tabs
		/// are enabled and there are other tabs in the window.
		/// </summary>
		public void SelectNextTab() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.selectNextTab();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Merges all windows into one window with multiple tabs
		/// when native tabs are enabled and there is more than one open window.
		/// </summary>
		public void MergeAllWindows() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.mergeAllWindows();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Moves the current tab into a new window if native tabs are enabled
		/// and there is more than one tab in the current window.
		/// </summary>
		public void MoveTabToNewWindow() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.moveTabToNewWindow();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Toggles the visibility of the tab bar if native tabs
		/// are enabled and there is only one tab in the current window.
		/// </summary>
		public void ToggleTabBar() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.toggleTabBar();"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Adds a window as a tab on this window,
		/// after the tab for the window instance.
		/// </summary>
		/// <param name="browserWindow"></param>
		/*
		public void AddTabbedWindow(BrowserWindow browserWindow) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.addTabbedWindow();"
			};
			ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// *macOS* Adds a vibrancy effect to the browser window.
		/// <para>Passing null or an empty string will remove the vibrancy effect on the window.</para>
		/// </summary>
		/// <param name="type"></param>
		public void SetVibrancy(string type) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setVibrancy(" + type.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Experimental* Sets the touchBar layout for the current window.
		/// <para>
		/// Specifying null or undefined clears the touch bar.
		/// This method only has an effect if the machine has a touch bar and is running on macOS 10.12.1+.
		/// </para>
		/// </summary>
		/*
		public void SetTouchBar(string touchBar) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setTouchBar(" + touchBar + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// *Experimental*
		/// </summary>
		/*
		public void SetBrowserView(string browserView) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setBrowserView(" + browserView + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// *Experimental* an attached BrowserView. Returns null if none is attached.
		/// <para>
		/// Note: The BrowserView API is currently experimental
		/// and may change or be removed in future Electron releases.
		/// </para>
		/// </summary>
		/*
		public void GetBrowserView(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"return window.getBrowserView();"
			};
			ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}
		//*/

		protected void _ExecuteJavaScript(string[] script) {
			_socketron.Main.ExecuteJavaScript(script);
		}

		protected void _ExecuteJavaScript(string[] script, Callback callback) {
			_socketron.Main.ExecuteJavaScript(script, callback);
		}

		protected void _ExecuteJavaScript(string[] script, Callback success, Callback error) {
			_socketron.Main.ExecuteJavaScript(script, success, error);
		}

		protected T _ExecuteJavaScriptBlocking<T>(string[] script) {
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

		protected static void _ExecuteJavaScript(Socketron socketron, string[] script, Callback success, Callback error) {
			socketron.Main.ExecuteJavaScript(script, success, error);
		}

		protected static T _ExecuteJavaScriptBlocking<T>(Socketron socketron, string[] script) {
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
