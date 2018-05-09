using System;
using Socketron;

namespace SocketronTest {
	class RendererTest : RendererObject {
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
			Console.WriteLine("fullscreen: " + document.fullscreen);
			Console.WriteLine("bgColor: " + document.bgColor);
			Console.WriteLine("devicePixelRatio: " + window.devicePixelRatio);
			//Console.WriteLine("location: " + window.location);
			document.write("aaa bbb");
			document.onclick = () => {
				Console.WriteLine("onclick");
			};
			//window.alert("test test");
			bool tt = window.confirm("test test");
			Console.WriteLine(tt);

			int id = window.requestAnimationFrame(() => {
				Console.WriteLine("requestAnimationFrame");
			});
			Console.WriteLine(id);
		}
	}
}
