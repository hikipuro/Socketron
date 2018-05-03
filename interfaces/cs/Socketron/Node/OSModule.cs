using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class OSModule : ElectronBase {
		public int id;

		public OSModule(Socketron socketron) {
			_socketron = socketron;
		}

		public void require() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var module = this.require({0});",
					"return {1};"
				),
				"os".Escape(),
				Script.AddObject("module")
			);
			id = _ExecuteJavaScriptBlocking<int>(script);
		}

		public string EOL {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var os = {0};",
						"return os.EOL;"
					),
					Script.GetObject(id)
				);
				return _ExecuteJavaScriptBlocking<string>(script);
			}
		}

		public string arch() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.arch();"
				),
				Script.GetObject(id)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		public JsonObject constants {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var os = {0};",
						"return os.constants;"
					),
					Script.GetObject(id)
				);
				object result = _ExecuteJavaScriptBlocking<object>(script);
				return new JsonObject(result);
			}
		}

		public object[] cpus() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.cpus();"
				),
				Script.GetObject(id)
			);
			return _ExecuteJavaScriptBlocking<object[]>(script);
		}

		public string endianness() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.endianness();"
				),
				Script.GetObject(id)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		public long freemem() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.freemem();"
				),
				Script.GetObject(id)
			);
			return _ExecuteJavaScriptBlocking<long>(script);
		}

		public string homedir() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.homedir();"
				),
				Script.GetObject(id)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		public string hostname() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.hostname();"
				),
				Script.GetObject(id)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		public object[] loadavg() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.loadavg();"
				),
				Script.GetObject(id)
			);
			return _ExecuteJavaScriptBlocking<object[]>(script);
		}

		public JsonObject networkInterfaces() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.networkInterfaces();"
				),
				Script.GetObject(id)
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return new JsonObject(result);
		}

		public string platform() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.platform();"
				),
				Script.GetObject(id)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		public string release() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.release();"
				),
				Script.GetObject(id)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		public string tmpdir() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.tmpdir();"
				),
				Script.GetObject(id)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		public long totalmem() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.totalmem();"
				),
				Script.GetObject(id)
			);
			return _ExecuteJavaScriptBlocking<long>(script);
		}

		public string type() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.type();"
				),
				Script.GetObject(id)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		public long uptime() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.uptime();"
				),
				Script.GetObject(id)
			);
			return _ExecuteJavaScriptBlocking<long>(script);
		}

		public JsonObject userInfo() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.userInfo();"
				),
				Script.GetObject(id)
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return new JsonObject(result);
		}

		public JsonObject userInfo(JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var os = {0};",
					"return os.userInfo({1});"
				),
				Script.GetObject(id),
				options.Stringify()
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return new JsonObject(result);
		}
	}
}
