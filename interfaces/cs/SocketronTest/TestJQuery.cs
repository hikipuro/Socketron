using System;
using System.Diagnostics;
using System.Threading;
using Socketron;

namespace SocketronTest {
	class TestJQuery {
		public event Action<string> Log;
		Socketron.Socketron socketron;

		public TestJQuery() {
			socketron = new Socketron.Socketron();
			//socketron.IsDebug = false;
			socketron.On("connect", (args) => {
				//Console.WriteLine("Connected");
				Run();
			});
			socketron.On("debug", (args) => {
				Console.WriteLine(args[0]);
				Log?.Invoke(args[0] as string);
			});
			//socketron.On("data", (args) => {
			//	Packet packet = (Packet)args[0];
			//	Console.WriteLine("Test: {0}, {1}", packet.SequenceId, packet.GetStringData());
			//});
			socketron.On("aaabbb", (args) => {
				int? value = args[0] as int?;
				Console.WriteLine("event aaabbb: {0}", value);
			});
			socketron.On("aaabbbccc", (args) => {
				Console.WriteLine("event aaabbbccc: {0}, {1}, {2}", args[0], args[1], args[2]);
			});
			socketron.On("window-close", (args) => {
				Console.WriteLine("event window-close: {0}", args[0]);
			});
			//try {
				socketron.Connect("127.0.0.1");
			//} catch (Exception e) {
			//	Console.WriteLine("Errror !!");
			//}
		}

		public void Close() {
			socketron.Close();
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
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			BrowserWindowOptions options = new BrowserWindowOptions();
			options.show = false;
			//options.width = 400;
			//options.height = 300;
			//options.backgroundColor = "#aaa";
			//options.opacity = 0.5;
			BrowserWindow window = BrowserWindow.Create(socketron, options);
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
			window.WebContents.On("did-navigate", (args) => {
				object[] list = args as object[];
				Console.WriteLine("did-navigate");
				if (list != null) {
					foreach (object item in list) {
						Console.WriteLine("\tParams: {0}", item);
					}
				}
			});

			var windows = BrowserWindow.GetAllWindows(socketron);
			foreach (var w in windows) {
				Console.WriteLine("window.id: {0}", w.ID);
				Console.WriteLine("window.getTitle: {0}", w.GetTitle());
			}

			Clipboard.WriteText(socketron, "aaa test");
			Console.WriteLine("Clipboard.ReadText: {0}", Clipboard.ReadText(socketron));

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

			Console.WriteLine("Notification.IsSupported: " + Notification.IsSupported(socketron));
			var notification = Notification.Create(socketron, new Notification.Options {
				title = "Title",
				body = "Body"
			});
			notification.On("show", (args) => {
				Console.WriteLine("Notification show event");
			});
			notification.Show();

			return;

			//window.SetFullScreen(true);
			stopwatch.Reset();
			stopwatch.Start();
			string title = window.GetTitle();
			stopwatch.Stop();
			Log(string.Format("ElapsedMilliseconds: {0}", stopwatch.ElapsedMilliseconds));
			Console.WriteLine("test 1: " + title);

			//window.LoadURL("http://google.com");

			Console.WriteLine("GetOSProcessId: " + window.WebContents.GetOSProcessId());

			window.LoadURL("file:///src/html/index.html");
			window.Show();
			window.WebContents.OpenDevTools();

			window.WebContents.SetIgnoreMenuShortcuts(true);

			//Thread.Sleep(1000);
			window.WebContents.ExecuteJavaScript("document.write('test')");
			Console.WriteLine("Test");

			return;

			stopwatch.Reset();
			stopwatch.Start();
			title = window.GetTitle();
			stopwatch.Stop();
			Log(string.Format("ElapsedMilliseconds: {0}", stopwatch.ElapsedMilliseconds));

			window.WebContents.OpenDevTools();
			Console.WriteLine("IsDevToolsOpened: {0}", window.WebContents.IsDevToolsOpened());

			socketron.On("BrowserWindow.close", (result) => {
				Console.WriteLine("Test close");
			});

			window.GetTitle((result) => {
				Console.WriteLine("test 2: " + result);
			});

			int[] size = window.GetSize();
			Console.WriteLine("GetSize: {0}, {1}", size[0], size[1]);

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
			window.GetSize((w, h) => {
				Console.WriteLine("GetSize: {0}, {1}", w, h);
			});
			window.SetSize(200, 150);
			Rectangle rect = window.GetContentBounds();
			Console.WriteLine("GetContentBounds: {0}, {1}", rect.x, rect.y);
			window.GetNativeWindowHandle((handle) => {
				Console.WriteLine("GetNativeWindowHandle: {0}", handle);
			});
		}

		public void Run() {
			if (!socketron.IsConnected) {
				return;
			}
			//socketron.IsDebug = false;

			//socketron.executeJavaScript("location.href='http://google.co.jp/'");
			//var t = await socketron.Main.GetProcessType();
			//Console.WriteLine("Test: " + t);
			//*
			socketron.Main.ExecuteJavaScript("console.log('TestJQuery')");
			socketron.Renderer.ExecuteJavaScript("console.log('TestJQuery')");

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
			socketron.Main.ExecuteJavaScript(new[] {
				"electron.app.on('window-all-closed', () => {",
				"emit('aaabbb', 12345);",
				"});",
				//"return this.process"
			}, (result) => {
				Console.WriteLine("error: {0}", result);
			});
			socketron.Renderer.ExecuteJavaScript(new[] {
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
			socketron.Renderer.InsertCSS(string.Join("\n", css));

			socketron.Renderer.InsertJavaScript("https://code.jquery.com/jquery-3.3.1.min.js", (result) => {
				socketron.Renderer.ExecuteJavaScript("console.log($)", (data2) => {
					Console.WriteLine("Test: console.log($)");
				});
				socketron.Renderer.ExecuteJavaScript("$(document.body).empty()");
				socketron.Renderer.ExecuteJavaScript("$(document.body).append('<div>Test</div>')");
				socketron.Renderer.ExecuteJavaScript("$(document.body).append('<button id=button1>button</button>')");
				socketron.Renderer.ExecuteJavaScript("$('#button1').click(() => { console.log('click button !'); })");
				socketron.Renderer.ExecuteJavaScript("$(document.body).append('<button id=button2>button</button>')");

				string[] scriptList = {
				"$('#button1').click(() => {",
				"	emit('aaabbb', 123);",
				"})"
			};
				socketron.Renderer.ExecuteJavaScript(scriptList);

				scriptList = new[] {
				"$('#button2').click(() => {",
				"	emit('aaabbbccc', '222', true, 111);",
				"})"
 			};
				socketron.Renderer.ExecuteJavaScript(scriptList);

			});

		}
	}
}
