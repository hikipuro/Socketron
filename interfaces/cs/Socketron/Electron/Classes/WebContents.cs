using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Render and control web pages.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class WebContents : JSModule {
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
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public WebContents(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// A Integer representing the unique ID of this WebContents.
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
		/// A Session used by this webContents.
		/// </summary>
		public Session session {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = {0}.session;",
						"return {1};"
					),
					Script.GetObject(_id),
					Script.AddObject("session")
				);
				int result = _ExecuteBlocking<int>(script);
				return new Session(_client, result);
			}
		}

		/// <summary>
		/// A WebContents instance that might own this WebContents.
		/// </summary>
		public WebContents hostWebContents {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var hostWebContents = {0}.hostWebContents;",
						"return {1};"
					),
					Script.GetObject(_id),
					Script.AddObject("hostWebContents")
				);
				int result = _ExecuteBlocking<int>(script);
				return new WebContents(_client, result);
			}
		}

		/// <summary>
		/// A WebContents of DevTools for this WebContents.
		/// </summary>
		public WebContents devToolsWebContents {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var devToolsWebContents = {0}.devToolsWebContents;",
						"return {1};"
					),
					Script.GetObject(_id),
					Script.AddObject("devToolsWebContents")
				);
				int result = _ExecuteBlocking<int>(script);
				return new WebContents(_client, result);
			}
		}

		/// <summary>
		/// A Debugger instance for this webContents.
		/// </summary>
		public Debugger debugger {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var debugger = {0}.debugger;",
						"return {1};"
					),
					Script.GetObject(_id),
					Script.AddObject("debugger")
				);
				int result = _ExecuteBlocking<int>(script);
				return new Debugger(_client, result);
			}
		}

		/// <summary>
		/// Loads the url in the window. The url must contain the protocol prefix,
		/// e.g. the http:// or file://.
		/// If the load should bypass http cache then use the pragma header to achieve it.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="options"></param>
		public void loadURL(string url, JsonObject options = null) {
			string script = string.Empty;
			if (options == null) {
				script = ScriptBuilder.Build(
					"{0}.loadURL({1});",
					Script.GetObject(_id),
					url.Escape()
				);
			} else {
				script = ScriptBuilder.Build(
					"{0}.loadURL({1},{2});",
					Script.GetObject(_id),
					url.Escape(),
					options.Stringify()
				);
			}
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Loads the given file in the window, filePath should be a path
		/// to an HTML file relative to the root of your application.
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
		/// Initiates a download of the resource at url without navigating.
		/// The will-download event of session will be triggered.
		/// </summary>
		/// <param name="url"></param>
		public void downloadURL(string url) {
			string script = ScriptBuilder.Build(
				"{0}.downloadURL({1});",
				Script.GetObject(_id),
				url.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - The URL of the current web page.
		/// </summary>
		/// <returns></returns>
		public string getURL() {
			string script = ScriptBuilder.Build(
				"return {0}.getURL();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns String - The title of the current web page.
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
		/// Returns Boolean - Whether the web page is destroyed.
		/// </summary>
		/// <returns></returns>
		public bool isDestroyed() {
			string script = ScriptBuilder.Build(
				"return {0}.isDestroyed();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Focuses the web page.
		/// </summary>
		public void focus() {
			string script = ScriptBuilder.Build(
				"{0}.focus();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the web page is focused.
		/// </summary>
		/// <returns></returns>
		public bool isFocused() {
			string script = ScriptBuilder.Build(
				"return {0}.isFocused();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether web page is still loading resources.
		/// </summary>
		/// <returns></returns>
		public bool isLoading() {
			string script = ScriptBuilder.Build(
				"return {0}.isLoading();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the main frame
		/// (and not just iframes or frames within it) is still loading.
		/// </summary>
		/// <returns></returns>
		public bool isLoadingMainFrame() {
			string script = ScriptBuilder.Build(
				"return {0}.isLoadingMainFrame();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the web page is waiting for
		/// a first-response from the main resource of the page.
		/// </summary>
		/// <returns></returns>
		public bool isWaitingForResponse() {
			string script = ScriptBuilder.Build(
				"return {0}.isWaitingForResponse();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Stops any pending navigation.
		/// </summary>
		public void stop() {
			string script = ScriptBuilder.Build(
				"{0}.stop();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Reloads the current web page.
		/// </summary>
		public void reload() {
			string script = ScriptBuilder.Build(
				"{0}.reload();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Reloads current page and ignores cache.
		/// </summary>
		public void reloadIgnoringCache() {
			string script = ScriptBuilder.Build(
				"{0}.reloadIgnoringCache();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the browser can go back to previous web page.
		/// </summary>
		/// <returns></returns>
		public bool canGoBack() {
			string script = ScriptBuilder.Build(
				"return {0}.canGoBack();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the browser can go forward to next web page.
		/// </summary>
		/// <returns></returns>
		public bool canGoForward() {
			string script = ScriptBuilder.Build(
				"return {0}.canGoForward();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the web page can go to offset.
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public bool canGoToOffset(int offset) {
			string script = ScriptBuilder.Build(
				"return {0}.canGoToOffset({1});",
				Script.GetObject(_id),
				offset
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Clears the navigation history.
		/// </summary>
		public void clearHistory() {
			string script = ScriptBuilder.Build(
				"{0}.clearHistory();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Makes the browser go back a web page.
		/// </summary>
		public void goBack() {
			string script = ScriptBuilder.Build(
				"{0}.goBack();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Makes the browser go forward a web page.
		/// </summary>
		public void goForward() {
			string script = ScriptBuilder.Build(
				"{0}.goForward();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Navigates browser to the specified absolute web page index.
		/// </summary>
		/// <param name="index"></param>
		public void goToIndex(int index) {
			string script = ScriptBuilder.Build(
				"{0}.goToIndex({1});",
				Script.GetObject(_id),
				index
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Navigates to the specified offset from the "current entry".
		/// </summary>
		/// <param name="offset"></param>
		public void goToOffset(int offset) {
			string script = ScriptBuilder.Build(
				"{0}.goToOffset({1});",
				Script.GetObject(_id),
				offset
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the renderer process has crashed.
		/// </summary>
		/// <returns></returns>
		public bool isCrashed() {
			string script = ScriptBuilder.Build(
				"return {0}.isCrashed();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Overrides the user agent for this web page.
		/// </summary>
		/// <param name="userAgent"></param>
		public void setUserAgent(string userAgent) {
			string script = ScriptBuilder.Build(
				"{0}.setUserAgent({1});",
				Script.GetObject(_id),
				userAgent.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - The user agent for this web page.
		/// </summary>
		/// <returns></returns>
		public string getUserAgent() {
			string script = ScriptBuilder.Build(
				"return {0}.getUserAgent();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Injects CSS into the current web page.
		/// </summary>
		/// <param name="css"></param>
		public void insertCSS(string css) {
			string script = ScriptBuilder.Build(
				"{0}.insertCSS({1});",
				Script.GetObject(_id),
				css.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		public void executeJavaScript(string code) {
			if (code == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				"{0}.executeJavaScript({1});",
				Script.GetObject(_id),
				code.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Experimental*
		/// Ignore application menu shortcuts while this web contents is focused.
		/// </summary>
		/// <param name="ignore"></param>
		public void setIgnoreMenuShortcuts(bool ignore) {
			string script = ScriptBuilder.Build(
				"{0}.setIgnoreMenuShortcuts({1});",
				Script.GetObject(_id),
				ignore.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Mute the audio on the current web page.
		/// </summary>
		/// <param name="muted"></param>
		public void setAudioMuted(bool muted) {
			string script = ScriptBuilder.Build(
				"{0}.setAudioMuted({1});",
				Script.GetObject(_id),
				muted.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether this page has been muted.
		/// </summary>
		/// <returns></returns>
		public bool isAudioMuted() {
			string script = ScriptBuilder.Build(
				"return {0}.isAudioMuted();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Changes the zoom factor to the specified factor.
		/// Zoom factor is zoom percent divided by 100, so 300% = 3.0.
		/// </summary>
		/// <param name="factor"></param>
		public void setZoomFactor(double factor) {
			string script = ScriptBuilder.Build(
				"{0}.setZoomFactor({1});",
				Script.GetObject(_id),
				factor
			);
			_ExecuteJavaScript(script);
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
			string eventName = "getZoomFactor";
			CallbackItem item = _client.Callbacks.Add(_id, eventName, (object args) => {
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				double zoomFactor = (double)argsList[0];
				callback?.Invoke(zoomFactor);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (zoomFactor) => {{",
						"this.emit('__event',{0},{1},{2},zoomFactor);",
					"}};",
					"return {3};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.getZoomFactor({1});",
				Script.GetObject(_id),
				Script.GetObject(item.ObjectId)
			);
			_ExecuteJavaScript(script);
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
			string script = ScriptBuilder.Build(
				"{0}.setZoomLevel({1});",
				Script.GetObject(_id),
				level
			);
			_ExecuteJavaScript(script);
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
			string eventName = "getZoomLevel";
			CallbackItem item = _client.Callbacks.Add(_id, eventName, (object args) => {
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				double zoomLevel = (double)argsList[0];
				callback?.Invoke(zoomLevel);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (zoomLevel) => {{",
						"this.emit('__event',{0},{1},{2},zoomLevel);",
					"}};",
					"return {3};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.getZoomLevel({1});",
				Script.GetObject(_id),
				Script.GetObject(item.ObjectId)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets the maximum and minimum pinch-to-zoom level.
		/// </summary>
		/// <param name="minimumLevel"></param>
		/// <param name="maximumLevel"></param>
		public void setVisualZoomLevelLimits(double minimumLevel, double maximumLevel) {
			string script = ScriptBuilder.Build(
				"{0}.setVisualZoomLevelLimits({1},{2});",
				Script.GetObject(_id),
				minimumLevel,
				maximumLevel
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets the maximum and minimum layout-based (i.e. non-visual) zoom level.
		/// </summary>
		/// <param name="minimumLevel"></param>
		/// <param name="maximumLevel"></param>
		public void setLayoutZoomLevelLimits(double minimumLevel, double maximumLevel) {
			string script = ScriptBuilder.Build(
				"{0}.setLayoutZoomLevelLimits({1},{2});",
				Script.GetObject(_id),
				minimumLevel,
				maximumLevel
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command undo in web page.
		/// </summary>
		public void undo() {
			string script = ScriptBuilder.Build(
				"{0}.undo();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command redo in web page.
		/// </summary>
		public void redo() {
			string script = ScriptBuilder.Build(
				"{0}.redo();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command cut in web page.
		/// </summary>
		public void cut() {
			string script = ScriptBuilder.Build(
				"{0}.cut();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command copy in web page.
		/// </summary>
		public void copy() {
			string script = ScriptBuilder.Build(
				"{0}.copy();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Copy the image at the given position to the clipboard.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void copyImageAt(int x, int y) {
			string script = ScriptBuilder.Build(
				"{0}.copyImageAt({1},{2});",
				Script.GetObject(_id), x, y
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command paste in web page.
		/// </summary>
		public void paste() {
			string script = ScriptBuilder.Build(
				"{0}.paste();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command pasteAndMatchStyle in web page.
		/// </summary>
		public void pasteAndMatchStyle() {
			string script = ScriptBuilder.Build(
				"{0}.pasteAndMatchStyle();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command delete in web page.
		/// </summary>
		public void delete() {
			string script = ScriptBuilder.Build(
				"{0}.delete();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command selectAll in web page.
		/// </summary>
		public void selectAll() {
			string script = ScriptBuilder.Build(
				"{0}.selectAll();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command unselect in web page.
		/// </summary>
		public void unselect() {
			string script = ScriptBuilder.Build(
				"{0}.unselect();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command replace in web page.
		/// </summary>
		/// <param name="text"></param>
		public void replace(string text) {
			string script = ScriptBuilder.Build(
				"{0}.replace({1});",
				Script.GetObject(_id),
				text.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command replaceMisspelling in web page.
		/// </summary>
		/// <param name="text"></param>
		public void replaceMisspelling(string text) {
			string script = ScriptBuilder.Build(
				"{0}.replaceMisspelling({1});",
				Script.GetObject(_id),
				text.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Inserts text to the focused element.
		/// </summary>
		/// <param name="text"></param>
		public void insertText(string text) {
			string script = ScriptBuilder.Build(
				"{0}.insertText({1});",
				Script.GetObject(_id),
				text.Escape()
			);
			_ExecuteJavaScript(script);
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
			string script = string.Empty;
			if (options == null) {
				script = ScriptBuilder.Build(
					"{0}.findInPage({1});",
					Script.GetObject(_id),
					text.Escape()
				);
			} else {
				script = ScriptBuilder.Build(
					"{0}.findInPage({1},{2});",
					Script.GetObject(_id),
					text.Escape(),
					options.Stringify()
				);
			}
			
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Stops any findInPage request for the webContents with the provided action.
		/// </summary>
		/// <param name="action">
		/// Specifies the action to take place when ending [webContents.findInPage] request.
		/// </param>
		public void stopFindInPage(string action) {
			string script = ScriptBuilder.Build(
				"{0}.stopFindInPage({1});",
				Script.GetObject(_id),
				action.Escape()
			);
			_ExecuteJavaScript(script);
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
			string eventName = "capturePage";
			CallbackItem item = _client.Callbacks.Add(_id, eventName, (object args) => {
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
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.capturePage({1},{2});",
				Script.GetObject(_id),
				rect.Stringify(),
				Script.GetObject(item.ObjectId)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Captures a snapshot of the page within rect. 
		/// </summary>
		/// <param name="callback"></param>
		public void capturePage(Action<NativeImage> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "capturePage";
			CallbackItem item = _client.Callbacks.Add(_id, eventName, (object args) => {
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
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.capturePage({1});",
				Script.GetObject(_id),
				Script.GetObject(item.ObjectId)
			);
			_ExecuteJavaScript(script);
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
			string eventName = "hasServiceWorker";
			CallbackItem item = _client.Callbacks.Add(_id, eventName, (object args) => {
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				callback?.Invoke((bool)argsList[0]);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (hasWorker) => {{",
						"this.emit('__event',{0},{1},{2},hasWorker);",
					"}};",
					"return {3};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.hasServiceWorker({1});",
				Script.GetObject(_id),
				Script.GetObject(item.ObjectId)
			);
			_ExecuteJavaScript(script);
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
			string eventName = "unregisterServiceWorker";
			CallbackItem item = _client.Callbacks.Add(_id, eventName, (object args) => {
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				callback?.Invoke((bool)argsList[0]);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (success) => {{",
						"this.emit('__event',{0},{1},{2},success);",
					"}};",
					"return {3};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.unregisterServiceWorker({1});",
				Script.GetObject(_id),
				Script.GetObject(item.ObjectId)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Get the system printer list.
		/// </summary>
		/// <returns></returns>
		public List<PrinterInfo> getPrinters() {
			string script = ScriptBuilder.Build(
				"return {0}.getPrinters();",
				Script.GetObject(_id)
			);
			object[] result = _ExecuteBlocking<object[]>(script);
			List<PrinterInfo> printers = new List<PrinterInfo>();
			foreach (object item in result) {
				PrinterInfo info = PrinterInfo.FromObject(item);
				printers.Add(info);
			}
			return printers;
		}

		/// <summary>
		/// Prints window's web page.
		/// When silent is set to true, Electron will pick the system's
		/// default printer if deviceName is empty and the default settings for printing.
		/// </summary>
		public void print() {
			string script = ScriptBuilder.Build(
				"{0}.print();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		public void print(JsonObject options) {
			string script = ScriptBuilder.Build(
				"{0}.print({1});",
				Script.GetObject(_id),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		public void print(JsonObject options, Action<bool> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "print";
			CallbackItem item = _client.Callbacks.Add(_id, eventName, (object args) => {
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				callback?.Invoke((bool)argsList[0]);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (success) => {{",
						"this.emit('__event',{0},{1},{2},success);",
					"}};",
					"return {3};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.print({1});",
				Script.GetObject(_id),
				Script.GetObject(item.ObjectId)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Prints window's web page as PDF with Chromium's preview printing custom settings.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="callback"></param>
		public void printToPDF(JsonObject options, Action<Error, Buffer> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "printToPDF";
			CallbackItem item = _client.Callbacks.Add(_id, eventName, (object args) => {
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				Error error = new Error(_client, (int)argsList[0]);
				Buffer data = new Buffer(_client, (int)argsList[1]);
				callback?.Invoke(error, data);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (error,data) => {{",
						"this.emit('__event',{0},{1},{2},{3},{4});",
					"}};",
					"return {5};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("error"),
				Script.AddObject("data"),
				Script.AddObject("callback")
			);
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.printToPDF({1},{2});",
				Script.GetObject(_id),
				options.Stringify(),
				Script.GetObject(item.ObjectId)
			);
			_ExecuteJavaScript(script);
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
			string script = ScriptBuilder.Build(
				"{0}.addWorkSpace({1});",
				Script.GetObject(_id),
				path.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Removes the specified path from DevTools workspace.
		/// </summary>
		/// <param name="path"></param>
		public void removeWorkSpace(string path) {
			string script = ScriptBuilder.Build(
				"{0}.removeWorkSpace({1});",
				Script.GetObject(_id),
				path.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Uses the devToolsWebContents as the target WebContents to show devtools.
		/// </summary>
		/// <param name="devToolsWebContents"></param>
		public void setDevToolsWebContents(WebContents devToolsWebContents) {
			string script = ScriptBuilder.Build(
				"{0}.setDevToolsWebContents({1});",
				Script.GetObject(_id),
				Script.GetObject(devToolsWebContents._id)
			);
			_ExecuteJavaScript(script);
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
			string script = string.Empty;
			if (options == null) {
				script = ScriptBuilder.Build(
					"{0}.openDevTools();",
					Script.GetObject(_id)
				);
			} else {
				script = ScriptBuilder.Build(
					"{0}.openDevTools({1});",
					Script.GetObject(_id),
					options.Stringify()
				);
			}
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Closes the devtools.
		/// </summary>
		public void closeDevTools() {
			string script = ScriptBuilder.Build(
				"{0}.closeDevTools();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the devtools is opened.
		/// </summary>
		/// <returns></returns>
		public bool isDevToolsOpened() {
			string script = ScriptBuilder.Build(
				"return {0}.isDevToolsOpened();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the devtools view is focused.
		/// </summary>
		/// <returns></returns>
		public bool isDevToolsFocused() {
			string script = ScriptBuilder.Build(
				"return {0}.isDevToolsFocused();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Toggles the developer tools.
		/// </summary>
		public void toggleDevTools() {
			string script = ScriptBuilder.Build(
				"{0}.toggleDevTools();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Starts inspecting element at position (x, y).
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void inspectElement(int x, int y) {
			string script = ScriptBuilder.Build(
				"{0}.inspectElement({1},{2});",
				Script.GetObject(_id), x, y
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Opens the developer tools for the service worker context.
		/// </summary>
		public void inspectServiceWorker() {
			string script = ScriptBuilder.Build(
				"{0}.inspectServiceWorker();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
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
				Script.GetObject(_id),
				channel,
				CreateParams(args)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Enable device emulation with the given parameters.
		/// </summary>
		/// <param name="parameters"></param>
		public void enableDeviceEmulation(JsonObject parameters) {
			string script = ScriptBuilder.Build(
				"{0}.enableDeviceEmulation({1});",
				Script.GetObject(_id),
				parameters.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Disable device emulation enabled by webContents.enableDeviceEmulation.
		/// </summary>
		public void disableDeviceEmulation() {
			string script = ScriptBuilder.Build(
				"{0}.disableDeviceEmulation();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sends an input event to the page.
		/// <para>
		/// Note: The BrowserWindow containing the contents needs to be focused for sendInputEvent() to work.
		/// </para>
		/// </summary>
		/// <param name="event"></param>
		public void sendInputEvent(JsonObject @event) {
			string script = ScriptBuilder.Build(
				"{0}.sendInputEvent({1});",
				Script.GetObject(_id),
				@event.Stringify()
			);
			_ExecuteJavaScript(script);
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
			string eventName = "beginFrameSubscription";
			CallbackItem item = _client.Callbacks.Add(_id, eventName, (object args) => {
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				Buffer frameBuffer = new Buffer(_client, (int)argsList[0]);
				Rectangle dirtyRect = Rectangle.FromObject(argsList[1]);
				callback?.Invoke(frameBuffer, dirtyRect);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (frameBuffer,dirtyRect) => {{",
						"this.emit('__event',{0},{1},{2},{3},dirtyRect);",
					"}};",
					"return {4};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("frameBuffer"),
				Script.AddObject("callback")
			);
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.beginFrameSubscription({1});",
				Script.GetObject(_id),
				Script.GetObject(item.ObjectId)
			);
			_ExecuteJavaScript(script);
		}

		public void beginFrameSubscription(bool onlyDirty, Action<Buffer, Rectangle> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "beginFrameSubscription";
			CallbackItem item = _client.Callbacks.Add(_id, eventName, (object args) => {
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				Buffer frameBuffer = new Buffer(_client, (int)argsList[0]);
				Rectangle dirtyRect = Rectangle.FromObject(argsList[1]);
				callback?.Invoke(frameBuffer, dirtyRect);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (frameBuffer,dirtyRect) => {{",
						"this.emit('__event',{0},{1},{2},{3},dirtyRect);",
					"}};",
					"return {4};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("frameBuffer"),
				Script.AddObject("callback")
			);
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.beginFrameSubscription({1},{2});",
				Script.GetObject(_id),
				onlyDirty.Escape(),
				Script.GetObject(item.ObjectId)
			);
			_ExecuteJavaScript(script);
		}


		/// <summary>
		/// End subscribing for frame presentation events.
		/// </summary>
		public void endFrameSubscription() {
			string script = ScriptBuilder.Build(
				"{0}.endFrameSubscription();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets the item as dragging item for current drag-drop operation.
		/// </summary>
		/// <param name="item"></param>
		public void startDrag(JsonObject item) {
			// TODO: fix item.icon param
			string script = ScriptBuilder.Build(
				"{0}.startDrag({1});",
				Script.GetObject(_id),
				item.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - true if the process of saving page has been initiated successfully.
		/// </summary>
		/// <param name="fullPath"></param>
		/// <param name="saveType"></param>
		/// <param name="callback"></param>
		/// <returns></returns>
		public bool savePage(string fullPath, string saveType, Action<Error> callback) {
			string eventName = "savePage";
			CallbackItem item = _client.Callbacks.Add(_id, eventName, (object args) => {
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				Error error = new Error(_client, (int)argsList[0]);
				callback?.Invoke(error);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (error) => {{",
						"this.emit('__event',{0},{1},{2},{3});",
					"}};",
					"return {4};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("error"),
				Script.AddObject("callback")
			);
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.savePage({1},{2},{3});",
				Script.GetObject(_id),
				fullPath.Escape(),
				saveType.Escape(),
				Script.GetObject(item.ObjectId)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS* 
		/// Shows pop-up dictionary that searches the selected word on the page.
		/// </summary>
		public void showDefinitionForSelection() {
			string script = ScriptBuilder.Build(
				"{0}.showDefinitionForSelection();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Set the size of the page.
		/// This is only supported for &lt;webview&gt; guest contents.
		/// </summary>
		/// <param name="options"></param>
		public void setSize(JsonObject options) {
			string script = ScriptBuilder.Build(
				"return {0}.setSize({1});",
				Script.GetObject(_id),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Indicates whether offscreen rendering is enabled.
		/// </summary>
		/// <returns></returns>
		public bool isOffscreen() {
			string script = ScriptBuilder.Build(
				"return {0}.isOffscreen();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// If offscreen rendering is enabled and not painting, start painting.
		/// </summary>
		public void startPainting() {
			string script = ScriptBuilder.Build(
				"{0}.startPainting();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// If offscreen rendering is enabled and painting, stop painting.
		/// </summary>
		public void stopPainting() {
			string script = ScriptBuilder.Build(
				"{0}.stopPainting();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - If offscreen rendering is enabled returns whether it is currently painting.
		/// </summary>
		/// <returns></returns>
		public bool isPainting() {
			string script = ScriptBuilder.Build(
				"return {0}.isPainting();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// If offscreen rendering is enabled sets the frame rate to the specified number.
		/// Only values between 1 and 60 are accepted.
		/// </summary>
		/// <param name="fps"></param>
		public void setFrameRate(int fps) {
			string script = ScriptBuilder.Build(
				"{0}.setFrameRate({1});",
				Script.GetObject(_id),
				fps
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Integer - If offscreen rendering is enabled returns the current frame rate.
		/// </summary>
		/// <returns></returns>
		public int getFrameRate() {
			string script = ScriptBuilder.Build(
				"return {0}.getFrameRate();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<int>(script);
		}

		/// <summary>
		/// Schedules a full repaint of the window this web contents is in.
		/// <para>
		/// If offscreen rendering is enabled invalidates the frame
		/// and generates a new one through the 'paint' event.
		/// </para>
		/// </summary>
		public void invalidate() {
			string script = ScriptBuilder.Build(
				"{0}.invalidate();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - Returns the WebRTC IP Handling Policy.
		/// </summary>
		/// <returns></returns>
		public string getWebRTCIPHandlingPolicy() {
			string script = ScriptBuilder.Build(
				"return {0}.getWebRTCIPHandlingPolicy();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Setting the WebRTC IP handling policy allows you to control
		/// which IPs are exposed via WebRTC. See BrowserLeaks for more details.
		/// </summary>
		/// <param name="policy"></param>
		public void setWebRTCIPHandlingPolicy(string policy) {
			string script = ScriptBuilder.Build(
				"{0}.setWebRTCIPHandlingPolicy({1});",
				Script.GetObject(_id),
				policy.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Integer - The operating system pid of the associated renderer process.
		/// </summary>
		/// <returns></returns>
		public int getOSProcessId() {
			string script = ScriptBuilder.Build(
				"return {0}.getOSProcessId();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<int>(script);
		}
	}
}
