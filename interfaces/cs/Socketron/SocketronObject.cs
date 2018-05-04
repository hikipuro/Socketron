namespace Socketron {
	public class SocketronObject : NodeJS {
		protected Electron electron;

		public override void Init(Socketron socketron) {
			base.Init(socketron);
			_socketron = socketron;
			electron = new Electron(socketron);
		}
	}
}
