using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Make HTTP/HTTPS requests.
	/// <para>Process: Main</para>
	/// <para>
	/// ClientRequest implements the Writable Stream interface and is therefore an EventEmitter.
	/// </para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class ClientRequestModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public ClientRequestModule() {
		}

		public ClientRequest Create(ClientRequest.Options options = null) {
			if (options == null) {
				options = new ClientRequest.Options();
			}
			return API.ApplyConstructor<ClientRequest>(options);
		}

		public ClientRequest Create(string options) {
			return Create(ClientRequest.Options.Parse(options));
		}
	}
}
