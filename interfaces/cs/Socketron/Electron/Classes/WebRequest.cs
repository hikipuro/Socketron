using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Intercept and modify the contents of a request at various stages of its lifetime.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class WebRequest : EventEmitter {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public WebRequest() {
		}

		public void onBeforeRequest(Action<OnBeforeRequestDetails, Action<Response>> listener) {
			onBeforeRequest(null, listener);
		}

		public void onBeforeRequest(OnBeforeRequestFilter filter, Action<OnBeforeRequestDetails, Action<Response>> listener) {
			string eventName = "_onBeforeRequest";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				OnBeforeRequestDetails details = API.CreateObject<OnBeforeRequestDetails>(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<Response> callback = (response) => {
					_callback.API.Invoke(response);
				};
				listener?.Invoke(details, callback);
			});
			if (filter == null) {
				API.Apply("onBeforeRequest", item);
			} else {
				API.Apply("onBeforeRequest", filter, item);
			}
		}

		public void onBeforeSendHeaders(Action listener) {
			onBeforeSendHeaders(null, listener);
		}

		public void onBeforeSendHeaders(OnBeforeSendHeadersFilter filter, Action listener) {
			string eventName = "_onBeforeSendHeaders";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				listener?.Invoke();
			});
			if (filter == null) {
				API.Apply("onBeforeSendHeaders", item);
			} else {
				API.Apply("onBeforeSendHeaders", filter, item);
			}
		}

		public void onSendHeaders(Action<OnSendHeadersDetails> listener) {
			onSendHeaders(null, listener);
		}

		public void onSendHeaders(OnSendHeadersFilter filter, Action<OnSendHeadersDetails> listener) {
			string eventName = "_onSendHeaders";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				OnSendHeadersDetails details = API.CreateObject<OnSendHeadersDetails>(args[0]);
				listener?.Invoke(details);
			});
			if (filter == null) {
				API.Apply("onSendHeaders", item);
			} else {
				API.Apply("onSendHeaders", filter, item);
			}
		}

		public void onHeadersReceived(Action listener) {
			onHeadersReceived(null, listener);
		}

		public void onHeadersReceived(OnHeadersReceivedFilter filter, Action listener) {
			string eventName = "_onHeadersReceived";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				listener?.Invoke();
			});
			if (filter == null) {
				API.Apply("onHeadersReceived", item);
			} else {
				API.Apply("onHeadersReceived", filter, item);
			}
		}

		public void onResponseStarted(Action<OnResponseStartedDetails> listener) {
			onResponseStarted(null, listener);
		}

		public void onResponseStarted(OnResponseStartedFilter filter, Action<OnResponseStartedDetails> listener) {
			string eventName = "_onResponseStarted";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				OnResponseStartedDetails details = API.CreateObject<OnResponseStartedDetails>(args[0]);
				listener?.Invoke(details);
			});
			if (filter == null) {
				API.Apply("onResponseStarted", item);
			} else {
				API.Apply("onResponseStarted", filter, item);
			}
		}

		public void onBeforeRedirect(Action<OnBeforeRedirectDetails> listener) {
			onBeforeRedirect(null, listener);
		}

		public void onBeforeRedirect(OnBeforeRedirectFilter filter, Action<OnBeforeRedirectDetails> listener) {
			string eventName = "_onBeforeRedirect";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				OnBeforeRedirectDetails details = API.CreateObject<OnBeforeRedirectDetails>(args[0]);
				listener?.Invoke(details);
			});
			if (filter == null) {
				API.Apply("onBeforeRedirect", item);
			} else {
				API.Apply("onBeforeRedirect", filter, item);
			}
		}

		public void onCompleted(Action<OnCompletedDetails> listener) {
			onCompleted(null, listener);
		}

		public void onCompleted(OnCompletedFilter filter, Action<OnCompletedDetails> listener) {
			string eventName = "_onCompleted";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				OnCompletedDetails details = API.CreateObject<OnCompletedDetails>(args[0]);
				listener?.Invoke(details);
			});
			if (filter == null) {
				API.Apply("onCompleted", item);
			} else {
				API.Apply("onCompleted", filter, item);
			}
		}

		public void onErrorOccurred(Action<OnErrorOccurredDetails> listener) {
			onErrorOccurred(null, listener);
		}

		public void onErrorOccurred(OnErrorOccurredFilter filter, Action<OnErrorOccurredDetails> listener) {
			string eventName = "_onErrorOccurred";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				OnErrorOccurredDetails details = API.CreateObject<OnErrorOccurredDetails>(args[0]);
				listener?.Invoke(details);
			});
			if (filter == null) {
				API.Apply("onErrorOccurred", item);
			} else {
				API.Apply("onErrorOccurred", filter, item);
			}
		}
	}
}
