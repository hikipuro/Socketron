namespace Socketron.DOM {
	public class DOMModule : JSModule {
		public int _webContentsId;

		protected override void _ExecuteJavaScript(string script) {
			if (_client.LocalConfig.IsDebug) {
				script = script + "\n/* " + _GetDebugInfo() + " */";
			}
			_client.Renderer.ExecuteJavaScript(_webContentsId, script);
		}

		protected override void _ExecuteJavaScript(string script, Callback success) {
			_client.Renderer.ExecuteJavaScript(_webContentsId, script, success);
		}

		protected override void _ExecuteJavaScript(string script, Callback success, Callback error) {
			_client.Renderer.ExecuteJavaScript(_webContentsId, script, success, error);
		}

		protected override T _ExecuteBlocking<T>(string script) {
			if (_client.LocalConfig.IsDebug) {
				script = script + "\n/* " + _GetDebugInfo() + " */";
			}
			return _client.Renderer.ExecuteJavaScriptBlocking<T>(_webContentsId, script);
		}

		public T GetObject<T>(string moduleName) where T : DOMModule, new() {
			if (moduleName == null) {
				return null;
			}
			string script = string.Empty;
			if (_id <= 0) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return {0};"
					),
					Script.AddObject(moduleName)
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var m = {0}.{1};",
						"return {2};"
					),
					Script.GetObject(_id),
					moduleName,
					Script.AddObject("m")
				);
			}
			int result = _ExecuteBlocking<int>(script);
			T module = new T() {
				_id = result,
				_webContentsId = _webContentsId
			};
			return module;
		}
	}
}
