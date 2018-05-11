using System;
using System.Diagnostics;
using Socketron;
using Socketron.Electron;

namespace SocketronTest {
	class TestJQuery : SocketronObject {
		public event Action<string> Log;

		public TestJQuery() {
			API.client = new SocketronClient();
			//socketron.IsDebug = false;
			API.client.On("connect", (args) => {
				//Console.WriteLine("Connected");
				Run();
			});
			API.client.On("debug", (args) => {
#if DEBUG
				Debug.WriteLine(args[0]);
#endif
				Log?.Invoke(args[0] as string);
			});
			//socketron.On("data", (args) => {
			//	Packet packet = (Packet)args[0];
			//	Console.WriteLine("Test: {0}, {1}", packet.SequenceId, packet.GetStringData());
			//});
			API.client.On("aaabbb", (args) => {
				int? value = args[0] as int?;
				Console.WriteLine("event aaabbb: {0}", value);
			});
			API.client.On("aaabbbccc", (args) => {
				Console.WriteLine("event aaabbbccc: {0}, {1}, {2}", args[0], args[1], args[2]);
			});
			API.client.On("window-close", (args) => {
				Console.WriteLine("event window-close: {0}", args[0]);
			});
			try {
				API.client.Connect("127.0.0.1");
			} catch (Exception) {
				Console.WriteLine("Connect errror");
				return;
			}
		}

		public void Close() {
			API.client.Close();
		}

		void Test() {
			console.log("test", 123, true, null);
			console.log(process.cpuUsage(), process.getVersionsChrome());
			console.API.Apply("log", "test", 123, true, null);

			Console.WriteLine("IsRegistered: " + electron.globalShortcut.isRegistered(Accelerator.CmdOrCtrl + "+A"));
			//electron.globalShortcut.register(Accelerator.CmdOrCtrl + "+A", () => {
			//	Console.WriteLine("Ctrl + A pressed");
			//});

			/*
			var b11 = Socketron.Buffer.alloc(1);
			var b2 = Socketron.Buffer.alloc(1);
			var b3 = Socketron.Buffer.alloc(1);
			var b4 = Socketron.Buffer.alloc(1);
			b2.Dispose();
			var b5 = Socketron.Buffer.alloc(1);
			var b6 = Socketron.Buffer.alloc(1);
			b3.Dispose();
			b11.Dispose();
			var b7 = Socketron.Buffer.alloc(1);
			var b8 = Socketron.Buffer.alloc(1);
			var b9 = Socketron.Buffer.alloc(1);
			return;
			//*/

			/*
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
			//*/

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
			var paths = electron.dialog.showOpenDialog(new Dialog.OpenDialogOptions {
				properties = new[] {
					Dialog.Properties.openFile
				}
			}, (a, b) => {
				Console.WriteLine("showOpenDialog: {0}, {1}", JSON.Stringify(a), JSON.Stringify(b));
			});
			//*/

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
			BrowserWindow window = electron.BrowserWindow.Create(options);
			stopwatch.Stop();
			Log?.Invoke(string.Format("ElapsedMilliseconds: {0}", stopwatch.ElapsedMilliseconds));
			
			window.loadURL("file:///src/html/index.html");

			JSCallback callback1 = (args) => {
				Console.WriteLine("ready-to-show:");
				window.show();

				/*
				window.webContents.printToPDF(new WebContents.PrintToPDFOptions(), (er, buf) => {
					console.log("printToPDF", er, buf.length);
					var buf2 = LocalBuffer.From(buf);
					buf2.Save("image3.pdf");
				});
				//*/

				setTimeout(() => {
					Console.WriteLine("setTimeout");
					window.capturePage(new Rectangle(100, 80), (image3) => {
						var buf = image3.toPNG();
						var buf2 = LocalBuffer.From(buf);
						buf2.Save("image3.png");
					});
				}, 500);
			};
			window.once("ready-to-show", callback1);
			//window.removeListener("ready-to-show", callback1);
			
			/*
			window.Execute(ScriptBuilder.Script(
				"self.on('close', (e) => {",
					"console.log('**************** close');",
					"e.preventDefault();",
				"});"
			));
			//*/

			window.on("close", (args) => {
				Console.WriteLine("close:");
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
			var menuItemOptions = MenuItem.Options.ParseArray(string.Join("", template));
			menuItemOptions[0].submenu[0].@checked = true;
			//Console.WriteLine(JSON.Stringify(menuItemOptions, true));
			Menu menu = electron.Menu.buildFromTemplate(menuItemOptions);
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
				Console.WriteLine("window.id: {0}", w.id);
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

			Console.WriteLine("Notification.IsSupported: " + electron.Notification.isSupported());
			var notification = electron.Notification.Create(new Notification.Options {
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

			API.client.On("BrowserWindow.close", (result) => {
				Console.WriteLine("Test close");
			});

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

			var buf3 = LocalBuffer.From(buf1);
			Console.WriteLine(buf3.Stringify());
			return buffer;
		}

		public void Run() {
			if (!API.client.IsConnected) {
				return;
			}
			Init(API.client);
			//socketron.IsDebug = false;

			//socketron.executeJavaScript("location.href='http://google.co.jp/'");
			//var t = await socketron.Main.GetProcessType();
			//Console.WriteLine("Test: " + t);
			//*
			API.client.Main.ExecuteJavaScript("console.log('TestJQuery')");

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

			/*
			JsonObject args = new JsonObject() {
				["properties"] = JsonObject.Array("openFile", "openDirectory", "multiSelections")
			};
			//args["properties"] = JsonObject.Array("openFile", "openDirectory", "multiSelections");
			socketron.Main.ShowOpenDialog(args,(data) => {
				Console.WriteLine("ShowOpenDialog: {0}", (data as object[])[0]);
			});
			//*/

			//Test();
			//for (var i = 0; i < 10000; i++) {
			//	Test2();
			//}

			//API.client.ExecuteJavaScriptBlocking<string>("return Function.prototype.toString(self);");
			Console.WriteLine(electron.screen.getAllDisplays().Stringify());
			//electron.app.dock.setIcon(electron.nativeImage.createEmpty());
			Console.WriteLine(electron.clipboard.availableFormats().Stringify());
			//Console.WriteLine(electron.session.defaultSession.getPreloads().Stringify());
			Console.WriteLine(JSON.Stringify(electron.session.defaultSession.getUserAgent()));
			electron.session.defaultSession.clearAuthCache(new RemoveClientCertificate() { origin = "aa" }, () => {
				Console.WriteLine("clearAuthCache");
			});

			//return;

			BrowserWindow.Options options = new BrowserWindow.Options();
			//options.webPreferences.nodeIntegration = false;
			//options.show = false;
			//options.width = 400;
			//options.height = 300;
			//options.backgroundColor = "#aaa";
			//options.opacity = 0.5;
			options.webPreferences.preload = API.client.RemoteConfig.String("path");
			BrowserWindow window = electron.BrowserWindow.Create(options);
			window.loadURL("file:///src/html/index.html");
			window.webContents.openDevTools();

			var windows = electron.BrowserWindow.getAllWindows();
			foreach (var w in windows) {
				Console.WriteLine("window.id: {0}", w.id);
				Console.WriteLine("window.getTitle: {0}", w.getTitle());
			}
			setTimeout(() => {
				Console.WriteLine(window.isFocused());
				Console.WriteLine(window.isFullScreen());
				Console.WriteLine(window.getBounds().Stringify());
				Console.WriteLine(window.getSize().Stringify());
				//window.setFullScreen(true);
				//window.destroy();
			}, 1000);

			window.webContents.on(WebContents.Events.DomReady, (args) => {
				Console.WriteLine(JSON.Stringify(args));
				return;
				string[] css = {
					"* {",
					"	font-family: sans-serif;",
					"	font-size: 24px;",
					"}",
				};
				window.webContents.insertCSS(string.Join("", css));
				window.webContents.executeJavaScript(
					string.Join("", new string[] {
						"window.nodeRequire = require;",
						"delete window.require;",
						"delete window.exports;",
						"delete window.module;"
					})
				);
				window.webContents.executeJavaScript(
					string.Join("", new string[] {
						"var script = document.createElement('script');",
						"script.src = 'https://code.jquery.com/jquery-3.3.1.min.js';",
						"document.head.appendChild(script);"
					})
				).then((args2) => {
					window.webContents.executeJavaScript("console.log($);");
					window.webContents.executeJavaScript("$(document.body).empty()");
					window.webContents.executeJavaScript("$(document.body).append('<div>Test</div>')");
					window.webContents.executeJavaScript("$(document.body).append('<button id=button1>button</button>')");
					window.webContents.executeJavaScript("$('#button1').click(() => { console.log('click button !'); })");
					window.webContents.executeJavaScript("$(document.body).append('<button id=button2>button</button>')");

					string[] scriptList = {
						"var electron = nodeRequire('electron');",
						"$('#button1').click(() => {",
						"	console.log(electron);",
						"	electron.ipcRenderer.send('_Socketron.quit');",
						"})"
					};
					window.webContents.executeJavaScript(string.Join("", scriptList));

					RendererTest renderer = new RendererTest();
					renderer.Init(API.client, window.webContents);
					renderer.Start();

					//string ua = API.client.Renderer.ExecuteJavaScriptBlocking<string>("return window.navigator.userAgent;");
					//Console.WriteLine(ua);
				});
			});
		}
	}
}
