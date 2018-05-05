namespace Socketron {
	public class SocketronObject : NodeJS {
		protected ElectronModule electron;

		public override void Init(SocketronClient client) {
			base.Init(client);
			_client = client;
			electron = new ElectronModule(client);
		}
	}
}
