using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Communicate asynchronously from the main process to renderer processes.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class IPCMainModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public IPCMainModule() {
		}

		public EventEmitter on(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.on(eventName, listener);
		}

		public EventEmitter once(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.once(eventName, listener);
		}

		public EventEmitter removeListener(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.removeListener(eventName, listener);
		}

		public EventEmitter removeAllListeners(string eventName) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.removeAllListeners(eventName);
		}
	}
}
