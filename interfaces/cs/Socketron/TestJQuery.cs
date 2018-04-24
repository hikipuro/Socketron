using System;

namespace Socketron {
	class TestJQuery {
		Socketron socketron;

		public TestJQuery() {
			socketron = new Socketron();
			socketron.On("connect", (args) => {
				//Console.WriteLine("Connected");
				Run();
			});
			socketron.On("debug", (args) => {
				Console.WriteLine(args[0]);
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
			socketron.Connect("127.0.0.1");
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

			string script = "console.log('Test: ' + process.type);";
			socketron.Main.ExecuteJavaScript(script);
		}

		public async void Run() {
			if (!socketron.IsConnected) {
				return;
			}

			//socketron.executeJavaScript("location.href='http://google.co.jp/'");
			//var t = await socketron.Main.GetProcessType();
			//Console.WriteLine("Test: " + t);
			//*
			socketron.Renderer.Log("TestJQuery", (data) => {
				Console.WriteLine("TestJQuery Callback");
			});

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

			//Test();
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
				"})"
			});
			socketron.Renderer.ExecuteJavaScript(new[] {
				"emit('aaabbb', 445566);",
				"return window.navigator.userAgent",
			}, (result) => {
				Console.WriteLine("userAgent: {0}", result);
			});

			string[] css = {
				"* {",
				"	font-family: sans-serif;",
				"	font-size: 20px;",
				"}",
			};
			socketron.Renderer.InsertCSS(string.Join("\n", css));

			socketron.Renderer.InsertJavaScript("https://code.jquery.com/jquery-3.3.1.min.js", (data) => {
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

				//string[] script = {
				//	"var _navigator = { };",
				//	"for (var i in navigator) _navigator[i] = navigator[i]",
				//	"Socketron.broadcast(JSON.stringify(_navigator) + '\\n');"
				//};
				//socketron.Run(string.Join("\n", script));
			});
			//*/

		}
	}
}
