using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class EventEmitter : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public EventEmitter() {
		}

		public EventEmitter addListener(string eventName, JSCallback listener) {
			if (listener == null) {
				return this;
			}
			CallbackItem item = API.CreateCallbackItem(eventName, listener);
			return API.ApplyAndGetObject<EventEmitter>(
				"addListener", eventName, item
			);
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
		/// </para>
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

		public EventEmitter setMaxListeners(int value) {
			return API.ApplyAndGetObject<EventEmitter>(
				"setMaxListeners", value
			);
		}

		public int getMaxListeners() {
			return API.Apply<int>("getMaxListeners");
		}

		public JSCallback[] listeners(string eventName) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public bool emit(params object[] args) {
			return API.Apply<bool>("emit", args);
		}

		public int listenerCount(string type) {
			return API.Apply<int>("listenerCount", type);
		}

		public EventEmitter prependListener(string eventName, JSCallback listener) {
			if (listener == null) {
				return this;
			}
			CallbackItem item = API.CreateCallbackItem(eventName, listener);
			return API.ApplyAndGetObject<EventEmitter>(
				"prependListener", eventName, item
			);
		}

		public EventEmitter prependOnceListener(string eventName, JSCallback listener) {
			if (listener == null) {
				return this;
			}
			CallbackItem item = API.CreateCallbackItem(eventName, listener);
			return API.ApplyAndGetObject<EventEmitter>(
				"prependOnceListener", eventName, item
			);
		}

		public string[] eventNames() {
			object[] result = API.Apply<object[]>("eventNames");
			return Array.ConvertAll(result, value => Convert.ToString(value));
		}
	}
}
