using System.Collections.Generic;

namespace Socketron {
	/// <summary>
	/// Render and control web pages.
	/// <para>Process: Main</para>
	/// </summary>
	public class WebContentsClass : ElectronBase {
		public WebContentsClass(Socketron socketron) {
			_socketron = socketron;
		}

		public List<WebContents> GetAllWebContents() {
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
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			List<WebContents> contentsList = new List<WebContents>();
			foreach (object[] item in result) {
				int id = (int)item[0];
				WebContents contents = new WebContents(_socketron) {
					ID = id
				};
				contentsList.Add(contents);
			}
			return contentsList;
		}

		public WebContents GetFocusedWebContents() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.getFocusedWebContents();",
					"if (contents == null) {{",
						"return null",
					"}}",
					"return contents.id;"
				)
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			WebContents contents = new WebContents(_socketron) {
				ID = result
			};
			return contents;
		}

		public WebContents FromId(int id) {
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
			int result = _ExecuteJavaScriptBlocking<int>(script);
			WebContents contents = new WebContents(_socketron) {
				ID = result
			};
			return contents;
		}
	}
}
