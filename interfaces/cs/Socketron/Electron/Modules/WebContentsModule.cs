using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Render and control web pages.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class WebContentsModule : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public WebContentsModule(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// Returns WebContents[] - An array of all WebContents instances.
		/// <para>
		/// This will contain web contents for all windows, webviews,
		/// opened devtools, and devtools extension background pages.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public List<WebContents> getAllWebContents() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var result = [];",
					"var list = {0}.getAllWebContents();",
					"for (var contents of list) {{",
						"result.push({1});",
					"}}",
					"return result;"
				),
				Script.GetObject(_id),
				Script.AddObject("contents")
			);
			object[] result = _ExecuteBlocking<object[]>(script);
			List<WebContents> contentsList = new List<WebContents>();
			foreach (object[] item in result) {
				int id = (int)item[0];
				WebContents contents = new WebContents(_client, id);
				contentsList.Add(contents);
			}
			return contentsList;
		}

		/// <summary>
		/// Returns WebContents - The web contents that is focused in this application,
		/// otherwise returns null.
		/// </summary>
		/// <returns></returns>
		public WebContents getFocusedWebContents() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.getFocusedWebContents();"
				),
				Script.GetObject(_id)
			);
			int result = _ExecuteBlocking<int>(script);
			return new WebContents(_client, result);
		}

		/// <summary>
		/// Returns WebContents - A WebContents instance with the given ID.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public WebContents fromId(int id) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = {0}.fromId({1});",
					"return {2};"
				),
				Script.GetObject(_id),
				id,
				Script.AddObject("contents")
			);
			int result = _ExecuteBlocking<int>(script);
			return new WebContents(_client, result);
		}
	}
}
