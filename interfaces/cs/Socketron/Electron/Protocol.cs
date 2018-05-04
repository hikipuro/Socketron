using System;

namespace Socketron {
	/// <summary>
	/// Register a custom protocol and intercept existing protocol requests.
	/// <para>Process: Main</para>
	/// </summary>
	public class Protocol : NodeBase {
		public Protocol(Socketron socketron) {
			_socketron = socketron;
		}

		public void RegisterStandardSchemes() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void RegisterServiceWorkerSchemes() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void RegisterFileProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void RegisterBufferProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void RegisterStringProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void RegisterHttpProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void RegisterStreamProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void UnregisterProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void IsProtocolHandled() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void InterceptFileProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void InterceptStringProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void InterceptBufferProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void InterceptHttpProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void InterceptStreamProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void UninterceptProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}
	}
}
