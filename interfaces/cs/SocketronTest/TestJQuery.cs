using System;
using System.Diagnostics;
using System.Windows.Forms;
using Socketron;

namespace SocketronTest {
	class TestJQuery : SocketronObject {
		public event Action<string> Log;

		public TestJQuery() {
			_socketron = new Socketron.Socketron();
			//socketron.IsDebug = false;
			_socketron.On("connect", (args) => {
				//Console.WriteLine("Connected");
				Run();
			});
			_socketron.On("debug", (args) => {
#if DEBUG
				Console.WriteLine(args[0]);
#endif
				Log?.Invoke(args[0] as string);
			});
			//socketron.On("data", (args) => {
			//	Packet packet = (Packet)args[0];
			//	Console.WriteLine("Test: {0}, {1}", packet.SequenceId, packet.GetStringData());
			//});
			_socketron.On("aaabbb", (args) => {
				int? value = args[0] as int?;
				Console.WriteLine("event aaabbb: {0}", value);
			});
			_socketron.On("aaabbbccc", (args) => {
				Console.WriteLine("event aaabbbccc: {0}, {1}, {2}", args[0], args[1], args[2]);
			});
			_socketron.On("window-close", (args) => {
				Console.WriteLine("event window-close: {0}", args[0]);
			});
			//try {
				_socketron.Connect("127.0.0.1");
			//} catch (Exception e) {
			//	Console.WriteLine("Errror !!");
			//}

			Init(_socketron);
		}

		public void Close() {
			_socketron.Close();
		}

		void Test() {
			/*
			SocketronData data = new SocketronData();
			data.Type = ProcessType.Browser;
			data.Function = "dialog.showOpenDialog";
			data.Data = "";
			Buffer buffer = Packet.CreateTextData(data);
			socketron.Write(buffer);
			*/

			//string script = "console.log('Test: ' + process.type);";
			//socketron.Main.ExecuteJavaScript(script);

			/*
			socketron.Main.ExecuteJavaScript(new[] {
				"var browserWindow = new electron.BrowserWindow({",
					"title: 'aaa',",
					"useContentSize: true,",
					"show: true",
				"});",
				"return browserWindow.id;"
			}, (result) => {
				int? id = result as int?;
				Console.WriteLine("Window id: {0}", id);

				socketron.Main.ExecuteJavaScript(new[] {
					"var browserWindow = electron.BrowserWindow.fromId(" + id + ");",
					"browserWindow.on('close', () => {",
						"emit('window-close', " + id + ");",
					"});",
					"browserWindow.maximize();"
				});
			});
			*/
			//electron.App.Quit();

			os.require();
			Console.WriteLine("os.EOL: " + os.EOL);

			path.require();
			Console.WriteLine("path.delimiter: " + path.delimiter);
			Console.WriteLine("path.join: " + path.join("a", "b", "ddd"));
			Console.WriteLine("path.win32: " + path.posix);

			console.clear();
			console.count();
			console.time();
			console.log();
			console.log("test");
			console.log("test", 1, 1.2345, true, false, null);
			console.timeEnd();
			console.trace("test");
			var t = setTimeout((args) => {
				Console.WriteLine("Timeout test *****");
				//process.abort();
			}, 1000);
			Console.WriteLine(JSON.Stringify(process.isDefaultApp()));
			//Test2();
			clearTimeout(t);
			var t3 = setImmediate((args) => {
				Console.WriteLine("setImmediate test");
			});
			clearImmediate(t3);
			return;
			var t2 = setInterval((args) => {
				Console.WriteLine("Timeout test *****");
			}, 12000);
			return;


			//var aa = new BrowserWindow.Options();
			//aa.type = BrowserWindow.Types.
			//electron.Dialog.ShowErrorBox("title", "content");
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			BrowserWindow.Options options = new BrowserWindow.Options();
			options.show = false;
			//options.width = 400;
			//options.height = 300;
			//options.backgroundColor = "#aaa";
			//options.opacity = 0.5;
			BrowserWindow window = electron.BrowserWindow.Create(options);
			stopwatch.Stop();
			Log(string.Format("ElapsedMilliseconds: {0}", stopwatch.ElapsedMilliseconds));
			
			window.LoadURL("file:///src/html/index.html");

			window.Once("ready-to-show", (args) => {
				Console.WriteLine("ready-to-show: {0}", args);
				window.Show();
			});
			window.On("close", (args) => {
				Console.WriteLine("close: {0}", args);
			});
			window.webContents.On("did-navigate", (args) => {
				object[] list = args as object[];
				Console.WriteLine("did-navigate");
				if (list != null) {
					foreach (object item in list) {
						Console.WriteLine("\tParams: {0}", item);
					}
				}
			});
			return;

			/*
			MenuItem.Options[] template = new MenuItem.Options[] {
				new MenuItem.Options() {
					label = "Edit",
					submenu = new MenuItem.Options[] {
						new MenuItem.Options() {
							role = "undo"
						},
						new MenuItem.Options() {
							role = "redo"
						}
					}
				}
			};
			Menu menu = Menu.BuildFromTemplate(socketron, template);
			//*/
			string[] template = new[] {
				"[",
					"{",
						"label: 'Edit',",
						"submenu: [",
							"{type: 'checkbox', label: 'Test'},",
							"{role: 'undo'},",
							"{type: 'separator'},",
							"{role: 'redo'}",
						"]",
					"}",
				"]"
			};
			var menuItemOptions = Socketron.MenuItem.Options.ParseArray(string.Join("", template));
			menuItemOptions[0].submenu[0].@checked = true;
			//Console.WriteLine(JSON.Stringify(menuItemOptions, true));
			Socketron.Menu menu = electron.Menu.BuildFromTemplate(menuItemOptions);
			return;

			/*
			string[] template = new[] {
				"[",
					"{",
						"label: 'Edit',",
						"submenu: [",
							"{role: 'undo'},",
							"{type: 'separator'},",
							"{role: 'redo'},",
						"]",
					"}",
				"]"
			};
			Menu menu = Menu.BuildFromTemplate(socketron, string.Join("", template));
			//*/
			electron.Menu.SetApplicationMenu(menu);

			int timer = setTimeout((args) => {
				Console.WriteLine("setTimeout test");
			}, 3000);
			//Node.ClearTimeout(socketron, timer);


			return;

			var windows = electron.BrowserWindow.GetAllWindows();
			foreach (var w in windows) {
				Console.WriteLine("window.id: {0}", w.ID);
				Console.WriteLine("window.getTitle: {0}", w.GetTitle());
			}

			electron.clipboard.WriteText("aaa test");
			Console.WriteLine("Clipboard.ReadText: {0}", electron.clipboard.ReadText());

			/*
			var paths = Dialog.ShowOpenDialog(socketron, new Dialog.OpenDialogOptions {
				properties = new[] {
					Dialog.Properties.openFile
				}
			});
			foreach (var path in paths) {
				Console.WriteLine("OpenDialog: {0}", path);
			}
			//*/

			/*
			GlobalShortcut.Register(socketron, Accelerator.CmdOrCtrl + "+A", (args) => {
				Console.WriteLine("Ctrl + A pressed");
				GlobalShortcut.Unregister(socketron, Accelerator.CmdOrCtrl + "+A");
			});
			//*/
			Console.WriteLine("GetCursorScreenPoint: {0}", electron.screen.GetCursorScreenPoint().Stringify());
			//Console.WriteLine("GetMenuBarHeight: {0}", Screen.GetMenuBarHeight(socketron));
			Console.WriteLine("GetPrimaryDisplay: {0}", electron.screen.GetPrimaryDisplay().Stringify());

			var displayList = electron.screen.GetAllDisplays();
			foreach (var display in displayList) {
				Console.WriteLine("GetAllDisplays: {0}", display.Stringify());
			}
			Console.WriteLine("GetDisplayNearestPoint: {0}", electron.screen.GetDisplayNearestPoint(new Point() { x = -10, y = 0 }).Stringify());
			Console.WriteLine("GetDisplayMatching: {0}", electron.screen.GetDisplayMatching(new Rectangle() { x = -10, y = 0 }).Stringify());

			//Shell.ShowItemInFolder(socketron, "c:/");
			//Shell.OpenExternal(socketron, "http://google.com");
			//Shell.Beep(socketron);

			return;

			Console.WriteLine("IsRegistered: " + electron.globalShortcut.IsRegistered(Accelerator.CmdOrCtrl + "+A"));
			
			Console.WriteLine("Notification.IsSupported: " + electron.Notification.IsSupported());
			var notification = electron.Notification.Create(new Notification.Options {
				title = "Title",
				body = "Body"
			});
			notification.On("show", (args) => {
				Console.WriteLine("Notification show event");
			});
			notification.Show();

			var image = electron.nativeImage.CreateEmpty();
			Console.WriteLine("image.IsEmpty: {0}", image.IsEmpty());
			Console.WriteLine("image.GetSize: {0}", image.GetSize());
			Console.WriteLine("image.GetAspectRatio: {0}", image.GetAspectRatio());
			Console.WriteLine("image.ToDataURL: {0}", image.ToDataURL());
			image.ToPNG();

			return;

			//window.SetFullScreen(true);
			stopwatch.Reset();
			stopwatch.Start();
			string title = window.GetTitle();
			stopwatch.Stop();
			Log(string.Format("ElapsedMilliseconds: {0}", stopwatch.ElapsedMilliseconds));
			Console.WriteLine("test 1: " + title);

			//window.LoadURL("http://google.com");

			Console.WriteLine("GetOSProcessId: " + window.webContents.GetOSProcessId());

			window.LoadURL("file:///src/html/index.html");
			window.Show();
			window.webContents.OpenDevTools();

			window.webContents.SetIgnoreMenuShortcuts(true);

			//Thread.Sleep(1000);
			window.webContents.ExecuteJavaScript("document.write('test')");
			Console.WriteLine("Test");

			return;

			stopwatch.Reset();
			stopwatch.Start();
			title = window.GetTitle();
			stopwatch.Stop();
			Log(string.Format("ElapsedMilliseconds: {0}", stopwatch.ElapsedMilliseconds));

			window.webContents.OpenDevTools();
			Console.WriteLine("IsDevToolsOpened: {0}", window.webContents.IsDevToolsOpened());

			_socketron.On("BrowserWindow.close", (result) => {
				Console.WriteLine("Test close");
			});

			Size size = window.GetSize();
			Console.WriteLine("GetSize: {0}", size.Stringify());

			ulong handle1 = window.GetNativeWindowHandle();
			Console.WriteLine("GetNativeWindowHandle: " + handle1);

			window.SetOpacity(0.755);
			double opacity = window.GetOpacity();
			Console.WriteLine("GetOpacity: " + opacity);

			bool b1 = window.IsFocused();
			Console.WriteLine("test 1: " + b1);

			Rectangle rect1 = window.GetContentBounds();
			string text = rect1.Stringify();
			Console.WriteLine("test 1: " + text);
			Rectangle rect2 = Rectangle.Parse(text);
			Console.WriteLine("test 1: {0}, {1}, {2}, {3}", rect2.x, rect2.y, rect2.width, rect2.height);

			//window.SetEnabled(false);
			window.SetSize(200, 150);
			Rectangle rect = window.GetContentBounds();
			Console.WriteLine("GetContentBounds: {0}, {1}", rect.x, rect.y);
			ulong handle = window.GetNativeWindowHandle();
			Console.WriteLine("GetNativeWindowHandle: {0}", handle);
		}

		public void Run() {
			if (!_socketron.IsConnected) {
				return;
			}
			//socketron.IsDebug = false;

			//socketron.executeJavaScript("location.href='http://google.co.jp/'");
			//var t = await socketron.Main.GetProcessType();
			//Console.WriteLine("Test: " + t);
			//*
			_socketron.Main.ExecuteJavaScript("console.log('TestJQuery')");
			_socketron.Renderer.ExecuteJavaScript("console.log('TestJQuery')");

			//socketron.Main.ExecuteJavaScript("return process.type;", (arg) => {
			//	Console.WriteLine("Test: " + arg);
			//});

			//return;
			/*
			socketron.Main.App.GetAppMemoryInfo((arg) => {
				object[] obj2 = arg as object[];
				foreach (var obj3 in obj2) {
					JsonObject obj = JsonObject.FromObject(obj3);
					Console.WriteLine("GetAppMemoryInfo: " + obj.Stringify());
				}
			});
			*/
			//return;

			/*
			JsonObject args = new JsonObject() {
				["properties"] = JsonObject.Array("openFile", "openDirectory", "multiSelections")
			};
			//args["properties"] = JsonObject.Array("openFile", "openDirectory", "multiSelections");
			socketron.Main.ShowOpenDialog(args,(data) => {
				Console.WriteLine("ShowOpenDialog: {0}", (data as object[])[0]);
			});
			//*/

			Test();
			return;
			/*socketron.Renderer.GetUserAgent((data) => {
				Console.WriteLine("UserAgent: {0}", data);
			});*/
			/*socketron.GetProcessType((data) => {
				Console.WriteLine("ProcessType: {0}", data.Arguments[0]);
			});
			socketron.GetProcessMemoryInfo((data) => {
				Console.WriteLine("ProcessMemoryInfo: {0}", data.Arguments[0]);
			});
			socketron.GetNavigator((data) => {
				Console.WriteLine("Navigator: {0}", data.Arguments[0]);
			});*/
			//socketron.WriteTextData(ProcessType.Browser, "exports.test.testFunc", JsonObject.Array(123, "abc"));
			//return;

			//socketron.Main.App.Quit();
			//socketron.Main.App.GetLocale((result) => {
			//	Console.WriteLine("GetLocale: {0}", result);
			//});
			_socketron.Main.ExecuteJavaScript(new[] {
				"electron.app.on('window-all-closed', () => {",
				"emit('aaabbb', 12345);",
				"});",
				//"return this.process"
			}, (result) => {
				Console.WriteLine("error: {0}", result);
			});
			_socketron.Renderer.ExecuteJavaScript(new[] {
				"emit('aaabbb', 445566);",
				"return window.navigator.userAgent",
			}, (result) => {
				Console.WriteLine("userAgent: {0}", result);
			});
			//string path = (string)await socketron.Main.ExecuteJavaScriptAsync(new[] {
			//	"return electron.app.getAppPath()",
			//});
			//Console.WriteLine("# getAppPath: {0}", path);

			//socketron.WriteTextData(ProcessType.Browser, "exports.test.testFunc2", JsonObject.Array(123, "abc"), (result) => {
			//	Console.WriteLine("error: {0}", result);
			//});
			//return;

			string[] css = {
				"* {",
				"	font-family: sans-serif;",
				"	font-size: 20px;",
				"}",
			};
			_socketron.Renderer.InsertCSS(string.Join("\n", css));

			_socketron.Renderer.InsertJavaScript("https://code.jquery.com/jquery-3.3.1.min.js", (result) => {
				_socketron.Renderer.ExecuteJavaScript("console.log($)", (data2) => {
					Console.WriteLine("Test: console.log($)");
				});
				_socketron.Renderer.ExecuteJavaScript("$(document.body).empty()");
				_socketron.Renderer.ExecuteJavaScript("$(document.body).append('<div>Test</div>')");
				_socketron.Renderer.ExecuteJavaScript("$(document.body).append('<button id=button1>button</button>')");
				_socketron.Renderer.ExecuteJavaScript("$('#button1').click(() => { console.log('click button !'); })");
				_socketron.Renderer.ExecuteJavaScript("$(document.body).append('<button id=button2>button</button>')");

				string[] scriptList = {
				"$('#button1').click(() => {",
				"	emit('aaabbb', 123);",
				"})"
			};
				_socketron.Renderer.ExecuteJavaScript(scriptList);

				scriptList = new[] {
				"$('#button2').click(() => {",
				"	emit('aaabbbccc', '222', true, 111);",
				"})"
 			};
				_socketron.Renderer.ExecuteJavaScript(scriptList);

			});

		}
	}
}
