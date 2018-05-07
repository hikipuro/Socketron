using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	public partial class NodeModules {
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
