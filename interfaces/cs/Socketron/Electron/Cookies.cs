using System;

namespace Socketron {
	/// <summary>
	/// Query and modify a session's cookies.
	/// <para>Process: Main</para>
	/// </summary>
	public class Cookies : ElectronBase {
		public class Events {
			public const string Changed = "changed";
		}

		public int id;

		public Cookies(Socketron socketron) {
			_socketron = socketron;
		}

		public void Get() {
			throw new NotImplementedException();
		}

		public void Set() {
			throw new NotImplementedException();
		}

		public void Remove() {
			throw new NotImplementedException();
		}

		public void FlushStore() {
			throw new NotImplementedException();
		}
	}
}
