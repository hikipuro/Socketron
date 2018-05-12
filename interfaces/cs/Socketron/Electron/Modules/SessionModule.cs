using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Manage browser sessions, cookies, cache, proxy settings, etc.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class SessionModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public SessionModule() {
		}

		/// <summary>
		/// A Session object, the default session object of the app.
		/// </summary>
		public Session defaultSession {
			get { return API.GetObject<Session>("defaultSession"); }
		}

		public Session fromPartition(string partition, JsonObject options = null) {
			if (options == null) {
				return API.ApplyAndGetObject<Session>("fromPartition", partition);
			} else {
				return API.ApplyAndGetObject<Session>("fromPartition", partition, options);
			}
		}
	}
}
