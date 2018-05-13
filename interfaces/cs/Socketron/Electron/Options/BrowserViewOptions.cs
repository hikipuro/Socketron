namespace Socketron.Electron {
	/// <summary>
	/// BrowserView constructor options.
	/// </summary>
	public class BrowserViewConstructorOptions {
		/// <summary>
		/// See BrowserWindow.
		/// </summary>
		public WebPreferences webPreferences;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static BrowserViewConstructorOptions Parse(string text) {
			return JSON.Parse<BrowserViewConstructorOptions>(text);
		}

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <returns></returns>
		public string Stringify() {
			return JSON.Stringify(this);
		}
	}

	/// <summary>
	/// BrowserView.setAutoResize() options.
	/// </summary>
	public class AutoResizeOptions {
		/// <summary>
		/// If true, the view's width will grow and shrink together with the window.
		/// false by default.
		/// </summary>
		public bool width;
		/// <summary>
		/// If true, the view's height will grow and shrink together with the window.
		/// false by default.
		/// </summary>
		public bool height;
	}
}
