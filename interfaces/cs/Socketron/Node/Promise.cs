using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Promise : JSObject {
		public Promise() {
		}

		public Promise then(JSCallback onFulfilled) {
			string eventName = "_then";
			CallbackItem onFulfilledItem = API.CreateCallbackItem(eventName, onFulfilled);
			return API.ApplyAndGetObject<Promise>("then", onFulfilledItem);
		}

		public Promise then(JSCallback onFulfilled, JSCallback onRejected) {
			string eventName = "_then";
			CallbackItem onFulfilledItem = API.CreateCallbackItem(eventName, onFulfilled);
			CallbackItem onRejectedItem = API.CreateCallbackItem(eventName, onRejected);
			return API.ApplyAndGetObject<Promise>("then", onFulfilledItem, onRejectedItem);
		}

		public Promise @catch(JSCallback onRejected) {
			string eventName = "_catch";
			CallbackItem onRejectedItem = API.CreateCallbackItem(eventName, onRejected);
			return API.ApplyAndGetObject<Promise>("catch", onRejectedItem);
		}

		public Promise @finally(JSCallback onFinally) {
			string eventName = "_finally";
			CallbackItem onFinallyItem = API.CreateCallbackItem(eventName, onFinally);
			return API.ApplyAndGetObject<Promise>("finally", onFinallyItem);
		}
	}
}
