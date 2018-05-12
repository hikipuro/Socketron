namespace Socketron.DOM {
	public class DOMModule : JSObject {
		public class SocketronDOMAPI: SocketronAPI {
			public int webContentsId;

			public override void ExecuteJavaScript(string script) {
				if (client.LocalConfig.IsDebug) {
					script = script + "\n/* " + GetDebugInfo() + " */";
				}
				client.Renderer.ExecuteJavaScript(webContentsId, script);
			}

			public override void ExecuteJavaScript(string script, Callback success) {
				client.Renderer.ExecuteJavaScript(webContentsId, script, success);
			}

			public override void ExecuteJavaScript(string script, Callback success, Callback error) {
				client.Renderer.ExecuteJavaScript(webContentsId, script, success, error);
			}

			public override T _ExecuteBlocking<T>(string script) {
				if (client.LocalConfig.IsDebug) {
					script = script + "\n/* " + GetDebugInfo() + " */";
				}
				return client.Renderer.ExecuteJavaScriptBlocking<T>(webContentsId, script);
			}

			public override int CacheScript(string script) {
				return client.Renderer.CacheScript(webContentsId, script);
			}

			public override T ExecuteCachedScript<T>(int script) {
				return client.Renderer.ExecuteCachedScript<T>(webContentsId, script);
			}

			public override T CreateObject<T>(int id) {
				T obj = new T();
				obj.API.client = client;
				obj.API.id = id;
				(obj.API as SocketronDOMAPI).webContentsId = webContentsId;
				return obj;
			}
		}

		public DOMModule() {
			API = new SocketronDOMAPI();
		}
	}
}
