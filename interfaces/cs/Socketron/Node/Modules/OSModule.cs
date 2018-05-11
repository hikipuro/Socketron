using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	public partial class NodeModules {
		/// <summary>
		/// OS module of the Node API.
		/// </summary>
		[type: SuppressMessage("Style", "IDE1006")]
		public class OS : JSModule {
			/// <summary>
			/// This constructor is used for internally by the library.
			/// </summary>
			public OS() {
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
				API.id = SocketronClient.ExecuteBlocking<int>(script);
			}
			//*/

			public string EOL {
				get {
					string script = ScriptBuilder.Build(
						"return {0}.EOL;",
						Script.GetObject(API.id)
					);
					return SocketronClient.ExecuteBlocking<string>(script);
				}
			}

			public string arch() {
				string script = ScriptBuilder.Build(
					"return {0}.arch();",
					Script.GetObject(API.id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public JsonObject constants {
				get {
					string script = ScriptBuilder.Build(
						"return {0}.constants;",
						Script.GetObject(API.id)
					);
					object result = SocketronClient.ExecuteBlocking<object>(script);
					return new JsonObject(result);
				}
			}

			public JsonObject[] cpus() {
				object[] result = API.Apply<object[]>("cpus");
				return Array.ConvertAll(
					result, value => new JsonObject(value)
				);
			}

			public string endianness() {
				string script = ScriptBuilder.Build(
					"return {0}.endianness();",
					Script.GetObject(API.id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public long freemem() {
				string script = ScriptBuilder.Build(
					"return {0}.freemem();",
					Script.GetObject(API.id)
				);
				return SocketronClient.ExecuteBlocking<long>(script);
			}

			public string homedir() {
				string script = ScriptBuilder.Build(
					"return {0}.homedir();",
					Script.GetObject(API.id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public string hostname() {
				string script = ScriptBuilder.Build(
					"return {0}.hostname();",
					Script.GetObject(API.id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public JsonObject[] loadavg() {
				object[] result = API.Apply<object[]>("loadavg");
				return Array.ConvertAll(
					result, value => new JsonObject(value)
				);
			}

			public JsonObject networkInterfaces() {
				string script = ScriptBuilder.Build(
					"return {0}.networkInterfaces();",
					Script.GetObject(API.id)
				);
				object result = SocketronClient.ExecuteBlocking<object>(script);
				return new JsonObject(result);
			}

			public string platform() {
				string script = ScriptBuilder.Build(
					"return {0}.platform();",
					Script.GetObject(API.id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public string release() {
				string script = ScriptBuilder.Build(
					"return {0}.release();",
					Script.GetObject(API.id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public string tmpdir() {
				string script = ScriptBuilder.Build(
					"return {0}.tmpdir();",
					Script.GetObject(API.id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public long totalmem() {
				string script = ScriptBuilder.Build(
					"return {0}.totalmem();",
					Script.GetObject(API.id)
				);
				return SocketronClient.ExecuteBlocking<long>(script);
			}

			public string type() {
				string script = ScriptBuilder.Build(
					"return {0}.type();",
					Script.GetObject(API.id)
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public long uptime() {
				string script = ScriptBuilder.Build(
					"return {0}.uptime();",
					Script.GetObject(API.id)
				);
				return SocketronClient.ExecuteBlocking<long>(script);
			}

			public JsonObject userInfo() {
				string script = ScriptBuilder.Build(
					"return {0}.userInfo();",
					Script.GetObject(API.id)
				);
				object result = SocketronClient.ExecuteBlocking<object>(script);
				return new JsonObject(result);
			}

			public JsonObject userInfo(JsonObject options) {
				string script = ScriptBuilder.Build(
					"return {0}.userInfo({1});",
					Script.GetObject(API.id),
					options.Stringify()
				);
				object result = SocketronClient.ExecuteBlocking<object>(script);
				return new JsonObject(result);
			}
		}
	}
}
