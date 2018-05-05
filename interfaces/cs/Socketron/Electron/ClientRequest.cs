using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Make HTTP/HTTPS requests.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class ClientRequest {
		public bool chunkedEncoding;

		public class Options {
			/// <summary>
			/// (optional) The HTTP request method. Defaults to the GET method.
			/// </summary>
			public string method;
			/// <summary>
			/// (optional) The request URL.
			/// Must be provided in the absolute form
			/// with the protocol scheme specified as http or https.
			/// </summary>
			public string url;
			/// <summary>
			/// (optional) The Session instance with which the request is associated.
			/// </summary>
			public Session session;
			/// <summary>
			/// (optional) The name of the partition with which the request is associated.
			/// Defaults to the empty string. The session option prevails on partition.
			/// Thus if a session is explicitly specified, partition is ignored.
			/// </summary>
			public string partition;
			/// <summary>
			/// (optional) The protocol scheme in the form 'scheme:'.
			/// Currently supported values are 'http:' or 'https:'. Defaults to 'http:'.
			/// </summary>
			public string protocol;
			/// <summary>
			/// (optional) The server host provided as a concatenation of the hostname
			/// and the port number 'hostname:port'.
			/// </summary>
			public string host;
			/// <summary>
			/// (optional) The server host name.
			/// </summary>
			public string hostname;
			/// <summary>
			/// (optional) The server's listening port number.
			/// </summary>
			public int? port;
			/// <summary>
			/// (optional) The path part of the request URL.
			/// </summary>
			public string path;
			/// <summary>
			/// (optional) The redirect mode for this request.
			/// Should be one of follow, error or manual.
			/// Defaults to follow.
			/// When mode is error, any redirection will be aborted.
			/// When mode is manual the redirection will be deferred
			/// until request.followRedirect is invoked.
			/// Listen for the redirect event in this mode
			/// to get more details about the redirect request.
			/// </summary>
			public string redirect;

			/// <summary>
			/// Create JSON text.
			/// </summary>
			/// <returns></returns>
			public string Stringify() {
				return JSON.Stringify(this);
			}
		}

		/// <summary>
		/// ClientRequest instance events.
		/// </summary>
		public class Events {
			public const string response = "response";
			public const string login = "login";
			public const string finish = "finish";
			public const string abort = "abort";
			public const string error = "error";
			public const string close = "close";
			public const string redirect = "redirect";
		}

		public void setHeader(string name, string value) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void getHeader(string name) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void removeHeader(string name) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void write(string chunk) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void end() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void abort() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void followRedirect() {
			// TODO: implement this
			throw new NotImplementedException();
		}
	}
}
