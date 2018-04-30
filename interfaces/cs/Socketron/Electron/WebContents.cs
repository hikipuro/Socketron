using System.Collections.Generic;

namespace Socketron {
	/// <summary>
	/// Render and control web pages.
	/// <para>Process: Main</para>
	/// </summary>
	public class WebContents {
		public const string Name = "webContents";
		public int ID = 0;
		protected BrowserWindow _window;

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		public WebContents(BrowserWindow browserWindow) {
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
			string[] script = new[] {
				"var contents = electron." + Name + ".fromId(" + ID + ");",
				"var listener = (...args) => {",
				"console.log(['__event'," + Name.Escape() + "," + _callbackListId + "].concat(args.shift()));",
					"emit.apply(this, ['__event'," + Name.Escape() + "," + _callbackListId + "].concat(args.shift()));",
				"};",
				"this._addClientEventListener(" + Name.Escape() + "," + _callbackListId + ",listener);",
				"contents.on(" + eventName.Escape() + ", listener);"
			};
			_callbackListId++;
			_window.ExecuteJavaScript(script);
		}

		public void Once(string eventName, Callback callback) {
			if (callback == null) {
				return;
			}
			_callbackList.Add(_callbackListId, callback);
			string[] script = new[] {
				"var contents = electron." + Name + ".fromId(" + ID + ");",
				"var listener = (...args) => {",
					"this._removeClientEventListener(" + Name.Escape() + "," + _callbackListId + ");",
					"emit.apply(this, ['__event'," + Name.Escape() + "," + _callbackListId + "].concat(args.shift()));",
				"};",
				"this._addClientEventListener(" + Name.Escape() + "," + _callbackListId + ",listener);",
				"contents.once(" + eventName.Escape() + ", listener);"
			};
			_callbackListId++;
			_window.ExecuteJavaScript(script);
		}

		public void LoadURL(string url) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.loadURL(" + url.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public void LoadFile(string filePath) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.loadFile(" + filePath.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public void DownloadURL(string url) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.downloadURL(" + url.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public string GetURL() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.getURL();"
			};
			return _window.ExecuteJavaScriptBlocking<string>(script);
		}

		public string GetTitle() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.getTitle();"
			};
			return _window.ExecuteJavaScriptBlocking<string>(script);
		}

		public bool IsDestroyed() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.isDestroyed();"
			};
			return _window.ExecuteJavaScriptBlocking<bool>(script);
		}

		public void Focus() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.focus();"
			};
			_window.ExecuteJavaScript(script);
		}

		public bool IsFocused() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.isFocused();"
			};
			return _window.ExecuteJavaScriptBlocking<bool>(script);
		}

		public bool IsLoading() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.isLoading();"
			};
			return _window.ExecuteJavaScriptBlocking<bool>(script);
		}

		public bool IsLoadingMainFrame() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.isLoadingMainFrame();"
			};
			return _window.ExecuteJavaScriptBlocking<bool>(script);
		}

		public bool IsWaitingForResponse() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.isWaitingForResponse();"
			};
			return _window.ExecuteJavaScriptBlocking<bool>(script);
		}

		public void Stop() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.stop();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void Reload() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.reload();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void ReloadIgnoringCache() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.reloadIgnoringCache();"
			};
			_window.ExecuteJavaScript(script);
		}

		public bool CanGoBack() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.canGoBack();"
			};
			return _window.ExecuteJavaScriptBlocking<bool>(script);
		}

		public bool CanGoForward() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.canGoForward();"
			};
			return _window.ExecuteJavaScriptBlocking<bool>(script);
		}

		public bool CanGoToOffset(int offset) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.canGoToOffset(" + offset + ");"
			};
			return _window.ExecuteJavaScriptBlocking<bool>(script);
		}

		public void ClearHistory() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.clearHistory();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void GoBack() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.goBack();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void GoForward() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.goForward();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void GoToIndex(int index) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.goToIndex(" + index + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public void GoToOffset(int offset) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.goToOffset(" + offset + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public bool IsCrashed() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.isCrashed();"
			};
			return _window.ExecuteJavaScriptBlocking<bool>(script);
		}

		public void SetUserAgent(string userAgent) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setUserAgent(" + userAgent.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public string GetUserAgent() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.getUserAgent();"
			};
			return _window.ExecuteJavaScriptBlocking<string>(script);
		}

		public void InsertCSS(string css) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.insertCSS(" + css.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public void ExecuteJavaScript(string code) {
			if (code == null) {
				return;
			}
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.executeJavaScript(" + code.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public void SetIgnoreMenuShortcuts(bool ignore) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setIgnoreMenuShortcuts(" + ignore.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public void SetAudioMuted(bool muted) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setAudioMuted(" + muted.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public bool IsAudioMuted() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.isAudioMuted();"
			};
			return _window.ExecuteJavaScriptBlocking<bool>(script);
		}

		public void SetZoomFactor(double factor) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setZoomFactor(" + factor + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		/*
		public double GetZoomFactor() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.getZoomFactor();"
			};
			return _window.ExecuteJavaScriptBlocking<double>(script);
		}
		//*/

		public void SetZoomLevel(double level) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setZoomLevel(" + level + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		/*
		public double GetZoomLevel() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.getZoomLevel();"
			};
			return _window.ExecuteJavaScriptBlocking<double>(script);
		}
		//*/

		public void SetVisualZoomLevelLimits(double minimumLevel, double maximumLevel) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setVisualZoomLevelLimits(" + minimumLevel + "," + maximumLevel + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public void SetLayoutZoomLevelLimits(double minimumLevel, double maximumLevel) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setLayoutZoomLevelLimits(" + minimumLevel + "," + maximumLevel + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public void Undo() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.undo();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void Redo() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.redo();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void Cut() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.cut();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void Copy() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.copy();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void CopyImageAt(int x, int y) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.copyImageAt(" + x + "," + y + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public void Paste() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.paste();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void PasteAndMatchStyle() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.pasteAndMatchStyle();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void Delete() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.delete();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void SelectAll() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.selectAll();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void Unselect() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.unselect();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void Replace(string text) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.replace(" + text.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public void ReplaceMisspelling(string text) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.replaceMisspelling(" + text.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public void InsertText(string text) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.insertText(" + text.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public void FindInPage(string text) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.findInPage(" + text.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		/*
		public void StopFindInPage(string text) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.stopFindInPage('" + text + "');"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void CapturePage(string text) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.capturePage('" + text + "');"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void HasServiceWorker(string text) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.hasServiceWorker('" + text + "');"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void UnregisterServiceWorker(string text) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.unregisterServiceWorker('" + text + "');"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void GetPrinters() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.getPrinters('" + text + "');"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void Print() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.print('" + text + "');"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void PrintToPDF() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.printToPDF('" + text + "');"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		public void AddWorkSpace(string path) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.addWorkSpace(" + path.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public void RemoveWorkSpace(string path) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.removeWorkSpace(" + path.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		/*
		public void SetDevToolsWebContents(string devToolsWebContents) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setDevToolsWebContents('" + devToolsWebContents + "');"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		public void OpenDevTools() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.openDevTools();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void CloseDevTools() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.closeDevTools();"
			};
			_window.ExecuteJavaScript(script);
		}

		public bool IsDevToolsOpened() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.isDevToolsOpened();"
			};
			return _window.ExecuteJavaScriptBlocking<bool>(script);
		}

		public void ToggleDevTools() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.toggleDevTools();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void InspectElement(int x, int y) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.inspectElement(" + x + "," + y + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public void InspectServiceWorker() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.inspectServiceWorker();"
			};
			_window.ExecuteJavaScript(script);
		}

		/*
		public void Send(string channel) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.send('" + channel + "');"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void EnableDeviceEmulation(string channel) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.enableDeviceEmulation('" + channel + "');"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		public void DisableDeviceEmulation() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.disableDeviceEmulation();"
			};
			_window.ExecuteJavaScript(script);
		}

		/*
		public void SendInputEvent(string channel) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.sendInputEvent('" + channel + "');"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void BeginFrameSubscription(string channel) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.beginFrameSubscription('" + channel + "');"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		public void EndFrameSubscription() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.endFrameSubscription();"
			};
			_window.ExecuteJavaScript(script);
		}

		/*
		public void StartDrag() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.startDrag();"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void SavePage(string fullPath, string saveType, string callback) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.savePage();"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		public void ShowDefinitionForSelection() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.showDefinitionForSelection();"
			};
			_window.ExecuteJavaScript(script);
		}

		/*
		public void SetSize() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setSize();"
			};
			_window.ExecuteJavaScript(script);
		}
		//*/

		public bool IsOffscreen() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.isOffscreen();"
			};
			return _window.ExecuteJavaScriptBlocking<bool>(script);
		}

		public void StartPainting() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.startPainting();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void StopPainting() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.stopPainting();"
			};
			_window.ExecuteJavaScript(script);
		}

		public bool IsPainting() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.isPainting();"
			};
			return _window.ExecuteJavaScriptBlocking<bool>(script);
		}

		public void SetFrameRate(int fps) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setFrameRate(" + fps + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public int GetFrameRate() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.getFrameRate();"
			};
			return _window.ExecuteJavaScriptBlocking<int>(script);
		}

		public void Invalidate() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.invalidate();"
			};
			_window.ExecuteJavaScript(script);
		}

		public string GetWebRTCIPHandlingPolicy() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.getWebRTCIPHandlingPolicy();"
			};
			return _window.ExecuteJavaScriptBlocking<string>(script);
		}

		public void SetWebRTCIPHandlingPolicy(string policy) {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.setWebRTCIPHandlingPolicy(" + policy.Escape() + ");"
			};
			_window.ExecuteJavaScript(script);
		}

		public int GetOSProcessId() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.getOSProcessId();"
			};
			return _window.ExecuteJavaScriptBlocking<int>(script);
		}
	}
}
