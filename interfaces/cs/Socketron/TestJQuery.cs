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
			socketron.On("return", (args) => {
				string key = args[0] as string;
				string value = args[1] as string;
				Console.WriteLine("Return text: {0} = {1}", key, value);
			});
			socketron.Connect("127.0.0.1");
		}

		public void Run() {
			if (!socketron.IsConnected) {
				return;
			}

			//socketron.executeJavaScript("location.href='http://google.co.jp/'");

			//*
			socketron.Log("TestJQuery");
			//socketron.ShowOpenDialog("{\"properties\": [\"openFile\", \"openDirectory\", \"multiSelections\"]}");

			string[] css = {
				"* {",
				"	font-family: sans-serif;",
				"	font-size: 20px;",
				"}",
			};
			socketron.InsertCSS(string.Join("\n", css));

			socketron.InsertJavaScript("https://code.jquery.com/jquery-3.3.1.min.js", () => {
				socketron.ExecuteJavaScript("console.log($)", () => {
					Console.WriteLine("Test: console.log($)");
				});
				socketron.ExecuteJavaScript("$(document.body).empty()");
				socketron.ExecuteJavaScript("$(document.body).append('<div>Test</div>')");
				socketron.ExecuteJavaScript("$(document.body).append('<button id=button1>button</button>')");
				socketron.ExecuteJavaScript("$('#button1').click(() => { console.log('click button !'); })");

				string[] scriptList = {
					"$('#button1').click(() => {",
					"	Socketron.return('" + socketron.ID + "', 'aaabbb', 123);",
					"})"
				};
				socketron.ExecuteJavaScript(scriptList);

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
