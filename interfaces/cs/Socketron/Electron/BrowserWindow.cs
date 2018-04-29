using System;
using System.Collections.Generic;
using System.Threading;

namespace Socketron {
	public class BrowserWindow {
		public const string Name = "BrowserWindow";
		public int ID = 0;
		public WebContents WebContents;
		protected Socketron _socketron;

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

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

		public static BrowserWindow Create(Socketron socketron, BrowserWindowOptions options = null) {
			if (options == null) {
				options = new BrowserWindowOptions();
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

		public void Destroy() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.destroy();"
			};
			_ExecuteJavaScript(script);
		}

		public void Close() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.close();"
			};
			_ExecuteJavaScript(script);
		}

		public void Focus() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.focus();"
			};
			_ExecuteJavaScript(script);
		}

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

		public bool IsDestroyed() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isDestroyed();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void Show() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.show();"
			};
			_ExecuteJavaScript(script);
		}

		public void ShowInactive() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.showInactive();"
			};
			_ExecuteJavaScript(script);
		}

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

		public bool IsModal() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isModal();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void Maximize() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.maximize();"
			};
			_ExecuteJavaScript(script);
		}

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

		public bool IsMaximized() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMaximized();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void Minimize() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.minimize();"
			};
			_ExecuteJavaScript(script);
		}

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

		public bool IsMinimized() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMinimized();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

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

		public bool IsFullScreen() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFullScreen();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void SetSimpleFullScreen(bool flag) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setSimpleFullScreen(" + flag.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void SetAspectRatio(double aspectRatio) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setAspectRatio(" + aspectRatio + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void PreviewFile(string path) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.previewFile(" + path.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void CloseFilePreview() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.closeFilePreview();"
			};
			_ExecuteJavaScript(script);
		}

		public void SetBounds(Rectangle bounds) {
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

		public void SetEnabled(bool enable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setEnabled(" + enable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void SetSize(int width, int height) {
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

		public void SetContentSize(int width, int height) {
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

		public bool IsResizable() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isResizable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void SetMovable(bool movable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMovable(" + movable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void IsMovable(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMovable();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsMovable() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMovable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void SetMinimizable(bool minimizable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMinimizable(" + minimizable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void IsMinimizable(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMinimizable();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsMinimizable() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMinimizable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void SetMaximizable(bool maximizable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMaximizable(" + maximizable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void IsMaximizable(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMaximizable();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsMaximizable() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMaximizable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void SetFullScreenable(bool fullscreenable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setFullScreenable(" + fullscreenable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void IsFullScreenable(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFullScreenable();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsFullScreenable() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFullScreenable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void SetClosable(bool closable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setClosable(" + closable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void IsClosable(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isClosable();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsClosable() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isClosable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void SetAlwaysOnTop(bool flag) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setAlwaysOnTop(" + flag.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void IsAlwaysOnTop(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isAlwaysOnTop();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsAlwaysOnTop() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isAlwaysOnTop();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void MoveTop() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.moveTop();"
			};
			_ExecuteJavaScript(script);
		}

		public void Center() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.center();"
			};
			_ExecuteJavaScript(script);
		}

		public void SetPosition(int x, int y) {
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

		public string GetTitle() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getTitle();"
			};
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		public void SetSheetOffset(double offsetY) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setSheetOffset(" + offsetY + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void FlashFrame(bool flag) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.flashFrame(" + flag.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void SetSkipTaskbar(bool skip) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setSkipTaskbar(" + skip.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

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

		/*
		public void HookWindowMessage() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.hookWindowMessage();"
			};
			ExecuteJavaScript(script);
		}
		//*/

		public void IsWindowMessageHooked(int message, Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isWindowMessageHooked(" + message + ");"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsWindowMessageHooked(int message) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isWindowMessageHooked(" + message + ");"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void UnhookWindowMessage(int message) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.unhookWindowMessage(" + message + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void UnhookAllWindowMessages() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.unhookAllWindowMessages();"
			};
			_ExecuteJavaScript(script);
		}

		public void SetRepresentedFilename(string filename) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setRepresentedFilename(" + filename.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void GetRepresentedFilename(Action<string> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getRepresentedFilename();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke(result as string);
			});
		}

		public string GetRepresentedFilename() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getRepresentedFilename();"
			};
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		public void SetDocumentEdited(bool edited) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setDocumentEdited(" + edited.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void IsDocumentEdited(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isDocumentEdited();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsDocumentEdited() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isDocumentEdited();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void FocusOnWebView() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.focusOnWebView();"
			};
			_ExecuteJavaScript(script);
		}

		public void BlurWebView() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.blurWebView();"
			};
			_ExecuteJavaScript(script);
		}

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

		public void LoadURL(string url) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.loadURL(" + url.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void LoadFile(string filePath) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.loadFile(" + filePath.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void Reload() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.reload();"
			};
			_ExecuteJavaScript(script);
		}

		/*
		public void SetMenu(string menu) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setMenu(" + menu + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

		public void SetProgressBar(double progress) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setProgressBar(" + progress + ");"
			};
			_ExecuteJavaScript(script);
		}

		/*
		public void SetOverlayIcon(string overlay) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setOverlayIcon(" + overlay + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

		public void SetHasShadow(bool hasShadow) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setHasShadow(" + hasShadow.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void HasShadow(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.hasShadow();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool HasShadow() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.hasShadow();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void SetOpacity(double opacity) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setOpacity(" + opacity + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void GetOpacity(Action<double> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getOpacity();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((double)result);
			});
		}

		public double GetOpacity() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getOpacity();"
			};
			return _ExecuteJavaScriptBlocking<double>(script);
		}

		/*
		public void SetThumbarButtons(string buttons) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setThumbarButtons(" + buttons + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

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

		public void SetThumbnailToolTip(string toolTip) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setThumbnailToolTip(" + toolTip.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/*
		public void SetAppDetails(string options) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setAppDetails(" + options + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

		public void ShowDefinitionForSelection() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.showDefinitionForSelection();"
			};
			_ExecuteJavaScript(script);
		}

		/*
		public void setIcon(string icon) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setIcon(" + icon + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

		public void SetAutoHideMenuBar(bool hide) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setAutoHideMenuBar(" + hide.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void IsMenuBarAutoHide(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMenuBarAutoHide();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsMenuBarAutoHide() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMenuBarAutoHide();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void SetMenuBarVisibility(bool visible) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMenuBarVisibility(" + visible.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void IsMenuBarVisible(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMenuBarVisible();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsMenuBarVisible() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMenuBarVisible();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void SetVisibleOnAllWorkspaces(bool visible) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setVisibleOnAllWorkspaces(" + visible.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void IsVisibleOnAllWorkspaces(Action<bool> callback) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isVisibleOnAllWorkspaces();"
			};
			_ExecuteJavaScript(script, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsVisibleOnAllWorkspaces() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isVisibleOnAllWorkspaces();"
			};
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public void SetIgnoreMouseEvents(bool ignore) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setIgnoreMouseEvents(" + ignore.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void SetContentProtection(bool enable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setContentProtection(" + enable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void SetFocusable(bool focusable) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setFocusable(" + focusable.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/*
		public void SetParentWindow(BrowserWindow parent) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setParentWindow(" + parent + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

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

		public void SetAutoHideCursor(bool autoHide) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setAutoHideCursor(" + autoHide.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		public void SelectPreviousTab() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.selectPreviousTab();"
			};
			_ExecuteJavaScript(script);
		}

		public void SelectNextTab() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.selectNextTab();"
			};
			_ExecuteJavaScript(script);
		}

		public void MergeAllWindows() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.mergeAllWindows();"
			};
			_ExecuteJavaScript(script);
		}

		public void MoveTabToNewWindow() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.moveTabToNewWindow();"
			};
			_ExecuteJavaScript(script);
		}

		public void ToggleTabBar() {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.toggleTabBar();"
			};
			_ExecuteJavaScript(script);
		}

		/*
		public void AddTabbedWindow(BrowserWindow browserWindow) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.addTabbedWindow();"
			};
			ExecuteJavaScript(script);
		}
		//*/

		public void SetVibrancy(string type) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setVibrancy(" + type.Escape() + ");"
			};
			_ExecuteJavaScript(script);
		}

		/*
		public void SetTouchBar(string touchBar) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setTouchBar(" + touchBar + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

		/*
		public void SetBrowserView(string browserView) {
			string[] script = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setBrowserView(" + browserView + ");"
			};
			ExecuteJavaScript(script);
		}
		//*/

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
