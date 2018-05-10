using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	public partial class NodeModules {
		/// <summary>
		/// Process module of the Node API.
		/// <para>
		/// The process object is a global that provides information about,
		/// and control over, the current Node.js process.
		/// As a global, it is always available to Node.js applications without using require().
		/// </para>
		/// </summary>
		[type: SuppressMessage("Style", "IDE1006")]
		public class Process : JSModule {
			/// <summary>
			/// This constructor is used for internally by the library.
			/// </summary>
			public Process() {
				API.client = SocketronClient.GetCurrent();
			}
			
			public void abort() {
				string script = "process.abort();";
				API.ExecuteJavaScript(script);
			}

			public string arch {
				get {
					string script = "return process.arch;";
					return API._ExecuteBlocking<string>(script);
				}
			}

			public string[] argv {
				get {
					string script = "return process.argv;";
					object[] result = API._ExecuteBlocking<object[]>(script);
					return Array.ConvertAll(
						result,
						value => Convert.ToString(value)
					);
				}
			}

			public string argv0 {
				get {
					string script = "return process.argv0;";
					return API._ExecuteBlocking<string>(script);
				}
			}

			public JsonObject channel {
				get {
					string script = "return process.channel;";
					object result = API._ExecuteBlocking<object>(script);
					return new JsonObject(result);
				}
			}

			public void chdir(string directory) {
				string script = ScriptBuilder.Build(
					"return process.chdir({0});",
					directory.Escape()
				);
				API.ExecuteJavaScript(script);
			}

			public JsonObject config {
				get {
					string script = "return process.config;";
					object result = API._ExecuteBlocking<object>(script);
					return new JsonObject(result);
				}
			}

			public bool connected {
				get {
					string script = ScriptBuilder.Build(
						"return {0}.connected;",
						Script.GetObject(API.id)
					);
					return API._ExecuteBlocking<bool>(script);
				}
			}

			public JsonObject cpuUsage(JsonObject previousValue = null) {
				object result = null;
				if (previousValue == null) {
					string script = ScriptBuilder.Build(
						"return {0}.cpuUsage();",
						Script.GetObject(API.id)
					);
					result = API._ExecuteBlocking<object>(script);
				} else {
					string script = ScriptBuilder.Build(
						"return {0}.cpuUsage({1});",
						Script.GetObject(API.id),
						previousValue.Stringify()
					);
					result = API._ExecuteBlocking<object>(script);
				}
				return new JsonObject(result);
			}

			public string cwd() {
				string script = "return process.cwd();";
				return API._ExecuteBlocking<string>(script);
			}

			public int debugPort {
				get {
					string script = "return process.debugPort;";
					return API._ExecuteBlocking<int>(script);
				}
			}

			public void disconnect() {
				string script = "process.disconnect();";
				API.ExecuteJavaScript(script);
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
					API.ExecuteJavaScript(script);
				} else {
					string script = ScriptBuilder.Build(
						"return process.emitWarning({0},{1});",
						warning.Escape(),
						options.Stringify()
					);
					API.ExecuteJavaScript(script);
				}
			}

			public JsonObject env {
				get {
					string script = "return process.env;";
					object result = API._ExecuteBlocking<object>(script);
					return new JsonObject(result);
				}
			}

			public string[] execArgv {
				get {
					string script = "return process.execArgv;";
					object[] result = API._ExecuteBlocking<object[]>(script);
					return Array.ConvertAll(
						result,
						value => Convert.ToString(value)
					);
				}
			}

			public string execPath {
				get {
					string script = "return process.execPath;";
					return API._ExecuteBlocking<string>(script);
				}
			}

			public void exit() {
				string script = "process.exit();";
				API.ExecuteJavaScript(script);
			}

			public void exit(int code) {
				string script = ScriptBuilder.Build(
					"return process.exit({0});",
					code
				);
				API.ExecuteJavaScript(script);
			}

			public int exitCode {
				get {
					string script = "return process.exitCode;";
					return API._ExecuteBlocking<int>(script);
				}
			}

			public int getegid() {
				string script = "return process.getegid();";
				return API._ExecuteBlocking<int>(script);
			}

			public JsonObject geteuid() {
				string script = "return process.geteuid();";
				object result = API._ExecuteBlocking<object>(script);
				return new JsonObject(result);
			}

			public JsonObject getgid() {
				string script = "return process.getgid();";
				object result = API._ExecuteBlocking<object>(script);
				return new JsonObject(result);
			}

			public int[] getgroups() {
				string script = "return process.execArgv;";
				object[] result = API._ExecuteBlocking<object[]>(script);
				return Array.ConvertAll(
					result,
					value => Convert.ToInt32(value)
				);
			}

			public int getuid() {
				string script = "return process.getuid();";
				return API._ExecuteBlocking<int>(script);
			}

			public bool hasUncaughtExceptionCaptureCallback() {
				string script = "return process.hasUncaughtExceptionCaptureCallback();";
				return API._ExecuteBlocking<bool>(script);
			}

			public int[] hrtime() {
				string script = "return process.hrtime();";
				object[] result = API._ExecuteBlocking<object[]>(script);
				return Array.ConvertAll(
					result,
					value => Convert.ToInt32(value)
				);
			}

			public void initgroups(string user, string extraGroup) {
				string script = ScriptBuilder.Build(
					"process.initgroups({0},{1});",
					user.Escape(),
					extraGroup.Escape()
				);
				API.ExecuteJavaScript(script);
			}

			public void kill(int pid) {
				string script = ScriptBuilder.Build(
					"process.kill({0});",
					pid
				);
				API.ExecuteJavaScript(script);
			}

			public JsonObject mainModule {
				get {
					string script = "return process.mainModule;";
					object result = API._ExecuteBlocking<object>(script);
					return new JsonObject(result);
				}
			}

			public JsonObject memoryUsage() {
				string script = "return process.memoryUsage();";
				object result = API._ExecuteBlocking<object>(script);
				return new JsonObject(result);
			}

			public void nextTick() {
				throw new NotImplementedException();
			}

			public bool noDeprecation {
				get {
					string script = "return process.noDeprecation;";
					return API._ExecuteBlocking<bool>(script);
				}
			}

			public int pid {
				get {
					string script = "return process.pid;";
					return API._ExecuteBlocking<int>(script);
				}
			}

			public string platform {
				get {
					string script = "return process.platform;";
					return API._ExecuteBlocking<string>(script);
				}
			}

			public int ppid {
				get {
					string script = "return process.ppid;";
					return API._ExecuteBlocking<int>(script);
				}
			}

			public JsonObject release {
				get {
					string script = "return process.release;";
					object result = API._ExecuteBlocking<object>(script);
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
				API.ExecuteJavaScript(script);
			}

			public void seteuid(string id) {
				string script = ScriptBuilder.Build(
					"process.seteuid({0});",
					id
				);
				API.ExecuteJavaScript(script);
			}

			public void setgid(string id) {
				string script = ScriptBuilder.Build(
					"process.setgid({0});",
					id
				);
				API.ExecuteJavaScript(script);
			}

			public void setgroups(int[] groups) {
				string script = ScriptBuilder.Build(
					"process.setgroups({0});",
					JSON.Stringify(groups)
				);
				API.ExecuteJavaScript(script);
			}

			public void setuid(string id) {
				string script = ScriptBuilder.Build(
					"process.setuid({0});",
					id
				);
				API.ExecuteJavaScript(script);
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
					return API._ExecuteBlocking<bool>(script);
				}
			}

			public string title {
				get {
					string script = "return process.title;";
					return API._ExecuteBlocking<string>(script);
				}
			}

			public bool traceDeprecation {
				get {
					string script = "return process.traceDeprecation;";
					return API._ExecuteBlocking<bool>(script);
				}
			}

			public int umask() {
				string script = "return process.umask();";
				return API._ExecuteBlocking<int>(script);
			}

			public int umask(int mask) {
				string script = ScriptBuilder.Build(
					"return process.umask({0});",
					mask
				);
				return API._ExecuteBlocking<int>(script);
			}

			public int uptime() {
				string script = "return process.uptime();";
				return API._ExecuteBlocking<int>(script);
			}

			public string version() {
				string script = "return process.version();";
				return API._ExecuteBlocking<string>(script);
			}

			public JsonObject versions {
				get {
					string script = "return process.versions;";
					object result = API._ExecuteBlocking<object>(script);
					return new JsonObject(result);
				}
			}
		}
	}
}
