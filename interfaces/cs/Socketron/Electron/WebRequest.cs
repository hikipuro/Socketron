using System;

namespace Socketron {
	/// <summary>
	/// Intercept and modify the contents of a request at various stages of its lifetime.
	/// <para>Process: Main</para>
	/// </summary>
	public class WebRequest {
		public void OnBeforeRequest() {
			throw new NotImplementedException();
		}

		public void OnBeforeSendHeaders() {
			throw new NotImplementedException();
		}

		public void OnSendHeaders() {
			throw new NotImplementedException();
		}

		public void OnHeadersReceived() {
			throw new NotImplementedException();
		}

		public void OnResponseStarted() {
			throw new NotImplementedException();
		}

		public void OnBeforeRedirect() {
			throw new NotImplementedException();
		}

		public void OnCompleted() {
			throw new NotImplementedException();
		}

		public void OnErrorOccurred() {
			throw new NotImplementedException();
		}
	}
}
