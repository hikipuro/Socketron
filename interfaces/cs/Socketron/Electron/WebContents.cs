using System;
using System.Collections.Generic;

namespace Socketron {
	/// <summary>
	/// Render and control web pages.
	/// <para>Process: Main</para>
	/// </summary>
	public class WebContents : ElectronBase {
		// TODO: add instance properties
		public const string Name = "webContents";
		public int ID = 0;
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

		public WebContents(Socketron socketron) {
			_socketron = socketron;
		}

		public WebContents(Socketron socketron, BrowserWindow browserWindow) {
			_socketron = socketron;
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
					ID,
					Script.AddObject("session")
				);
				int result = _ExecuteJavaScriptBlocking<int>(script);
				return new Session(_socketron) {
					id = result
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
					ID,
					Script.AddObject("hostWebContents")
				);
				int result = _ExecuteJavaScriptBlocking<int>(script);
				return new WebContents(_socketron) {
					ID = result
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
					ID,
					Script.AddObject("devToolsWebContents")
				);
				int result = _ExecuteJavaScriptBlocking<int>(script);
				return new WebContents(_socketron) {
					ID = result
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
					ID,
					Script.AddObject("debugger")
				);
				int result = _ExecuteJavaScriptBlocking<int>(script);
				return new Debugger(_socketron) {
					id = result
				};
			}
		}

		public void On(string eventName, Callback callback) {
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
				ID,
				Name.Escape(),
				_callbackListId,
				eventName.Escape()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		public void Once(string eventName, Callback callback) {
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
				ID,
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
		public void LoadURL(string url, JsonObject options = null) {
			string script = string.Empty;
			if (options == null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"contents.loadURL({1});"
					),
					ID,
					url.Escape()
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"contents.loadURL({1},{2});"
					),
					ID,
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
		public void LoadFile(string filePath) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.loadFile({1});"
				),
				ID,
				filePath.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Initiates a download of the resource at url without navigating.
		/// The will-download event of session will be triggered.
		/// </summary>
		/// <param name="url"></param>
		public void DownloadURL(string url) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.downloadURL({1});"
				),
				ID,
				url.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - The URL of the current web page.
		/// </summary>
		/// <returns></returns>
		public string GetURL() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.getURL();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// Returns String - The title of the current web page.
		/// </summary>
		/// <returns></returns>
		public string GetTitle() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.getTitle();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the web page is destroyed.
		/// </summary>
		/// <returns></returns>
		public bool IsDestroyed() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isDestroyed();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Focuses the web page.
		/// </summary>
		public void Focus() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.focus();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the web page is focused.
		/// </summary>
		/// <returns></returns>
		public bool IsFocused() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isFocused();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether web page is still loading resources.
		/// </summary>
		/// <returns></returns>
		public bool IsLoading() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isLoading();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the main frame
		/// (and not just iframes or frames within it) is still loading.
		/// </summary>
		/// <returns></returns>
		public bool IsLoadingMainFrame() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isLoadingMainFrame();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the web page is waiting for
		/// a first-response from the main resource of the page.
		/// </summary>
		/// <returns></returns>
		public bool IsWaitingForResponse() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isWaitingForResponse();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Stops any pending navigation.
		/// </summary>
		public void Stop() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.stop();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Reloads the current web page.
		/// </summary>
		public void Reload() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.reload();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Reloads current page and ignores cache.
		/// </summary>
		public void ReloadIgnoringCache() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.reloadIgnoringCache();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the browser can go back to previous web page.
		/// </summary>
		/// <returns></returns>
		public bool CanGoBack() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.canGoBack();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the browser can go forward to next web page.
		/// </summary>
		/// <returns></returns>
		public bool CanGoForward() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.canGoForward();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the web page can go to offset.
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		public bool CanGoToOffset(int offset) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.canGoToOffset({1});"
				),
				ID,
				offset
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Clears the navigation history.
		/// </summary>
		public void ClearHistory() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.clearHistory();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Makes the browser go back a web page.
		/// </summary>
		public void GoBack() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.goBack();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Makes the browser go forward a web page.
		/// </summary>
		public void GoForward() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.goForward();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Navigates browser to the specified absolute web page index.
		/// </summary>
		/// <param name="index"></param>
		public void GoToIndex(int index) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.goToIndex({1});"
				),
				ID,
				index
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Navigates to the specified offset from the "current entry".
		/// </summary>
		/// <param name="offset"></param>
		public void GoToOffset(int offset) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.goToOffset({1});"
				),
				ID,
				offset
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the renderer process has crashed.
		/// </summary>
		/// <returns></returns>
		public bool IsCrashed() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isCrashed();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Overrides the user agent for this web page.
		/// </summary>
		/// <param name="userAgent"></param>
		public void SetUserAgent(string userAgent) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setUserAgent({1});"
				),
				ID,
				userAgent.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - The user agent for this web page.
		/// </summary>
		/// <returns></returns>
		public string GetUserAgent() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.getUserAgent();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// Injects CSS into the current web page.
		/// </summary>
		/// <param name="css"></param>
		public void InsertCSS(string css) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.insertCSS({1});"
				),
				ID,
				css.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		public void ExecuteJavaScript(string code) {
			if (code == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.executeJavaScript({1});"
				),
				ID,
				code.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Experimental*
		/// Ignore application menu shortcuts while this web contents is focused.
		/// </summary>
		/// <param name="ignore"></param>
		public void SetIgnoreMenuShortcuts(bool ignore) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setIgnoreMenuShortcuts({1});"
				),
				ID,
				ignore.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Mute the audio on the current web page.
		/// </summary>
		/// <param name="muted"></param>
		public void SetAudioMuted(bool muted) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setAudioMuted({1});"
				),
				ID,
				muted.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether this page has been muted.
		/// </summary>
		/// <returns></returns>
		public bool IsAudioMuted() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isAudioMuted();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Changes the zoom factor to the specified factor.
		/// Zoom factor is zoom percent divided by 100, so 300% = 3.0.
		/// </summary>
		/// <param name="factor"></param>
		public void SetZoomFactor(double factor) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setZoomFactor({1});"
				),
				ID,
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
		public double GetZoomFactor() {
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
		public void SetZoomLevel(double level) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setZoomLevel({1});"
				),
				ID,
				level
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sends a request to get current zoom level, the callback will be called with callback(zoomLevel).
		/// </summary>
		/// <returns></returns>
		/*
		public double GetZoomLevel() {
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
		public void SetVisualZoomLevelLimits(double minimumLevel, double maximumLevel) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setVisualZoomLevelLimits({1},{2});"
				),
				ID,
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
		public void SetLayoutZoomLevelLimits(double minimumLevel, double maximumLevel) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setLayoutZoomLevelLimits({1},{2});"
				),
				ID,
				minimumLevel,
				maximumLevel
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command undo in web page.
		/// </summary>
		public void Undo() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.undo();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command redo in web page.
		/// </summary>
		public void Redo() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.redo();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command cut in web page.
		/// </summary>
		public void Cut() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.cut();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command copy in web page.
		/// </summary>
		public void Copy() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.copy();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Copy the image at the given position to the clipboard.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void CopyImageAt(int x, int y) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.copyImageAt({1},{2});"
				),
				ID, x, y
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command paste in web page.
		/// </summary>
		public void Paste() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.paste();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command pasteAndMatchStyle in web page.
		/// </summary>
		public void PasteAndMatchStyle() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.pasteAndMatchStyle();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command delete in web page.
		/// </summary>
		public void Delete() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.delete();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command selectAll in web page.
		/// </summary>
		public void SelectAll() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.selectAll();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command unselect in web page.
		/// </summary>
		public void Unselect() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.unselect();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command replace in web page.
		/// </summary>
		/// <param name="text"></param>
		public void Replace(string text) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.replace({1});"
				),
				ID,
				text.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Executes the editing command replaceMisspelling in web page.
		/// </summary>
		/// <param name="text"></param>
		public void ReplaceMisspelling(string text) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.replaceMisspelling({1});"
				),
				ID,
				text.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Inserts text to the focused element.
		/// </summary>
		/// <param name="text"></param>
		public void InsertText(string text) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.insertText({1});"
				),
				ID,
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
		public void FindInPage(string text, JsonObject options = null) {
			string script = string.Empty;
			if (options == null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"contents.findInPage({1});"
					),
					ID,
					text.Escape()
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"contents.findInPage({1},{2});"
					),
					ID,
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
		public void StopFindInPage(string action) {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.stopFindInPage('" + action + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void CapturePage(string text) {
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
		public void HasServiceWorker(string text) {
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
		public void UnregisterServiceWorker(string text) {
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
		public void GetPrinters() {
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
		public void Print() {
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
		public void PrintToPDF() {
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
		public void AddWorkSpace(string path) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.addWorkSpace({1});"
				),
				ID,
				path.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Removes the specified path from DevTools workspace.
		/// </summary>
		/// <param name="path"></param>
		public void RemoveWorkSpace(string path) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.removeWorkSpace({1});"
				),
				ID,
				path.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Uses the devToolsWebContents as the target WebContents to show devtools.
		/// </summary>
		/// <param name="devToolsWebContents"></param>
		/*
		public void SetDevToolsWebContents(string devToolsWebContents) {
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
		public void OpenDevTools(JsonObject options = null) {
			string script = string.Empty;
			if (options == null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"contents.openDevTools();"
					),
					ID
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var contents = electron.webContents.fromId({0});",
						"contents.openDevTools({1});"
					),
					ID,
					options.Stringify()
				);
			}
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Closes the devtools.
		/// </summary>
		public void CloseDevTools() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.closeDevTools();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the devtools is opened.
		/// </summary>
		/// <returns></returns>
		public bool IsDevToolsOpened() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isDevToolsOpened();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the devtools view is focused.
		/// </summary>
		/// <returns></returns>
		public bool IsDevToolsFocused() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isDevToolsFocused();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Toggles the developer tools.
		/// </summary>
		public void ToggleDevTools() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.toggleDevTools();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Starts inspecting element at position (x, y).
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void InspectElement(int x, int y) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.inspectElement({1},{2});"
				),
				ID, x, y
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Opens the developer tools for the service worker context.
		/// </summary>
		public void InspectServiceWorker() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.inspectServiceWorker();"
				),
				ID
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
		public void Send(string channel, params object[] args) {
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
		public void EnableDeviceEmulation(JsonObject parameters) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.enableDeviceEmulation({1});"
				),
				ID,
				parameters.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Disable device emulation enabled by webContents.enableDeviceEmulation.
		/// </summary>
		public void DisableDeviceEmulation() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.disableDeviceEmulation();"
				),
				ID
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
		public void SendInputEvent(JsonObject @event) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.sendInputEvent({1});"
				),
				ID,
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
		public void BeginFrameSubscription(string channel) {
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
		public void EndFrameSubscription() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.endFrameSubscription();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/*
		public void StartDrag() {
			// TODO: implement this
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.startDrag();"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void SavePage(string fullPath, string saveType, string callback) {
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
		public void ShowDefinitionForSelection() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.showDefinitionForSelection();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Set the size of the page.
		/// This is only supported for &lt;webview&gt; guest contents.
		/// </summary>
		/*
		public void SetSize() {
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
		public bool IsOffscreen() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isOffscreen();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// If offscreen rendering is enabled and not painting, start painting.
		/// </summary>
		public void StartPainting() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.startPainting();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// If offscreen rendering is enabled and painting, stop painting.
		/// </summary>
		public void StopPainting() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.stopPainting();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - If offscreen rendering is enabled returns whether it is currently painting.
		/// </summary>
		/// <returns></returns>
		public bool IsPainting() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.isPainting();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// If offscreen rendering is enabled sets the frame rate to the specified number.
		/// Only values between 1 and 60 are accepted.
		/// </summary>
		/// <param name="fps"></param>
		public void SetFrameRate(int fps) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setFrameRate({1});"
				),
				ID,
				fps
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Integer - If offscreen rendering is enabled returns the current frame rate.
		/// </summary>
		/// <returns></returns>
		public int GetFrameRate() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.getFrameRate();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<int>(script);
		}

		/// <summary>
		/// Schedules a full repaint of the window this web contents is in.
		/// <para>
		/// If offscreen rendering is enabled invalidates the frame
		/// and generates a new one through the 'paint' event.
		/// </para>
		/// </summary>
		public void Invalidate() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.invalidate();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - Returns the WebRTC IP Handling Policy.
		/// </summary>
		/// <returns></returns>
		public string GetWebRTCIPHandlingPolicy() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.getWebRTCIPHandlingPolicy();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// Setting the WebRTC IP handling policy allows you to control
		/// which IPs are exposed via WebRTC. See BrowserLeaks for more details.
		/// </summary>
		/// <param name="policy"></param>
		public void SetWebRTCIPHandlingPolicy(string policy) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.setWebRTCIPHandlingPolicy({1});"
				),
				ID,
				policy.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Integer - The operating system pid of the associated renderer process.
		/// </summary>
		/// <returns></returns>
		public int GetOSProcessId() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"return contents.getOSProcessId();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<int>(script);
		}
	}
}
