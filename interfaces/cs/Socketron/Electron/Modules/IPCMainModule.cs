using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Socketron.Electron {
	/// <summary>
	/// Communicate asynchronously from the main process to renderer processes.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class IPCMainModule : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public IPCMainModule(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}
	}
}
