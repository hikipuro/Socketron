using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class NetModule : NodeBase {
		public NetModule(Socketron socketron) {
			_socketron = socketron;
		}

		public void createConnection() {

		}
	}
}
