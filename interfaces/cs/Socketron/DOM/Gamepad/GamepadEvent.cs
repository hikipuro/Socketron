using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class GamepadEvent : DOMModule {
		public GamepadEvent() {
		}

		public Gamepad gamepad {
			get { return API.GetObject<Gamepad>("gamepad"); }
		}
	}
}
