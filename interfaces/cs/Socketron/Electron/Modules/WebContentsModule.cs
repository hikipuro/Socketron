using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Render and control web pages.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class WebContentsModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public WebContentsModule() {
		}

		/// <summary>
		/// Returns WebContents[] - An array of all WebContents instances.
		/// <para>
		/// This will contain web contents for all windows, webviews,
		/// opened devtools, and devtools extension background pages.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public WebContents[] getAllWebContents() {
			return API.ApplyAndGetObjectList<WebContents>("getAllWebContents");
		}

		/// <summary>
		/// Returns WebContents - The web contents that is focused in this application,
		/// otherwise returns null.
		/// </summary>
		/// <returns></returns>
		public WebContents getFocusedWebContents() {
			return API.ApplyAndGetObject<WebContents>("getFocusedWebContents");
		}

		/// <summary>
		/// Returns WebContents - A WebContents instance with the given ID.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public WebContents fromId(int id) {
			return API.ApplyAndGetObject<WebContents>("fromId", id);
		}
	}
}
