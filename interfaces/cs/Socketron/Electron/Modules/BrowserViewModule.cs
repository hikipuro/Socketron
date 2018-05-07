using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create and control views.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class BrowserViewModule : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public BrowserViewModule(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// *Experimental*
		/// Create a new BrowserView instance.
		/// </summary>
		public BrowserView Create(WebPreferences options = null) {
			if (options == null) {
				options = new WebPreferences();
			}
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = new {0}({1});",
					"return {2};"
				),
				Script.GetObject(_id),
				options.Stringify(),
				Script.AddObject("view")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			return new BrowserView(_client, result);
		}


		/// <summary>
		/// Returns BrowserView[] - An array of all opened BrowserViews.
		/// </summary>
		/// <returns></returns>
		public List<BrowserView> getAllViews() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var result = [];",
					"var views = {0}.getAllViews();",
					"for (var view of views) {{",
						"result.push({1});",
					"}}",
					"return result;"
				),
				Script.GetObject(_id),
				Script.AddObject("view")
			);
			object[] result = _ExecuteBlocking<object[]>(script);
			List<BrowserView> views = new List<BrowserView>();
			foreach (object item in result) {
				int id = (int)item;
				BrowserView view = new BrowserView(_client, id);
				views.Add(view);
			}
			return views;
		}

		/// <summary>
		/// Returns BrowserView | null - The BrowserView that owns
		/// the given webContents or null if the contents are not owned by a BrowserView.
		/// </summary>
		/// <param name="webContents"></param>
		/// <returns></returns>
		public BrowserView fromWebContents(WebContents webContents) {
			if (webContents == null) {
				return null;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = {0}.fromWebContents({1});",
					"if (view == null) {{",
						"return null;",
					"}}",
					"return {1};"
				),
				Script.GetObject(_id),
				Script.GetObject(webContents._id),
				Script.AddObject("view")
			);
			int result = _ExecuteBlocking<int>(script);
			return new BrowserView(_client, result);
		}

		/// <summary>
		/// Returns BrowserView - The view with the given id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public BrowserView fromId(int id) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = {0}.fromId({1});",
					"if (view == null) {{",
						"return null;",
					"}}",
					"return {2};"
				),
				Script.GetObject(_id),
				id,
				Script.AddObject("view")
			);
			int result = _ExecuteBlocking<int>(script);
			return new BrowserView(_client, result);
		}
	}
}
