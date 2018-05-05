using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	public partial class NodeModules {
		[type: SuppressMessage("Style", "IDE1006")]
		public class OS : NodeModule {
			public OS() {
				_client = SocketronClient.GetCurrent();
			}

			/*
			public void require() {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var module = this.require({0});",
						"return {1};"
					),
					"os".Escape(),
					Script.AddObject("module")
				);
				_id = SocketronClient.ExecuteBlocking<int>(script);
			}
			//*/

			public string EOL {
				get {
					string script = ScriptBuilder.Build(
						"return {0}.EOL;",
						Script.GetObject(_id)
					);
					return SocketronClient.ExecuteBlocking<string>(script);
				}
			}

			public string arch() {
				string script = ScriptBuilder.Build(
					"return {0}.arch();",
					Script.GetObject(_id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public JsonObject constants {
				get {
					string script = ScriptBuilder.Build(
						"return {0}.constants;",
						Script.GetObject(_id)
					);
					object result = SocketronClient.ExecuteBlocking<object>(script);
					return new JsonObject(result);
				}
			}

			public List<JsonObject> cpus() {
				string script = ScriptBuilder.Build(
					"return {0}.cpus();",
					Script.GetObject(_id)
				);
				object[] result = SocketronClient.ExecuteBlocking<object[]>(script);
				return JsonObject.FromArray(result);
			}

			public string endianness() {
				string script = ScriptBuilder.Build(
					"return {0}.endianness();",
					Script.GetObject(_id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public long freemem() {
				string script = ScriptBuilder.Build(
					"return {0}.freemem();",
					Script.GetObject(_id)
				);
				return SocketronClient.ExecuteBlocking<long>(script);
			}

			public string homedir() {
				string script = ScriptBuilder.Build(
					"return {0}.homedir();",
					Script.GetObject(_id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public string hostname() {
				string script = ScriptBuilder.Build(
					"return {0}.hostname();",
					Script.GetObject(_id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public List<JsonObject> loadavg() {
				string script = ScriptBuilder.Build(
					"return {0}.loadavg();",
					Script.GetObject(_id)
				);
				object[] result = SocketronClient.ExecuteBlocking<object[]>(script);
				return JsonObject.FromArray(result);
			}

			public JsonObject networkInterfaces() {
				string script = ScriptBuilder.Build(
					"return {0}.networkInterfaces();",
					Script.GetObject(_id)
				);
				object result = SocketronClient.ExecuteBlocking<object>(script);
				return new JsonObject(result);
			}

			public string platform() {
				string script = ScriptBuilder.Build(
					"return {0}.platform();",
					Script.GetObject(_id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public string release() {
				string script = ScriptBuilder.Build(
					"return {0}.release();",
					Script.GetObject(_id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public string tmpdir() {
				string script = ScriptBuilder.Build(
					"return {0}.tmpdir();",
					Script.GetObject(_id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public long totalmem() {
				string script = ScriptBuilder.Build(
					"return {0}.totalmem();",
					Script.GetObject(_id)
				);
				return SocketronClient.ExecuteBlocking<long>(script);
			}

			public string type() {
				string script = ScriptBuilder.Build(
					"return {0}.type();",
					Script.GetObject(_id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public long uptime() {
				string script = ScriptBuilder.Build(
					"return {0}.uptime();",
					Script.GetObject(_id)
				);
				return SocketronClient.ExecuteBlocking<long>(script);
			}

			public JsonObject userInfo() {
				string script = ScriptBuilder.Build(
					"return {0}.userInfo();",
					Script.GetObject(_id)
				);
				object result = SocketronClient.ExecuteBlocking<object>(script);
				return new JsonObject(result);
			}

			public JsonObject userInfo(JsonObject options) {
				string script = ScriptBuilder.Build(
					"return {0}.userInfo({1});",
					Script.GetObject(_id),
					options.Stringify()
				);
				object result = SocketronClient.ExecuteBlocking<object>(script);
				return new JsonObject(result);
			}
		}
	}
}
