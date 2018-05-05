using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Render and control web pages.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class WebContents : NodeModule {
		// TODO: add instance properties
		public const string Name = "webContents";
		public int id;
		protected BrowserWindow _window;

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		/// <summary>
		/// WebContents instance events.
		/// </summary>
		public class Events {
			public const string DidFinishLoad = "did-finish-load";
			public const string DidFailLoad = "did-fail-load";
			public const string DidFrameFinishLoad = "did-frame-finish-load";
			public const string DidStartLoading = "did-start-loading";
			public const string DidStopLoading = "did-stop-loading";
			/// <summary>*Deprecated*</summary>
			[Obsolete]
			public const string DidGetResponseDetails = "did-get-response-details";
			/// <summary>*Deprecated*</summary>
			[Obsolete]
			public const string DidGetRedirectRequest = "did-get-redirect-request";
			public const string DomReady = "dom-ready";
			public const string PageFaviconUpdated = "page-favicon-updated";
			public const string NewWindow = "new-window";
			public const string WillNavigate = "will-navigate";
			public const string DidNavigate = "did-navigate";
			public const string DidNavigateInPage = "did-navigate-in-page";
			public const string WillPreventUnload = "will-prevent-unload";
			public const string Crashed = "crashed";
			public const string PluginCrashed = "plugin-crashed";
			public const string Destroyed = "destroyed";
			public const string BeforeInputEvent = "before-input-event";
			public const string DevtoolsOpened = "devtools-opened";
			public const string DevtoolsClosed = "devtools-closed";
			public const string DevtoolsFocused = "devtools-focused";
			public const string CertificateError = "certificate-error";
			public const string SelectClientCertificate = "select-client-certificate";
			public const string Login = "login";
			public const string FoundInPage = "found-in-page";
			public const string MediaStartedPlaying = "media-started-playing";
			public const string MediaPaused = "media-paused";
			public const string UpdateTargetUrl = "update-target-url";
			public const string CursorChanged = "cursor-changed";
			public const string ContextMenu = "context-menu";
			public const string SelectBluetoothDevice = "select-bluetooth-device";
			public const string Paint = "paint";
			public const string DevtoolsReloadPage = "devtools-reload-page";
			public const string WillAttachWebview = "will-attach-webview";
			public const string DidAttachWebview = "did-attach-webview";
			public const string ConsoleMessage = "console-message";
		}

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public WebContents(SocketronClient client) {
			_disposeManually = true;
			_client = client;
		}

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="browserWindow"></param>
		public WebContents(SocketronClient client, BrowserWindow browserWindow) {
			_disposeManually = true;
			_client = client;
			_window = browserWindow;
		}

		public static Callback GetCallbackFromId(ushort id) {
			if (!_callbackList.ContainsKey(id)) {
				return null;
			}
			return _callbackList[id];
		}

		public Session session {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"var session = contents.session;",
						"return {1};"
					),
					_id,
					Script.AddObject("session")
				);
				int result = _ExecuteBlocking<int>(script);
				return new Session(_client) {
					_id = result
				};
			}
		}

		public WebContents hostWebContents {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"var hostWebContents = contents.hostWebContents;",
						"return {1};"
					),
					_id,
					Script.AddObject("hostWebContents")
				);
				int result = _ExecuteBlocking<int>(script);
				return new WebContents(_client) {
					_id = result
				};
			}
		}

		public WebContents devToolsWebContents {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"var devToolsWebContents = contents.devToolsWebContents;",
						"return {1};"
					),
					_id,
					Script.AddObject("devToolsWebContents")
				);
				int result = _ExecuteBlocking<int>(script);
				return new WebContents(_client) {
					_id = result
				};
			}
		}

		public Debugger debugger {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"var debugger = contents.debugger;",
						"return {1};"
					),
					_id,
					Script.AddObject("debugger")
				);
				int result = _ExecuteBlocking<int>(script);
				return new Debugger(_client) {
					_id = result
				};
			}
		}

		public void on(string eventName, Callback callback) {
			if (callback == null) {
				return;
			}
			_callbackList.Add(_callbackListId, callback);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.{0}.fromId({1});",
					"var listener = (e, ...args) => {{",
						"emit.apply(this, ['__event',{2},{3}].concat(args));",
					"}};",
					"this._addClientEventListener({2},{3},listener);",
					"contents.on({4}, listener);"
				),
				Name,
				_id,
				Name.Escape(),
				_callbackListId,
				eventName.Escape()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		public void once(string eventName, Callback callback) {
			if (callback == null) {
				return;
			}
			_callbackList.Add(_callbackListId, callback);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.{0}.fromId({1});",
					"var listener = (e, ...args) => {{",
						"this._removeClientEventListener({2},{3});",
						"emit.apply(this, ['__event',{2},{3}].concat(args));",
					"}};",
					"this._addClientEventListener({2},{3},listener);",
					"contents.once({4}, listener);"
				),
				Name,
				_id,
				Name.Escape(),
				_callbackListId,
				eventName.Escape()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
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
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"contents.loadURL({1});"
					),
					_id,
					url.Escape()
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"contents.loadURL({1},{2});"
					),
					_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.loadFile({1});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.downloadURL({1});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.getURL();"
				),
				_id
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns String - The title of the current web page.
		/// </summary>
		/// <returns></returns>
		public string getTitle() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.getTitle();"
				),
				_id
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the web page is destroyed.
		/// </summary>
		/// <returns></returns>
		public bool isDestroyed() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isDestroyed();"
				),
				_id
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Focuses the web page.
		/// </summary>
		public void focus() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.focus();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the web page is focused.
		/// </summary>
		/// <returns></returns>
		public bool isFocused() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isFocused();"
				),
				_id
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether web page is still loading resources.
		/// </summary>
		/// <returns></returns>
		public bool isLoading() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isLoading();"
				),
				_id
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isLoadingMainFrame();"
				),
				_id
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isWaitingForResponse();"
				),
				_id
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Stops any pending navigation.
		/// </summary>
		public void stop() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.stop();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Reloads the current web page.
		/// </summary>
		public void reload() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.reload();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Reloads current page and ignores cache.
		/// </summary>
		public void reloadIgnoringCache() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.reloadIgnoringCache();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the browser can go back to previous web page.
		/// </summary>
		/// <returns></returns>
		public bool canGoBack() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.canGoBack();"
				),
				_id
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the browser can go forward to next web page.
		/// </summary>
		/// <returns></returns>
		public bool canGoForward() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.canGoForward();"
				),
				_id
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.canGoToOffset({1});"
				),
				_id,
				offset
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Clears the navigation history.
		/// </summary>
		public void clearHistory() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.clearHistory();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Makes the browser go back a web page.
		/// </summary>
		public void goBack() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.goBack();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Makes the browser go forward a web page.
		/// </summary>
		public void goForward() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.goForward();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Navigates browser to the specified absolute web page index.
		/// </summary>
		/// <param name="index"></param>
		public void goToIndex(int index) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.goToIndex({1});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.goToOffset({1});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isCrashed();"
				),
				_id
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Overrides the user agent for this web page.
		/// </summary>
		/// <param name="userAgent"></param>
		public void setUserAgent(string userAgent) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setUserAgent({1});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.getUserAgent();"
				),
				_id
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Injects CSS into the current web page.
		/// </summary>
		/// <param name="css"></param>
		public void insertCSS(string css) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.insertCSS({1});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.executeJavaScript({1});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setIgnoreMenuShortcuts({1});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setAudioMuted({1});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isAudioMuted();"
				),
				_id
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setZoomFactor({1});"
				),
				_id,
				factor
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sends a request to get current zoom factor,
		/// the callback will be called with callback(zoomFactor).
		/// </summary>
		/// <returns></returns>
		/*
		public double getZoomFactor() {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.getZoomFactor();"
			};
			return _ExecuteJavaScriptBlocking<double>(script);
		}
		//*/

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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setZoomLevel({1});"
				),
				_id,
				level
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sends a request to get current zoom level, the callback will be called with callback(zoomLevel).
		/// </summary>
		/// <returns></returns>
		/*
		public double getZoomLevel() {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.getZoomLevel();"
			};
			return _ExecuteJavaScriptBlocking<double>(script);
		}
		//*/

		/// <summary>
		/// Sets the maximum and minimum pinch-to-zoom level.
		/// </summary>
		/// <param name="minimumLevel"></param>
		/// <param name="maximumLevel"></param>
		public void setVisualZoomLevelLimits(double minimumLevel, double maximumLevel) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setVisualZoomLevelLimits({1},{2});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setLayoutZoomLevelLimits({1},{2});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.undo();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command redo in web page.
		/// </summary>
		public void redo() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.redo();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command cut in web page.
		/// </summary>
		public void cut() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.cut();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command copy in web page.
		/// </summary>
		public void copy() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.copy();"
				),
				_id
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.copyImageAt({1},{2});"
				),
				_id, x, y
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command paste in web page.
		/// </summary>
		public void paste() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.paste();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command pasteAndMatchStyle in web page.
		/// </summary>
		public void pasteAndMatchStyle() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.pasteAndMatchStyle();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command delete in web page.
		/// </summary>
		public void delete() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.delete();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command selectAll in web page.
		/// </summary>
		public void selectAll() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.selectAll();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command unselect in web page.
		/// </summary>
		public void unselect() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.unselect();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command replace in web page.
		/// </summary>
		/// <param name="text"></param>
		public void replace(string text) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.replace({1});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.replaceMisspelling({1});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.insertText({1});"
				),
				_id,
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
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"contents.findInPage({1});"
					),
					_id,
					text.Escape()
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"contents.findInPage({1},{2});"
					),
					_id,
					text.Escape(),
					options.Stringify()
				);
			}
			
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Stops any findInPage request for the webContents with the provided action.
		/// </summary>
		/// <param name="action"></param>
		/*
		public void stopFindInPage(string action) {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.stopFindInPage('" + action + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void capturePage(string text) {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.capturePage('" + text + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// Checks if any ServiceWorker is registered and returns a boolean as response to callback.
		/// </summary>
		/// <param name="text"></param>
		/*
		public void hasServiceWorker(string text) {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.hasServiceWorker('" + text + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// Unregisters any ServiceWorker if present and returns a boolean as response
		/// to callback when the JS promise is fulfilled or false when the JS promise is rejected.
		/// </summary>
		/// <param name="text"></param>
		/*
		public void unregisterServiceWorker(string text) {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.unregisterServiceWorker('" + text + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// Get the system printer list.
		/// </summary>
		/*
		public void getPrinters() {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.getPrinters('" + text + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// Prints window's web page.
		/// When silent is set to true, Electron will pick the system's
		/// default printer if deviceName is empty and the default settings for printing.
		/// </summary>
		/*
		public void print() {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.print('" + text + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// Prints window's web page as PDF with Chromium's preview printing custom settings.
		/// </summary>
		/*
		public void printToPDF() {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.printToPDF('" + text + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.addWorkSpace({1});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.removeWorkSpace({1});"
				),
				_id,
				path.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Uses the devToolsWebContents as the target WebContents to show devtools.
		/// </summary>
		/// <param name="devToolsWebContents"></param>
		/*
		public void setDevToolsWebContents(string devToolsWebContents) {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setDevToolsWebContents('" + devToolsWebContents + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

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
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"contents.openDevTools();"
					),
					_id
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"contents.openDevTools({1});"
					),
					_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.closeDevTools();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the devtools is opened.
		/// </summary>
		/// <returns></returns>
		public bool isDevToolsOpened() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isDevToolsOpened();"
				),
				_id
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the devtools view is focused.
		/// </summary>
		/// <returns></returns>
		public bool isDevToolsFocused() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isDevToolsFocused();"
				),
				_id
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Toggles the developer tools.
		/// </summary>
		public void toggleDevTools() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.toggleDevTools();"
				),
				_id
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.inspectElement({1},{2});"
				),
				_id, x, y
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Opens the developer tools for the service worker context.
		/// </summary>
		public void inspectServiceWorker() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.inspectServiceWorker();"
				),
				_id
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
		/*
		public void send(string channel, params object[] args) {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.send('" + channel + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// Enable device emulation with the given parameters.
		/// </summary>
		/// <param name="parameters"></param>
		public void enableDeviceEmulation(JsonObject parameters) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.enableDeviceEmulation({1});"
				),
				_id,
				parameters.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Disable device emulation enabled by webContents.enableDeviceEmulation.
		/// </summary>
		public void disableDeviceEmulation() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.disableDeviceEmulation();"
				),
				_id
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.sendInputEvent({1});"
				),
				_id,
				@event.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Begin subscribing for presentation events and captured frames,
		/// the callback will be called with callback(frameBuffer, dirtyRect)
		/// when there is a presentation event.
		/// </summary>
		/*
		public void beginFrameSubscription(string channel) {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.beginFrameSubscription('" + channel + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// End subscribing for frame presentation events.
		/// </summary>
		public void endFrameSubscription() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.endFrameSubscription();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/*
		public void startDrag() {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.startDrag();"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void savePage(string fullPath, string saveType, string callback) {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.savePage();"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// *macOS* 
		/// Shows pop-up dictionary that searches the selected word on the page.
		/// </summary>
		public void showDefinitionForSelection() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.showDefinitionForSelection();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Set the size of the page.
		/// This is only supported for &lt;webview&gt; guest contents.
		/// </summary>
		/*
		public void setSize() {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setSize();"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/// <summary>
		/// Returns Boolean - Indicates whether offscreen rendering is enabled.
		/// </summary>
		/// <returns></returns>
		public bool isOffscreen() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isOffscreen();"
				),
				_id
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// If offscreen rendering is enabled and not painting, start painting.
		/// </summary>
		public void startPainting() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.startPainting();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// If offscreen rendering is enabled and painting, stop painting.
		/// </summary>
		public void stopPainting() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.stopPainting();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - If offscreen rendering is enabled returns whether it is currently painting.
		/// </summary>
		/// <returns></returns>
		public bool isPainting() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isPainting();"
				),
				_id
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setFrameRate({1});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.getFrameRate();"
				),
				_id
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.invalidate();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - Returns the WebRTC IP Handling Policy.
		/// </summary>
		/// <returns></returns>
		public string getWebRTCIPHandlingPolicy() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.getWebRTCIPHandlingPolicy();"
				),
				_id
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setWebRTCIPHandlingPolicy({1});"
				),
				_id,
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
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.getOSProcessId();"
				),
				_id
			);
			return _ExecuteBlocking<int>(script);
		}
	}
}
