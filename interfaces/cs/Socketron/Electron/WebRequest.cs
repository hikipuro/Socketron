using System;

namespace Socketron {
	/// <summary>
	/// Intercept and modify the contents of a request at various stages of its lifetime.
	/// <para>Process: Main</para>
	/// </summary>
	public class WebRequest : NodeModule {
		public WebRequest(SocketronClient client) {
			_client = client;
		}

		public void OnBeforeRequest() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void OnBeforeSendHeaders() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void OnSendHeaders() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void OnHeadersReceived() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void OnResponseStarted() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void OnBeforeRedirect() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void OnCompleted() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void OnErrorOccurred() {
			// TODO: implement this
			throw new NotImplementedException();
		}
	}
}
