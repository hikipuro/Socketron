using System;
using System.Diagnostics;
using Socketron;
using Socketron.DOM;

namespace SocketronTest {
	class RendererTest : RendererObject {
		Gamepad gamepad;
		double prev;

		public void Start() {
			document.addEventListener("DOMContentLoaded", (args) => {
				Console.WriteLine("DOMContentLoaded");
			});
			document.addEventListener("ready", (args) => {
				Console.WriteLine("ready");
			});
			Console.WriteLine("innerWidth: " + window.innerWidth);
			Console.WriteLine("userAgent: " + navigator.userAgent);
			Console.WriteLine("characterSet: " + document.characterSet);
			Console.WriteLine("contentType: " + document.contentType);
			Console.WriteLine("documentURI: " + document.documentURI);
			Console.WriteLine("visibilityState: " + document.visibilityState);
			Console.WriteLine("title: " + document.title);
			Console.WriteLine("bgColor: " + document.bgColor);
			Console.WriteLine("devicePixelRatio: " + window.devicePixelRatio);
			//Console.WriteLine("location: " + window.location);

			var body = document.querySelector("body");
			Console.WriteLine("body: " + body.tagName);
			var e1 = document.firstChild;
			Console.WriteLine("body: " + e1.nodeName);
			Console.WriteLine("body: " + e1.nextSibling.nodeType);

			document.write("aaa bbb");
			document.onclick = (a) => {
				Console.WriteLine("onclick");
			};
			//window.alert("test test");
			//bool tt = window.confirm("test test");
			//Console.WriteLine(tt);

			int id = window.requestAnimationFrame(() => {
				Console.WriteLine("requestAnimationFrame");
			});
			Console.WriteLine(id);
			//*/

			var canvas = document.createElement("canvas") as HTMLCanvasElement;
			canvas.id = "test";
			var context = canvas.getContext("2d") as CanvasRenderingContext2D;
			context.fillStyle = "#ff0000";
			context.fillRect(10, 20, 100, 80);
			document.body.appendChild(canvas);

			window.addEventListener("gamepadconnected", (e) => {
				Console.WriteLine("gamepadconnected");
				var gamepads = navigator.getGamepads();
				Console.WriteLine("gamepads: {0}", gamepads.Length);
				foreach (var gamepad in gamepads) {
					if (gamepad == null) {
						continue;
					}
					this.gamepad = gamepad;
					Console.WriteLine(gamepad.id);
				}
				window.requestAnimationFrame(Update);
			});
		}

		protected void Update() {
			//try {
				var gamepads = navigator.getGamepads();
				if (gamepads.Length <= 0) {
					return;
				}
				var gamepad = gamepads[0];
				var buttons = gamepad.buttons;
				if (buttons.Length <= 0) {
					return;
				}
				double value = buttons[0].value;
				if (prev != value) {
					Debug.WriteLine(gamepad.buttons[0].value);
				}
				prev = value;
				//Debug.WriteLine("update");
				window.requestAnimationFrame(Update);

				//foreach (var g in gamepads) {
				//	if (g == null) continue;
				//	g.Dispose();
				//}
			//} catch (Exception) {
			//}
		}
	}
}
