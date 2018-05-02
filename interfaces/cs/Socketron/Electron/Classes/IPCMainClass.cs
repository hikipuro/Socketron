using System;

namespace Socketron {
	/// <summary>
	/// Communicate asynchronously from the main process to renderer processes.
	/// <para>Process: Main</para>
	/// </summary>
	public class IPCMainClass : ElectronBase {
		public IPCMainClass(Socketron socketron) {
			_socketron = socketron;
		}

		public void On() {
			throw new NotImplementedException();
		}

		public void Once() {
			throw new NotImplementedException();
		}

		public void RemoveListener() {
			throw new NotImplementedException();
		}

		public void RemoveAllListeners() {
			throw new NotImplementedException();
		}
	}
}
