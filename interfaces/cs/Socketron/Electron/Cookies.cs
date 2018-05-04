using System;

namespace Socketron {
	/// <summary>
	/// Query and modify a session's cookies.
	/// <para>Process: Main</para>
	/// </summary>
	public class Cookies : NodeBase {
		public class Events {
			public const string Changed = "changed";
		}

		public Cookies(Socketron socketron) {
			_socketron = socketron;
		}

		public void Get() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void Set() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void Remove() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void FlushStore() {
			// TODO: implement this
			throw new NotImplementedException();
		}
	}
}
