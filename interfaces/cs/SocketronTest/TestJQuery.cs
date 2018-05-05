using System;
using System.Diagnostics;
using Socketron;

namespace SocketronTest {
	class TestJQuery : SocketronObject {
		public event Action<string> Log;

		public TestJQuery() {
			_client = new SocketronClient();
			//socketron.IsDebug = false;
			_client.On("connect", (args) => {
				//Console.WriteLine("Connected");
				Run();
			});
			_client.On("debug", (args) => {
#if DEBUG
				Console.WriteLine(args[0]);
#endif
				Log?.Invoke(args[0] as string);
			});
			//socketron.On("data", (args) => {
			//	Packet packet = (Packet)args[0];
			//	Console.WriteLine("Test: {0}, {1}", packet.SequenceId, packet.GetStringData());
			//});
			_client.On("aaabbb", (args) => {
				int? value = args[0] as int?;
				Console.WriteLine("event aaabbb: {0}", value);
			});
			_client.On("aaabbbccc", (args) => {
				Console.WriteLine("event aaabbbccc: {0}, {1}, {2}", args[0], args[1], args[2]);
			});
			_client.On("window-close", (args) => {
				Console.WriteLine("event window-close: {0}", args[0]);
			});
			try {
				_client.Connect("127.0.0.1");
			} catch (Exception) {
				Console.WriteLine("Connect errror");
				return;
			}

			Init(_client);
		}

		public void Close() {
			_client.Close();
		}

		void Test() {
			var os = require<NodeModules.OS>("os");
			Console.WriteLine(os.cpus().Stringify());

			electron.protocol.uninterceptProtocol("a", (er) => {
				Console.WriteLine("uninterceptProtocol: {0}", er.toString());
			});

			electron.contentTracing.getCategories((str) => {
				Console.WriteLine("getCategories: {0}", str.Stringify());
			});
			electron.contentTracing.getTraceBufferUsage((a, b) => {
				Console.WriteLine("getTraceBufferUsage: {0}, {1}", a, b);
			});

				//electron.app.on(App.Events.Ready, (args) => {
				var image4 = NativeImage.createFromPath("a");
				var appIcon = new Tray(image4);
				var contextMenu = Menu.buildFromTemplate("[" +
					"{label: 'Item1', type: 'radio'}," +
					"{label: 'Item2', type: 'radio'}" +
				"]");
				contextMenu.items[0].@checked = false;
				contextMenu.items[1].@checked = false;
				contextMenu.items[0].click = (_) => {
					Console.WriteLine("on click 0");
					contextMenu.items[0].@checked = true;
				};
				contextMenu.items[1].click = (_) => {
					Console.WriteLine("on click 1");
					contextMenu.items[1].@checked = true;
				};
				appIcon.setContextMenu(contextMenu);
			//});

			return;
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

			var paths = electron.dialog.showOpenDialog(new Dialog.OpenDialogOptions {
				properties = new[] {
					Dialog.Properties.openFile
				}
			}, (a, b) => {
				Console.WriteLine("showOpenDialog: {0}, {1}", JSON.Stringify(a), JSON.Stringify(b));
			});
			//foreach (var path in paths) {
			//	Console.WriteLine("OpenDialog: {0}", path);
			//}
			//*/
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

			/*
			fs.require();
			Console.WriteLine("fs.existsSync: " + fs.existsSync(""));

			os.require();
			Console.WriteLine("os.EOL: " + os.EOL);

			electron.app.getFileIcon(
				null,
				(error, image2) => {
					LocalBuffer buffer = image2.toPNG();
					buffer.Save("image2.png");
				}
			);
			return;
			//*/

			/*
			path.require();
			Console.WriteLine("path.delimiter: " + path.delimiter);
			Console.WriteLine("path.join: " + path.join("a", "b", "ddd"));
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
			//*/

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
			BrowserWindow window = new BrowserWindow(options);
			stopwatch.Stop();
			Log(string.Format("ElapsedMilliseconds: {0}", stopwatch.ElapsedMilliseconds));
			
			window.loadURL("file:///src/html/index.html");

			window.once("ready-to-show", (args) => {
				Console.WriteLine("ready-to-show: {0}", args);
				window.show();

				setTimeout(() => {
					window.capturePage(new Rectangle(100, 80), (image3) => {
						image3.toPNG().Save("image3.png");
					});
				}, 500);
			});
			window.on("close", (args) => {
				Console.WriteLine("close: {0}", args);
			});
			window.webContents.on("did-navigate", (args) => {
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
			Socketron.Menu menu = electron.Menu.buildFromTemplate(menuItemOptions);
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
			electron.Menu.setApplicationMenu(menu);

			int timer = setTimeout(() => {
				Console.WriteLine("setTimeout test");
			}, 3000);
			//Node.ClearTimeout(socketron, timer);


			return;

			var windows = electron.BrowserWindow.getAllWindows();
			foreach (var w in windows) {
				Console.WriteLine("window.id: {0}", w._id);
				Console.WriteLine("window.getTitle: {0}", w.getTitle());
			}

			electron.clipboard.writeText("aaa test");
			Console.WriteLine("Clipboard.ReadText: {0}", electron.clipboard.readText());


			/*
			GlobalShortcut.Register(socketron, Accelerator.CmdOrCtrl + "+A", (args) => {
				Console.WriteLine("Ctrl + A pressed");
				GlobalShortcut.Unregister(socketron, Accelerator.CmdOrCtrl + "+A");
			});
			//*/
			Console.WriteLine("GetCursorScreenPoint: {0}", electron.screen.getCursorScreenPoint().Stringify());
			//Console.WriteLine("GetMenuBarHeight: {0}", Screen.GetMenuBarHeight(socketron));
			Console.WriteLine("GetPrimaryDisplay: {0}", electron.screen.getPrimaryDisplay().Stringify());

			var displayList = electron.screen.getAllDisplays();
			foreach (var display in displayList) {
				Console.WriteLine("GetAllDisplays: {0}", display.Stringify());
			}
			Console.WriteLine("GetDisplayNearestPoint: {0}", electron.screen.getDisplayNearestPoint(new Point() { x = -10, y = 0 }).Stringify());
			Console.WriteLine("GetDisplayMatching: {0}", electron.screen.getDisplayMatching(new Rectangle() { x = -10, y = 0 }).Stringify());

			//Shell.ShowItemInFolder(socketron, "c:/");
			//Shell.OpenExternal(socketron, "http://google.com");
			//Shell.Beep(socketron);

			return;

			Console.WriteLine("IsRegistered: " + electron.globalShortcut.isRegistered(Accelerator.CmdOrCtrl + "+A"));
			
			Console.WriteLine("Notification.IsSupported: " + electron.Notification.isSupported());
			var notification = new Notification(new Notification.Options {
				title = "Title",
				body = "Body"
			});
			notification.on("show", (args) => {
				Console.WriteLine("Notification show event");
			});
			notification.show();

			var image = electron.nativeImage.createEmpty();
			Console.WriteLine("image.IsEmpty: {0}", image.isEmpty());
			Console.WriteLine("image.GetSize: {0}", image.getSize());
			Console.WriteLine("image.GetAspectRatio: {0}", image.getAspectRatio());
			Console.WriteLine("image.ToDataURL: {0}", image.toDataURL());
			image.toPNG();

			return;

			//window.SetFullScreen(true);
			stopwatch.Reset();
			stopwatch.Start();
			string title = window.getTitle();
			stopwatch.Stop();
			Log(string.Format("ElapsedMilliseconds: {0}", stopwatch.ElapsedMilliseconds));
			Console.WriteLine("test 1: " + title);

			//window.LoadURL("http://google.com");

			Console.WriteLine("GetOSProcessId: " + window.webContents.getOSProcessId());

			window.loadURL("file:///src/html/index.html");
			window.show();
			window.webContents.openDevTools();

			window.webContents.setIgnoreMenuShortcuts(true);

			//Thread.Sleep(1000);
			window.webContents.executeJavaScript("document.write('test')");
			Console.WriteLine("Test");

			return;

			stopwatch.Reset();
			stopwatch.Start();
			title = window.getTitle();
			stopwatch.Stop();
			Log(string.Format("ElapsedMilliseconds: {0}", stopwatch.ElapsedMilliseconds));

			window.webContents.openDevTools();
			Console.WriteLine("IsDevToolsOpened: {0}", window.webContents.isDevToolsOpened());

			_client.On("BrowserWindow.close", (result) => {
				Console.WriteLine("Test close");
			});

			Size size = window.getSize();
			Console.WriteLine("GetSize: {0}", size.Stringify());

			ulong handle1 = window.getNativeWindowHandle();
			Console.WriteLine("GetNativeWindowHandle: " + handle1);

			window.setOpacity(0.755);
			double opacity = window.getOpacity();
			Console.WriteLine("GetOpacity: " + opacity);

			bool b1 = window.isFocused();
			Console.WriteLine("test 1: " + b1);
			
			Rectangle rect1 = window.getContentBounds();
			string text = rect1.Stringify();
			Console.WriteLine("test 1: " + text);
			Rectangle rect2 = Rectangle.Parse(text);
			Console.WriteLine("test 1: {0}, {1}, {2}, {3}", rect2.x, rect2.y, rect2.width, rect2.height);

			//window.SetEnabled(false);
			window.setSize(200, 150);
			Rectangle rect = window.getContentBounds();
			Console.WriteLine("GetContentBounds: {0}, {1}", rect.x, rect.y);
			ulong handle = window.getNativeWindowHandle();
			Console.WriteLine("GetNativeWindowHandle: {0}", handle);
		}

		public Socketron.Buffer Test2() {
			var app = electron.app;
			Console.WriteLine(app.isReady());
			Console.WriteLine(electron.systemPreferences.getAccentColor());
			Color c = Color.FromARGB(electron.systemPreferences.getAccentColor());
			Console.WriteLine(c.Stringify());
			var buf1 = Socketron.Buffer.alloc(3, "abcde");
			var buf2 = Socketron.Buffer.alloc(5, "fghijk");
			var buffer = Socketron.Buffer.allocUnsafeSlow(5);
			//buf1[1] = 11;
			console.log(buf1.toJSON().Stringify());
			console.log(buf1.toString());
			//console.log(Socketron.Buffer.isBuffer(buf1), Socketron.Buffer.concat(buf1, buf2), buffer, Socketron.Buffer.byteLength("abcdあ"));

			var buf3 = LocalBuffer.FromRemote(buf1);
			Console.WriteLine(buf3.Stringify());
			return buffer;
		}

		public void Run() {
			if (!_client.IsConnected) {
				return;
			}
			//socketron.IsDebug = false;

			//socketron.executeJavaScript("location.href='http://google.co.jp/'");
			//var t = await socketron.Main.GetProcessType();
			//Console.WriteLine("Test: " + t);
			//*
			_client.Main.ExecuteJavaScript("console.log('TestJQuery')");
			_client.Renderer.ExecuteJavaScript("console.log('TestJQuery')");

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
			//for (var i = 0; i < 10000; i++) {
			//	Test2();
			//}
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
			_client.Main.ExecuteJavaScript(new[] {
				"electron.app.on('window-all-closed', () => {",
				"emit('aaabbb', 12345);",
				"});",
				//"return this.process"
			}, (result) => {
				Console.WriteLine("error: {0}", result);
			});
			_client.Renderer.ExecuteJavaScript(new[] {
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
			_client.Renderer.InsertCSS(string.Join("\n", css));

			_client.Renderer.InsertJavaScript("https://code.jquery.com/jquery-3.3.1.min.js", (result) => {
				_client.Renderer.ExecuteJavaScript("console.log($)", (data2) => {
					Console.WriteLine("Test: console.log($)");
				});
				_client.Renderer.ExecuteJavaScript("$(document.body).empty()");
				_client.Renderer.ExecuteJavaScript("$(document.body).append('<div>Test</div>')");
				_client.Renderer.ExecuteJavaScript("$(document.body).append('<button id=button1>button</button>')");
				_client.Renderer.ExecuteJavaScript("$('#button1').click(() => { console.log('click button !'); })");
				_client.Renderer.ExecuteJavaScript("$(document.body).append('<button id=button2>button</button>')");

				string[] scriptList = {
				"$('#button1').click(() => {",
				"	emit('aaabbb', 123);",
				"})"
			};
				_client.Renderer.ExecuteJavaScript(scriptList);

				scriptList = new[] {
				"$('#button2').click(() => {",
				"	emit('aaabbbccc', '222', true, 111);",
				"})"
 			};
				_client.Renderer.ExecuteJavaScript(scriptList);

			});

		}
	}
}
