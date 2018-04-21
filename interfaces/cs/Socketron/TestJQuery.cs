﻿using System;

namespace Socketron {
	class TestJQuery {
		Socketron socketron;

		public TestJQuery() {
			socketron = new Socketron();
			socketron.On("connect", (args) => {
				Console.WriteLine("Connected");
				Run();
			});
			socketron.On("debug", (args) => {
				Console.WriteLine(args[0]);
			});
			socketron.On("data", (args) => {
				Packet packet = (Packet)args[0];
				Console.WriteLine("Test: {0}, {1}", packet.SequenceId, packet.GetStringData());
			});
			socketron.Connect("127.0.0.1");
		}

		public void Run() {
			if (!socketron.IsConnected) {
				return;
			}

			//socketron.Run("location.href='http://google.co.jp/'");

			//*
			socketron.Log("TestJQuery");

			string[] css = {
				"* {",
				"	font-family: sans-serif;",
				"	font-size: 20px;",
				"}",
			};
			socketron.AppendStyle(string.Join("\n", css));

			socketron.ImportScript("https://code.jquery.com/jquery-3.3.1.min.js", () => {
				socketron.Run("console.log($)", () => {
					Console.WriteLine("Test: console.log($)");
				});
				socketron.Run("$(document.body).empty()");
				socketron.Run("$(document.body).append('<div>Test</div>')");
				socketron.Run("$(document.body).append('<button id=button1>button</button>')");
				socketron.Run("$('#button1').click(() => { console.log('click button !'); })");

				string[] scriptList = {
					"$('#button1').click(() => {",
					"	Socketron.return('" + socketron.ID + "', 'aaabbb');",
					"})"
				};
				string script = string.Join("\n", scriptList);
				Console.WriteLine(script);
				socketron.Run(script);

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
