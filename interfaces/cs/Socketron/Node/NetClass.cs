using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class NetClass : ElectronBase {
		public NetClass(Socketron socketron) {
			_socketron = socketron;
		}

		public void createConnection() {

		}
	}
}
