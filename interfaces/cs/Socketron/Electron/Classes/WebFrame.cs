using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Customize the rendering of the current web page.
	/// <para>Process: Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class WebFrame: JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public WebFrame() {
		}

		public WebFrame top {
			get { return API.GetObject<WebFrame>("top"); }
		}

		public WebFrame opener {
			get { return API.GetObject<WebFrame>("opener"); }
		}

		public WebFrame parent {
			get { return API.GetObject<WebFrame>("parent"); }
		}

		public WebFrame firstChild {
			get { return API.GetObject<WebFrame>("firstChild"); }
		}

		public WebFrame nextSibling {
			get { return API.GetObject<WebFrame>("nextSibling"); }
		}

		public int routingId {
			get { return API.GetProperty<int>("routingId"); }
		}

		public void setZoomFactor(double factor) {
			API.Apply("setZoomFactor", factor);
		}

		public double getZoomFactor() {
			return API.Apply<double>("getZoomFactor");
		}

		public void setZoomLevel(double level) {
			API.Apply("setZoomLevel", level);
		}

		public double getZoomLevel() {
			return API.Apply<double>("getZoomLevel");
		}

		public void setVisualZoomLevelLimits(double minimumLevel, double maximumLevel) {
			API.Apply("setVisualZoomLevelLimits", minimumLevel, maximumLevel);
		}

		public void setLayoutZoomLevelLimits(double minimumLevel, double maximumLevel) {
			API.Apply("setLayoutZoomLevelLimits", minimumLevel, maximumLevel);
		}

		public void setSpellCheckProvider(string language, bool autoCorrectWord, JsonObject provider) {
			API.Apply("setSpellCheckProvider", language, autoCorrectWord, provider);
		}

		public void registerURLSchemeAsSecure(string scheme) {
			// TODO: implement this
			API.Apply("registerURLSchemeAsSecure", scheme);
		}

		public void registerURLSchemeAsBypassingCSP(string scheme) {
			// TODO: implement this
			API.Apply("registerURLSchemeAsBypassingCSP", scheme);
		}

		public void registerURLSchemeAsPrivileged(string scheme) {
			// TODO: implement this
			API.Apply("registerURLSchemeAsPrivileged", scheme);
		}

		public void insertText(string text) {
			API.Apply("insertText", text);
		}

		public void executeJavaScript(string code) {
			API.Apply("executeJavaScript", code);
		}

		public void executeJavaScriptInIsolatedWorld(int worldId) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void setIsolatedWorldContentSecurityPolicy(int worldId, string csp) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void setIsolatedWorldHumanReadableName(int worldId, string name) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void setIsolatedWorldSecurityOrigin(int worldId, string securityOrigin) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public JsonObject getResourceUsage() {
			object result = API.Apply<object>("getResourceUsage");
			return new JsonObject(result);
		}

		public void clearCache() {
			API.Apply("clearCache");
		}

		public WebFrame getFrameForSelector(string selector) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public WebFrame findFrameByName(string name) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public WebFrame findFrameByRoutingId(int routingId) {
			// TODO: implement this
			throw new NotImplementedException();
		}
	}
}
