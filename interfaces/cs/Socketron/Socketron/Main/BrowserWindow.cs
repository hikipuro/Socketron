using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Script.Serialization;

namespace Socketron {

	public class Rectangle {
		public int X;
		public int Y;
		public int Width;
		public int Height;

		class Converter : JavaScriptConverter {
			public override IEnumerable<Type> SupportedTypes {
				get { return new List<Type>() { typeof(Rectangle) }; }
			}

			public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer) {
				var rect = new Rectangle();
				rect.X = GetIntValue(dictionary, "x");
				rect.Y = GetIntValue(dictionary, "y");
				rect.Width = GetIntValue(dictionary, "width");
				rect.Height = GetIntValue(dictionary, "height");
				return rect;
			}

			public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer) {
				var result = new Dictionary<string, object>();
				var rect = obj as Rectangle;
				if (rect == null) {
					return result;
				}
				result["x"] = rect.X;
				result["y"] = rect.Y;
				result["width"] = rect.Width;
				result["height"] = rect.Height;
				return result;
			}

			protected int GetIntValue(IDictionary<string, object> dictionary, string key) {
				if (dictionary.ContainsKey(key) && dictionary[key].GetType() == typeof(int)) {
					return (int)dictionary[key];
				}
				return 0;
			}
		}

		public static Rectangle Parse(string text) {
			var serializer = new JavaScriptSerializer();
			serializer.RegisterConverters(new JavaScriptConverter[] { new Converter() });
			return serializer.Deserialize<Rectangle>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			serializer.RegisterConverters(new JavaScriptConverter[] { new Converter() });
			return serializer.Serialize(this);
		}
	}

	public class BrowserWindow : EventEmitter {
		public int ID = 0;
		public WebContents WebContents;
		protected Socketron _socketron;

		public BrowserWindow() {
		}

		public static void Create(Socketron socketron, Action<BrowserWindow> callback) {
			string[] scriptList = new[] {
				"var browserWindow = new electron.BrowserWindow({",
					"title: 'aaa',",
					"useContentSize: true,",
					"show: true",
				"});",
				"return [browserWindow.id, browserWindow.webContents.id];"
			};
			socketron.Main.ExecuteJavaScript(
				scriptList,
				(result) => {
					object[] list = result as object[];
					int? windowId = list[0] as int?;
					int? contentsId = list[1] as int?;
					if (windowId != null && contentsId != null) {
						BrowserWindow window = new BrowserWindow();
						window.ID = (int)windowId;
						window._socketron = socketron;
						window.WebContents = new WebContents(window);
						window.WebContents.ID = (int)contentsId;
						callback?.Invoke(window);
					} else {
						Console.Error.WriteLine("error");
					}
				}, (result) => {
					Console.Error.WriteLine("error");
				}
			);
		}

		public static void GetAllWindows() {

		}

		public static void GetFocusedWindow() {

		}

		public void ExecuteJavaScript(string script, Callback success = null, Callback error = null) {
			_socketron.Main.ExecuteJavaScript(script, success, error);
		}

		public void ExecuteJavaScript(string[] scriptList, Callback success = null, Callback error = null) {
			_socketron.Main.ExecuteJavaScript(scriptList, success, error);
		}

		public T ExecuteJavaScriptBlocking<T>(string[] scriptList) {
			return _ExecuteJavaScriptBlocking<T>(scriptList);
		}

		public void On(string eventName) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.on('" + eventName + "', () => {" +
					"emit('BrowserWindow.close'," + ID + ");",
				"});"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void Destroy() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.destroy();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void Close() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.close();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void Focus() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.focus();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void Blur() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.blur();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsFocused(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFocused();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsFocused() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFocused();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void IsDestroyed(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isDestroyed();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsDestroyed() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isDestroyed();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void Show() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.show();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void ShowInactive() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.showInactive();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void Hide() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.hide();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsVisible(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isVisible();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsVisible() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isVisible();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void IsModal(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isModal();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsModal() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isModal();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void Maximize() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.maximize();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void Unmaximize() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.unmaximize();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsMaximized(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMaximized();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsMaximized() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMaximized();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void Minimize() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.minimize();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void Restore() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.restore();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsMinimized(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMinimized();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsMinimized() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMinimized();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void SetFullScreen(bool flag) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setFullScreen(" + flag.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsFullScreen(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFullScreen();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsFullScreen() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFullScreen();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void SetSimpleFullScreen(bool flag) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setSimpleFullScreen(" + flag.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void SetAspectRatio(double aspectRatio) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setAspectRatio(" + aspectRatio.ToString() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void PreviewFile(string path) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.previewFile(" + path + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void CloseFilePreview() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.closeFilePreview();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void SetBounds(Rectangle bounds) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setBounds({",
					"x:" + bounds.X + ",",
					"y:" + bounds.Y + ",",
					"width:" + bounds.Width + ",",
					"height:" + bounds.Height,
				"});"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void GetContentBounds(Action<Rectangle> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getContentBounds();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				JsonObject json = new JsonObject(result);
				Rectangle rect = new Rectangle() {
					X = (int)json["x"],
					Y = (int)json["y"],
					Width = (int)json["width"],
					Height = (int)json["height"]
				};
				callback?.Invoke(rect);
			});
		}

		public Rectangle GetContentBounds() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getContentBounds();"
			};
			object result = _ExecuteJavaScriptBlocking<object>(scriptList);
			JsonObject json = new JsonObject(result);
			Rectangle rect = new Rectangle() {
				X = (int)json["x"],
				Y = (int)json["y"],
				Width = (int)json["width"],
				Height = (int)json["height"]
			};
			return rect;
		}

		public void SetEnabled(bool enable) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setEnabled(" + enable.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void SetSize(int width, int height) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setSize(" + width + "," + height + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void GetSize(Action<int, int> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getSize();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				object[] resultList = result as object[];
				int width = (int)resultList[0];
				int height = (int)resultList[1];
				callback?.Invoke(width, height);
			});
		}

		public int[] GetSize() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getSize();"
			};
			object[] result = _ExecuteJavaScriptBlocking<object[]>(scriptList);
			int width = (int)result[0];
			int height = (int)result[1];
			return new int[] { width, height };
		}

		public void SetContentSize(int width, int height) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setContentSize(" + width + "," + height + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void GetContentSize(Action<int, int> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getContentSize();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				object[] resultList = result as object[];
				int width = (int)resultList[0];
				int height = (int)resultList[1];
				callback?.Invoke(width, height);
			});
		}

		public int[] GetContentSize() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getContentSize();"
			};
			object[] result = _ExecuteJavaScriptBlocking<object[]>(scriptList);
			int width = (int)result[0];
			int height = (int)result[1];
			return new int[] { width, height };
		}

		public void SetMinimumSize(int width, int height) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMinimumSize(" + width + "," + height + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void GetMinimumSize(Action<int, int> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getMinimumSize();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				object[] resultList = result as object[];
				int width = (int)resultList[0];
				int height = (int)resultList[1];
				callback?.Invoke(width, height);
			});
		}

		public int[] GetMinimumSize() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getMinimumSize();"
			};
			object[] result = _ExecuteJavaScriptBlocking<object[]>(scriptList);
			int width = (int)result[0];
			int height = (int)result[1];
			return new int[] { width, height };
		}

		public void SetMaximumSize(int width, int height) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMaximumSize(" + width + "," + height + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void GetMaximumSize(Action<int, int> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getMaximumSize();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				object[] resultList = result as object[];
				int width = (int)resultList[0];
				int height = (int)resultList[1];
				callback?.Invoke(width, height);
			});
		}

		public int[] GetMaximumSize() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getMaximumSize();"
			};
			object[] result = _ExecuteJavaScriptBlocking<object[]>(scriptList);
			int width = (int)result[0];
			int height = (int)result[1];
			return new int[] { width, height };
		}

		public void SetResizable(bool resizable) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setResizable(" + resizable.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsResizable(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isResizable();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsResizable() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isResizable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void SetMovable(bool movable) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMovable(" + movable.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsMovable(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMovable();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsMovable() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMovable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void SetMinimizable(bool minimizable) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMinimizable(" + minimizable.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsMinimizable(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMinimizable();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsMinimizable() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMinimizable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void SetMaximizable(bool maximizable) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMaximizable(" + maximizable.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsMaximizable(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMaximizable();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsMaximizable() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMaximizable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void SetFullScreenable(bool fullscreenable) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setFullScreenable(" + fullscreenable.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsFullScreenable(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFullScreenable();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsFullScreenable() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isFullScreenable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void SetClosable(bool closable) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setClosable(" + closable.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsClosable(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isClosable();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsClosable() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isClosable();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void SetAlwaysOnTop(bool flag) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setAlwaysOnTop(" + flag.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsAlwaysOnTop(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isAlwaysOnTop();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsAlwaysOnTop() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isAlwaysOnTop();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void MoveTop() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.moveTop();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void Center() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.center();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void SetPosition(int x, int y) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setPosition(" + x + "," + y + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void GetPosition(Action<int, int> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getPosition();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				object[] resultList = result as object[];
				int x = (int)resultList[0];
				int y = (int)resultList[1];
				callback?.Invoke(x, y);
			});
		}

		public int[] GetPosition() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getPosition();"
			};
			object[] result = _ExecuteJavaScriptBlocking<object[]>(scriptList);
			int x = (int)result[0];
			int y = (int)result[1];
			return new int[] { x, y };
		}

		public void SetTitle(string title) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setTitle(" + title + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void GetTitle(Callback<string> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getTitle();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke(result as string);
			});
		}

		public string GetTitle() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getTitle();"
			};
			return _ExecuteJavaScriptBlocking<string>(scriptList);
		}

		public void SetSheetOffset(double offsetY) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setSheetOffset(" + offsetY + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void FlashFrame(bool flag) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.flashFrame(" + flag.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void SetSkipTaskbar(bool skip) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setSkipTaskbar(" + skip.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void SetKiosk(bool flag) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setKiosk(" + flag.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsKiosk(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isKiosk();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsKiosk() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isKiosk();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void GetNativeWindowHandle(Action<ulong> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getNativeWindowHandle();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
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
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getNativeWindowHandle();"
			};
			object result = _ExecuteJavaScriptBlocking<object>(scriptList);
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
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.hookWindowMessage();"
			};
			ExecuteJavaScript(scriptList);
		}
		//*/

		public void IsWindowMessageHooked(int message, Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isWindowMessageHooked(" + message + ");"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsWindowMessageHooked(int message) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isWindowMessageHooked(" + message + ");"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void UnhookWindowMessage(int message) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.unhookWindowMessage(" + message + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void UnhookAllWindowMessages() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.unhookAllWindowMessages();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void SetRepresentedFilename(string filename) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setRepresentedFilename(" + filename + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void GetRepresentedFilename(Action<string> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getRepresentedFilename();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke(result as string);
			});
		}

		public string GetRepresentedFilename() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getRepresentedFilename();"
			};
			return _ExecuteJavaScriptBlocking<string>(scriptList);
		}

		public void SetDocumentEdited(bool edited) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setDocumentEdited(" + edited.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsDocumentEdited(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isDocumentEdited();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsDocumentEdited() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isDocumentEdited();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void FocusOnWebView() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.focusOnWebView();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void BlurWebView() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.blurWebView();"
			};
			_ExecuteJavaScript(scriptList);
		}

		/*
		public void CapturePage(Rectangle rect, Action callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.capturePage();"
			};
			ExecuteJavaScript(scriptList, (result) => {
			});
		}
		//*/

		public void LoadURL(string url) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.loadURL(" + url + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void LoadFile(string filePath) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.loadFile(" + filePath + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void Reload() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.reload();"
			};
			_ExecuteJavaScript(scriptList);
		}

		/*
		public void SetMenu(string menu) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setMenu(" + menu + ");"
			};
			ExecuteJavaScript(scriptList);
		}
		//*/

		public void SetProgressBar(double progress) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setProgressBar(" + progress + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		/*
		public void SetOverlayIcon(string overlay) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setOverlayIcon(" + overlay + ");"
			};
			ExecuteJavaScript(scriptList);
		}
		//*/

		public void SetHasShadow(bool hasShadow) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setHasShadow(" + hasShadow.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void HasShadow(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.hasShadow();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool HasShadow() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.hasShadow();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void SetOpacity(double opacity) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setOpacity(" + opacity + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void GetOpacity(Action<double> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getOpacity();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((double)result);
			});
		}

		public double GetOpacity() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.getOpacity();"
			};
			return _ExecuteJavaScriptBlocking<double>(scriptList);
		}

		/*
		public void SetThumbarButtons(string buttons) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setThumbarButtons(" + buttons + ");"
			};
			ExecuteJavaScript(scriptList);
		}
		//*/

		public void SetThumbnailClip(Rectangle region) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setThumbnailClip({",
					"x:" + region.X + ",",
					"y:" + region.Y + ",",
					"width:" + region.Width + ",",
					"height:" + region.Height,
				");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void SetThumbnailToolTip(string toolTip) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setThumbnailToolTip(" + toolTip + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		/*
		public void SetAppDetails(string options) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setAppDetails(" + options + ");"
			};
			ExecuteJavaScript(scriptList);
		}
		//*/

		public void ShowDefinitionForSelection() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.showDefinitionForSelection();"
			};
			_ExecuteJavaScript(scriptList);
		}

		/*
		public void setIcon(string icon) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setIcon(" + icon + ");"
			};
			ExecuteJavaScript(scriptList);
		}
		//*/

		public void SetAutoHideMenuBar(bool hide) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setAutoHideMenuBar(" + hide.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsMenuBarAutoHide(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMenuBarAutoHide();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsMenuBarAutoHide() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMenuBarAutoHide();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void SetMenuBarVisibility(bool visible) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setMenuBarVisibility(" + visible.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsMenuBarVisible(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMenuBarVisible();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsMenuBarVisible() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isMenuBarVisible();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void SetVisibleOnAllWorkspaces(bool visible) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setVisibleOnAllWorkspaces(" + visible.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void IsVisibleOnAllWorkspaces(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isVisibleOnAllWorkspaces();"
			};
			_ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}

		public bool IsVisibleOnAllWorkspaces() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"return window.isVisibleOnAllWorkspaces();"
			};
			return _ExecuteJavaScriptBlocking<bool>(scriptList);
		}

		public void SetIgnoreMouseEvents(bool ignore) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setIgnoreMouseEvents(" + ignore.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void SetContentProtection(bool enable) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setContentProtection(" + enable.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void SetFocusable(bool focusable) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setFocusable(" + focusable.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		/*
		public void SetParentWindow(BrowserWindow parent) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setParentWindow(" + parent + ");"
			};
			ExecuteJavaScript(scriptList);
		}
		//*/

		/*
		public void GetParentWindow(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"return window.getParentWindow();"
			};
			ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}
		//*/

		/*
		public void GetChildWindows(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"return window.getChildWindows();"
			};
			ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}
		//*/

		public void SetAutoHideCursor(bool autoHide) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setAutoHideCursor(" + autoHide.ToString().ToLower() + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void SelectPreviousTab() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.selectPreviousTab();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void SelectNextTab() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.selectNextTab();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void MergeAllWindows() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.mergeAllWindows();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void MoveTabToNewWindow() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.moveTabToNewWindow();"
			};
			_ExecuteJavaScript(scriptList);
		}

		public void ToggleTabBar() {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.toggleTabBar();"
			};
			_ExecuteJavaScript(scriptList);
		}

		/*
		public void AddTabbedWindow(BrowserWindow browserWindow) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.addTabbedWindow();"
			};
			ExecuteJavaScript(scriptList);
		}
		//*/

		public void SetVibrancy(string type) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + ID + ");",
				"window.setVibrancy(" + type + ");"
			};
			_ExecuteJavaScript(scriptList);
		}

		/*
		public void SetTouchBar(string touchBar) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setTouchBar(" + touchBar + ");"
			};
			ExecuteJavaScript(scriptList);
		}
		//*/

		/*
		public void SetBrowserView(string browserView) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"window.setBrowserView(" + browserView + ");"
			};
			ExecuteJavaScript(scriptList);
		}
		//*/

		/*
		public void GetBrowserView(Action<bool> callback) {
			string[] scriptList = new[] {
				"var window = electron.BrowserWindow.fromId(" + id + ");",
				"return window.getBrowserView();"
			};
			ExecuteJavaScript(scriptList, (result) => {
				callback?.Invoke((bool)result);
			});
		}
		//*/

		protected void _ExecuteJavaScript(string[] scriptList) {
			_socketron.Main.ExecuteJavaScript(scriptList);
		}

		protected void _ExecuteJavaScript(string[] scriptList, Callback callback) {
			_socketron.Main.ExecuteJavaScript(scriptList, callback);
		}

		protected void _ExecuteJavaScript(string[] scriptList, Callback success, Callback error) {
			_socketron.Main.ExecuteJavaScript(scriptList, success, error);
		}

		protected T _ExecuteJavaScriptBlocking<T>(string[] scriptList) {
			bool done = false;
			T value = default(T);

			_ExecuteJavaScript(scriptList, (result) => {
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
				Thread.Sleep(1);
			}
			return value;
		}
	}
}
