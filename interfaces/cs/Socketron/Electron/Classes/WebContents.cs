using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Render and control web pages.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class WebContents : EventEmitter {
		/// <summary>
		/// WebContents instance events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when the navigation is done,
			/// i.e. the spinner of the tab has stopped spinning,
			/// and the onload event was dispatched.
			/// </summary>
			public const string DidFinishLoad = "did-finish-load";
			/// <summary>
			/// This event is like did-finish-load but emitted
			/// when the load failed or was cancelled,
			/// e.g. window.stop() is invoked.
			/// The full list of error codes and their meaning is available here.
			/// </summary>
			public const string DidFailLoad = "did-fail-load";
			/// <summary>
			/// Emitted when a frame has done navigation.
			/// </summary>
			public const string DidFrameFinishLoad = "did-frame-finish-load";
			/// <summary>
			/// Corresponds to the points in time when the spinner of the tab started spinning.
			/// </summary>
			public const string DidStartLoading = "did-start-loading";
			/// <summary>
			/// Corresponds to the points in time when the spinner of the tab stopped spinning.
			/// </summary>
			public const string DidStopLoading = "did-stop-loading";
			/// <summary>
			/// *Deprecated*
			/// Emitted when details regarding a requested resource are available.
			/// </summary>
			[Obsolete]
			public const string DidGetResponseDetails = "did-get-response-details";
			/// <summary>
			/// *Deprecated*
			/// Emitted when a redirect is received while requesting a resource.
			/// </summary>
			[Obsolete]
			public const string DidGetRedirectRequest = "did-get-redirect-request";
			/// <summary>
			/// Emitted when the document in the given frame is loaded.
			/// </summary>
			public const string DomReady = "dom-ready";
			/// <summary>
			/// Emitted when page receives favicon urls.
			/// </summary>
			public const string PageFaviconUpdated = "page-favicon-updated";
			/// <summary>
			/// Emitted when the page requests to open a new window for a url.
			/// </summary>
			public const string NewWindow = "new-window";
			/// <summary>
			/// Emitted when a user or the page wants to start navigation.
			/// </summary>
			public const string WillNavigate = "will-navigate";
			/// <summary>
			/// Emitted when any frame (including main) starts navigating.
			/// </summary>
			public const string DidStartNavigation = "did-start-navigation";
			/// <summary>
			/// Emitted when a main frame navigation is done.
			/// </summary>
			public const string DidNavigate = "did-navigate";
			/// <summary>
			/// Emitted when any frame navigation is done.
			/// </summary>
			public const string DidFrameNavigate = "did-frame-navigate";
			/// <summary>
			/// Emitted when an in-page navigation happened in any frame.
			/// </summary>
			public const string DidNavigateInPage = "did-navigate-in-page";
			/// <summary>
			/// Emitted when a beforeunload event handler is attempting to cancel a page unload.
			/// </summary>
			public const string WillPreventUnload = "will-prevent-unload";
			/// <summary>
			/// Emitted when the renderer process crashes or is killed.
			/// </summary>
			public const string Crashed = "crashed";
			/// <summary>
			/// Emitted when the web page becomes unresponsive.
			/// </summary>
			public const string Unresponsive = "unresponsive";
			/// <summary>
			/// Emitted when the unresponsive web page becomes responsive again.
			/// </summary>
			public const string Responsive = "responsive";
			/// <summary>
			/// Emitted when a plugin process has crashed.
			/// </summary>
			public const string PluginCrashed = "plugin-crashed";
			/// <summary>
			/// Emitted when webContents is destroyed.
			/// </summary>
			public const string Destroyed = "destroyed";
			/// <summary>
			/// Emitted before dispatching the keydown and keyup events in the page.
			/// </summary>
			public const string BeforeInputEvent = "before-input-event";
			/// <summary>
			/// Emitted when DevTools is opened.
			/// </summary>
			public const string DevtoolsOpened = "devtools-opened";
			/// <summary>
			/// Emitted when DevTools is closed.
			/// </summary>
			public const string DevtoolsClosed = "devtools-closed";
			/// <summary>
			/// Emitted when DevTools is focused / opened.
			/// </summary>
			public const string DevtoolsFocused = "devtools-focused";
			/// <summary>
			/// Emitted when failed to verify the certificate for url.
			/// </summary>
			public const string CertificateError = "certificate-error";
			/// <summary>
			/// Emitted when a client certificate is requested.
			/// </summary>
			public const string SelectClientCertificate = "select-client-certificate";
			/// <summary>
			/// Emitted when webContents wants to do basic auth.
			/// </summary>
			public const string Login = "login";
			/// <summary>
			/// Emitted when a result is available for [webContents.findInPage] request.
			/// </summary>
			public const string FoundInPage = "found-in-page";
			/// <summary>
			/// Emitted when media starts playing.
			/// </summary>
			public const string MediaStartedPlaying = "media-started-playing";
			/// <summary>
			/// Emitted when media is paused or done playing.
			/// </summary>
			public const string MediaPaused = "media-paused";
			/// <summary>
			/// Emitted when a page's theme color changes.
			/// </summary>
			public const string DidChangeThemeColor = "did-change-theme-color";
			/// <summary>
			/// Emitted when mouse moves over a link or the keyboard moves the focus to a link.
			/// </summary>
			public const string UpdateTargetUrl = "update-target-url";
			/// <summary>
			/// Emitted when the cursor's type changes.
			/// </summary>
			public const string CursorChanged = "cursor-changed";
			/// <summary>
			/// Emitted when there is a new context menu that needs to be handled.
			/// </summary>
			public const string ContextMenu = "context-menu";
			/// <summary>
			/// Emitted when bluetooth device needs to be selected
			/// on call to navigator.bluetooth.requestDevice. 
			/// </summary>
			public const string SelectBluetoothDevice = "select-bluetooth-device";
			/// <summary>
			/// Emitted when a new frame is generated.
			/// </summary>
			public const string Paint = "paint";
			/// <summary>
			/// Emitted when the devtools window instructs the webContents to reload.
			/// </summary>
			public const string DevtoolsReloadPage = "devtools-reload-page";
			/// <summary>
			/// Emitted when a &lt;webview&gt;'s web contents is being attached to this web contents.
			/// </summary>
			public const string WillAttachWebview = "will-attach-webview";
			/// <summary>
			/// Emitted when a &lt;webview&gt; has been attached to this web contents.
			/// </summary>
			public const string DidAttachWebview = "did-attach-webview";
			/// <summary>
			/// Emitted when the associated window logs a console message.
			/// </summary>
			public const string ConsoleMessage = "console-message";
		}

		/// <summary>
		/// savePage() save types.
		/// </summary>
		public class SaveType {
			public const string HTMLOnly = "HTMLOnly";
			public const string HTMLComplete = "HTMLComplete";
			public const string MHTML = "MHTML";
		}

		/// <summary>
		/// printToPDF() options.
		/// </summary>
		public class PrintToPDFOptions {
			public int? marginsType;
			public string pageSize;
			public bool? printBackground;
			public bool? printSelectionOnly;
			public bool? landscape;

			/// <summary>
			/// Parse JSON text.
			/// </summary>
			/// <param name="text"></param>
			/// <returns></returns>
			public static PrintToPDFOptions Parse(string text) {
				return JSON.Parse<PrintToPDFOptions>(text);
			}

			/// <summary>
			/// Create JSON text.
			/// </summary>
			/// <returns></returns>
			public string Stringify() {
				return JSON.Stringify(this);
			}
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public WebContents() {
		}

		/// <summary>
		/// A Integer representing the unique ID of this WebContents.
		/// </summary>
		public int id {
			get { return API.GetProperty<int>("id"); }
		}

		/// <summary>
		/// A Session used by this webContents.
		/// </summary>
		public Session session {
			get { return API.GetObject<Session>("session"); }
		}

		/// <summary>
		/// A WebContents instance that might own this WebContents.
		/// </summary>
		public WebContents hostWebContents {
			get { return API.GetObject<WebContents>("hostWebContents"); }
		}

		/// <summary>
		/// A WebContents of DevTools for this WebContents.
		/// </summary>
		public WebContents devToolsWebContents {
			get { return API.GetObject<WebContents>("devToolsWebContents"); }
		}

		/// <summary>
		/// A Debugger instance for this webContents.
		/// </summary>
		public Debugger debugger {
			get { return API.GetObject<Debugger>("debugger"); }
		}

		/// <summary>
		/// Loads the url in the window. The url must contain the protocol prefix,
		/// e.g. the http:// or file://.
		/// If the load should bypass http cache then use the pragma header to achieve it.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="options"></param>
		public void loadURL(string url, JsonObject options = null) {
			if (options == null) {
				API.Apply("loadURL", url);
			} else {
				API.Apply("loadURL", url, options);
			}
		}

		/// <summary>
		/// Loads the given file in the window, filePath should be a path
		/// to an HTML file relative to the root of your application.
		/// </summary>
		/// <param name="filePath"></param>
		public void loadFile(string filePath) {
			API.Apply("loadFile", filePath);
		}

		/// <summary>
		/// Initiates a download of the resource at url without navigating.
		/// The will-download event of session will be triggered.
		/// </summary>
		/// <param name="url"></param>
		public void downloadURL(string url) {
			API.Apply("downloadURL", url);
		}

		/// <summary>
		/// Returns String - The URL of the current web page.
		/// </summary>
		/// <returns></returns>
		public string getURL() {
			return API.Apply<string>("getURL");
		}

		/// <summary>
		/// Returns String - The title of the current web page.
		/// </summary>
		/// <returns></returns>
		public string getTitle() {
			return API.Apply<string>("getTitle");
		}

		/// <summary>
		/// Returns Boolean - Whether the web page is destroyed.
		/// </summary>
		/// <returns></returns>
		public bool isDestroyed() {
			return API.Apply<bool>("isDestroyed");
		}

		/// <summary>
		/// Focuses the web page.
		/// </summary>
		public void focus() {
			API.Apply("focus");
		}

		/// <summary>
		/// Returns Boolean - Whether the web page is focused.
		/// </summary>
		/// <returns></returns>
		public bool isFocused() {
			return API.Apply<bool>("isFocused");
		}

		/// <summary>
		/// Returns Boolean - Whether web page is still loading resources.
		/// </summary>
		/// <returns></returns>
		public bool isLoading() {
			return API.Apply<bool>("isLoading");
		}

		/// <summary>
		/// Returns Boolean - Whether the main frame
		/// (and not just iframes or frames within it) is still loading.
		/// </summary>
		/// <returns></returns>
		public bool isLoadingMainFrame() {
			return API.Apply<bool>("isLoadingMainFrame");
		}

		/// <summary>
		/// Returns Boolean - Whether the web page is waiting for
		/// a first-response from the main resource of the page.
		/// </summary>
		/// <returns></returns>
		public bool isWaitingForResponse() {
			return API.Apply<bool>("isWaitingForResponse");
		}

		/// <summary>
		/// Stops any pending navigation.
		/// </summary>
		public void stop() {
			API.Apply("stop");
		}

		/// <summary>
		/// Reloads the current web page.
		/// </summary>
		public void reload() {
			API.Apply("reload");
		}

		/// <summary>
		/// Reloads current page and ignores cache.
		/// </summary>
		public void reloadIgnoringCache() {
			API.Apply("reloadIgnoringCache");
		}

		/// <summary>
		/// Returns Boolean - Whether the browser can go back to previous web page.
		/// </summary>
		/// <returns></returns>
		public bool canGoBack() {
			return API.Apply<bool>("canGoBack");
		}

		/// <summary>
		/// Returns Boolean - Whether the browser can go forward to next web page.
		/// </summary>
		/// <returns></returns>
		public bool canGoForward() {
			return API.Apply<bool>("canGoForward");
		}

		/// <summary>
		/// Returns Boolean - Whether the web page can go to offset.
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public bool canGoToOffset(int offset) {
			return API.Apply<bool>("canGoToOffset", offset);
		}

		/// <summary>
		/// Clears the navigation history.
		/// </summary>
		public void clearHistory() {
			API.Apply("clearHistory");
		}

		/// <summary>
		/// Makes the browser go back a web page.
		/// </summary>
		public void goBack() {
			API.Apply("goBack");
		}

		/// <summary>
		/// Makes the browser go forward a web page.
		/// </summary>
		public void goForward() {
			API.Apply("goForward");
		}

		/// <summary>
		/// Navigates browser to the specified absolute web page index.
		/// </summary>
		/// <param name="index"></param>
		public void goToIndex(int index) {
			API.Apply("goToIndex", index);
		}

		/// <summary>
		/// Navigates to the specified offset from the "current entry".
		/// </summary>
		/// <param name="offset"></param>
		public void goToOffset(int offset) {
			API.Apply("goToOffset", offset);
		}

		/// <summary>
		/// Returns Boolean - Whether the renderer process has crashed.
		/// </summary>
		/// <returns></returns>
		public bool isCrashed() {
			return API.Apply<bool>("isCrashed");
		}

		/// <summary>
		/// Overrides the user agent for this web page.
		/// </summary>
		/// <param name="userAgent"></param>
		public void setUserAgent(string userAgent) {
			API.Apply("setUserAgent", userAgent);
		}

		/// <summary>
		/// Returns String - The user agent for this web page.
		/// </summary>
		/// <returns></returns>
		public string getUserAgent() {
			return API.Apply<string>("getUserAgent");
		}

		/// <summary>
		/// Injects CSS into the current web page.
		/// </summary>
		/// <param name="css"></param>
		public void insertCSS(string css) {
			API.Apply("insertCSS", css);
		}

		/// <summary>
		/// Evaluates code in page.
		/// </summary>
		/// <param name="code"></param>
		/// <returns>
		/// A promise that resolves with the result
		/// of the executed code or is rejected if the result
		/// of the code is a rejected promise.
		/// </returns>
		public Promise executeJavaScript(string code) {
			return API.ApplyAndGetObject<Promise>(
				"executeJavaScript", code
			);
		}

		public Promise executeJavaScript(string code, bool userGesture) {
			return API.ApplyAndGetObject<Promise>(
				"executeJavaScript", code, userGesture
			);
		}

		public Promise executeJavaScript(string code, bool userGesture, Action<JSObject> callback) {
			string eventName = "_executeJavaScript";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (args) => {
				API.RemoveCallbackItem(eventName, item);
				JSObject result = API.CreateObject<JSObject>(args[0]);
				callback?.Invoke(result);
			});
			return API.ApplyAndGetObject<Promise>(
				"executeJavaScript", code, userGesture, item
			);
		}

		/// <summary>
		/// *Experimental*
		/// Ignore application menu shortcuts while this web contents is focused.
		/// </summary>
		/// <param name="ignore"></param>
		public void setIgnoreMenuShortcuts(bool ignore) {
			API.Apply("setIgnoreMenuShortcuts", ignore);
		}

		/// <summary>
		/// Mute the audio on the current web page.
		/// </summary>
		/// <param name="muted"></param>
		public void setAudioMuted(bool muted) {
			API.Apply("setAudioMuted", muted);
		}

		/// <summary>
		/// Returns Boolean - Whether this page has been muted.
		/// </summary>
		/// <returns></returns>
		public bool isAudioMuted() {
			return API.Apply<bool>("isAudioMuted");
		}

		/// <summary>
		/// Changes the zoom factor to the specified factor.
		/// Zoom factor is zoom percent divided by 100, so 300% = 3.0.
		/// </summary>
		/// <param name="factor"></param>
		public void setZoomFactor(double factor) {
			API.Apply("setZoomFactor", factor);
		}

		/// <summary>
		/// Sends a request to get current zoom factor,
		/// the callback will be called with callback(zoomFactor).
		/// </summary>
		/// <param name="callback"></param>
		public void getZoomFactor(Action<double> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_getZoomFactor";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				double zoomFactor = Convert.ToDouble(args[0]);
				callback?.Invoke(zoomFactor);
			});
			API.Apply("getZoomFactor", item);
		}

		/// <summary>
		/// Changes the zoom level to the specified level.
		/// <para>
		/// The original size is 0 and each increment above or below represents
		/// zooming 20% larger or smaller to default limits of 300% and 50% of original size,
		/// respectively. The formula for this is scale := 1.2 ^ level.
		/// </para>
		/// </summary>
		/// <param name="level"></param>
		public void setZoomLevel(double level) {
			API.Apply("setZoomLevel", level);
		}

		/// <summary>
		/// Sends a request to get current zoom level,
		/// the callback will be called with callback(zoomLevel).
		/// </summary>
		/// <param name="callback"></param>
		public void getZoomLevel(Action<double> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_getZoomLevel";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				double zoomLevel = Convert.ToDouble(args[0]);
				callback?.Invoke(zoomLevel);
			});
			API.Apply("getZoomLevel", item);
		}

		/// <summary>
		/// Sets the maximum and minimum pinch-to-zoom level.
		/// </summary>
		/// <param name="minimumLevel"></param>
		/// <param name="maximumLevel"></param>
		public void setVisualZoomLevelLimits(double minimumLevel, double maximumLevel) {
			API.Apply("setVisualZoomLevelLimits", minimumLevel, maximumLevel);
		}

		/// <summary>
		/// Sets the maximum and minimum layout-based (i.e. non-visual) zoom level.
		/// </summary>
		/// <param name="minimumLevel"></param>
		/// <param name="maximumLevel"></param>
		public void setLayoutZoomLevelLimits(double minimumLevel, double maximumLevel) {
			API.Apply("setLayoutZoomLevelLimits", minimumLevel, maximumLevel);
		}

		/// <summary>
		/// Executes the editing command undo in web page.
		/// </summary>
		public void undo() {
			API.Apply("undo");
		}

		/// <summary>
		/// Executes the editing command redo in web page.
		/// </summary>
		public void redo() {
			API.Apply("redo");
		}

		/// <summary>
		/// Executes the editing command cut in web page.
		/// </summary>
		public void cut() {
			API.Apply("cut");
		}

		/// <summary>
		/// Executes the editing command copy in web page.
		/// </summary>
		public void copy() {
			API.Apply("copy");
		}

		/// <summary>
		/// Copy the image at the given position to the clipboard.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void copyImageAt(int x, int y) {
			API.Apply("copyImageAt", x, y);
		}

		/// <summary>
		/// Executes the editing command paste in web page.
		/// </summary>
		public void paste() {
			API.Apply("paste");
		}

		/// <summary>
		/// Executes the editing command pasteAndMatchStyle in web page.
		/// </summary>
		public void pasteAndMatchStyle() {
			API.Apply("pasteAndMatchStyle");
		}

		/// <summary>
		/// Executes the editing command delete in web page.
		/// </summary>
		public void delete() {
			API.Apply("delete");
		}

		/// <summary>
		/// Executes the editing command selectAll in web page.
		/// </summary>
		public void selectAll() {
			API.Apply("selectAll");
		}

		/// <summary>
		/// Executes the editing command unselect in web page.
		/// </summary>
		public void unselect() {
			API.Apply("unselect");
		}

		/// <summary>
		/// Executes the editing command replace in web page.
		/// </summary>
		/// <param name="text"></param>
		public void replace(string text) {
			API.Apply("replace", text);
		}

		/// <summary>
		/// Executes the editing command replaceMisspelling in web page.
		/// </summary>
		/// <param name="text"></param>
		public void replaceMisspelling(string text) {
			API.Apply("replaceMisspelling", text);
		}

		/// <summary>
		/// Inserts text to the focused element.
		/// </summary>
		/// <param name="text"></param>
		public void insertText(string text) {
			API.Apply("insertText", text);
		}

		/// <summary>
		/// Returns Integer - The request id used for the request.
		/// <para>
		/// Starts a request to find all matches for the text in the web page.
		/// The result of the request can be obtained by subscribing to found-in-page event.
		/// </para>
		/// </summary>
		/// <param name="text"></param>
		/// <param name="options"></param>
		public void findInPage(string text, JsonObject options = null) {
			if (options == null) {
				API.Apply("findInPage", text);
			} else {
				API.Apply("findInPage", text, options);
			}
		}

		/// <summary>
		/// Stops any findInPage request for the webContents with the provided action.
		/// </summary>
		/// <param name="action">
		/// Specifies the action to take place when ending [webContents.findInPage] request.
		/// </param>
		public void stopFindInPage(string action) {
			API.Apply("stopFindInPage", action);
		}

		/// <summary>
		/// Captures a snapshot of the page within rect. 
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
				NativeImage image = API.CreateObject<NativeImage>(args[0]);
				callback?.Invoke(image);
			});
			API.Apply("capturePage", rect, item);
		}

		/// <summary>
		/// Captures a snapshot of the page within rect. 
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
				NativeImage image = API.CreateObject<NativeImage>(args[0]);
				callback?.Invoke(image);
			});
			API.Apply("capturePage", item);
		}

		/// <summary>
		/// Checks if any ServiceWorker is registered 
		/// and returns a boolean as response to callback.
		/// </summary>
		/// <param name="callback"></param>
		public void hasServiceWorker(Action<bool> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_hasServiceWorker";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				bool hasWorker = Convert.ToBoolean(args[0]);
				callback?.Invoke(hasWorker);
			});
			API.Apply("hasServiceWorker", item);
		}

		/// <summary>
		/// Unregisters any ServiceWorker if present and returns a boolean as response
		/// to callback when the JS promise is fulfilled or false when the JS promise is rejected.
		/// </summary>
		/// <param name="text"></param>
		public void unregisterServiceWorker(Action<bool> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_unregisterServiceWorker";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				bool success = Convert.ToBoolean(args[0]);
				callback?.Invoke(success);
			});
			API.Apply("unregisterServiceWorker", item);
		}

		/// <summary>
		/// Get the system printer list.
		/// </summary>
		/// <returns></returns>
		public PrinterInfo[] getPrinters() {
			object[] result = API.Apply<object[]>("getPrinters");
			return Array.ConvertAll(result, value => PrinterInfo.Parse(value as string));
		}

		/// <summary>
		/// Prints window's web page.
		/// When silent is set to true, Electron will pick the system's
		/// default printer if deviceName is empty and the default settings for printing.
		/// </summary>
		public void print() {
			API.Apply("print");
		}

		public void print(JsonObject options) {
			API.Apply("print", options);
		}

		public void print(JsonObject options, Action<bool> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_print";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				bool success = Convert.ToBoolean(args[0]);
				callback?.Invoke(success);
			});
			API.Apply("print", options, item);
		}

		/// <summary>
		/// Prints window's web page as PDF with Chromium's preview printing custom settings.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="callback"></param>
		public void printToPDF(PrintToPDFOptions options, Action<Error, Buffer> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_printToPDF";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Error error = API.CreateObject<Error>(args[0]);
				Buffer data = API.CreateObject<Buffer>(args[1]);
				callback?.Invoke(error, data);
			});
			API.Apply("printToPDF", options, item);
		}

		/// <summary>
		/// Adds the specified path to DevTools workspace.
		/// </summary>
		/// <example>
		/// Must be used after DevTools creation:
		/// <code>
		/// const {BrowserWindow} = require('electron')
		/// let win = new BrowserWindow()
		/// win.webContents.on('devtools-opened', () => {
		///		win.webContents.addWorkSpace(__dirname)
		/// })
		/// </code>
		/// </example>
		/// <param name="path"></param>
		public void addWorkSpace(string path) {
			API.Apply("addWorkSpace", path);
		}

		/// <summary>
		/// Removes the specified path from DevTools workspace.
		/// </summary>
		/// <param name="path"></param>
		public void removeWorkSpace(string path) {
			API.Apply("removeWorkSpace", path);
		}

		/// <summary>
		/// Uses the devToolsWebContents as the target WebContents to show devtools.
		/// </summary>
		/// <param name="devToolsWebContents"></param>
		public void setDevToolsWebContents(WebContents devToolsWebContents) {
			string script = ScriptBuilder.Build(
				"{0}.setDevToolsWebContents({1});",
				Script.GetObject(API.id),
				Script.GetObject(devToolsWebContents.API.id)
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Opens the devtools.
		/// <para>
		/// When contents is a &lt;webview&gt; tag, the mode would be detach by default,
		/// explicitly passing an empty mode can force using last used dock state.
		/// </para>
		/// </summary>
		/// <param name="options"></param>
		public void openDevTools(JsonObject options = null) {
			if (options == null) {
				API.Apply("openDevTools");
			} else {
				API.Apply("openDevTools", options);
			}
		}

		/// <summary>
		/// Closes the devtools.
		/// </summary>
		public void closeDevTools() {
			API.Apply("closeDevTools");
		}

		/// <summary>
		/// Returns Boolean - Whether the devtools is opened.
		/// </summary>
		/// <returns></returns>
		public bool isDevToolsOpened() {
			return API.Apply<bool>("isDevToolsOpened");
		}

		/// <summary>
		/// Returns Boolean - Whether the devtools view is focused.
		/// </summary>
		/// <returns></returns>
		public bool isDevToolsFocused() {
			return API.Apply<bool>("isDevToolsFocused");
		}

		/// <summary>
		/// Toggles the developer tools.
		/// </summary>
		public void toggleDevTools() {
			API.Apply("toggleDevTools");
		}

		/// <summary>
		/// Starts inspecting element at position (x, y).
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void inspectElement(int x, int y) {
			API.Apply("inspectElement", x, y);
		}

		/// <summary>
		/// Opens the developer tools for the service worker context.
		/// </summary>
		public void inspectServiceWorker() {
			API.Apply("inspectServiceWorker");
		}

		/// <summary>
		/// Send an asynchronous message to renderer process via channel,
		/// you can also send arbitrary arguments.
		/// Arguments will be serialized in JSON internally
		/// and hence no functions or prototype chain will be included.
		/// </summary>
		/// <param name="channel"></param>
		/// <param name="args"></param>
		public void send(string channel, params object[] args) {
			string script = ScriptBuilder.Build(
				"{0}.send({1},{2});",
				Script.GetObject(API.id),
				channel,
				API.CreateParams(args)
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Enable device emulation with the given parameters.
		/// </summary>
		/// <param name="parameters"></param>
		public void enableDeviceEmulation(JsonObject parameters) {
			API.Apply("enableDeviceEmulation", parameters);
		}

		/// <summary>
		/// Disable device emulation enabled by webContents.enableDeviceEmulation.
		/// </summary>
		public void disableDeviceEmulation() {
			API.Apply("disableDeviceEmulation");
		}

		/// <summary>
		/// Sends an input event to the page.
		/// <para>
		/// Note: The BrowserWindow containing the contents needs to be focused for sendInputEvent() to work.
		/// </para>
		/// </summary>
		/// <param name="event"></param>
		public void sendInputEvent(JsonObject @event) {
			API.Apply("sendInputEvent", @event);
		}

		/// <summary>
		/// Begin subscribing for presentation events and captured frames,
		/// the callback will be called with callback(frameBuffer, dirtyRect)
		/// when there is a presentation event.
		/// </summary>
		public void beginFrameSubscription(Action<Buffer, Rectangle> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_beginFrameSubscription";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				// TODO: fix rectangle param
				API.RemoveCallbackItem(eventName, item);
				Buffer frameBuffer = API.CreateObject<Buffer>(args[0]);
				JSObject _dirtyRect = API.CreateObject<JSObject>(args[1]);
				Rectangle dirtyRect = Rectangle.FromObject(_dirtyRect.API.GetValue());
				callback?.Invoke(frameBuffer, dirtyRect);
			});
			API.Apply("beginFrameSubscription", item);
		}

		public void beginFrameSubscription(bool onlyDirty, Action<Buffer, Rectangle> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_beginFrameSubscription";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				// TODO: fix rectangle param
				API.RemoveCallbackItem(eventName, item);
				Buffer frameBuffer = API.CreateObject<Buffer>(args[0]);
				JSObject _dirtyRect = API.CreateObject<JSObject>(args[1]);
				Rectangle dirtyRect = Rectangle.FromObject(_dirtyRect.API.GetValue());
				callback?.Invoke(frameBuffer, dirtyRect);
			});
			API.Apply("beginFrameSubscription", onlyDirty, item);
		}


		/// <summary>
		/// End subscribing for frame presentation events.
		/// </summary>
		public void endFrameSubscription() {
			API.Apply("endFrameSubscription");
		}

		/// <summary>
		/// Sets the item as dragging item for current drag-drop operation.
		/// </summary>
		/// <param name="item"></param>
		public void startDrag(JsonObject item) {
			// TODO: fix item.icon param
			API.Apply("startDrag", item);
		}

		/// <summary>
		/// Returns Boolean - true if the process of saving page has been initiated successfully.
		/// </summary>
		/// <param name="fullPath"></param>
		/// <param name="saveType"></param>
		/// <param name="callback"></param>
		/// <returns></returns>
		public bool savePage(string fullPath, string saveType, Action<Error> callback) {
			if (callback == null) {
				return false;
			}
			string eventName = "_savePage";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				Error error = API.CreateObject<Error>(args[0]);
				callback?.Invoke(error);
			});
			return API.Apply<bool>("savePage", fullPath, saveType, item);
		}

		/// <summary>
		/// *macOS* 
		/// Shows pop-up dictionary that searches the selected word on the page.
		/// </summary>
		public void showDefinitionForSelection() {
			API.Apply("showDefinitionForSelection");
		}

		/// <summary>
		/// Set the size of the page.
		/// This is only supported for &lt;webview&gt; guest contents.
		/// </summary>
		/// <param name="options"></param>
		public void setSize(JsonObject options) {
			API.Apply("setSize", options);
		}

		/// <summary>
		/// Returns Boolean - Indicates whether offscreen rendering is enabled.
		/// </summary>
		/// <returns></returns>
		public bool isOffscreen() {
			return API.Apply<bool>("isOffscreen");
		}

		/// <summary>
		/// If offscreen rendering is enabled and not painting, start painting.
		/// </summary>
		public void startPainting() {
			API.Apply("startPainting");
		}

		/// <summary>
		/// If offscreen rendering is enabled and painting, stop painting.
		/// </summary>
		public void stopPainting() {
			API.Apply("stopPainting");
		}

		/// <summary>
		/// Returns Boolean - If offscreen rendering is enabled returns whether it is currently painting.
		/// </summary>
		/// <returns></returns>
		public bool isPainting() {
			return API.Apply<bool>("isPainting");
		}

		/// <summary>
		/// If offscreen rendering is enabled sets the frame rate to the specified number.
		/// Only values between 1 and 60 are accepted.
		/// </summary>
		/// <param name="fps"></param>
		public void setFrameRate(int fps) {
			API.Apply("setFrameRate", fps);
		}

		/// <summary>
		/// Returns Integer - If offscreen rendering is enabled returns the current frame rate.
		/// </summary>
		/// <returns></returns>
		public int getFrameRate() {
			return API.Apply<int>("getFrameRate");
		}

		/// <summary>
		/// Schedules a full repaint of the window this web contents is in.
		/// <para>
		/// If offscreen rendering is enabled invalidates the frame
		/// and generates a new one through the 'paint' event.
		/// </para>
		/// </summary>
		public void invalidate() {
			API.Apply("invalidate");
		}

		/// <summary>
		/// Returns String - Returns the WebRTC IP Handling Policy.
		/// </summary>
		/// <returns></returns>
		public string getWebRTCIPHandlingPolicy() {
			return API.Apply<string>("getWebRTCIPHandlingPolicy");
		}

		/// <summary>
		/// Setting the WebRTC IP handling policy allows you to control
		/// which IPs are exposed via WebRTC. See BrowserLeaks for more details.
		/// </summary>
		/// <param name="policy"></param>
		public void setWebRTCIPHandlingPolicy(string policy) {
			API.Apply("setWebRTCIPHandlingPolicy", policy);
		}

		/// <summary>
		/// Returns Integer - The operating system pid of the associated renderer process.
		/// </summary>
		/// <returns></returns>
		public long getOSProcessId() {
			return API.Apply<long>("getOSProcessId");
		}
	}
}
