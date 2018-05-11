using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Register a custom protocol and intercept existing protocol requests.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class ProtocolModule : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public ProtocolModule() {
		}

		public void registerStandardSchemes(string[] schemes, JsonObject options = null) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerServiceWorkerSchemes(string[] schemes) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerFileProtocol(string scheme, Action handler, Action completion = null) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerBufferProtocol(string scheme, Action handler, Action completion = null) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerStringProtocol(string scheme, Action handler, Action completion = null) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerHttpProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerStreamProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Unregisters the custom protocol of scheme.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="completion"></param>
		public void unregisterProtocol(string scheme, Action<Error> completion = null) {
			string eventName = "_unregisterProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Error error = null;
				if (args[0] != null) {
					error = API.CreateObject<Error>((int)args[0]);
				}
				completion?.Invoke(error);
			});
			API.Apply("unregisterProtocol", scheme, item);
		}

		public void isProtocolHandled(string scheme, Action<string> callback) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void interceptFileProtocol(string scheme) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void interceptStringProtocol(string scheme) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void interceptBufferProtocol(string scheme) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void interceptHttpProtocol(string scheme) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void interceptStreamProtocol(string scheme, Action<JsonObject, Action<object>> handler, Action<Error> completion = null) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Remove the interceptor installed for scheme and restore its original handler.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="completion">(optional)</param>
		public void uninterceptProtocol(string scheme, Action<Error> completion = null) {
			string eventName = "_uninterceptProtocol";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Error error = null;
				if (args[0] != null) {
					error = API.CreateObject<Error>((int)args[0]);
				}
				completion?.Invoke(error);
			});
			API.Apply("uninterceptProtocol", scheme, item);
		}
	}
}
