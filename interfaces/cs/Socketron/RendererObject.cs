using Socketron.DOM;
using Socketron.Electron;

namespace Socketron {
	public class RendererObject : DOMModule {
		protected Window window;
		protected Navigator navigator;
		protected Document document;

		public void Init(SocketronClient client, WebContents webContents) {
			_client = client;
			_webContentsId = webContents.id;
			window = GetObject<Window>("window");
			navigator = window.navigator;
			document = window.document;
		}
	}
}
