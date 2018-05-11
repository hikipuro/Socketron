using System;
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
		public BrowserWindow() {
		}

		/// <summary>
		/// A Integer representing the unique ID of the window.
		/// </summary>
		public int id {
			get { return API.GetProperty<int>("id"); }
		}

		/// <summary>
		/// A WebContents object this window owns.
		/// <para>
		/// All web page related events and operations will be done via it.
		/// </para>
		/// </summary>
		public WebContents webContents {
			get { return API.GetObject<WebContents>("webContents"); }
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
			API.Apply("destroy");
		}

		/// <summary>
		/// Try to close the window.
		/// <para>
		/// This has the same effect as a user manually clicking the close button of the window.
		/// The web page may cancel the close though. See the close event.
		/// </para>
		/// </summary>
		public void close() {
			API.Apply("close");
		}

		/// <summary>
		/// Focuses on the window.
		/// </summary>
		public void focus() {
			API.Apply("focus");
		}

		/// <summary>
		/// Removes focus from the window.
		/// </summary>
		public void blur() {
			API.Apply("blur");
		}

		/// <summary>
		/// Whether the window is focused.
		/// </summary>
		/// <returns>bool</returns>
		public bool isFocused() {
			return API.Apply<bool>("isFocused");
		}

		/// <summary>
		/// Whether the window is destroyed.
		/// </summary>
		/// <returns>bool</returns>
		public bool isDestroyed() {
			return API.Apply<bool>("isDestroyed");
		}

		/// <summary>
		/// Shows and gives focus to the window.
		/// </summary>
		public void show() {
			API.Apply("show");
		}

		/// <summary>
		/// Shows the window but doesn't focus on it.
		/// </summary>
		public void showInactive() {
			API.Apply("showInactive");
		}

		/// <summary>
		/// Hides the window.
		/// </summary>
		public void hide() {
			API.Apply("hide");
		}

		/// <summary>
		/// Whether the window is visible to the user.
		/// </summary>
		/// <returns>bool</returns>
		public bool isVisible() {
			return API.Apply<bool>("isVisible");
		}

		/// <summary>
		/// Whether current window is a modal window.
		/// </summary>
		/// <returns>bool</returns>
		public bool isModal() {
			return API.Apply<bool>("isModal");
		}

		/// <summary>
		/// Maximizes the window.
		/// <para>
		/// This will also show (but not focus) the window if it isn't being displayed already.
		/// </para>
		/// </summary>
		public void maximize() {
			API.Apply("maximize");
		}

		/// <summary>
		/// Unmaximizes the window.
		/// </summary>
		public void unmaximize() {
			API.Apply("unmaximize");
		}

		/// <summary>
		/// Whether the window is maximized.
		/// </summary>
		/// <returns>bool</returns>
		public bool isMaximized() {
			return API.Apply<bool>("isMaximized");
		}

		/// <summary>
		/// Minimizes the window.
		/// <para>
		/// On some platforms the minimized window will be shown in the Dock.
		/// </para>
		/// </summary>
		public void minimize() {
			API.Apply("minimize");
		}

		/// <summary>
		/// Restores the window from minimized state to its previous state.
		/// </summary>
		public void restore() {
			API.Apply("restore");
		}

		/// <summary>
		/// Whether the window is minimized.
		/// </summary>
		/// <returns>bool</returns>
		public bool isMinimized() {
			return API.Apply<bool>("isMinimized");
		}

		/// <summary>
		/// Sets whether the window should be in fullscreen mode.
		/// </summary>
		/// <param name="flag">bool</param>
		public void setFullScreen(bool flag) {
			API.Apply("setFullScreen", flag);
		}

		/// <summary>
		/// Whether the window is in fullscreen mode.
		/// </summary>
		/// <returns>bool</returns>
		public bool isFullScreen() {
			return API.Apply<bool>("isFullScreen");
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
			API.Apply("setSimpleFullScreen", flag);
		}

		/// <summary>
		/// *macOS*
		/// Whether the window is in simple (pre-Lion) fullscreen mode.
		/// </summary>
		/// <returns>bool</returns>
		public bool isSimpleFullScreen() {
			return API.Apply<bool>("isSimpleFullScreen");
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
			if (size == null) {
				API.Apply("setAspectRatio", aspectRatio);
			} else {
				API.Apply("setAspectRatio", aspectRatio, size);
			}
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
			if (displayName == null) {
				API.Apply("previewFile", path);
			} else {
				API.Apply("previewFile", path, displayName);
			}
		}

		/// <summary>
		/// *macOS*
		/// Closes the currently open Quick Look panel.
		/// </summary>
		public void closeFilePreview() {
			API.Apply("closeFilePreview");
		}

		/// <summary>
		/// Resizes and moves the window to the supplied bounds.
		/// </summary>
		/// <param name="bounds">Rectangle.</param>
		public void setBounds(Rectangle bounds) {
			API.Apply("setBounds", bounds);
		}

		/// <summary>
		/// Resizes and moves the window to the supplied bounds.
		/// </summary>
		/// <param name="bounds">Rectangle.</param>
		/// <param name="animate">*macOS*</param>
		public void setBounds(Rectangle bounds, bool animate) {
			API.Apply("setBounds", bounds, animate);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>Rectangle.</returns>
		public Rectangle getBounds() {
			object result = API.Apply<object>("getBounds");
			return Rectangle.FromObject(result);
		}

		/// <summary>
		/// Resizes and moves the window's client area (e.g. the web page) to the supplied bounds.
		/// </summary>
		/// <param name="bounds">Rectangle.</param>
		public void setContentBounds(Rectangle bounds) {
			API.Apply("setContentBounds", bounds);
		}

		/// <summary>
		/// Resizes and moves the window's client area (e.g. the web page) to the supplied bounds.
		/// </summary>
		/// <param name="bounds">Rectangle.</param>
		/// <param name="animate">*macOS*</param>
		public void setContentBounds(Rectangle bounds, bool animate) {
			API.Apply("setContentBounds", bounds, animate);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>Rectangle.</returns>
		public Rectangle getContentBounds() {
			object result = API.Apply<object>("getContentBounds");
			return Rectangle.FromObject(result);
		}

		/// <summary>
		/// Disable or enable the window.
		/// </summary>
		/// <param name="enable">bool</param>
		public void setEnabled(bool enable) {
			API.Apply("setEnabled", enable);
		}

		/// <summary>
		/// Resizes the window to width and height.
		/// </summary>
		/// <param name="width">int</param>
		/// <param name="height">int</param>
		public void setSize(int width, int height) {
			API.Apply("setSize", width, height);
		}

		/// <summary>
		/// Resizes the window to width and height.
		/// </summary>
		/// <param name="width">int</param>
		/// <param name="height">int</param>
		/// <param name="animate">*macOS*</param>
		public void setSize(int width, int height, bool animate) {
			API.Apply("setSize", width, height, animate);
		}

		/// <summary>
		/// Contains the window's width and height.
		/// </summary>
		/// <returns></returns>
		public int[] getSize() {
			object[] result = API.Apply<object[]>("getSize");
			return Array.ConvertAll(result, value => Convert.ToInt32(value));
		}

		/// <summary>
		/// Resizes the window's client area (e.g. the web page) to width and height.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void setContentSize(int width, int height) {
			API.Apply("setContentSize", width, height);
		}

		/// <summary>
		/// Resizes the window's client area (e.g. the web page) to width and height.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="animate">*macOS*</param>
		public void setContentSize(int width, int height, bool animate) {
			API.Apply("setContentSize", width, height, animate);
		}

		/// <summary>
		/// Contains the window's client area's width and height.
		/// </summary>
		/// <returns></returns>
		public int[] getContentSize() {
			object[] result = API.Apply<object[]>("getContentSize");
			return Array.ConvertAll(result, value => Convert.ToInt32(value));
		}

		/// <summary>
		/// Sets the minimum size of window to width and height.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void setMinimumSize(int width, int height) {
			API.Apply("setMinimumSize", width, height);
		}

		/// <summary>
		/// Contains the window's minimum width and height.
		/// </summary>
		/// <returns></returns>
		public int[] getMinimumSize() {
			object[] result = API.Apply<object[]>("getMinimumSize");
			return Array.ConvertAll(result, value => Convert.ToInt32(value));
		}

		/// <summary>
		/// Sets the maximum size of window to width and height.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void setMaximumSize(int width, int height) {
			API.Apply("setMaximumSize", width, height);
		}

		/// <summary>
		/// Contains the window's maximum width and height.
		/// </summary>
		/// <returns></returns>
		public int[] getMaximumSize() {
			object[] result = API.Apply<object[]>("getMaximumSize");
			return Array.ConvertAll(result, value => Convert.ToInt32(value));
		}

		/// <summary>
		/// Sets whether the window can be manually resized by user.
		/// </summary>
		/// <param name="resizable"></param>
		public void setResizable(bool resizable) {
			API.Apply("setResizable", resizable);
		}

		/// <summary>
		/// Whether the window can be manually resized by user.
		/// </summary>
		/// <returns></returns>
		public bool isResizable() {
			return API.Apply<bool>("isResizable");
		}

		/// <summary>
		/// *macOS Windows*
		/// Sets whether the window can be moved by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="movable"></param>
		public void setMovable(bool movable) {
			API.Apply("setMovable", movable);
		}
		
		/// <summary>
		/// *macOS Windows*
		/// Whether the window can be moved by user.
		///	On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool isMovable() {
			return API.Apply<bool>("isMovable");
		}

		/// <summary>
		/// *macOS Windows*
		/// Sets whether the window can be manually minimized by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="minimizable"></param>
		public void setMinimizable(bool minimizable) {
			API.Apply("setMinimizable", minimizable);
		}
		
		/// <summary>
		/// *macOS Windows*
		/// Whether the window can be manually minimized by user.
		/// On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool isMinimizable() {
			return API.Apply<bool>("isMinimizable");
		}

		/// <summary>
		/// *macOS Windows*
		/// Sets whether the window can be manually maximized by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="maximizable"></param>
		public void setMaximizable(bool maximizable) {
			API.Apply("setMaximizable", maximizable);
		}

		/// <summary>
		/// *macOS Windows*
		/// Whether the window can be manually maximized by user.
		/// On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool isMaximizable() {
			return API.Apply<bool>("isMaximizable");
		}

		/// <summary>
		/// Sets whether the maximize/zoom window button toggles fullscreen mode or maximizes the window.
		/// </summary>
		/// <param name="fullscreenable"></param>
		public void setFullScreenable(bool fullscreenable) {
			API.Apply("setFullScreenable", fullscreenable);
		}

		/// <summary>
		/// Whether the maximize/zoom window button toggles fullscreen mode or maximizes the window.
		/// </summary>
		/// <returns></returns>
		public bool isFullScreenable() {
			return API.Apply<bool>("isFullScreenable");
		}

		/// <summary>
		/// *macOS Windows*
		/// Sets whether the window can be manually closed by user.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="closable"></param>
		public void setClosable(bool closable) {
			API.Apply("setClosable", closable);
		}

		/// <summary>
		/// *macOS Windows*
		/// Whether the window can be manually closed by user.
		/// On Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool isClosable() {
			return API.Apply<bool>("isClosable");
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
			API.Apply("setAlwaysOnTop", flag);
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
			API.Apply("setAlwaysOnTop", flag, level, relativeLevel);
		}

		/// <summary>
		/// Whether the window is always on top of other windows.
		/// </summary>
		/// <returns></returns>
		public bool isAlwaysOnTop() {
			return API.Apply<bool>("isAlwaysOnTop");
		}

		/// <summary>
		/// *macOS Windows*
		/// Moves window to top(z-order) regardless of focus.
		/// </summary>
		public void moveTop() {
			API.Apply("moveTop");
		}

		/// <summary>
		/// Moves window to the center of the screen.
		/// </summary>
		public void center() {
			API.Apply("center");
		}

		/// <summary>
		/// Moves window to x and y.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void setPosition(int x, int y) {
			API.Apply("setPosition", x, y);
		}

		/// <summary>
		/// Moves window to x and y.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="animate">*macOS*</param>
		public void setPosition(int x, int y, bool animate) {
			API.Apply("setPosition", x, y, animate);
		}

		/// <summary>
		/// Contains the window's current position.
		/// </summary>
		/// <returns></returns>
		public int[] getPosition() {
			object[] result = API.Apply<object[]>("getPosition");
			return Array.ConvertAll(result, value => Convert.ToInt32(value));
		}

		/// <summary>
		/// Changes the title of native window to title.
		/// </summary>
		/// <param name="title"></param>
		public void setTitle(string title) {
			API.Apply("setTitle", title);
		}
		
		/// <summary>
		/// The title of the native window.
		/// <para>
		/// Note: The title of web page can be different from the title of the native window.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public string getTitle() {
			return API.Apply<string>("getTitle");
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
			API.Apply("setSheetOffset", offsetY);
		}

		public void setSheetOffset(double offsetY, double offsetX) {
			API.Apply("setSheetOffset", offsetY, offsetX);
		}

		/// <summary>
		/// Starts or stops flashing the window to attract user's attention.
		/// </summary>
		/// <param name="flag"></param>
		public void flashFrame(bool flag) {
			API.Apply("flashFrame", flag);
		}

		/// <summary>
		/// Makes the window not show in the taskbar.
		/// </summary>
		/// <param name="skip"></param>
		public void setSkipTaskbar(bool skip) {
			API.Apply("setSkipTaskbar", skip);
		}

		/// <summary>
		/// Enters or leaves the kiosk mode.
		/// </summary>
		/// <param name="flag"></param>
		public void setKiosk(bool flag) {
			API.Apply("setKiosk", flag);
		}

		/// <summary>
		/// Whether the window is in kiosk mode.
		/// </summary>
		/// <returns></returns>
		public bool isKiosk() {
			return API.Apply<bool>("isKiosk");
		}

		/// <summary>
		/// The native type of the handle is HWND on Windows, NSView* on macOS,
		/// and Window (unsigned long) on Linux.
		/// </summary>
		/// <returns>The platform-specific handle of the window.</returns>
		public ulong getNativeWindowHandle() {
			string script = ScriptBuilder.Build(
				"return {0}.getNativeWindowHandle();",
				Script.GetObject(API.id)
			);
			object result = API._ExecuteBlocking<object>(script);
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
		public void hookWindowMessage(int message, Action callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_hookWindowMessage";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				callback?.Invoke();
			});
			API.Apply("hookWindowMessage", message, item);
		}

		/// <summary>
		/// *Windows*
		/// true or false depending on whether the message is hooked.
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		public bool isWindowMessageHooked(int message) {
			return API.Apply<bool>("isWindowMessageHooked", message);
		}

		/// <summary>
		/// *Windows*
		/// Unhook the window message.
		/// </summary>
		/// <param name="message"></param>
		public void unhookWindowMessage(int message) {
			API.Apply("unhookWindowMessage", message);
		}

		/// <summary>
		/// *Windows*
		/// Unhooks all of the window messages.
		/// </summary>
		public void unhookAllWindowMessages() {
			API.Apply("unhookAllWindowMessages");
		}

		/// <summary>
		/// *macOS*
		/// Sets the pathname of the file the window represents,
		/// and the icon of the file will show in window's title bar.
		/// </summary>
		/// <param name="filename"></param>
		public void setRepresentedFilename(string filename) {
			API.Apply("setRepresentedFilename", filename);
		}

		/// <summary>
		/// *macOS*
		/// The pathname of the file the window represents.
		/// </summary>
		/// <returns></returns>
		public string getRepresentedFilename() {
			return API.Apply<string>("getRepresentedFilename");
		}

		/// <summary>
		/// *macOS*
		/// Specifies whether the window’s document has been edited,
		/// and the icon in title bar will become gray when set to true.
		/// </summary>
		/// <param name="edited"></param>
		public void setDocumentEdited(bool edited) {
			API.Apply("setDocumentEdited", edited);
		}
		
		/// <summary>
		/// *macOS*
		/// Whether the window's document has been edited.
		/// </summary>
		/// <returns></returns>
		public bool isDocumentEdited() {
			return API.Apply<bool>("isDocumentEdited");
		}

		/// <summary>
		/// 
		/// </summary>
		public void focusOnWebView() {
			API.Apply("focusOnWebView");
		}

		/// <summary>
		/// 
		/// </summary>
		public void blurWebView() {
			API.Apply("blurWebView");
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
			string eventName = "_capturePage";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				NativeImage image = API.CreateObject<NativeImage>((int)args[0]);
				callback?.Invoke(image);
			});
			API.Apply("capturePage", rect, item);
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
			string eventName = "_capturePage";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				NativeImage image = API.CreateObject<NativeImage>((int)args[0]);
				callback?.Invoke(image);
			});
			API.Apply("capturePage", item);
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
			API.Apply("loadURL", url);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="url"></param>
		/// <param name="options"></param>
		public void loadURL(string url, JsonObject options) {
			API.Apply("loadURL", url, options);
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
			API.Apply("loadFile", filePath);
		}

		/// <summary>
		/// Same as webContents.reload.
		/// </summary>
		public void reload() {
			API.Apply("reload");
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
					Script.GetObject(API.id)
				);
			} else {
				script = ScriptBuilder.Build(
					"{0}.setMenu({1});",
					Script.GetObject(API.id),
					Script.GetObject(menu.API.id)
				);
			}
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets progress value in progress bar. Valid range is [0, 1.0].
		/// </summary>
		/// <param name="progress"></param>
		public void setProgressBar(double progress) {
			API.Apply("setProgressBar", progress);
		}

		/// <summary>
		/// Sets progress value in progress bar. Valid range is [0, 1.0].
		/// </summary>
		/// <param name="progress"></param>
		/// <param name="options"></param>
		public void setProgressBar(double progress, JsonObject options) {
			API.Apply("setProgressBar", progress, options);
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
				Script.GetObject(API.id),
				Script.GetObject(overlay.API.id),
				description.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Sets whether the window should have a shadow.
		/// On Windows and Linux does nothing.
		/// </summary>
		/// <param name="hasShadow"></param>
		public void setHasShadow(bool hasShadow) {
			API.Apply("setHasShadow", hasShadow);
		}
		
		/// <summary>
		/// *macOS*
		/// Whether the window has a shadow.
		/// On Windows and Linux always returns true.
		/// </summary>
		/// <returns></returns>
		public bool hasShadow() {
			return API.Apply<bool>("hasShadow");
		}

		/// <summary>
		/// *Windows macOS*
		/// Sets the opacity of the window.
		/// On Linux does nothing.
		/// </summary>
		/// <param name="opacity">between 0.0 (fully transparent) and 1.0 (fully opaque)</param>
		public void setOpacity(double opacity) {
			API.Apply("setOpacity", opacity);
		}

		/// <summary>
		/// *Windows macOS*
		/// between 0.0 (fully transparent) and 1.0 (fully opaque)
		/// </summary>
		/// <returns></returns>
		public double getOpacity() {
			return API.Apply<double>("getOpacity");
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
			API.Apply("setThumbnailClip", region);
		}

		/// <summary>
		/// *Windows*
		/// Sets the toolTip that is displayed
		/// when hovering over the window thumbnail in the taskbar.
		/// </summary>
		/// <param name="toolTip"></param>
		public void setThumbnailToolTip(string toolTip) {
			API.Apply("setThumbnailToolTip", toolTip);
		}

		/// <summary>
		/// *Windows*
		/// Sets the properties for the window's taskbar button.
		/// </summary>
		/// <param name="options"></param>
		public void setAppDetails(JsonObject options) {
			API.Apply("setAppDetails", options);
		}

		/// <summary>
		/// *macOS*
		/// Same as webContents.showDefinitionForSelection().
		/// </summary>
		public void showDefinitionForSelection() {
			API.Apply("showDefinitionForSelection");
		}

		/// <summary>
		/// *Windows Linux*
		/// Changes window icon.
		/// </summary>
		/// <param name="icon"></param>
		public void setIcon(NativeImage icon) {
			string script = ScriptBuilder.Build(
				"{0}.setIcon({1});",
				Script.GetObject(API.id),
				Script.GetObject(icon.API.id)
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets whether the window menu bar should hide itself automatically.
		/// Once set the menu bar will only show when users press the single Alt key.
		/// </summary>
		/// <param name="hide"></param>
		public void setAutoHideMenuBar(bool hide) {
			API.Apply("setAutoHideMenuBar", hide);
		}
		
		/// <summary>
		/// Whether menu bar automatically hides itself.
		/// </summary>
		/// <returns></returns>
		public bool isMenuBarAutoHide() {
			return API.Apply<bool>("isMenuBarAutoHide");
		}

		/// <summary>
		/// *Windows Linux*
		/// Sets whether the menu bar should be visible.
		/// If the menu bar is auto-hide, users can still bring up the menu bar by pressing the single Alt key.
		/// </summary>
		/// <param name="visible"></param>
		public void setMenuBarVisibility(bool visible) {
			API.Apply("setMenuBarVisibility", visible);
		}
		
		/// <summary>
		/// Whether the menu bar is visible.
		/// </summary>
		/// <returns></returns>
		public bool isMenuBarVisible() {
			return API.Apply<bool>("isMenuBarVisible");
		}

		/// <summary>
		/// Sets whether the window should be visible on all workspaces.
		/// <para>Note: This API does nothing on Windows.</para>
		/// </summary>
		/// <param name="visible"></param>
		public void setVisibleOnAllWorkspaces(bool visible) {
			API.Apply("setVisibleOnAllWorkspaces", visible);
		}
		
		/// <summary>
		/// Whether the window is visible on all workspaces.
		/// <para>Note: This API always returns false on Windows.</para>
		/// </summary>
		/// <returns></returns>
		public bool isVisibleOnAllWorkspaces() {
			return API.Apply<bool>("isVisibleOnAllWorkspaces");
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
			API.Apply("setIgnoreMouseEvents", ignore);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ignore"></param>
		/// <param name="options"></param>
		public void setIgnoreMouseEvents(bool ignore, JsonObject options) {
			API.Apply("setIgnoreMouseEvents", ignore, options);
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
			API.Apply("setContentProtection", enable);
		}

		/// <summary>
		/// *Windows*
		/// Changes whether the window can be focused.
		/// </summary>
		/// <param name="focusable"></param>
		public void setFocusable(bool focusable) {
			API.Apply("setFocusable", focusable);
		}

		/// <summary>
		/// *Linux macOS*
		/// Sets parent as current window's parent window,
		/// passing null will turn current window into a top-level window.
		/// </summary>
		/// <param name="parent"></param>
		public void setParentWindow(BrowserWindow parent) {
			API.Apply("setParentWindow", parent);
		}

		/// <summary>
		/// The parent window.
		/// </summary>
		public BrowserWindow getParentWindow() {
			return API.ApplyAndGetObject<BrowserWindow>("getParentWindow");
		}

		/// <summary>
		/// All child windows.
		/// </summary>
		/// <returns></returns>
		public BrowserWindow[] getChildWindows() {
			return API.ApplyAndGetObjectList<BrowserWindow>("getChildWindows");
		}

		/// <summary>
		/// *macOS*
		/// Controls whether to hide cursor when typing.
		/// </summary>
		/// <param name="autoHide"></param>
		public void setAutoHideCursor(bool autoHide) {
			API.Apply("setAutoHideCursor", autoHide);
		}

		/// <summary>
		/// *macOS*
		/// Selects the previous tab when native tabs
		/// are enabled and there are other tabs in the window.
		/// </summary>
		public void selectPreviousTab() {
			API.Apply("selectPreviousTab");
		}

		/// <summary>
		/// *macOS*
		/// Selects the next tab when native tabs
		/// are enabled and there are other tabs in the window.
		/// </summary>
		public void selectNextTab() {
			API.Apply("selectNextTab");
		}

		/// <summary>
		/// *macOS*
		/// Merges all windows into one window with multiple tabs
		/// when native tabs are enabled and there is more than one open window.
		/// </summary>
		public void mergeAllWindows() {
			API.Apply("mergeAllWindows");
		}

		/// <summary>
		/// *macOS*
		/// Moves the current tab into a new window if native tabs are enabled
		/// and there is more than one tab in the current window.
		/// </summary>
		public void moveTabToNewWindow() {
			API.Apply("moveTabToNewWindow");
		}

		/// <summary>
		/// *macOS*
		/// Toggles the visibility of the tab bar if native tabs
		/// are enabled and there is only one tab in the current window.
		/// </summary>
		public void toggleTabBar() {
			API.Apply("toggleTabBar");
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
				Script.GetObject(API.id),
				Script.GetObject(browserWindow.API.id)
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Adds a vibrancy effect to the browser window.
		/// <para>Passing null or an empty string will remove the vibrancy effect on the window.</para>
		/// </summary>
		/// <param name="type"></param>
		public void setVibrancy(string type) {
			API.Apply("setVibrancy", type);
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
			API.Apply("setTouchBar", touchBar);
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		public void setBrowserView(BrowserView browserView) {
			API.Apply("setBrowserView", browserView);
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
			return API.ApplyAndGetObject<BrowserView>("getBrowserView");
		}
	}
}
