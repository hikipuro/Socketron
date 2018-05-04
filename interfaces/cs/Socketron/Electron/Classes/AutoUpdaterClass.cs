using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Enable apps to automatically update themselves.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class AutoUpdaterClass : NodeBase {
		public AutoUpdaterClass(Socketron socketron) {
			_socketron = socketron;
		}

		public void setFeedURL(JsonObject options) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public string getFeedURL(JsonObject options) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void checkForUpdates() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void quitAndInstall() {
			// TODO: implement this
			throw new NotImplementedException();
		}
	}
}
