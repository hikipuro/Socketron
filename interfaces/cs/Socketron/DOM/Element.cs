using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Element : Node {
		public Element() {
			_client = SocketronClient.GetCurrent();
		}
	}
}
