using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Customize the rendering of the current web page.
	/// <para>Process: Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class WebFrame: JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public WebFrame() {
		}

		/// <summary>
		/// A WebFrame representing top frame in frame hierarchy to which webFrame belongs,
		/// the property would be null if top frame is not in the current renderer process.
		/// </summary>
		public WebFrame top {
			get { return API.GetObject<WebFrame>("top"); }
		}

		/// <summary>
		/// A WebFrame representing the frame which opened webFrame,
		/// the property would be null if there's no opener
		/// or opener is not in the current renderer process.
		/// </summary>
		public WebFrame opener {
			get { return API.GetObject<WebFrame>("opener"); }
		}

		/// <summary>
		/// A WebFrame representing parent frame of webFrame,
		/// the property would be null if webFrame is top
		/// or parent is not in the current renderer process.
		/// </summary>
		public WebFrame parent {
			get { return API.GetObject<WebFrame>("parent"); }
		}

		/// <summary>
		/// A WebFrame representing the first child frame of webFrame,
		/// the property would be null if webFrame has no children
		/// or if first child is not in the current renderer process.
		/// </summary>
		public WebFrame firstChild {
			get { return API.GetObject<WebFrame>("firstChild"); }
		}

		/// <summary>
		/// A WebFrame representing next sibling frame,
		/// the property would be null if webFrame is the last frame in its parent
		/// or if the next sibling is not in the current renderer process.
		/// </summary>
		public WebFrame nextSibling {
			get { return API.GetObject<WebFrame>("nextSibling"); }
		}

		/// <summary>
		/// An Integer representing the unique frame id in the current renderer process.
		/// Distinct WebFrame instances that refer to the same underlying frame will have the same routingId.
		/// </summary>
		public int routingId {
			get { return API.GetProperty<int>("routingId"); }
		}

		/// <summary>
		/// Changes the zoom factor to the specified factor.
		/// Zoom factor is zoom percent divided by 100, so 300% = 3.0.
		/// </summary>
		/// <param name="factor">Zoom factor.</param>
		public void setZoomFactor(double factor) {
			API.Apply("setZoomFactor", factor);
		}

		/// <summary>
		/// The current zoom factor.
		/// </summary>
		/// <returns></returns>
		public double getZoomFactor() {
			return API.Apply<double>("getZoomFactor");
		}

		/// <summary>
		/// Changes the zoom level to the specified level.
		/// <para>
		/// The original size is 0 and each increment above or below represents
		/// zooming 20% larger or smaller to default limits of 300% and 50%
		/// of original size, respectively.
		/// </para>
		/// </summary>
		/// <param name="level">Zoom level.</param>
		public void setZoomLevel(double level) {
			API.Apply("setZoomLevel", level);
		}

		/// <summary>
		/// The current zoom level.
		/// </summary>
		/// <returns></returns>
		public double getZoomLevel() {
			return API.Apply<double>("getZoomLevel");
		}

		/// <summary>
		/// Sets the maximum and minimum pinch-to-zoom level.
		/// </summary>
		/// <param name="minimumLevel"></param>
		/// <param name="maximumLevel"></param>
		public void setVisualZoomLevelLimits(double minimumLevel, double maximumLevel) {
			API.Apply("setVisualZoomLevelLimits", minimumLevel, maximumLevel);
		}

		/// <summary>
		/// Sets the maximum and minimum layout-based (i.e. non-visual) zoom level.
		/// </summary>
		/// <param name="minimumLevel"></param>
		/// <param name="maximumLevel"></param>
		public void setLayoutZoomLevelLimits(double minimumLevel, double maximumLevel) {
			API.Apply("setLayoutZoomLevelLimits", minimumLevel, maximumLevel);
		}

		/// <summary>
		/// Sets a provider for spell checking in input fields and text areas.
		/// </summary>
		/// <param name="language"></param>
		/// <param name="autoCorrectWord"></param>
		/// <param name="provider"></param>
		public void setSpellCheckProvider(string language, bool autoCorrectWord, JsonObject provider) {
			API.Apply("setSpellCheckProvider", language, autoCorrectWord, provider);
		}

		/// <summary>
		/// Registers the scheme as secure scheme.
		/// </summary>
		/// <param name="scheme"></param>
		public void registerURLSchemeAsSecure(string scheme) {
			API.Apply("registerURLSchemeAsSecure", scheme);
		}

		/// <summary>
		/// Resources will be loaded from this scheme regardless
		/// of the current page's Content Security Policy.
		/// </summary>
		/// <param name="scheme"></param>
		public void registerURLSchemeAsBypassingCSP(string scheme) {
			API.Apply("registerURLSchemeAsBypassingCSP", scheme);
		}

		/// <summary>
		/// Registers the scheme as secure, bypasses content security policy for resources,
		/// allows registering ServiceWorker and supports fetch API.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="options"></param>
		public void registerURLSchemeAsPrivileged(string scheme, JsonObject options = null) {
			if (options == null) {
				API.Apply("registerURLSchemeAsPrivileged", scheme);
			} else {
				API.Apply("registerURLSchemeAsPrivileged", scheme, options);
			}
		}

		/// <summary>
		/// Inserts text to the focused element.
		/// </summary>
		/// <param name="text"></param>
		public void insertText(string text) {
			API.Apply("insertText", text);
		}

		/// <summary>
		/// Evaluates code in page.
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public Promise executeJavaScript(string code) {
			return API.ApplyAndGetObject<Promise>("executeJavaScript", code);
		}

		/// <summary>
		/// Evaluates code in page.
		/// </summary>
		/// <param name="code"></param>
		/// <param name="userGesture"></param>
		/// <param name="callback"></param>
		/// <returns></returns>
		public Promise executeJavaScript(string code, bool userGesture, Action callback) {
			// TODO: fix callback
			string eventName = "_executeJavaScript";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			return API.ApplyAndGetObject<Promise>(
				"executeJavaScript", code, userGesture, item
			);
		}

		/// <summary>
		/// Work like executeJavaScript but evaluates scripts in isolated context.
		/// </summary>
		/// <param name="worldId"></param>
		/// <param name="scripts"></param>
		/// <returns></returns>
		public Promise executeJavaScriptInIsolatedWorld(int worldId, WebSource[] scripts) {
			return API.ApplyAndGetObject<Promise>(
				"executeJavaScriptInIsolatedWorld", worldId, scripts
			);
		}

		/// <summary>
		/// Work like executeJavaScript but evaluates scripts in isolated context.
		/// </summary>
		/// <param name="worldId"></param>
		/// <param name="scripts"></param>
		/// <param name="userGesture"></param>
		/// <param name="callback"></param>
		/// <returns></returns>
		public Promise executeJavaScriptInIsolatedWorld(int worldId, WebSource[] scripts, bool userGesture, Action callback) {
			// TODO: fix callback
			string eventName = "_executeJavaScriptInIsolatedWorld";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			return API.ApplyAndGetObject<Promise>(
				"executeJavaScriptInIsolatedWorld",
				worldId, scripts, userGesture, item
			);
		}

		/// <summary>
		/// Set the content security policy of the isolated world.
		/// </summary>
		/// <param name="worldId"></param>
		/// <param name="csp"></param>
		public void setIsolatedWorldContentSecurityPolicy(int worldId, string csp) {
			API.Apply("setIsolatedWorldContentSecurityPolicy", worldId, csp);
		}

		/// <summary>
		/// Set the name of the isolated world. Useful in devtools.
		/// </summary>
		/// <param name="worldId"></param>
		/// <param name="name"></param>
		public void setIsolatedWorldHumanReadableName(int worldId, string name) {
			API.Apply("setIsolatedWorldHumanReadableName", worldId, name);
		}

		/// <summary>
		/// Set the security origin of the isolated world.
		/// </summary>
		/// <param name="worldId"></param>
		/// <param name="securityOrigin"></param>
		public void setIsolatedWorldSecurityOrigin(int worldId, string securityOrigin) {
			API.Apply("setIsolatedWorldSecurityOrigin", worldId, securityOrigin);
		}

		/// <summary>
		/// Returns an object describing usage information of Blink's internal memory caches.
		/// </summary>
		/// <returns></returns>
		public JsonObject getResourceUsage() {
			object result = API.Apply("getResourceUsage");
			return new JsonObject(result);
		}

		/// <summary>
		/// Attempts to free memory that is no longer being used
		/// (like images from a previous navigation).
		/// </summary>
		public void clearCache() {
			API.Apply("clearCache");
		}

		/// <summary>
		/// The frame element in webFrame's document selected by selector.
		/// </summary>
		/// <param name="selector"></param>
		/// <returns></returns>
		public WebFrame getFrameForSelector(string selector) {
			return API.ApplyAndGetObject<WebFrame>("getFrameForSelector", selector);
		}

		/// <summary>
		/// A child of webFrame with the supplied name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public WebFrame findFrameByName(string name) {
			return API.ApplyAndGetObject<WebFrame>("findFrameByName", name);
		}

		/// <summary>
		/// Returns WebFrame - that has the supplied routingId, null if not found.
		/// </summary>
		/// <param name="routingId"></param>
		/// <returns></returns>
		public WebFrame findFrameByRoutingId(int routingId) {
			return API.ApplyAndGetObject<WebFrame>("findFrameByRoutingId", routingId);
		}
	}
}
