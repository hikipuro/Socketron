using Socketron.DOM;
using Socketron.Electron;

namespace Socketron {
	public class RendererObject : Window {
		public void Init(SocketronClient client, WebContents webContents) {
			API.client = client;
			(API as SocketronDOMAPI).webContentsId = webContents.id;
			API.id = _GetWindowObjectId();
		}

		private int _GetWindowObjectId() {
			string script = ScriptBuilder.Build(
				"return {0};",
				Script.AddObject("window")
			);
			return API._ExecuteBlocking<int>(script);
		}
	}
}
