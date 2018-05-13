using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Issue HTTP/HTTPS requests using Chromium's native networking library.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Net : EventEmitter {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public Net() {
		}

		/// <summary>
		/// Creates a ClientRequest instance using the provided options
		/// which are directly forwarded to the ClientRequest constructor.
		/// <para>
		/// The net.request method would be used to issue both secure
		/// and insecure HTTP requests according to the specified protocol scheme in the options object.
		/// </para>
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public ClientRequest request(ClientRequest.Options options) {
			return API.ApplyAndGetObject<ClientRequest>("request", options);
		}

		public ClientRequest request(string options) {
			ClientRequest.Options option = ClientRequest.Options.Parse(options);
			return request(option);
		}
	}
}
