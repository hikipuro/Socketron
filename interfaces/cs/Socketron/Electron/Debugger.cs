using System;

namespace Socketron {
	/// <summary>
	/// An alternate transport for Chrome's remote debugging protocol.
	/// <para>Process: Main</para>
	/// </summary>
	public class Debugger {
		public class Events {
			public const string Detach = "detach";
			public const string Message = "message";
		}

		public void Attach() {
			throw new NotImplementedException();
		}

		public void IsAttached() {
			throw new NotImplementedException();
		}

		public void Detach() {
			throw new NotImplementedException();
		}

		public void SendCommand() {
			throw new NotImplementedException();
		}
	}
}
