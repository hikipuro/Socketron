using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create and control views.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class BrowserViewModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public BrowserViewModule() {
		}

		/// <summary>
		/// *Experimental*
		/// Create a new BrowserView instance.
		/// </summary>
		public BrowserView Create(WebPreferences options = null) {
			if (options == null) {
				options = new WebPreferences();
			}
			return API.ApplyConstructor<BrowserView>(options);
		}


		/// <summary>
		/// Returns BrowserView[] - An array of all opened BrowserViews.
		/// </summary>
		/// <returns></returns>
		public BrowserView[] getAllViews() {
			return API.ApplyAndGetObjectList<BrowserView>("getAllViews");
		}

		/// <summary>
		/// Returns BrowserView | null - The BrowserView that owns
		/// the given webContents or null if the contents are not owned by a BrowserView.
		/// </summary>
		/// <param name="webContents"></param>
		/// <returns></returns>
		public BrowserView fromWebContents(WebContents webContents) {
			return API.ApplyAndGetObject<BrowserView>("fromWebContents", webContents);
		}

		/// <summary>
		/// Returns BrowserView - The view with the given id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public BrowserView fromId(int id) {
			return API.ApplyAndGetObject<BrowserView>("fromId", id);
		}
	}
}
