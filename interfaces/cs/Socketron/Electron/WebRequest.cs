using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Intercept and modify the contents of a request at various stages of its lifetime.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class WebRequest : NodeModule {
		public WebRequest(SocketronClient client) {
			_client = client;
		}

		public void onBeforeRequest(Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onBeforeRequest(JsonObject filter, Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onBeforeSendHeaders(Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onBeforeSendHeaders(JsonObject filter, Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onSendHeaders(Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onSendHeaders(JsonObject filter, Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onHeadersReceived(Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onHeadersReceived(JsonObject filter, Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onResponseStarted(Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onResponseStarted(JsonObject filter, Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onBeforeRedirect(Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onBeforeRedirect(JsonObject filter, Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onCompleted(Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onCompleted(JsonObject filter, Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onErrorOccurred(Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void onErrorOccurred(JsonObject filter, Action listener) {
			// TODO: implement this
			throw new NotImplementedException();
		}
	}
}
