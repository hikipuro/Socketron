using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Communicate asynchronously from the main process to renderer processes.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class IPCMain : EventEmitter {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public IPCMain() {
		}
	}
}
