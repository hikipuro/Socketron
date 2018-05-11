using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class EventEmitter : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public EventEmitter() {
		}

		public EventEmitter on(string eventName, JSCallback listener) {
			if (listener == null) {
				return this;
			}
			CallbackItem item = API.CreateCallbackItem(eventName, listener);
			return API.ApplyAndGetObject<EventEmitter>("on", eventName, item);
		}
	}
}
