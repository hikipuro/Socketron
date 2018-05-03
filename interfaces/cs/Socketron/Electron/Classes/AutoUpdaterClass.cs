using System;

namespace Socketron {
	public class AutoUpdaterClass : ElectronBase {
		public AutoUpdaterClass(Socketron socketron) {
			_socketron = socketron;
		}

		public void SetFeedURL(JsonObject options) {
			throw new NotImplementedException();
		}

		public string GetFeedURL(JsonObject options) {
			throw new NotImplementedException();
		}

		public void CheckForUpdates() {
			throw new NotImplementedException();
		}

		public void QuitAndInstall() {
			throw new NotImplementedException();
		}
	}
}
