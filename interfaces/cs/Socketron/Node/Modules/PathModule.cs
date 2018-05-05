using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	public partial class NodeModules {
		[type: SuppressMessage("Style", "IDE1006")]
		public class Path : NodeModule {
			public Path() {
				_client = SocketronClient.GetCurrent();
			}

			/*
			public void require() {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var module = this.require({0});",
						"return {1};"
					),
					"path".Escape(),
					Script.AddObject("module")
				);
				_id = SocketronClient.ExecuteBlocking<int>(script);
			}
			//*/

			public string basename(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var path = {0};",
						"return path.basename({1});"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public string basename(string path, string ext) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var path = {0};",
						"return path.basename({1},{2});"
					),
					Script.GetObject(_id),
					path.Escape(),
					ext.Escape()
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public string delimiter {
				get {
					string script = ScriptBuilder.Build(
						ScriptBuilder.Script(
							"var path = {0};",
							"return path.delimiter;"
						),
						Script.GetObject(_id)
					);
					return SocketronClient.ExecuteBlocking<string>(script);
				}
			}

			public string dirname(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var path = {0};",
						"return path.dirname({1});"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public string extname(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var path = {0};",
						"return path.extname({1});"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public string format(JsonObject pathObject) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var path = {0};",
						"return path.format({1});"
					),
					Script.GetObject(_id),
					pathObject.Stringify()
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public bool isAbsolute(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var path = {0};",
						"return path.isAbsolute({1});"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				return SocketronClient.ExecuteBlocking<bool>(script);
			}

			public string join(params string[] paths) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var path = {0};",
						"return path.join({1});"
					),
					Script.GetObject(_id),
					paths.Escape()
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public string normalize(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var path = {0};",
						"return path.normalize({1});"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public JsonObject parse(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var path = {0};",
						"return path.parse({1});"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				object result = SocketronClient.ExecuteBlocking<object>(script);
				return new JsonObject(result);
			}

			/*
			public JsonObject posix {
				get {
					string script = ScriptBuilder.Build(
						ScriptBuilder.Script(
							"var path = {0};",
							"return path.posix;"
						),
						Script.GetObject(id)
					);
					object result = SocketronClient.ExecuteBlocking<object>(script);
					return new JsonObject(result);
				}
			}
			//*/

			public string relative(string from, string to) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var path = {0};",
						"return path.relative({1},{2});"
					),
					Script.GetObject(_id),
					from.Escape(),
					to.Escape()
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public string resolve(params string[] paths) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var path = {0};",
						"return path.resolve({1});"
					),
					Script.GetObject(_id),
					paths.Escape()
				);
				return SocketronClient.ExecuteBlocking<string>(script);
			}

			public string sep {
				get {
					string script = ScriptBuilder.Build(
						ScriptBuilder.Script(
							"var path = {0};",
							"return path.sep;"
						),
						Script.GetObject(_id)
					);
					return SocketronClient.ExecuteBlocking<string>(script);
				}
			}

			/*
			public JsonObject win32 {
				get {
					string script = ScriptBuilder.Build(
						ScriptBuilder.Script(
							"var path = {0};",
							"return path.win32;"
						),
						Script.GetObject(id)
					);
					object result = SocketronClient.ExecuteBlocking<object>(script);
					return new JsonObject(result);
				}
			}
			//*/
		}
	}
}
