namespace Socketron {
	public class Client : LocalEventEmitter {
		public virtual bool IsConnected {
			get { return false; }
		}

		public virtual void Connect(string hostname, int port) {
		}

		public virtual void Connect(string hostname, string pipename) {
		}

		public virtual void Close() {
		}

		public virtual void Write(byte[] bytes) {
		}
	}
}
