using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create and control views.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class BrowserView : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public BrowserView() {
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the BrowserView constructors,
		/// please use electron.BrowserView.Create() method instead.
		/// </para>
		/// </summary>
		/// <param name="client"></param>
		public BrowserView(SocketronClient client, int id) {
			API.client = client;
			API.id = id;
		}

		/// <summary>
		/// *Experimental*
		/// A WebContents object owned by this view.
		/// </summary>
		public WebContents webContents {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var webContents = {0}.webContents;",
						"return {1};"
					),
					Script.GetObject(API.id),
					Script.AddObject("webContents")
				);
				int result = API._ExecuteBlocking<int>(script);
				return new WebContents(API.client, result);
			}
		}

		/// <summary>
		/// *Experimental*
		/// A Integer representing the unique ID of the view.
		/// </summary>
		public int id {
			get {
				string script = ScriptBuilder.Build(
					"return {0}.id;",
					Script.GetObject(API.id)
				);
				return API._ExecuteBlocking<int>(script);

			}
		}

		/// <summary>
		/// Force closing the view, the unload and beforeunload events
		/// won't be emitted for the web page.
		/// After you're done with a view, call this function in order to
		/// free memory and other resources as soon as possible.
		/// </summary>
		public void destroy() {
			string script = ScriptBuilder.Build(
				"{0}.destroy();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the view is destroyed.
		/// </summary>
		/// <returns></returns>
		public bool isDestroyed() {
			string script = ScriptBuilder.Build(
				"return {0}.isDestroyed();",
				Script.GetObject(API.id)
			);
			return API._ExecuteBlocking<bool>(script);
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
				"{0}.setAutoResize({{width:{1},height:{2}}});",
				Script.GetObject(API.id),
				width.Escape(),
				height.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Experimental*
		/// Resizes and moves the view to the supplied bounds relative to the window.
		/// </summary>
		/// <param name="bounds"></param>
		public void setBounds(Rectangle bounds) {
			string script = ScriptBuilder.Build(
				"{0}.setBounds({1});",
				Script.GetObject(API.id),
				bounds.Stringify()
			);
			API.ExecuteJavaScript(script);
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
				"{0}.setBackgroundColor({1});",
				Script.GetObject(API.id),
				color
			);
			API.ExecuteJavaScript(script);
		}
	}
}
