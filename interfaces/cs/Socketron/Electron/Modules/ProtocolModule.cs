using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Register a custom protocol and intercept existing protocol requests.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class ProtocolModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public ProtocolModule() {
		}

		/// <summary>
		/// A standard scheme adheres to what RFC 3986 calls generic URI syntax.
		/// For example http and https are standard schemes, while file is not.
		/// </summary>
		/// <param name="schemes">
		/// Custom schemes to be registered as standard schemes.
		/// </param>
		/// <param name="options"></param>
		public void registerStandardSchemes(string[] schemes, JsonObject options = null) {
			if (options == null) {
				API.Apply("registerStandardSchemes", schemes as object);
			} else {
				API.Apply("registerStandardSchemes", schemes, options);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="schemes">
		/// Custom schemes to be registered to handle service workers.
		/// </param>
		public void registerServiceWorkerSchemes(string[] schemes) {
			API.Apply("registerServiceWorkerSchemes", schemes as object);
		}

		/// <summary>
		/// Registers a protocol of scheme that will send the file as a response.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="handler"></param>
		/// <param name="completion"></param>
		public void registerFileProtocol(string scheme, Action<JsonObject, Action<string>> handler, Action<Error> completion = null) {
			string eventName = "_registerFileProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				JsonObject request = new JsonObject(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<string> callback = (filePath) => {
					_callback?.API.Invoke(filePath);
				};
				handler?.Invoke(request, callback);
			});
			if (completion == null) {
				API.Apply("registerFileProtocol", scheme, item);
			} else {
				string eventName2 = "_registerFileProtocol_completion";
				CallbackItem item2 = null;
				item2 = API.CreateCallbackItem(eventName2, (object[] args) => {
					Error error = API.CreateObject<Error>(args[0]);
					completion?.Invoke(error);
				});
				API.Apply("registerFileProtocol", scheme, item, item2);
			}
		}

		/// <summary>
		/// Registers a protocol of scheme that will send a Buffer as a response.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="handler"></param>
		/// <param name="completion"></param>
		public void registerBufferProtocol(string scheme, Action<JsonObject, Action<Buffer>> handler, Action<Error> completion = null) {
			string eventName = "_registerBufferProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				JsonObject request = new JsonObject(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<Buffer> callback = (buffer) => {
					_callback?.API.Invoke(buffer);
				};
				handler?.Invoke(request, callback);
			});
			if (completion == null) {
				API.Apply("registerBufferProtocol", scheme, item);
			} else {
				string eventName2 = "_registerBufferProtocol_completion";
				CallbackItem item2 = null;
				item2 = API.CreateCallbackItem(eventName2, (object[] args) => {
					Error error = API.CreateObject<Error>(args[0]);
					completion?.Invoke(error);
				});
				API.Apply("registerBufferProtocol", scheme, item, item2);
			}
		}

		/// <summary>
		/// Registers a protocol of scheme that will send a Buffer as a response.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="handler"></param>
		/// <param name="completion"></param>
		public void registerBufferProtocol(string scheme, Action<JsonObject, Action<MimeTypedBuffer>> handler, Action<Error> completion = null) {
			string eventName = "_registerBufferProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				JsonObject request = new JsonObject(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<MimeTypedBuffer> callback = (buffer) => {
					_callback?.API.Invoke(buffer);
				};
				handler?.Invoke(request, callback);
			});
			if (completion == null) {
				API.Apply("registerBufferProtocol", scheme, item);
			} else {
				string eventName2 = "_registerBufferProtocol_completion";
				CallbackItem item2 = null;
				item2 = API.CreateCallbackItem(eventName2, (object[] args) => {
					Error error = API.CreateObject<Error>(args[0]);
					completion?.Invoke(error);
				});
				API.Apply("registerBufferProtocol", scheme, item, item2);
			}
		}

		/// <summary>
		/// Registers a protocol of scheme that will send a String as a response.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="handler"></param>
		/// <param name="completion"></param>
		public void registerStringProtocol(string scheme, Action<JsonObject, Action<string>> handler, Action<Error> completion = null) {
			string eventName = "_registerStringProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				JsonObject request = new JsonObject(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<string> callback = (data) => {
					_callback?.API.Invoke(data);
				};
				handler?.Invoke(request, callback);
			});
			if (completion == null) {
				API.Apply("registerStringProtocol", scheme, item);
			} else {
				string eventName2 = "_registerStringProtocol_completion";
				CallbackItem item2 = null;
				item2 = API.CreateCallbackItem(eventName2, (object[] args) => {
					Error error = API.CreateObject<Error>(args[0]);
					completion?.Invoke(error);
				});
				API.Apply("registerStringProtocol", scheme, item, item2);
			}
		}

		/// <summary>
		/// Registers a protocol of scheme that will send an HTTP request as a response.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="handler"></param>
		/// <param name="completion"></param>
		public void registerHttpProtocol(string scheme, Action<JsonObject, Action<JsonObject>> handler, Action<Error> completion = null) {
			string eventName = "_registerHttpProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				JsonObject request = new JsonObject(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<JsonObject> callback = (redirectRequest) => {
					_callback?.API.Invoke(redirectRequest);
				};
				handler?.Invoke(request, callback);
			});
			if (completion == null) {
				API.Apply("registerHttpProtocol", scheme, item);
			} else {
				string eventName2 = "_registerHttpProtocol_completion";
				CallbackItem item2 = null;
				item2 = API.CreateCallbackItem(eventName2, (object[] args) => {
					Error error = API.CreateObject<Error>(args[0]);
					completion?.Invoke(error);
				});
				API.Apply("registerHttpProtocol", scheme, item, item2);
			}
		}

		/// <summary>
		/// Registers a protocol of scheme that will send a Readable as a response.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="handler"></param>
		/// <param name="completion"></param>
		public void registerStreamProtocol(string scheme, Action<JsonObject, Action<Readable>> handler, Action<Error> completion = null) {
			string eventName = "_registerStreamProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				JsonObject request = new JsonObject(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<Readable> callback = (stream) => {
					_callback?.API.Invoke(stream);
				};
				handler?.Invoke(request, callback);
			});
			if (completion == null) {
				API.Apply("registerStreamProtocol", scheme, item);
			} else {
				string eventName2 = "_registerStreamProtocol_completion";
				CallbackItem item2 = null;
				item2 = API.CreateCallbackItem(eventName2, (object[] args) => {
					Error error = API.CreateObject<Error>(args[0]);
					completion?.Invoke(error);
				});
				API.Apply("registerStreamProtocol", scheme, item, item2);
			}
		}

		/// <summary>
		/// Registers a protocol of scheme that will send a Readable as a response.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="handler"></param>
		/// <param name="completion"></param>
		public void registerStreamProtocol(string scheme, Action<JsonObject, Action<StreamProtocolResponse>> handler, Action<Error> completion = null) {
			string eventName = "_registerStreamProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				JsonObject request = new JsonObject(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<StreamProtocolResponse> callback = (stream) => {
					_callback?.API.Invoke(stream);
				};
				handler?.Invoke(request, callback);
			});
			if (completion == null) {
				API.Apply("registerStreamProtocol", scheme, item);
			} else {
				string eventName2 = "_registerStreamProtocol_completion";
				CallbackItem item2 = null;
				item2 = API.CreateCallbackItem(eventName2, (object[] args) => {
					Error error = API.CreateObject<Error>(args[0]);
					completion?.Invoke(error);
				});
				API.Apply("registerStreamProtocol", scheme, item, item2);
			}
		}

		/// <summary>
		/// Unregisters the custom protocol of scheme.
		/// </summary>
		/// <param name="scheme"></param>
		public void unregisterProtocol(string scheme) {
			API.Apply("unregisterProtocol", scheme);
		}

		/// <summary>
		/// Unregisters the custom protocol of scheme.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="completion"></param>
		public void unregisterProtocol(string scheme, Action<Error> completion) {
			string eventName = "_unregisterProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Error error = API.CreateObject<Error>(args[0]);
				completion?.Invoke(error);
			});
			API.Apply("unregisterProtocol", scheme, item);
		}

		/// <summary>
		/// The callback will be called with a boolean that indicates
		/// whether there is already a handler for scheme.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="callback"></param>
		public void isProtocolHandled(string scheme, Action<Error> callback) {
			string eventName = "_isProtocolHandled";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Error error = API.CreateObject<Error>(args[0]);
				callback?.Invoke(error);
			});
			API.Apply("isProtocolHandled", scheme, item);
		}

		/// <summary>
		/// Intercepts scheme protocol and uses handler
		/// as the protocol's new handler which sends a file as a response.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="handler"></param>
		/// <param name="completion"></param>
		public void interceptFileProtocol(string scheme, Action<JsonObject, Action<string>> handler, Action<Error> completion = null) {
			string eventName = "_interceptFileProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				JsonObject request = new JsonObject(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<string> callback = (filePath) => {
					_callback?.API.Invoke(filePath);
				};
				handler?.Invoke(request, callback);
			});
			if (completion == null) {
				API.Apply("interceptFileProtocol", scheme, item);
			} else {
				string eventName2 = "_interceptFileProtocol_completion";
				CallbackItem item2 = null;
				item2 = API.CreateCallbackItem(eventName2, (object[] args) => {
					Error error = API.CreateObject<Error>(args[0]);
					completion?.Invoke(error);
				});
				API.Apply("interceptFileProtocol", scheme, item, item2);
			}
		}

		/// <summary>
		/// Intercepts scheme protocol and uses handler
		/// as the protocol's new handler which sends a String as a response.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="handler"></param>
		/// <param name="completion"></param>
		public void interceptStringProtocol(string scheme, Action<JsonObject, Action<string>> handler, Action<Error> completion = null) {
			string eventName = "_interceptStringProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				JsonObject request = new JsonObject(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<string> callback = (data) => {
					_callback?.API.Invoke(data);
				};
				handler?.Invoke(request, callback);
			});
			if (completion == null) {
				API.Apply("interceptStringProtocol", scheme, item);
			} else {
				string eventName2 = "_interceptStringProtocol_completion";
				CallbackItem item2 = null;
				item2 = API.CreateCallbackItem(eventName2, (object[] args) => {
					Error error = API.CreateObject<Error>(args[0]);
					completion?.Invoke(error);
				});
				API.Apply("interceptStringProtocol", scheme, item, item2);
			}
		}

		/// <summary>
		/// Intercepts scheme protocol and uses handler
		/// as the protocol's new handler which sends a Buffer as a response.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="handler"></param>
		/// <param name="completion"></param>
		public void interceptBufferProtocol(string scheme, Action<JsonObject, Action<Buffer>> handler, Action<Error> completion = null) {
			string eventName = "_interceptBufferProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				JsonObject request = new JsonObject(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<Buffer> callback = (buffer) => {
					_callback?.API.Invoke(buffer);
				};
				handler?.Invoke(request, callback);
			});
			if (completion == null) {
				API.Apply("interceptBufferProtocol", scheme, item);
			} else {
				string eventName2 = "_interceptBufferProtocol_completion";
				CallbackItem item2 = null;
				item2 = API.CreateCallbackItem(eventName2, (object[] args) => {
					Error error = API.CreateObject<Error>(args[0]);
					completion?.Invoke(error);
				});
				API.Apply("interceptBufferProtocol", scheme, item, item2);
			}
		}

		/// <summary>
		/// Intercepts scheme protocol and uses handler
		/// as the protocol's new handler which sends a new HTTP request as a response.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="handler"></param>
		/// <param name="completion"></param>
		public void interceptHttpProtocol(string scheme, Action<JsonObject, Action<JsonObject>> handler, Action<Error> completion = null) {
			string eventName = "_interceptHttpProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				JsonObject request = new JsonObject(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<JsonObject> callback = (redirectRequest) => {
					_callback?.API.Invoke(redirectRequest);
				};
				handler?.Invoke(request, callback);
			});
			if (completion == null) {
				API.Apply("interceptHttpProtocol", scheme, item);
			} else {
				string eventName2 = "_interceptHttpProtocol_completion";
				CallbackItem item2 = null;
				item2 = API.CreateCallbackItem(eventName2, (object[] args) => {
					Error error = API.CreateObject<Error>(args[0]);
					completion?.Invoke(error);
				});
				API.Apply("interceptHttpProtocol", scheme, item, item2);
			}
		}

		/// <summary>
		/// Same as protocol.registerStreamProtocol,
		/// except that it replaces an existing protocol handler.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="handler"></param>
		/// <param name="completion"></param>
		public void interceptStreamProtocol(string scheme, Action<JsonObject, Action<Readable>> handler, Action<Error> completion = null) {
			string eventName = "_interceptStreamProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				JsonObject request = new JsonObject(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<Readable> callback = (stream) => {
					_callback?.API.Invoke(stream);
				};
				handler?.Invoke(request, callback);
			});
			if (completion == null) {
				API.Apply("interceptStreamProtocol", scheme, item);
			} else {
				string eventName2 = "_interceptStreamProtocol_completion";
				CallbackItem item2 = null;
				item2 = API.CreateCallbackItem(eventName2, (object[] args) => {
					Error error = API.CreateObject<Error>(args[0]);
					completion?.Invoke(error);
				});
				API.Apply("interceptStreamProtocol", scheme, item, item2);
			}
		}

		/// <summary>
		/// Same as protocol.registerStreamProtocol,
		/// except that it replaces an existing protocol handler.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="handler"></param>
		/// <param name="completion"></param>
		public void interceptStreamProtocol(string scheme, Action<JsonObject, Action<StreamProtocolResponse>> handler, Action<Error> completion = null) {
			string eventName = "_interceptStreamProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				JsonObject request = new JsonObject(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<StreamProtocolResponse> callback = (stream) => {
					_callback?.API.Invoke(stream);
				};
				handler?.Invoke(request, callback);
			});
			if (completion == null) {
				API.Apply("interceptStreamProtocol", scheme, item);
			} else {
				string eventName2 = "_interceptStreamProtocol_completion";
				CallbackItem item2 = null;
				item2 = API.CreateCallbackItem(eventName2, (object[] args) => {
					Error error = API.CreateObject<Error>(args[0]);
					completion?.Invoke(error);
				});
				API.Apply("interceptStreamProtocol", scheme, item, item2);
			}
		}

		/// <summary>
		/// Remove the interceptor installed for scheme and restore its original handler.
		/// </summary>
		/// <param name="scheme"></param>
		public void uninterceptProtocol(string scheme) {
			API.Apply("uninterceptProtocol", scheme);
		}

		/// <summary>
		/// Remove the interceptor installed for scheme and restore its original handler.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="completion"></param>
		public void uninterceptProtocol(string scheme, Action<Error> completion) {
			string eventName = "_uninterceptProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Error error = API.CreateObject<Error>(args[0]);
				completion?.Invoke(error);
			});
			API.Apply("uninterceptProtocol", scheme, item);
		}
	}
}
