using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create and control views.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class BrowserView : EventEmitter {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the BrowserView constructors,
		/// please use electron.BrowserView.Create() method instead.
		/// </para>
		/// </summary>
		public BrowserView() {
		}

		/// <summary>
		/// *Experimental*
		/// A WebContents object owned by this view.
		/// </summary>
		public WebContents webContents {
			get { return API.GetObject<WebContents>("webContents"); }
		}

		/// <summary>
		/// *Experimental*
		/// A Integer representing the unique ID of the view.
		/// </summary>
		public int id {
			get { return API.GetProperty<int>("id"); }
		}

		/// <summary>
		/// Force closing the view, the unload and beforeunload events
		/// won't be emitted for the web page.
		/// After you're done with a view, call this function in order to
		/// free memory and other resources as soon as possible.
		/// </summary>
		public void destroy() {
			API.Apply("destroy");
		}

		/// <summary>
		/// Returns Boolean - Whether the view is destroyed.
		/// </summary>
		/// <returns></returns>
		public bool isDestroyed() {
			return API.Apply<bool>("isDestroyed");
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
			API.Apply("setAutoResize", width, height);
		}

		/// <summary>
		/// *Experimental*
		/// Resizes and moves the view to the supplied bounds relative to the window.
		/// </summary>
		/// <param name="bounds"></param>
		public void setBounds(Rectangle bounds) {
			API.Apply("setBounds", bounds);
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="color">
		/// Color in #aarrggbb or #argb form.
		/// The alpha channel is optional.
		/// </param>
		public void setBackgroundColor(string color) {
			API.Apply("setBackgroundColor", color);
		}
	}
}
