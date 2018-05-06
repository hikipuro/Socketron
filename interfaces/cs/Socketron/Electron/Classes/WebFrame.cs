using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Customize the rendering of the current web page.
	/// <para>Process: Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class WebFrame {
		public WebFrame top {
			get {
				// TODO: implement this
				throw new NotImplementedException();
			}
		}

		public WebFrame opener {
			get {
				// TODO: implement this
				throw new NotImplementedException();
			}
		}

		public WebFrame parent {
			get {
				// TODO: implement this
				throw new NotImplementedException();
			}
		}

		public WebFrame firstChild {
			get {
				// TODO: implement this
				throw new NotImplementedException();
			}
		}

		public WebFrame nextSibling {
			get {
				// TODO: implement this
				throw new NotImplementedException();
			}
		}

		public int routingId {
			get {
				// TODO: implement this
				throw new NotImplementedException();
			}
		}

		public void setZoomFactor(double factor) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public double getZoomFactor() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void setZoomLevel(double level) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public double getZoomLevel() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void setVisualZoomLevelLimits(double minimumLevel, double maximumLevel) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void setLayoutZoomLevelLimits(double minimumLevel, double maximumLevel) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void setSpellCheckProvider(string language, bool autoCorrectWord, JsonObject provider) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerURLSchemeAsSecure(string scheme) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerURLSchemeAsBypassingCSP(string scheme) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerURLSchemeAsPrivileged(string scheme) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void insertText(string text) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void executeJavaScript(string code) {
			// TODO: implement this
			throw new NotImplementedException();
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
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void clearCache() {
			// TODO: implement this
			throw new NotImplementedException();
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
