using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Issue HTTP/HTTPS requests using Chromium's native networking library.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Net : NodeModule {
		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public Net(SocketronClient client) {
			_client = client;
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
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var request = electron.net.request({0});",
					"return {1};"
				),
				options.Stringify(),
				Script.AddObject("request")
			);
			int result = _ExecuteBlocking<int>(script);
			return new ClientRequest(_client, result);
		}

		public ClientRequest request(string options) {
			ClientRequest.Options option = ClientRequest.Options.Parse(options);
			return request(option);
		}
	}
}
