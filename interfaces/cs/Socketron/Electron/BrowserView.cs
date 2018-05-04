using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Socketron {
	/// <summary>
	/// Create and control views.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class BrowserView : NodeBase {
		public const string Name = "BrowserView";

		/// <summary>
		/// *Experimental*
		/// </summary>
		public BrowserView() {
			_disposeManually = true;
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="socketron"></param>
		public BrowserView(Socketron socketron) {
			_socketron = socketron;
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="socketron"></param>
		public BrowserView(Socketron socketron, int id) {
			_socketron = socketron;
			this.id = id;
		}

		public static List<BrowserView> getAllViews(Socketron socketron) {
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
			object[] result = _ExecuteJavaScriptBlocking<object[]>(socketron, script);
			List<BrowserView> views = new List<BrowserView>();
			foreach (object item in result) {
				int id = (int)item;
				BrowserView view = new BrowserView(socketron, id);
				views.Add(view);
			}
			return views;
		}

		public static BrowserView fromWebContents(Socketron socketron, WebContents webContents) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"var view = electron.BrowserView.fromWebContents(contents);",
					"if (view == null) {{",
						"return null;",
					"}}",
					"return view.id;"
				),
				webContents.id
			);
			int result = _ExecuteJavaScriptBlocking<int>(socketron, script);
			BrowserView view = new BrowserView(socketron, result);
			return view;
		}

		public static BrowserView fromId(Socketron socketron, int id) {
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
			int result = _ExecuteJavaScriptBlocking<int>(socketron, script);
			BrowserView view = new BrowserView(socketron, result);
			return view;
		}

		public void destroy() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"view.destroy();"
				),
				id
			);
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public bool isDestroyed() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"return view.isDestroyed();"
				),
				id
			);
			return _ExecuteJavaScriptBlocking<bool>(_socketron, script);
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
				id,
				width.Escape(),
				height.Escape()
			);
			_ExecuteJavaScript(_socketron, script, null, null);
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
				id,
				bounds.Stringify()
			);
			_ExecuteJavaScript(_socketron, script, null, null);
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
				id,
				color
			);
			_ExecuteJavaScript(_socketron, script, null, null);
		}
	}
}
