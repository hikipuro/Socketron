using System;

namespace Socketron {
	/// <summary>
	/// Query and modify a session's cookies.
	/// <para>Process: Main</para>
	/// </summary>
	public class Cookies : NodeModule {
		public class Events {
			public const string Changed = "changed";
		}

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public Cookies(SocketronClient client) {
			_client = client;
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
