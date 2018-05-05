using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	public partial class NodeModules {
		[type: SuppressMessage("Style", "IDE1006")]
		public class FS : NodeModule {
			public FS() {
				_client = SocketronClient.GetCurrent();
			}

			/*
			public void require() {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var module = this.require({0});",
						"return {1};"
					),
					"fs".Escape(),
					Script.AddObject("module")
				);
				_id = _ExecuteBlocking<int>(script);
			}
			//*/

			public bool existsSync(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"return fs.existsSync({1});"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				return _ExecuteBlocking<bool>(script);
			}

			public JsonObject linkSync(string existingPath, string newPath) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"return fs.linkSync({1});"
					),
					Script.GetObject(_id),
					existingPath.Escape(),
					newPath.Escape()
				);
				object result = _ExecuteBlocking<object>(script);
				return new JsonObject(result);
			}

			public JsonObject lstatSync(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"return fs.lstatSync({1});"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				object result = _ExecuteBlocking<object>(script);
				return new JsonObject(result);
			}

			public string mkdirSync(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"return fs.mkdirSync({1});"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				return _ExecuteBlocking<string>(script);
			}

			public string mkdtempSync(string prefix) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"return fs.mkdtempSync({1});"
					),
					Script.GetObject(_id),
					prefix.Escape()
				);
				return _ExecuteBlocking<string>(script);
			}

			public int openSync(string path, string flags) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"return fs.readdirSync({1},{2});"
					),
					Script.GetObject(_id),
					path.Escape(),
					flags.Escape()
				);
				return _ExecuteBlocking<int>(script);
			}

			public JsonObject readdirSync(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"return fs.readdirSync({1});"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				object result = _ExecuteBlocking<object>(script);
				return new JsonObject(result);
			}

			public JsonObject readFileSync(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"return fs.readFileSync({1});"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				object result = _ExecuteBlocking<object>(script);
				return new JsonObject(result);
			}

			/*
			public bool readSync(int fd, LocalBuffer buffer, int offset, int length, int position) {
				// TODO: implement this
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"return fs.readSync({1},{2},{3},{4},{5});"
					),
					Script.GetObject(id),
					fd,
					buffer,
					offset,
					length,
					position
				);
				return _ExecuteJavaScriptBlocking<bool>(script);
			}
			//*/

			public void realpathSync(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"fs.realpathSync({1});",
						"return 1;"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				_ExecuteBlocking<int>(script);
			}

			public void renameSync(string oldPath, string newPath) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"fs.renameSync({1});",
						"return 1;"
					),
					Script.GetObject(_id),
					oldPath.Escape(),
					newPath.Escape()
				);
				_ExecuteBlocking<int>(script);
			}

			public void rmdirSync(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"fs.rmdirSync({1});",
						"return 1;"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				_ExecuteBlocking<int>(script);
			}

			public void truncateSync(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"fs.truncateSync({1});",
						"return 1;"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				_ExecuteBlocking<int>(script);
			}

			public void unlinkSync(string path) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"fs.unlinkSync({1});",
						"return 1;"
					),
					Script.GetObject(_id),
					path.Escape()
				);
				_ExecuteBlocking<int>(script);
			}

			public void writeFileSync(string file, string data) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var fs = {0};",
						"fs.writeFileSync({1});",
						"return 1;"
					),
					Script.GetObject(_id),
					file.Escape(),
					data.Escape()
				);
				_ExecuteBlocking<int>(script);
			}
		}
	}
}
