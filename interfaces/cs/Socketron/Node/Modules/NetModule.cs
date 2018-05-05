using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class NetModule : NodeModule {
		public NetModule() {
			_client = SocketronClient.GetCurrent();
		}

		public void createConnection() {

		}
	}
}
