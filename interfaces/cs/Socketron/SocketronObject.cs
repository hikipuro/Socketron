namespace Socketron {
	public class SocketronObject {
		protected Socketron socketron;
		protected Electron electron;

		public void Init(Socketron socketron) {
			this.socketron = socketron;
			electron = new Electron(socketron);
		}
	}
}
