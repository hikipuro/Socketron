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
		public BrowserView(JsonObject options = null) {
			_disposeManually = true;

			if (options == null) {
				options = new JsonObject();
			}
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = new electron.BrowserView({0});",
					"return view.id;"
				),
				options.Stringify()
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			_client = client;
			_id = result;
		}

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public BrowserView(SocketronClient client, int id) {
			_disposeManually = true;
			_client = client;
			_id = id;
		}

		/// <summary>
		/// *Experimental*
		/// A WebContents object owned by this view.
		/// </summary>
		public WebContents webContents {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var view = electron.BrowserView.fromId({0});",
						"return view.webContents.id;"
					),
					_id
				);
				int result = _ExecuteBlocking<int>(script);
				return new WebContents(_client, result);
			}
		}

		/// <summary>
		/// *Experimental*
		/// A Integer representing the unique ID of the view.
		/// </summary>
		public int id {
			get { return _id; }
		}

		/// <summary>
		/// Returns BrowserView[] - An array of all opened BrowserViews.
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// Returns BrowserView | null - The BrowserView that owns
		/// the given webContents or null if the contents are not owned by a BrowserView.
		/// </summary>
		/// <param name="webContents"></param>
		/// <returns></returns>
		public static BrowserView fromWebContents(WebContents webContents) {
			if (webContents == null) {
				return null;
			}
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

		/// <summary>
		/// Returns BrowserView - The view with the given id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
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

		/// <summary>
		/// Force closing the view, the unload and beforeunload events
		/// won't be emitted for the web page.
		/// After you're done with a view, call this function in order to
		/// free memory and other resources as soon as possible.
		/// </summary>
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

		/// <summary>
		/// Returns Boolean - Whether the view is destroyed.
		/// </summary>
		/// <returns></returns>
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
		/// <param name="width">
		/// If true, the view's width will grow and shrink
		/// together with the window. false by default.
		/// </param>
		/// <param name="height">
		/// If true, the view's height will grow and shrink
		/// together with the window. false by default.
		/// </param>
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
		/// Resizes and moves the view to the supplied bounds relative to the window.
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
		/// <param name="color">
		/// Color in #aarrggbb or #argb form.
		/// The alpha channel is optional.
		/// </param>
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
