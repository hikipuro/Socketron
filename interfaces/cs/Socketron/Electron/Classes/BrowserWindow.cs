using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create and control browser windows.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public partial class BrowserWindow : JSModule {
		/// <summary>
		/// BrowserWindow instance events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when the document changed its title,
			/// calling event.preventDefault() will prevent
			/// the native window's title from changing.
			/// </summary>
			public const string PageTitleUpdated = "page-title-updated";
			/// <summary>
			/// Emitted when the window is going to be closed.
			/// It's emitted before the beforeunload and unload event of the DOM.
			/// Calling event.preventDefault() will cancel the close.
			/// </summary>
			public const string Close = "close";
			/// <summary>
			/// Emitted when the window is closed.
			/// After you have received this event you should remove the reference
			/// to the window and avoid using it any more.
			/// </summary>
			public const string Closed = "closed";
			/// <summary>
			/// *Windows*
			/// Emitted when window session is going to end due to force shutdown
			/// or machine restart or session log off.
			/// </summary>
			public const string SessionEnd = "session-end";
			/// <summary>
			/// Emitted when the web page becomes unresponsive.
			/// </summary>
			public const string Unresponsive = "unresponsive";
			/// <summary>
			/// Emitted when the unresponsive web page becomes responsive again.
			/// </summary>
			public const string Responsive = "responsive";
			/// <summary>
			/// Emitted when the window loses focus.
			/// </summary>
			public const string Blur = "blur";
			/// <summary>
			/// Emitted when the window gains focus.
			/// </summary>
			public const string Focus = "focus";
			/// <summary>
			/// Emitted when the window is shown.
			/// </summary>
			public const string Show = "show";
			/// <summary>
			/// Emitted when the window is hidden.
			/// </summary>
			public const string Hide = "hide";
			/// <summary>
			/// Emitted when the web page has been rendered
			/// (while not being shown) and window can be displayed
			/// without a visual flash.
			/// </summary>
			public const string ReadyToShow = "ready-to-show";
			/// <summary>
			/// Emitted when window is maximized.
			/// </summary>
			public const string Maximize = "maximize";
			/// <summary>
			/// Emitted when the window exits from a maximized state.
			/// </summary>
			public const string Unmaximize = "unmaximize";
			/// <summary>
			/// Emitted when the window is minimized.
			/// </summary>
			public const string Minimize = "minimize";
			/// <summary>
			/// Emitted when the window is restored from a minimized state.
			/// </summary>
			public const string Restore = "restore";
			/// <summary>
			/// Emitted when the window is being resized.
			/// </summary>
			public const string Resize = "resize";
			/// <summary>
			/// Emitted when the window is being moved to a new position.
			/// <para>
			/// Note: On macOS this event is just an alias of moved.
			/// </para>
			/// </summary>
			public const string Move = "move";
			/// <summary>
			/// *macOS*
			/// Emitted once when the window is moved to a new position.
			/// </summary>
			public const string Moved = "moved";
			/// <summary>
			/// Emitted when the window enters a full-screen state.
			/// </summary>
			public const string EnterFullScreen = "enter-full-screen";
			/// <summary>
			/// Emitted when the window leaves a full-screen state.
			/// </summary>
			public const string LeaveFullScreen = "leave-full-screen";
			/// <summary>
			/// Emitted when the window enters a full-screen state triggered by HTML API.
			/// </summary>
			public const string EnterHtmlFullScreen = "enter-html-full-screen";
			/// <summary>
			/// Emitted when the window leaves a full-screen state triggered by HTML API.
			/// </summary>
			public const string LeaveHtmlFullScreen = "leave-html-full-screen";
			/// <summary>
			/// *Windows*
			/// Emitted when an App Command is invoked.
			/// </summary>
			public const string AppCommand = "app-command";
			/// <summary>
			/// *macOS*
			/// Emitted when scroll wheel event phase has begun.
			/// </summary>
			public const string ScrollTouchBegin = "scroll-touch-begin";
			/// <summary>
			/// *macOS*
			/// Emitted when scroll wheel event phase has ended.
			/// </summary>
			public const string ScrollTouchEnd = "scroll-touch-end";
			/// <summary>
			/// *macOS*
			/// Emitted when scroll wheel event phase filed upon reaching the edge of element.
			/// </summary>
			public const string ScrollTouchEdge = "scroll-touch-edge";
			/// <summary>
			/// *macOS*
			/// Emitted on 3-finger swipe.
			/// Possible directions are up, right, down, left.
			/// </summary>
			public const string Swipe = "swipe";
			/// <summary>
			/// *macOS*
			/// Emitted when the window opens a sheet.
			/// </summary>
			public const string SheetBegin = "sheet-begin";
			/// <summary>
			/// *macOS*
			/// Emitted when the window has closed a sheet.
			/// </summary>
			public const string SheetEnd = "sheet-end";
			/// <summary>
			/// *macOS*
			/// Emitted when the native new tab button is clicked.
			/// </summary>
			public const string NewWindowForTab = "new-window-for-tab";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the BrowserWindow constructors,
		/// please use electron.BrowserWindow.Create() method instead.
		/// </para>
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public BrowserWindow(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/*
		public BrowserWindow(Options options) {
			if (options == null) {
				options = new Options();
			}
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = new {0}({1});",
					"return {2};"
				),
				Script.GetObject(_Class._id),
				options.Stringify(),
				Script.AddObject("window")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			_client = client;
			_id = result;
		}

		public BrowserWindow(string options) : this(Options.Parse(options)) {
		}

		/// <summary>
		/// Returns BrowserWindow[] - An array of all opened browser windows.
		/// </summary>
		/// <returns></returns>
		public static List<BrowserWindow> getAllWindows() {
			return _Class.getAllWindows();
		}

		/// <summary>
		/// Returns BrowserWindow | null - The window that is focused in this application,
		/// otherwise returns null.
		/// </summary>
		/// <returns></returns>
		public static BrowserWindow getFocusedWindow() {
			return _Class.getFocusedWindow();
		}

		/// <summary>
		/// Returns BrowserWindow - The window that owns the given webContents.
		/// </summary>
		/// <param name="webContents"></param>
		/// <returns></returns>
		public static BrowserWindow fromWebContents(WebContents webContents) {
			return _Class.fromWebContents(webContents);
		}

		/// <summary>
		/// Returns BrowserWindow | null - The window that owns the given browserView.
		/// If the given view is not attached to any window, returns null.
		/// </summary>
		/// <param name="browserView"></param>
		/// <returns></returns>
		public static BrowserWindow fromBrowserView(BrowserView browserView) {
			return _Class.fromBrowserView(browserView);
		}

		/// <summary>
		/// Returns BrowserWindow - The window with the given id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static BrowserWindow fromId(int id) {
			return _Class.fromId(id);
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
		public static void addExtension(string path) {
			_Class.addExtension(path);
		}

		/// <summary>
		/// Remove a Chrome extension by name.
		/// <para>
		/// Note: This API cannot be called before the ready event of the app module is emitted.
		/// </para>
		/// </summary>
		/// <param name="name"></param>
		public static void removeExtension(string name) {
			_Class.removeExtension(name);
		}

		/// <summary>
		/// Returns Object - The keys are the extension names
		/// and each value is an Object containing name and version properties.
		/// <para>
		/// Note: This API cannot be called before the ready event of the app module is emitted.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public static JsonObject getExtensions() {
			return _Class.getExtensions();
		}

		/// <summary>
		/// Adds DevTools extension located at path, and returns extension's name.
		/// </summary>
		/// <param name="path"></param>
		public static void addDevToolsExtension(string path) {
			_Class.addDevToolsExtension(path);
		}

		/// <summary>
		/// Remove a DevTools extension by name.
		/// </summary>
		/// <param name="name"></param>
		public static void removeDevToolsExtension(string name) {
			_Class.removeDevToolsExtension(name);
		}

		/// <summary>
		/// Returns Object - The keys are the extension names
		/// and each value is an Object containing name and version properties.
		/// </summary>
		/// <returns></returns>
		public static JsonObject getDevToolsExtensions() {
			return _Class.getDevToolsExtensions();
		}
		//*/

		/// <summary>
		/// A Integer representing the unique ID of the window.
		/// </summary>
		public int id {
			get {
				string script = ScriptBuilder.Build(
					"return {0}.id;",
					Script.GetObject(_id)
				);
				return _ExecuteBlocking<int>(script);
			}
		}

		/// <summary>
		/// A WebContents object this window owns.
		/// <para>
		/// All web page related events and operations will be done via it.
		/// </para>
		/// </summary>
		public WebContents webContents {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var webContents = {0}.webContents;",
						"return {1};"
					),
					Script.GetObject(_id),
					Script.AddObject("webContents")
				);
				int result = _ExecuteBlocking<int>(script);
				return new WebContents(_client, result);
			}
		}

		/// <summary>
		/// Force closing the window.
		/// <para>
		/// Force closing the window, the unload and beforeunload event won't be emitted for the web page,
		/// and close event will also not be emitted for this window,
		/// but it guarantees the closed event will be emitted.
		/// </para>
		/// </summary>
		public void destroy() {
			string script = ScriptBuilder.Build(
				"{0}.destroy();",
				Script.GetObject(_id)
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
		public void close() {
			string script = ScriptBuilder.Build(
				"{0}.close();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Focuses on the window.
		/// </summary>
		public void focus() {
			string script = ScriptBuilder.Build(
				"{0}.focus();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Removes focus from the window.
		/// </summary>
		public void blur() {
			string script = ScriptBuilder.Build(
				"{0}.blur();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is focused.
		/// </summary>
		/// <returns>bool</returns>
		public bool isFocused() {
			string script = ScriptBuilder.Build(
				"return {0}.isFocused();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Whether the window is destroyed.
		/// </summary>
		/// <returns>bool</returns>
		public bool isDestroyed() {
			string script = ScriptBuilder.Build(
				"return {0}.isDestroyed();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Shows and gives focus to the window.
		/// </summary>
		public void show() {
			string script = ScriptBuilder.Build(
				"{0}.show();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Shows the window but doesn't focus on it.
		/// </summary>
		public void showInactive() {
			string script = ScriptBuilder.Build(
				"{0}.showInactive();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Hides the window.
		/// </summary>
		public void hide() {
			string script = ScriptBuilder.Build(
				"{0}.hide();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is visible to the user.
		/// </summary>
		/// <returns>bool</returns>
		public bool isVisible() {
			string script = ScriptBuilder.Build(
				"return {0}.isVisible();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Whether current window is a modal window.
		/// </summary>
		/// <returns>bool</returns>
		public bool isModal() {
			string script = ScriptBuilder.Build(
				"return {0}.isModal();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Maximizes the window.
		/// <para>
		/// This will also show (but not focus) the window if it isn't being displayed already.
		/// </para>
		/// </summary>
		public void maximize() {
			string script = ScriptBuilder.Build(
				"{0}.maximize();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Unmaximizes the window.
		/// </summary>
		public void unmaximize() {
			string script = ScriptBuilder.Build(
				"{0}.unmaximize();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is maximized.
		/// </summary>
		/// <returns>bool</returns>
		public bool isMaximized() {
			string script = ScriptBuilder.Build(
				"return {0}.isMaximized();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Minimizes the window.
		/// <para>
		/// On some platforms the minimized window will be shown in the Dock.
		/// </para>
		/// </summary>
		public void minimize() {
			string script = ScriptBuilder.Build(
				"{0}.minimize();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Restores the window from minimized state to its previous state.
		/// </summary>
		public void restore() {
			string script = ScriptBuilder.Build(
				"{0}.restore();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is minimized.
		/// </summary>
		/// <returns>bool</returns>
		public bool isMinimized() {
			string script = ScriptBuilder.Build(
				"return {0}.isMinimized();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Sets whether the window should be in fullscreen mode.
		/// </summary>
		/// <param name="flag">bool</param>
		public void setFullScreen(bool flag) {
			string script = ScriptBuilder.Build(
				"{0}.setFullScreen({1});",
				Script.GetObject(_id),
				flag.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is in fullscreen mode.
		/// </summary>
		/// <returns>bool</returns>
		public bool isFullScreen() {
			string script = ScriptBuilder.Build(
				"return {0}.isFullScreen();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS*
		/// Enters or leaves simple fullscreen mode.
		/// <para>
		/// Simple fullscreen mode emulates the native fullscreen behavior
		/// found in versions of Mac OS X prior to Lion (10.7).
		/// </para>
		/// </summary>
		/// <param name="flag">bool</param>
		public void setSimpleFullScreen(bool flag) {
			string script = ScriptBuilder.Build(
				"{0}.setSimpleFullScreen({1});",
				Script.GetObject(_id),
				flag.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Whether the window is in simple (pre-Lion) fullscreen mode.
		/// </summary>
		/// <returns>bool</returns>
		public bool isSimpleFullScreen() {
			string script = ScriptBuilder.Build(
				"return {0}.isSimpleFullScreen();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS*
		/// This will make a window maintain an aspect ratio.
		/// <para>
		/// The extra size allows a developer to have space, specified in pixels,
		/// not included within the aspect ratio calculations.
		/// This API already takes into account the difference between a window's size and its content size.
		/// </para>
		/// </summary>
		/// <param name="aspectRatio">The aspect ratio to maintain for some portion of the content view.</param>
		/// <param name="size">The extra size not to be included while maintaining the aspect ratio.</param>
		public void setAspectRatio(double aspectRatio, Size size = null) {
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
				"{0}.setAspectRatio({1});",
				Script.GetObject(_id),
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Uses Quick Look to preview a file at a given path.
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
		public void previewFile(string path, string displayName = null) {
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
				"{0}.previewFile({1});",
				Script.GetObject(_id),
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Closes the currently open Quick Look panel.
		/// </summary>
		public void closeFilePreview() {
			string script = ScriptBuilder.Build(
				"{0}.closeFilePreview();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Resizes and moves the window to the supplied bounds.
		/// </summary>
		/// <param name="bounds">Rectangle.</param>
		public void setBounds(Rectangle bounds) {
			string script = ScriptBuilder.Build(
				"{0}.setBounds({1});",
				Script.GetObject(_id),
				bounds.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Resizes and moves the window to the supplied bounds.
		/// </summary>
		/// <param name="bounds">Rectangle.</param>
		/// <param name="animate">*macOS*</param>
		public void setBounds(Rectangle bounds, bool animate) {
			string script = ScriptBuilder.Build(
				"{0}.setBounds({1},{2});",
				Script.GetObject(_id),
				bounds.Stringify(),
				animate.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>Rectangle.</returns>
		public Rectangle getBounds() {
			string script = ScriptBuilder.Build(
				"return {0}.getBounds();",
				Script.GetObject(_id)
			);
			object result = _ExecuteBlocking<object>(script);
			return Rectangle.FromObject(result);
		}

		/// <summary>
		/// Resizes and moves the window's client area (e.g. the web page) to the supplied bounds.
		/// </summary>
		/// <param name="bounds">Rectangle.</param>
		public void setContentBounds(Rectangle bounds) {
			string script = ScriptBuilder.Build(
				"{0}.setContentBounds({1});",
				Script.GetObject(_id),
				bounds.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Resizes and moves the window's client area (e.g. the web page) to the supplied bounds.
		/// </summary>
		/// <param name="bounds">Rectangle.</param>
		/// <param name="animate">*macOS*</param>
		public void setContentBounds(Rectangle bounds, bool animate) {
			string script = ScriptBuilder.Build(
				"{0}.setContentBounds({1},{2});",
				Script.GetObject(_id),
				bounds.Stringify(),
				animate.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>Rectangle.</returns>
		public Rectangle getContentBounds() {
			string script = ScriptBuilder.Build(
				"return {0}.getContentBounds();",
				Script.GetObject(_id)
			);
			object result = _ExecuteBlocking<object>(script);
			return Rectangle.FromObject(result);
		}

		/// <summary>
		/// Disable or enable the window.
		/// </summary>
		/// <param name="enable">bool</param>
		public void setEnabled(bool enable) {
			string script = ScriptBuilder.Build(
				"{0}.setEnabled({1});",
				Script.GetObject(_id),
				enable.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Resizes the window to width and height.
		/// </summary>
		/// <param name="width">int</param>
		/// <param name="height">int</param>
		public void setSize(int width, int height) {
			string script = ScriptBuilder.Build(
				"{0}.setSize({1},{2});",
				Script.GetObject(_id),
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
		public void setSize(int width, int height, bool animate) {
			string script = ScriptBuilder.Build(
				"{0}.setSize({1},{2},{3});",
				Script.GetObject(_id),
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
		public Size getSize() {
			string script = ScriptBuilder.Build(
				"return {0}.getSize();",
				Script.GetObject(_id)
			);
			object[] result = _ExecuteBlocking<object[]>(script);
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
		public void setContentSize(int width, int height) {
			string script = ScriptBuilder.Build(
				"{0}.setContentSize({1},{2});",
				Script.GetObject(_id),
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
		public void setContentSize(int width, int height, bool animate) {
			string script = ScriptBuilder.Build(
				"{0}.setContentSize({1},{2},{3});",
				Script.GetObject(_id),
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
		public Size getContentSize() {
			string script = ScriptBuilder.Build(
				"return {0}.getContentSize();",
				Script.GetObject(_id)
			);
			object[] result = _ExecuteBlocking<object[]>(script);
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
		public void setMinimumSize(int width, int height) {
			string script = ScriptBuilder.Build(
				"{0}.setMinimumSize({1},{2});",
				Script.GetObject(_id),
				width,
				height
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Contains the window's minimum width and height.
		/// </summary>
		/// <returns></returns>
		public Size getMinimumSize() {
			string script = ScriptBuilder.Build(
				"return {0}.getMinimumSize();",
				Script.GetObject(_id)
			);
			object[] result = _ExecuteBlocking<object[]>(script);
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
		public void setMaximumSize(int width, int height) {
			string script = ScriptBuilder.Build(
				"{0}.setMaximumSize({1},{2});",
				Script.GetObject(_id),
				width,
				height
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Contains the window's maximum width and height.
		/// </summary>
		/// <returns></returns>
		public Size getMaximumSize() {
			string script = ScriptBuilder.Build(
				"return {0}.getMaximumSize();",
				Script.GetObject(_id)
			);
			object[] result = _ExecuteBlocking<object[]>(script);
			return new Size() {
				width = (int)result[0],
				height = (int)result[1]
			};
		}

		/// <summary>
		/// Sets whether the window can be manually resized by user.
		/// </summary>
		/// <param name="resizable"></param>
		public void setResizable(bool resizable) {
			string script = ScriptBuilder.Build(
				"{0}.setResizable({1});",
				Script.GetObject(_id),
				resizable.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window can be manually resized by user.
		/// </summary>
		/// <returns></returns>
		public bool isResizable() {
			string script = ScriptBuilder.Build(
				"return {0}.isResizable();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Sets whether the window can be moved by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="movable"></param>
		public void setMovable(bool movable) {
			string script = ScriptBuilder.Build(
				"{0}.setMovable({1});",
				Script.GetObject(_id),
				movable.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// *macOS Windows*
		/// Whether the window can be moved by user.
		///	On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool isMovable() {
			string script = ScriptBuilder.Build(
				"return {0}.isMovable();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Sets whether the window can be manually minimized by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="minimizable"></param>
		public void setMinimizable(bool minimizable) {
			string script = ScriptBuilder.Build(
				"{0}.setMinimizable({1});",
				Script.GetObject(_id),
				minimizable.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// *macOS Windows*
		/// Whether the window can be manually minimized by user.
		/// On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool isMinimizable() {
			string script = ScriptBuilder.Build(
				"return {0}.isMinimizable();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Sets whether the window can be manually maximized by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="maximizable"></param>
		public void setMaximizable(bool maximizable) {
			string script = ScriptBuilder.Build(
				"{0}.setMaximizable({1});",
				Script.GetObject(_id),
				maximizable.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Whether the window can be manually maximized by user.
		/// On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool isMaximizable() {
			string script = ScriptBuilder.Build(
				"return {0}.isMaximizable();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Sets whether the maximize/zoom window button toggles fullscreen mode or maximizes the window.
		/// </summary>
		/// <param name="fullscreenable"></param>
		public void setFullScreenable(bool fullscreenable) {
			string script = ScriptBuilder.Build(
				"{0}.setFullScreenable({1});",
				Script.GetObject(_id),
				fullscreenable.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the maximize/zoom window button toggles fullscreen mode or maximizes the window.
		/// </summary>
		/// <returns></returns>
		public bool isFullScreenable() {
			string script = ScriptBuilder.Build(
				"return {0}.isFullScreenable();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Sets whether the window can be manually closed by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="closable"></param>
		public void setClosable(bool closable) {
			string script = ScriptBuilder.Build(
				"{0}.setClosable({1});",
				Script.GetObject(_id),
				closable.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Whether the window can be manually closed by user.
		/// On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool isClosable() {
			string script = ScriptBuilder.Build(
				"return {0}.isClosable();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Sets whether the window should show always on top of other windows.
		/// <para>
		/// After setting this, the window is still a normal window,
		/// not a toolbox window which can not be focused on.
		/// </para>
		/// </summary>
		/// <param name="flag"></param>
		public void setAlwaysOnTop(bool flag) {
			string script = ScriptBuilder.Build(
				"{0}.setAlwaysOnTop({1});",
				Script.GetObject(_id),
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
		public void setAlwaysOnTop(bool flag, string level, int relativeLevel = 0) {
			string script = ScriptBuilder.Build(
				"{0}.setAlwaysOnTop({1},{2},{3});",
				Script.GetObject(_id),
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
		public bool isAlwaysOnTop() {
			string script = ScriptBuilder.Build(
				"return {0}.isAlwaysOnTop();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Moves window to top(z-order) regardless of focus.
		/// </summary>
		public void moveTop() {
			string script = ScriptBuilder.Build(
				"{0}.moveTop();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Moves window to the center of the screen.
		/// </summary>
		public void center() {
			string script = ScriptBuilder.Build(
				"{0}.center();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Moves window to x and y.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void setPosition(int x, int y) {
			string script = ScriptBuilder.Build(
				"{0}.setPosition({1},{2});",
				Script.GetObject(_id),
				x, y
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Moves window to x and y.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="animate">*macOS*</param>
		public void setPosition(int x, int y, bool animate) {
			string script = ScriptBuilder.Build(
				"{0}.setPosition({1},{2},{3});",
				Script.GetObject(_id),
				x, y, animate.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Contains the window's current position.
		/// </summary>
		/// <returns></returns>
		public Point getPosition() {
			string script = ScriptBuilder.Build(
				"return {0}.getPosition();",
				Script.GetObject(_id)
			);
			object[] result = _ExecuteBlocking<object[]>(script);
			return new Point() {
				x = (int)result[0],
				y = (int)result[1]
			};
		}

		/// <summary>
		/// Changes the title of native window to title.
		/// </summary>
		/// <param name="title"></param>
		public void setTitle(string title) {
			string script = ScriptBuilder.Build(
				"{0}.setTitle({1});",
				Script.GetObject(_id),
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
		public string getTitle() {
			string script = ScriptBuilder.Build(
				"return {0}.getTitle();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// *macOS*
		/// Changes the attachment point for sheets on macOS. 
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
		public void setSheetOffset(double offsetY) {
			string script = ScriptBuilder.Build(
				"{0}.setSheetOffset({1});",
				Script.GetObject(_id),
				offsetY
			);
			_ExecuteJavaScript(script);
		}

		public void setSheetOffset(double offsetY, double offsetX) {
			string script = ScriptBuilder.Build(
				"{0}.setSheetOffset({1},{2});",
				Script.GetObject(_id),
				offsetY,
				offsetX
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Starts or stops flashing the window to attract user's attention.
		/// </summary>
		/// <param name="flag"></param>
		public void flashFrame(bool flag) {
			string script = ScriptBuilder.Build(
				"{0}.flashFrame({1});",
				Script.GetObject(_id),
				flag.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Makes the window not show in the taskbar.
		/// </summary>
		/// <param name="skip"></param>
		public void setSkipTaskbar(bool skip) {
			string script = ScriptBuilder.Build(
				"{0}.setSkipTaskbar({1});",
				Script.GetObject(_id),
				skip.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Enters or leaves the kiosk mode.
		/// </summary>
		/// <param name="flag"></param>
		public void setKiosk(bool flag) {
			string script = ScriptBuilder.Build(
				"{0}.setSkipTaskbar({1});",
				Script.GetObject(_id),
				flag.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Whether the window is in kiosk mode.
		/// </summary>
		/// <returns></returns>
		public bool isKiosk() {
			string script = ScriptBuilder.Build(
				"return {0}.isKiosk();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// The native type of the handle is HWND on Windows, NSView* on macOS,
		/// and Window (unsigned long) on Linux.
		/// </summary>
		/// <returns>The platform-specific handle of the window.</returns>
		public ulong getNativeWindowHandle() {
			string script = ScriptBuilder.Build(
				"return {0}.getNativeWindowHandle();",
				Script.GetObject(_id)
			);
			object result = _ExecuteBlocking<object>(script);
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
		/// *Windows*
		/// Hooks a windows message.
		/// The callback is called when the message is received in the WndProc.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="callback">JavaScript string</param>
		public void hookWindowMessage(int message, string callback) {
			// TODO: fix callback
			string script = ScriptBuilder.Build(
				"{0}.hookWindowMessage({1},{2});",
				Script.GetObject(_id),
				message,
				callback.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows*
		/// true or false depending on whether the message is hooked.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public bool isWindowMessageHooked(int message) {
			string script = ScriptBuilder.Build(
				"return {0}.isWindowMessageHooked({1});",
				Script.GetObject(_id),
				message
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows*
		/// Unhook the window message.
		/// </summary>
		/// <param name="message"></param>
		public void unhookWindowMessage(int message) {
			string script = ScriptBuilder.Build(
				"{0}.unhookWindowMessage({1});",
				Script.GetObject(_id),
				message
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows*
		/// Unhooks all of the window messages.
		/// </summary>
		public void unhookAllWindowMessages() {
			string script = ScriptBuilder.Build(
				"{0}.unhookAllWindowMessages();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Sets the pathname of the file the window represents,
		/// and the icon of the file will show in window's title bar.
		/// </summary>
		/// <param name="filename"></param>
		public void setRepresentedFilename(string filename) {
			string script = ScriptBuilder.Build(
				"{0}.setRepresentedFilename({1});",
				Script.GetObject(_id),
				filename.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// The pathname of the file the window represents.
		/// </summary>
		/// <returns></returns>
		public string getRepresentedFilename() {
			string script = ScriptBuilder.Build(
				"return {0}.getRepresentedFilename();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// *macOS*
		/// Specifies whether the window’s document has been edited,
		/// and the icon in title bar will become gray when set to true.
		/// </summary>
		/// <param name="edited"></param>
		public void setDocumentEdited(bool edited) {
			string script = ScriptBuilder.Build(
				"{0}.setDocumentEdited({1});",
				Script.GetObject(_id),
				edited.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// *macOS*
		/// Whether the window's document has been edited.
		/// </summary>
		/// <returns></returns>
		public bool isDocumentEdited() {
			string script = ScriptBuilder.Build(
				"return {0}.isDocumentEdited();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// 
		/// </summary>
		public void focusOnWebView() {
			string script = ScriptBuilder.Build(
				"{0}.focusOnWebView();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// 
		/// </summary>
		public void blurWebView() {
			string script = ScriptBuilder.Build(
				"{0}.blurWebView();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Captures a snapshot of the page within rect.
		/// <para>Same as webContents.capturePage([rect, ]callback).</para>
		/// </summary>
		/// <param name="rect">The area of the page to be captured.</param>
		/// <param name="callback"></param>
		public void capturePage(Rectangle rect, Action<NativeImage> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "capturePage";
			CallbackItem item = null;
			item = _client.Callbacks.Add(_id, eventName, (object args) => {
				_client.Callbacks.RemoveItem(_id, eventName, item.CallbackId);
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				NativeImage image = new NativeImage(_client, (int)argsList[0]);
				callback?.Invoke(image);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (image) => {{",
						"this.emit('__event',{0},{1},{2},{3});",
					"}};",
					"return {4};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("image"),
				Script.AddObject("callback")
			);
			long objectId = _ExecuteBlocking<long>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.capturePage({1},{2});",
				Script.GetObject(_id),
				rect.Stringify(),
				Script.GetObject(objectId)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Captures a snapshot of the page within rect.
		/// <para>Same as webContents.capturePage([rect, ]callback).</para>
		/// </summary>
		/// <param name="callback"></param>
		public void capturePage(Action<NativeImage> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "capturePage";
			CallbackItem item = null;
			item = _client.Callbacks.Add(_id, eventName, (object args) => {
				_client.Callbacks.RemoveItem(_id, eventName, item.CallbackId);
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				NativeImage image = new NativeImage(_client, (int)argsList[0]);
				callback?.Invoke(image);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (image) => {{",
						"this.emit('__event',{0},{1},{2},{3});",
					"}};",
					"return {4};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("image"),
				Script.AddObject("callback")
			);
			long objectId = _ExecuteBlocking<long>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.capturePage({1});",
				Script.GetObject(_id),
				Script.GetObject(objectId)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Same as webContents.loadURL(url[, options]).
		/// <para>
		/// The url can be a remote address (e.g. http://)
		/// or a path to a local HTML file using the file:// protocol.
		/// </para>
		/// </summary>
		/// <param name="url"></param>
		public void loadURL(string url) {
			string script = ScriptBuilder.Build(
				"{0}.loadURL({1});",
				Script.GetObject(_id),
				url.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <param name="options"></param>
		public void loadURL(string url, JsonObject options) {
			string script = ScriptBuilder.Build(
				"{0}.loadURL({1},{2});",
				Script.GetObject(_id),
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
		public void loadFile(string filePath) {
			string script = ScriptBuilder.Build(
				"{0}.loadFile({1});",
				Script.GetObject(_id),
				filePath.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Same as webContents.reload.
		/// </summary>
		public void reload() {
			string script = ScriptBuilder.Build(
				"{0}.reload();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}


		/// <summary>
		/// *Linux Windows*
		/// Sets the menu as the window's menu bar,
		/// setting it to null will remove the menu bar.
		/// </summary>
		/// <param name="menu"></param>
		public void setMenu(Menu menu) {
			string script = string.Empty;
			if (menu == null) {
				script = ScriptBuilder.Build(
					"{0}.setMenu(null);",
					Script.GetObject(_id)
				);
			} else {
				script = ScriptBuilder.Build(
					"{0}.setMenu({1});",
					Script.GetObject(_id),
					Script.GetObject(menu._id)
				);
			}
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets progress value in progress bar. Valid range is [0, 1.0].
		/// </summary>
		/// <param name="progress"></param>
		public void setProgressBar(double progress) {
			string script = ScriptBuilder.Build(
				"{0}.setProgressBar({1});",
				Script.GetObject(_id),
				progress
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets progress value in progress bar. Valid range is [0, 1.0].
		/// </summary>
		/// <param name="progress"></param>
		/// <param name="options"></param>
		public void setProgressBar(double progress, JsonObject options) {
			string script = ScriptBuilder.Build(
				"{0}.setProgressBar({1},{2});",
				Script.GetObject(_id),
				progress,
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows*
		/// Sets a 16 x 16 pixel overlay onto the current taskbar icon,
		/// usually used to convey some sort of application status or to passively notify the user.
		/// </summary>
		/// <param name="overlay">
		/// the icon to display on the bottom right corner of the taskbar icon.
		/// If this parameter is null, the overlay is cleared
		/// </param>
		/// <param name="description">
		/// a description that will be provided to Accessibility screen readers
		/// </param>
		public void setOverlayIcon(NativeImage overlay, string description) {
			string script = ScriptBuilder.Build(
				"{0}.setOverlayIcon({1},{2});",
				Script.GetObject(_id),
				Script.GetObject(overlay._id),
				description.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Sets whether the window should have a shadow.
		/// On Windows and Linux does nothing.
		/// </summary>
		/// <param name="hasShadow"></param>
		public void setHasShadow(bool hasShadow) {
			string script = ScriptBuilder.Build(
				"{0}.setHasShadow({1});",
				Script.GetObject(_id),
				hasShadow.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// *macOS*
		/// Whether the window has a shadow.
		/// On Windows and Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool hasShadow() {
			string script = ScriptBuilder.Build(
				"return {0}.hasShadow();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows macOS*
		/// Sets the opacity of the window.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="opacity">between 0.0 (fully transparent) and 1.0 (fully opaque)</param>
		public void setOpacity(double opacity) {
			string script = ScriptBuilder.Build(
				"{0}.setOpacity({1});",
				Script.GetObject(_id),
				opacity
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows macOS*
		/// between 0.0 (fully transparent) and 1.0 (fully opaque)
		/// </summary>
		/// <returns></returns>
		public double getOpacity() {
			string script = ScriptBuilder.Build(
				"return {0}.getOpacity();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<double>(script);
		}

		/// <summary>
		/// *Windows*
		/// Add a thumbnail toolbar with a specified set of buttons
		/// to the thumbnail image of a window in a taskbar button layout.
		/// Returns a Boolean object indicates whether the thumbnail has been added successfully.
		/// </summary>
		/// <param name="buttons"></param>
		public void setThumbarButtons(ThumbarButton[] buttons) {
			// TODO: implement this
			throw new NotImplementedException();
			/*
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var window = ({0});",
					"return {0}.setThumbarButtons();"
				),
				ID
			);
			ExecuteJavaScript(script);
			//*/
		}

		/// <summary>
		/// *Windows*
		/// Sets the region of the window to show as the thumbnail image displayed
		/// when hovering over the window in the taskbar.
		/// You can reset the thumbnail to be the entire window by specifying an empty region:
		/// {x: 0, y: 0, width: 0, height: 0}.
		/// </summary>
		/// <param name="region">Region of the window</param>
		public void setThumbnailClip(Rectangle region) {
			string script = ScriptBuilder.Build(
				"{0}.setThumbnailClip({1});",
				Script.GetObject(_id),
				region.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows*
		/// Sets the toolTip that is displayed
		/// when hovering over the window thumbnail in the taskbar.
		/// </summary>
		/// <param name="toolTip"></param>
		public void setThumbnailToolTip(string toolTip) {
			string script = ScriptBuilder.Build(
				"{0}.setThumbnailToolTip({1});",
				Script.GetObject(_id),
				toolTip.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows*
		/// Sets the properties for the window's taskbar button.
		/// </summary>
		/// <param name="options"></param>
		public void setAppDetails(JsonObject options) {
			string script = ScriptBuilder.Build(
				"{0}.setAppDetails({1});",
				Script.GetObject(_id),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Same as webContents.showDefinitionForSelection().
		/// </summary>
		public void showDefinitionForSelection() {
			string script = ScriptBuilder.Build(
				"{0}.showDefinitionForSelection();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows Linux*
		/// Changes window icon.
		/// </summary>
		/// <param name="icon"></param>
		public void setIcon(NativeImage icon) {
			string script = ScriptBuilder.Build(
				"{0}.setIcon({1});",
				Script.GetObject(_id),
				Script.GetObject(icon._id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets whether the window menu bar should hide itself automatically.
		/// Once set the menu bar will only show when users press the single Alt key.
		/// </summary>
		/// <param name="hide"></param>
		public void setAutoHideMenuBar(bool hide) {
			string script = ScriptBuilder.Build(
				"{0}.setAutoHideMenuBar({1});",
				Script.GetObject(_id),
				hide.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// Whether menu bar automatically hides itself.
		/// </summary>
		/// <returns></returns>
		public bool isMenuBarAutoHide() {
			string script = ScriptBuilder.Build(
				"return {0}.isMenuBarAutoHide();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows Linux*
		/// Sets whether the menu bar should be visible.
		/// If the menu bar is auto-hide, users can still bring up the menu bar by pressing the single Alt key.
		/// </summary>
		/// <param name="visible"></param>
		public void setMenuBarVisibility(bool visible) {
			string script = ScriptBuilder.Build(
				"{0}.setMenuBarVisibility({1});",
				Script.GetObject(_id),
				visible.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// Whether the menu bar is visible.
		/// </summary>
		/// <returns></returns>
		public bool isMenuBarVisible() {
			string script = ScriptBuilder.Build(
				"return {0}.isMenuBarVisible();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Sets whether the window should be visible on all workspaces.
		/// <para>Note: This API does nothing on Windows.</para>
		/// </summary>
		/// <param name="visible"></param>
		public void setVisibleOnAllWorkspaces(bool visible) {
			string script = ScriptBuilder.Build(
				"{0}.setVisibleOnAllWorkspaces({1});",
				Script.GetObject(_id),
				visible.Escape()
			);
			_ExecuteJavaScript(script);
		}
		
		/// <summary>
		/// Whether the window is visible on all workspaces.
		/// <para>Note: This API always returns false on Windows.</para>
		/// </summary>
		/// <returns></returns>
		public bool isVisibleOnAllWorkspaces() {
			string script = ScriptBuilder.Build(
				"return {0}.isVisibleOnAllWorkspaces();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Makes the window ignore all mouse events.
		/// <para>
		/// All mouse events happened in this window will be passed to the window below this window,
		/// but if this window has focus, it will still receive keyboard events.
		/// </para>
		/// </summary>
		/// <param name="ignore"></param>
		public void setIgnoreMouseEvents(bool ignore) {
			string script = ScriptBuilder.Build(
				"{0}.setIgnoreMouseEvents({1});",
				Script.GetObject(_id),
				ignore.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ignore"></param>
		/// <param name="options"></param>
		public void setIgnoreMouseEvents(bool ignore, JsonObject options) {
			string script = ScriptBuilder.Build(
				"{0}.setIgnoreMouseEvents({1},{2});",
				Script.GetObject(_id),
				ignore.Escape(),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Prevents the window contents from being captured by other apps.
		/// <para>
		/// On macOS it sets the NSWindow's sharingType to NSWindowSharingNone.
		/// On Windows it calls SetWindowDisplayAffinity with WDA_MONITOR.
		/// </para>
		/// </summary>
		/// <param name="enable"></param>
		public void setContentProtection(bool enable) {
			string script = ScriptBuilder.Build(
				"{0}.setContentProtection({1});",
				Script.GetObject(_id),
				enable.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows*
		/// Changes whether the window can be focused.
		/// </summary>
		/// <param name="focusable"></param>
		public void setFocusable(bool focusable) {
			string script = ScriptBuilder.Build(
				"{0}.setFocusable({1});",
				Script.GetObject(_id),
				focusable.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Linux macOS*
		/// Sets parent as current window's parent window,
		/// passing null will turn current window into a top-level window.
		/// </summary>
		/// <param name="parent"></param>
		public void setParentWindow(BrowserWindow parent) {
			string script = string.Empty;
			if (parent == null) {
				script = ScriptBuilder.Build(
					"{0}.setParentWindow(null);",
					Script.GetObject(_id)
				);
			} else {
				script = ScriptBuilder.Build(
					"{0}.setParentWindow({1});",
					Script.GetObject(_id),
					Script.GetObject(parent._id)
				);
			}
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// The parent window.
		/// </summary>
		public BrowserWindow getParentWindow() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var parent = {0}.getParentWindow();",
					"if (parent == null) {{",
						"return null;",
					"}}",
					"return {1};"
				),
				Script.GetObject(_id),
				Script.AddObject("parent")
			);
			int result = _ExecuteBlocking<int>(script);
			return new BrowserWindow(_client, result);
		}

		/// <summary>
		/// All child windows.
		/// </summary>
		/// <param name="callback"></param>
		/// <returns></returns>
		public List<BrowserWindow> getChildWindows(Action<bool> callback) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var result = [];",
					"var windows = {0}.getChildWindows();",
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
		/// *macOS*
		/// Controls whether to hide cursor when typing.
		/// </summary>
		/// <param name="autoHide"></param>
		public void setAutoHideCursor(bool autoHide) {
			string script = ScriptBuilder.Build(
				"{0}.setAutoHideCursor({1});",
				Script.GetObject(_id),
				autoHide.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Selects the previous tab when native tabs
		/// are enabled and there are other tabs in the window.
		/// </summary>
		public void selectPreviousTab() {
			string script = ScriptBuilder.Build(
				"{0}.selectPreviousTab();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Selects the next tab when native tabs
		/// are enabled and there are other tabs in the window.
		/// </summary>
		public void selectNextTab() {
			string script = ScriptBuilder.Build(
				"{0}.selectNextTab();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Merges all windows into one window with multiple tabs
		/// when native tabs are enabled and there is more than one open window.
		/// </summary>
		public void mergeAllWindows() {
			string script = ScriptBuilder.Build(
				"{0}.mergeAllWindows();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Moves the current tab into a new window if native tabs are enabled
		/// and there is more than one tab in the current window.
		/// </summary>
		public void moveTabToNewWindow() {
			string script = ScriptBuilder.Build(
				"{0}.moveTabToNewWindow();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Toggles the visibility of the tab bar if native tabs
		/// are enabled and there is only one tab in the current window.
		/// </summary>
		public void toggleTabBar() {
			string script = ScriptBuilder.Build(
				"{0}.toggleTabBar();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Adds a window as a tab on this window,
		/// after the tab for the window instance.
		/// </summary>
		/// <param name="browserWindow"></param>
		public void addTabbedWindow(BrowserWindow browserWindow) {
			string script = ScriptBuilder.Build(
				"{0}.addTabbedWindow({1});",
				Script.GetObject(_id),
				Script.GetObject(browserWindow._id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Adds a vibrancy effect to the browser window.
		/// <para>Passing null or an empty string will remove the vibrancy effect on the window.</para>
		/// </summary>
		/// <param name="type"></param>
		public void setVibrancy(string type) {
			string script = ScriptBuilder.Build(
				"{0}.setVibrancy({1});",
				Script.GetObject(_id),
				type.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Experimental*
		/// Sets the touchBar layout for the current window.
		/// <para>
		/// Specifying null or undefined clears the touch bar.
		/// This method only has an effect if the machine has a touch bar and is running on macOS 10.12.1+.
		/// </para>
		/// </summary>
		public void setTouchBar(TouchBar touchBar) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		public void setBrowserView(BrowserView browserView) {
			string script = ScriptBuilder.Build(
				"{0}.setBrowserView({1});",
				Script.GetObject(_id),
				Script.GetObject(browserView._id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Experimental*
		/// an attached BrowserView.
		/// Returns null if none is attached.
		/// <para>
		/// Note: The BrowserView API is currently experimental
		/// and may change or be removed in future Electron releases.
		/// </para>
		/// </summary>
		public BrowserView getBrowserView() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = {0}.getBrowserView();",
					"return {1};"
				),
				Script.GetObject(_id),
				Script.AddObject("view")
			);
			int result = _ExecuteBlocking<int>(script);
			return new BrowserView(_client, result);
		}
	}
}
