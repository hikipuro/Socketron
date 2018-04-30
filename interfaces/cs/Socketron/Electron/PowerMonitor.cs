using System;

namespace Socketron {
	/// <summary>
	/// Monitor power state changes.
	/// <para>Process: Main</para>
	/// </summary>
	public class PowerMonitor {
		public class Events {
			public const string Suspend = "suspend";
			public const string Resume = "resume";
			/// <summary>*Windows*</summary>
			public const string OnAC = "on-ac";
			/// <summary>*Windows*</summary>
			public const string OnBattery = "on-battery";
			/// <summary>*Linux macOS*</summary>
			public const string Shutdown = "shutdown";
		}

		public void QuerySystemIdleState() {
			throw new NotImplementedException();
		}

		public void QuerySystemIdleTime() {
			throw new NotImplementedException();
		}
	}
}
