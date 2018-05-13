using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Use main process modules from the renderer process.
	/// <para>Process: Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Remote : EventEmitter {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public Remote() {
		}

		/// <summary>
		/// The process object in the main process.
		/// This is the same as remote.getGlobal('process') but is cached.
		/// </summary>
		public NodeModules.Process process {
			get { return API.GetObject<NodeModules.Process>("process"); }
		}

		/// <summary>
		/// Returns any - The object returned by require(module) in the main process.
		/// Modules specified by their relative path will resolve relative to
		/// the entrypoint of the main process.
		/// </summary>
		/// <param name="module"></param>
		/// <returns></returns>
		public T require<T>(string module) where T: JSObject, new() {
			return API.ApplyAndGetObject<T>("require", module);
		}

		/// <summary>
		/// Returns BrowserWindow - The window to which this web page belongs.
		/// </summary>
		/// <returns></returns>
		public BrowserWindow getCurrentWindow() {
			return API.ApplyAndGetObject<BrowserWindow>("getCurrentWindow");
		}

		/// <summary>
		/// Returns WebContents - The web contents of this web page.
		/// </summary>
		/// <returns></returns>
		public WebContents getCurrentWebContents() {
			return API.ApplyAndGetObject<WebContents>("getCurrentWebContents");
		}

		/// <summary>
		/// Returns any - The global variable of name (e.g. global[name]) in the main process.
		/// </summary>
		/// <returns></returns>
		public JsonObject getGlobal() {
			object result = API.Apply("getGlobal");
			return new JsonObject(result);
		}
	}
}
