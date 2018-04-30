using System;
using System.Collections.Generic;
using System.Threading;

namespace Socketron {
	/// <summary>
	/// Create and control views.
	/// <para>Process: Main</para>
	/// </summary>
	public class BrowserView {
		public const string Name = "BrowserView";
		public int ID = 0;
		protected Socketron _socketron;

		/// <summary>
		/// *Experimental*
		/// </summary>
		public BrowserView() {
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="socketron"></param>
		public BrowserView(Socketron socketron) {
			_socketron = socketron;
		}

		public static List<BrowserView> GetAllViews(Socketron socketron) {
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
				BrowserView view = new BrowserView() {
					ID = id,
					_socketron = socketron
				};
				views.Add(view);
			}
			return views;
		}

		public static BrowserView FromWebContents(Socketron socketron, WebContents webContents) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var contents = electron.webContents.fromId({0});",
					"var view = electron.BrowserView.fromWebContents(contents);",
					"if (view == null) {{",
						"return null;",
					"}}",
					"return view.id;"
				),
				webContents.ID
			);
			int result = _ExecuteJavaScriptBlocking<int>(socketron, script);
			BrowserView view = new BrowserView() {
				ID = result,
				_socketron = socketron
			};
			return view;
		}

		public static BrowserView FromId(Socketron socketron, int id) {
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
			BrowserView view = new BrowserView() {
				ID = result,
				_socketron = socketron
			};
			return view;
		}

		public void Destroy() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"view.destroy();"
				),
				ID
			);
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public bool IsDestroyed() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"return view.isDestroyed();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(_socketron, script);
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void SetAutoResize(bool width, bool height) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"view.setAutoResize({{width:{1},height:{2}}});"
				),
				ID,
				width.Escape(),
				height.Escape()
			);
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="bounds"></param>
		public void SetBounds(Rectangle bounds) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"view.setBounds({1});"
				),
				ID,
				bounds.Stringify()
			);
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="color"></param>
		public void SetBackgroundColor(string color) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var view = electron.BrowserView.fromId({0});",
					"view.setBackgroundColor({1});"
				),
				ID,
				color
			);
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		protected static void _ExecuteJavaScript(Socketron socketron, string script, Callback success, Callback error) {
			socketron.Main.ExecuteJavaScript(script, success, error);
		}

		protected static T _ExecuteJavaScriptBlocking<T>(Socketron socketron, string script) {
			bool done = false;
			T value = default(T);

			_ExecuteJavaScript(socketron, script, (result) => {
				if (result == null) {
					done = true;
					return;
				}
				if (typeof(T) == typeof(double)) {
					//Console.WriteLine(result.GetType());
					if (result.GetType() == typeof(int)) {
						result = (double)(int)result;
					} else if (result.GetType() == typeof(Decimal)) {
						result = (double)(Decimal)result;
					}
				}
				value = (T)result;
				done = true;
			}, (result) => {
				Console.Error.WriteLine("error: BrowserView._ExecuteJavaScriptBlocking");
				throw new InvalidOperationException(result as string);
				//done = true;
			});

			while (!done) {
				Thread.Sleep(TimeSpan.FromTicks(1));
			}
			return value;
		}
	}
}
