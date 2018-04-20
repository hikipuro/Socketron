using System;

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
				Console.WriteLine("Test 2: {0}, {1}", packet.SequenceId, packet.GetStringData());
			});
			socketron.Connect("127.0.0.1");
		}

		~TestJQuery() {
			socketron.Close();
		}

		public void Run() {
			if (!socketron.IsConnected) {
				return;
			}

			socketron.ImportScript("https://code.jquery.com/jquery-3.3.1.min.js", () => {
				socketron.Run("console.log($)", () => {
					Console.WriteLine("Test: console.log($)");
				});
				socketron.Run("$(document.body).empty()");
				socketron.Run("$(document.body).append('<div>Test</div>')");
				socketron.Run("$(document.body).append('<button>button</button>')");
				string[] script = {
					"var _navigator = { };",
					"for (var i in navigator) _navigator[i] = navigator[i]",
					"Socketron.broadcast(JSON.stringify(_navigator) + '\\n');"
				};
				//socketron.Run(string.Join("\n", script));
			});

		}
	}
}
