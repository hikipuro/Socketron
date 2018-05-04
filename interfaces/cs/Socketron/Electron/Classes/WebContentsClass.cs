using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Render and control web pages.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class WebContentsClass : NodeBase {
		public WebContentsClass(Socketron socketron) {
			_socketron = socketron;
		}

		public List<WebContents> getAllWebContents() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var result = [];",
					"var list = electron.webContents.getAllWebContents();",
					"for (var contents of list) {{",
						"result.push([contents.id]);",
					"}}",
					"return result;"
				)
			);
			object[] result = _ExecuteBlocking<object[]>(script);
			List<WebContents> contentsList = new List<WebContents>();
			foreach (object[] item in result) {
				int id = (int)item[0];
				WebContents contents = new WebContents(_socketron) {
					id = id
				};
				contentsList.Add(contents);
			}
			return contentsList;
		}

		public WebContents getFocusedWebContents() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.getFocusedWebContents();",
					"if (contents == null) {{",
						"return null",
					"}}",
					"return contents.id;"
				)
			);
			int result = _ExecuteBlocking<int>(script);
			WebContents contents = new WebContents(_socketron) {
				id = result
			};
			return contents;
		}

		public WebContents fromId(int id) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"if (contents == null) {{",
						"return null;",
					"}}",
					"return contents.id;"
				),
				id
			);
			int result = _ExecuteBlocking<int>(script);
			WebContents contents = new WebContents(_socketron) {
				id = result
			};
			return contents;
		}
	}
}
