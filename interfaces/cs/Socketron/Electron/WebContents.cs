using System;
using System.Collections.Generic;

namespace Socketron {
	/// <summary>
	/// Render and control web pages.
	/// <para>Process: Main</para>
	/// </summary>
	public class WebContents : ElectronBase {
		public const string Name = "webContents";
		public int ID = 0;
		protected BrowserWindow _window;

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

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

		public void LoadURL(string url) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.loadURL({1});"
				),
				ID,
				url.Escape()
			);
			_ExecuteJavaScript(script);
		}

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

		/*
		public double GetZoomFactor() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.getZoomFactor();"
			};
			return _ExecuteJavaScriptBlocking<double>(script);
		}
		//*/

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

		/*
		public double GetZoomLevel() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.getZoomLevel();"
			};
			return _ExecuteJavaScriptBlocking<double>(script);
		}
		//*/

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

		public void FindInPage(string text) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.findInPage({1});"
				),
				ID,
				text.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/*
		public void StopFindInPage(string text) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.stopFindInPage('" + text + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void CapturePage(string text) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.capturePage('" + text + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void HasServiceWorker(string text) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.hasServiceWorker('" + text + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void UnregisterServiceWorker(string text) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.unregisterServiceWorker('" + text + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void GetPrinters() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.getPrinters('" + text + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void Print() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.print('" + text + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void PrintToPDF() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.printToPDF('" + text + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

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

		/*
		public void SetDevToolsWebContents(string devToolsWebContents) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setDevToolsWebContents('" + devToolsWebContents + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		public void OpenDevTools() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"contents.openDevTools();"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

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

		/*
		public void Send(string channel) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.send('" + channel + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void EnableDeviceEmulation(string channel) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.enableDeviceEmulation('" + channel + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

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

		/*
		public void SendInputEvent(string channel) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.sendInputEvent('" + channel + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void BeginFrameSubscription(string channel) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.beginFrameSubscription('" + channel + "');"
			};
			_ExecuteJavaScript(script);
		}
		//*/

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
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.startDrag();"
			};
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void SavePage(string fullPath, string saveType, string callback) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.savePage();"
			};
			_ExecuteJavaScript(script);
		}
		//*/

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

		/*
		public void SetSize() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setSize();"
			};
			_ExecuteJavaScript(script);
		}
		//*/

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
