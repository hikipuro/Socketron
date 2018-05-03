using System;

namespace Socketron {
	/// <summary>
	/// Register a custom protocol and intercept existing protocol requests.
	/// <para>Process: Main</para>
	/// </summary>
	public class Protocol : ElectronBase {
		public int id;

		public Protocol(Socketron socketron) {
			_socketron = socketron;
		}

		public void RegisterStandardSchemes() {
			throw new NotImplementedException();
		}

		public void RegisterServiceWorkerSchemes() {
			throw new NotImplementedException();
		}

		public void RegisterFileProtocol() {
			throw new NotImplementedException();
		}

		public void RegisterBufferProtocol() {
			throw new NotImplementedException();
		}

		public void RegisterStringProtocol() {
			throw new NotImplementedException();
		}

		public void RegisterHttpProtocol() {
			throw new NotImplementedException();
		}

		public void RegisterStreamProtocol() {
			throw new NotImplementedException();
		}

		public void UnregisterProtocol() {
			throw new NotImplementedException();
		}

		public void IsProtocolHandled() {
			throw new NotImplementedException();
		}

		public void InterceptFileProtocol() {
			throw new NotImplementedException();
		}

		public void InterceptStringProtocol() {
			throw new NotImplementedException();
		}

		public void InterceptBufferProtocol() {
			throw new NotImplementedException();
		}

		public void InterceptHttpProtocol() {
			throw new NotImplementedException();
		}

		public void InterceptStreamProtocol() {
			throw new NotImplementedException();
		}

		public void UninterceptProtocol() {
			throw new NotImplementedException();
		}
	}
}
