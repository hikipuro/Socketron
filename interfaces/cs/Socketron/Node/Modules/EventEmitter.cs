using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class EventEmitter : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public EventEmitter() {
		}

		/// <summary>
		/// Listens to channel, when a new message arrives listener
		/// would be called with listener(event, args...).
		/// </summary>
		/// <param name="eventName"></param>
		/// <param name="listener"></param>
		/// <returns></returns>
		public EventEmitter on(string eventName, JSCallback listener) {
			if (listener == null) {
				return this;
			}
			CallbackItem item = API.CreateCallbackItem(eventName, listener);
			return API.ApplyAndGetObject<EventEmitter>("on", eventName, item);
		}

		/// <summary>
		/// Adds a one time listener function for the event.
		/// <para>
		/// This listener is invoked only the next time a message is sent to channel,
		/// after which it is removed.
		/// </summary>
		/// <param name="eventName"></param>
		/// <param name="listener"></param>
		public EventEmitter once(string eventName, JSCallback listener) {
			if (listener == null) {
				return this;
			}
			CallbackItem item = API.CreateCallbackItem(eventName, listener);
			return API.ApplyAndGetObject<EventEmitter>(
				"once", eventName, item
			);
		}

		/// <summary>
		/// Removes the specified listener from the listener array for the specified channel.
		/// </summary>
		/// <param name="eventName"></param>
		/// <param name="listener"></param>
		public EventEmitter removeListener(string eventName, JSCallback listener) {
			if (listener == null) {
				return this;
			}
			CallbackItem item = API.GetCallbackItem(eventName, listener);
			API.RemoveCallbackItem(eventName, item);
			return API.ApplyAndGetObject<EventEmitter>(
				"removeListener", eventName, item
			);
		}

		/// <summary>
		/// Removes all listeners, or those of the specified channel.
		/// </summary>
		/// <param name="eventName"></param>
		public EventEmitter removeAllListeners(string eventName) {
			API.client.Callbacks.RemoveEvents(API.id, eventName);
			return API.ApplyAndGetObject<EventEmitter>(
				"removeAllListeners", eventName
			);
		}

		/// <summary>
		/// Removes all listeners.
		/// </summary>
		public EventEmitter removeAllListeners() {
			API.client.Callbacks.RemoveInstanceEvents(API.id);
			return API.ApplyAndGetObject<EventEmitter>(
				"removeAllListeners"
			);
		}
	}
}
