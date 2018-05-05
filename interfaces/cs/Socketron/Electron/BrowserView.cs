using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Create and control views.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class BrowserView : NodeModule {
		public const string Name = "BrowserView";

		/// <summary>
		/// *Experimental*
		/// </summary>
		public BrowserView() {
			_disposeManually = true;
		}

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public BrowserView(SocketronClient client) {
			_disposeManually = true;
			_client = client;
		}

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public BrowserView(SocketronClient client, int id) {
			_disposeManually = true;
			_client = client;
			this._id = id;
		}

		public static List<BrowserView> getAllViews() {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var result = [];",
					"var views = electron.BrowserView.getAllViews();",
					"for (var view of views) {{",
						"result.push(view.id);",
					"}}",
					"return result;"
				)
			);
			object[] result = client.ExecuteJavaScriptBlocking<object[]>(script);
			List<BrowserView> views = new List<BrowserView>();
			foreach (object item in result) {
				int id = (int)item;
				BrowserView view = new BrowserView(client, id);
				views.Add(view);
			}
			return views;
		}

		public static BrowserView fromWebContents(WebContents webContents) {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"var view = electron.BrowserView.fromWebContents(contents);",
					"if (view == null) {{",
						"return null;",
					"}}",
					"return view.id;"
				),
				webContents._id
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			BrowserView view = new BrowserView(client, result);
			return view;
		}

		public static BrowserView fromId(int id) {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"if (view == null) {{",
						"return null;",
					"}}",
					"return view.id;"
				),
				id
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			BrowserView view = new BrowserView(client, result);
			return view;
		}

		public void destroy() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"view.destroy();"
				),
				_id
			);
			_ExecuteJavaScript(script);
		}

		public bool isDestroyed() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"return view.isDestroyed();"
				),
				_id
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void setAutoResize(bool width, bool height) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"view.setAutoResize({{width:{1},height:{2}}});"
				),
				_id,
				width.Escape(),
				height.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="bounds"></param>
		public void setBounds(Rectangle bounds) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"view.setBounds({1});"
				),
				_id,
				bounds.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="color"></param>
		public void setBackgroundColor(string color) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"view.setBackgroundColor({1});"
				),
				_id,
				color
			);
			_ExecuteJavaScript(script);
		}
	}
}
