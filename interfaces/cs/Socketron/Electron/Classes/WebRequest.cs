using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Intercept and modify the contents of a request at various stages of its lifetime.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class WebRequest : JSObject {
		public class Details : JSObject {
			public int id {
				get { return API.GetProperty<int>("id"); }
			}
			public string url {
				get { return API.GetProperty<string>("url"); }
			}
			public string method {
				get { return API.GetProperty<string>("method"); }
			}
			public int webContentsId {
				get { return API.GetProperty<int>("webContentsId"); }
			}
			public string resourceType {
				get { return API.GetProperty<string>("resourceType"); }
			}
			public double timestamp {
				get { return API.GetProperty<double>("timestamp"); }
			}
			public bool fromCache {
				get { return API.GetProperty<bool>("fromCache"); }
			}
			public string statusLine {
				get { return API.GetProperty<string>("statusLine"); }
			}
			public int statusCode {
				get { return API.GetProperty<int>("statusCode"); }
			}
			public string redirectURL {
				get { return API.GetProperty<string>("redirectURL"); }
			}
			public string ip {
				get { return API.GetProperty<string>("ip"); }
			}
			public string error {
				get { return API.GetProperty<string>("error"); }
			}
			public UploadData[] uploadData {
				get { return API.GetObjectList<UploadData>("uploadData"); }
			}
			public JsonObject requestHeaders {
				get {
					object result = API.GetProperty<object>("requestHeaders");
					return new JsonObject(result);
				}
			}
			public JsonObject responseHeaders {
				get {
					object result = API.GetProperty<object>("responseHeaders");
					return new JsonObject(result);
				}
			}
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public WebRequest() {
		}

		public void onBeforeRequest(Action<Details, Action<JsonObject>> listener) {
			onBeforeRequest(null, listener);
		}

		public void onBeforeRequest(JsonObject filter, Action<Details, Action<JsonObject>> listener) {
			string eventName = "_onBeforeRequest";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Details details = API.CreateObject<Details>(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<JsonObject> callback = (response) => {
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

		public void onBeforeSendHeaders(Action<Details, Action<JsonObject>> listener) {
			onBeforeSendHeaders(null, listener);
		}

		public void onBeforeSendHeaders(JsonObject filter, Action<Details, Action<JsonObject>> listener) {
			string eventName = "_onBeforeSendHeaders";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Details details = API.CreateObject<Details>(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<JsonObject> callback = (response) => {
					_callback.API.Invoke(response);
				};
				listener?.Invoke(details, callback);
			});
			if (filter == null) {
				API.Apply("onBeforeSendHeaders", item);
			} else {
				API.Apply("onBeforeSendHeaders", filter, item);
			}
		}

		public void onSendHeaders(Action<Details> listener) {
			onSendHeaders(null, listener);
		}

		public void onSendHeaders(JsonObject filter, Action<Details> listener) {
			string eventName = "_onSendHeaders";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Details details = API.CreateObject<Details>(args[0]);
				listener?.Invoke(details);
			});
			if (filter == null) {
				API.Apply("onSendHeaders", item);
			} else {
				API.Apply("onSendHeaders", filter, item);
			}
		}

		public void onHeadersReceived(Action<Details, Action<JsonObject>> listener) {
			onHeadersReceived(null, listener);
		}

		public void onHeadersReceived(JsonObject filter, Action<Details, Action<JsonObject>> listener) {
			string eventName = "_onHeadersReceived";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Details details = API.CreateObject<Details>(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<JsonObject> callback = (response) => {
					_callback.API.Invoke(response);
				};
				listener?.Invoke(details, callback);
			});
			if (filter == null) {
				API.Apply("onHeadersReceived", item);
			} else {
				API.Apply("onHeadersReceived", filter, item);
			}
		}

		public void onResponseStarted(Action<Details> listener) {
			onResponseStarted(null, listener);
		}

		public void onResponseStarted(JsonObject filter, Action<Details> listener) {
			string eventName = "_onResponseStarted";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Details details = API.CreateObject<Details>(args[0]);
				listener?.Invoke(details);
			});
			if (filter == null) {
				API.Apply("onResponseStarted", item);
			} else {
				API.Apply("onResponseStarted", filter, item);
			}
		}

		public void onBeforeRedirect(Action<Details> listener) {
			onBeforeRedirect(null, listener);
		}

		public void onBeforeRedirect(JsonObject filter, Action<Details> listener) {
			string eventName = "_onBeforeRedirect";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Details details = API.CreateObject<Details>(args[0]);
				listener?.Invoke(details);
			});
			if (filter == null) {
				API.Apply("onBeforeRedirect", item);
			} else {
				API.Apply("onBeforeRedirect", filter, item);
			}
		}

		public void onCompleted(Action<Details> listener) {
			onCompleted(null, listener);
		}

		public void onCompleted(JsonObject filter, Action<Details> listener) {
			string eventName = "_onCompleted";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Details details = API.CreateObject<Details>(args[0]);
				listener?.Invoke(details);
			});
			if (filter == null) {
				API.Apply("onCompleted", item);
			} else {
				API.Apply("onCompleted", filter, item);
			}
		}

		public void onErrorOccurred(Action<Details> listener) {
			onErrorOccurred(null, listener);
		}

		public void onErrorOccurred(JsonObject filter, Action<Details> listener) {
			string eventName = "_onErrorOccurred";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Details details = API.CreateObject<Details>(args[0]);
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
