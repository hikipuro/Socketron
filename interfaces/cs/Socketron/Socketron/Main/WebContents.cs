using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socketron {
	public class WebContents {
		public int ID = 0;
		protected BrowserWindow _window;

		public WebContents(BrowserWindow browserWindow) {
			_window = browserWindow;
		}

		public void OpenDevTools() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.openDevTools();"
			};
			_window.ExecuteJavaScript(script);
		}

		public void CloseDevTools() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"contents.closeDevTools();"
			};
			_window.ExecuteJavaScript(script);
		}

		public bool IsDevToolsOpened() {
			string[] script = new[] {
				"var contents = electron.webContents.fromId(" + ID + ");",
				"return contents.isDevToolsOpened();"
			};
			return _window.ExecuteJavaScriptBlocking<bool>(script);
		}
	}
}
