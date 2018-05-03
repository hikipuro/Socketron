using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Socketron {
	/// <summary>
	/// The process object is a global that provides information about,
	/// and control over, the current Node.js process.
	/// As a global, it is always available to Node.js applications without using require().
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class ProcessClass : ElectronBase {
		public ProcessClass(Socketron socketron) {
			_socketron = socketron;
		}

		public void ExecuteJavaScript(string script) {
			_ExecuteJavaScript(script);
		}

		public T ExecuteJavaScriptBlocking<T>(string script) {
			return _ExecuteJavaScriptBlocking<T>(script);
		}

		public void abort() {
			string script = "process.abort();";
			_ExecuteJavaScript(script);
		}

		public string arch {
			get {
				string script = "return process.arch;";
				return _ExecuteJavaScriptBlocking<string>(script);
			}
		}

		public string[] argv {
			get {
				string script = "return process.argv;";
				object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
				return result.Cast<string>().ToArray();
			}
		}

		public string argv0 {
			get {
				string script = "return process.argv0;";
				return _ExecuteJavaScriptBlocking<string>(script);
			}
		}

		public JsonObject channel {
			get {
				string script = "return process.channel;";
				object result = _ExecuteJavaScriptBlocking<object>(script);
				return new JsonObject(result);
			}
		}

		public void chdir(string directory) {
			string script = ScriptBuilder.Build(
				"return process.chdir({0});",
				directory.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public JsonObject config {
			get {
				string script = "return process.config;";
				object result = _ExecuteJavaScriptBlocking<object>(script);
				return new JsonObject(result);
			}
		}

		public bool connected {
			get {
				string script = "return process.connected;";
				return _ExecuteJavaScriptBlocking<bool>(script);
			}
		}

		public JsonObject cpuUsage(JsonObject previousValue = null) {
			object result = null;
			if (previousValue == null) {
				string script = "return process.cpuUsage();";
				result = _ExecuteJavaScriptBlocking<object>(script);
			} else {
				string script = ScriptBuilder.Build(
					"return process.cpuUsage({0});",
					previousValue.Stringify()
				);
				result = _ExecuteJavaScriptBlocking<object>(script);
			}
			return new JsonObject(result);
		}

		public string cwd() {
			string script = "return process.cwd();";
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		public int debugPort {
			get {
				string script = "return process.debugPort;";
				return _ExecuteJavaScriptBlocking<int>(script);
			}
		}

		public void disconnect() {
			string script = "process.disconnect();";
			_ExecuteJavaScript(script);
		}

		public void dlopen(JsonObject module, string filename, string flags = null) {
			throw new NotImplementedException();
		}

		public void emitWarning(string warning, JsonObject options = null) {
			if (options == null) {
				string script = ScriptBuilder.Build(
					"return process.emitWarning({0});",
					warning.Escape()
				);
				_ExecuteJavaScript(script);
			} else {
				string script = ScriptBuilder.Build(
					"return process.emitWarning({0},{1});",
					warning.Escape(),
					options.Stringify()
				);
				_ExecuteJavaScript(script);
			}
		}

		public JsonObject env {
			get {
				string script = "return process.env;";
				object result = _ExecuteJavaScriptBlocking<object>(script);
				return new JsonObject(result);
			}
		}

		public string[] execArgv {
			get {
				string script = "return process.execArgv;";
				object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
				return result.Cast<string>().ToArray();
			}
		}

		public string execPath {
			get {
				string script = "return process.execPath;";
				return _ExecuteJavaScriptBlocking<string>(script);
			}
		}

		public void exit() {
			string script = "process.exit();";
			_ExecuteJavaScript(script);
		}

		public void exit(int code) {
			string script = ScriptBuilder.Build(
				"return process.exit({0});",
				code
			);
			_ExecuteJavaScript(script);
		}

		public int exitCode {
			get {
				string script = "return process.exitCode;";
				return _ExecuteJavaScriptBlocking<int>(script);
			}
		}

		public int getegid() {
			string script = "return process.getegid();";
			return _ExecuteJavaScriptBlocking<int>(script);
		}

		public JsonObject geteuid() {
			string script = "return process.geteuid();";
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return new JsonObject(result);
		}

		public JsonObject getgid() {
			string script = "return process.getgid();";
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return new JsonObject(result);
		}

		public int[] getgroups() {
			string script = "return process.execArgv;";
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			return result.Cast<int>().ToArray();
		}

		public int getuid() {
			string script = "return process.getuid();";
			return _ExecuteJavaScriptBlocking<int>(script);
		}

		public bool hasUncaughtExceptionCaptureCallback() {
			string script = "return process.hasUncaughtExceptionCaptureCallback();";
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		public int[] hrtime() {
			string script = "return process.hrtime();";
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			return result.Cast<int>().ToArray();
		}

		public void initgroups(string user, string extraGroup) {
			string script = ScriptBuilder.Build(
				"process.initgroups({0},{1});",
				user.Escape(),
				extraGroup.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public void kill(int pid) {
			string script = ScriptBuilder.Build(
				"process.kill({0});",
				pid
			);
			_ExecuteJavaScript(script);
		}

		public JsonObject mainModule {
			get {
				string script = "return process.mainModule;";
				object result = _ExecuteJavaScriptBlocking<object>(script);
				return new JsonObject(result);
			}
		}

		public JsonObject memoryUsage() {
			string script = "return process.memoryUsage();";
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return new JsonObject(result);
		}

		public void nextTick() {
			throw new NotImplementedException();
		}

		public bool noDeprecation {
			get {
				string script = "return process.noDeprecation;";
				return _ExecuteJavaScriptBlocking<bool>(script);
			}
		}

		public int pid {
			get {
				string script = "return process.pid;";
				return _ExecuteJavaScriptBlocking<int>(script);
			}
		}

		public string platform {
			get {
				string script = "return process.platform;";
				return _ExecuteJavaScriptBlocking<string>(script);
			}
		}

		public int ppid {
			get {
				string script = "return process.ppid;";
				return _ExecuteJavaScriptBlocking<int>(script);
			}
		}

		public JsonObject release {
			get {
				string script = "return process.release;";
				object result = _ExecuteJavaScriptBlocking<object>(script);
				return new JsonObject(result);
			}
		}

		public void send(JsonObject message) {
			throw new NotImplementedException();
		}

		public void setegid(string id) {
			string script = ScriptBuilder.Build(
				"process.setegid({0});",
				id
			);
			_ExecuteJavaScript(script);
		}

		public void seteuid(string id) {
			string script = ScriptBuilder.Build(
				"process.seteuid({0});",
				id
			);
			_ExecuteJavaScript(script);
		}

		public void setgid(string id) {
			string script = ScriptBuilder.Build(
				"process.setgid({0});",
				id
			);
			_ExecuteJavaScript(script);
		}

		public void setgroups(int[] groups) {
			string script = ScriptBuilder.Build(
				"process.setgroups({0});",
				JSON.Stringify(groups)
			);
			_ExecuteJavaScript(script);
		}

		public void setuid(string id) {
			string script = ScriptBuilder.Build(
				"process.setuid({0});",
				id
			);
			_ExecuteJavaScript(script);
		}

		public void setUncaughtExceptionCaptureCallback() {
			throw new NotImplementedException();
		}

		public object stderr {
			get {
				throw new NotImplementedException();
			}
		}

		public object stdin {
			get {
				throw new NotImplementedException();
			}
		}

		public object stdout {
			get {
				throw new NotImplementedException();
			}
		}

		public bool throwDeprecation {
			get {
				string script = "return process.throwDeprecation;";
				return _ExecuteJavaScriptBlocking<bool>(script);
			}
		}

		public string title {
			get {
				string script = "return process.title;";
				return _ExecuteJavaScriptBlocking<string>(script);
			}
		}

		public bool traceDeprecation {
			get {
				string script = "return process.traceDeprecation;";
				return _ExecuteJavaScriptBlocking<bool>(script);
			}
		}

		public int umask() {
			string script = "return process.umask();";
			return _ExecuteJavaScriptBlocking<int>(script);
		}

		public int umask(int mask) {
			string script = ScriptBuilder.Build(
				"return process.umask({0});",
				mask
			);
			return _ExecuteJavaScriptBlocking<int>(script);
		}

		public int uptime() {
			string script = "return process.uptime();";
			return _ExecuteJavaScriptBlocking<int>(script);
		}

		public string version() {
			string script = "return process.version();";
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		public JsonObject versions {
			get {
				string script = "return process.versions;";
				object result = _ExecuteJavaScriptBlocking<object>(script);
				return new JsonObject(result);
			}
		}
	}
}
