using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	public partial class NodeModules {
		/// <summary>
		/// Net module of the Node API.
		/// </summary>
		[type: SuppressMessage("Style", "IDE1006")]
		public class Net : JSModule {
			public Net() {
				_client = SocketronClient.GetCurrent();
			}

			public void createConnection() {

			}
		}
	}
}
