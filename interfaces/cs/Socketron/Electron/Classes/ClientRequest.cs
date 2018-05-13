using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Make HTTP/HTTPS requests.
	/// <para>Process: Main</para>
	/// <para>
	/// ClientRequest implements the Writable Stream interface and is therefore an EventEmitter.
	/// </para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class ClientRequest : EventEmitter {
		/// <summary>
		/// ClientRequest constructor options.
		/// </summary>
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
			/// Parse JSON text.
			/// </summary>
			/// <param name="text"></param>
			/// <returns></returns>
			public static Options Parse(string text) {
				return JSON.Parse<Options>(text);
			}

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
			/// <summary>
			/// Returns: response IncomingMessage - An object representing the HTTP response message.
			/// </summary>
			public const string response = "response";
			/// <summary>
			/// Emitted when an authenticating proxy is asking for user credentials.
			/// </summary>
			public const string login = "login";
			/// <summary>
			/// Emitted just after the last chunk of the request's data
			/// has been written into the request object.
			/// </summary>
			public const string finish = "finish";
			/// <summary>
			/// Emitted when the request is aborted.
			/// The abort event will not be fired if the request is already closed.
			/// </summary>
			public const string abort = "abort";
			/// <summary>
			/// Emitted when the net module fails to issue a network request.
			/// <para>
			/// Typically when the request object emits an error event,
			/// a close event will subsequently follow and no response object will be provided.
			/// </para>
			/// </summary>
			public const string error = "error";
			/// <summary>
			/// Emitted as the last event in the HTTP request-response transaction.
			/// <para>
			/// The close event indicates that no more events will be emitted
			/// on either the request or response objects.
			/// </para>
			/// </summary>
			public const string close = "close";
			/// <summary>
			/// Emitted when there is redirection and the mode is manual.
			/// Calling request.followRedirect will continue with the redirection.
			/// </summary>
			public const string redirect = "redirect";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the ClientRequest constructors,
		/// please use electron.ClientRequest.Create() method instead.
		/// </para>
		/// </summary>
		public ClientRequest() {
		}

		/// <summary>
		/// A Boolean specifying whether the request will use
		/// HTTP chunked transfer encoding or not.
		/// Defaults to false. 
		/// </summary>
		public bool chunkedEncoding {
			get { return API.GetProperty<bool>("chunkedEncoding"); }
		}

		/// <summary>
		/// Adds an extra HTTP header.
		/// <para>
		/// The header name will issued as it is without lowercasing.
		/// It can be called only before first write.
		/// Calling this method after the first write will throw an error.
		/// If the passed value is not a String, its toString() method
		/// will be called to obtain the final value.
		/// </para>
		/// </summary>
		/// <param name="name">An extra HTTP header name.</param>
		/// <param name="value">An extra HTTP header value.</param>
		public void setHeader(string name, string value) {
			API.Apply("setHeader", name, value);
		}

		/// <summary>
		/// Returns Object - The value of a previously set extra header name.
		/// </summary>
		/// <param name="name">Specify an extra header name.</param>
		/// <returns></returns>
		public JsonObject getHeader(string name) {
			object result = API.Apply("getHeader", name);
			return new JsonObject(result);
		}

		/// <summary>
		/// Removes a previously set extra header name.
		/// <para>
		/// This method can be called only before first write.
		/// Trying to call it after the first write will throw an error.
		/// </para>
		/// </summary>
		/// <param name="name">Specify an extra header name.</param>
		public void removeHeader(string name) {
			API.Apply("removeHeader", name);
		}

		/// <summary>
		/// callback is essentially a dummy function introduced in the purpose
		/// of keeping similarity with the Node.js API.
		/// It is called asynchronously in the next tick after chunk content
		/// have been delivered to the Chromium networking layer.
		/// Contrary to the Node.js implementation, it is not guaranteed that
		/// chunk content have been flushed on the wire before callback is called.
		/// <para>
		/// Adds a chunk of data to the request body.
		/// The first write operation may cause the request headers to be issued on the wire.
		/// After the first write operation, it is not allowed to add or remove a custom header.
		/// </para>
		/// </summary>
		/// <param name="chunk">
		/// A chunk of the request body's data.
		/// <para>
		/// If it is a string, it is converted into a Buffer using the specified encoding.
		/// </para>
		/// </param>
		/// <param name="encoding">
		/// (optional) Used to convert string chunks into Buffer objects.
		/// Defaults to 'utf-8'.
		/// </param>
		/// <param name="callback">
		/// (optional) Called after the write operation ends.
		/// </param>
		public void write(string chunk, string encoding, Action callback) {
			string eventName = "_write";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			API.Apply("write", chunk, encoding, item);
		}

		public void write(Buffer chunk, string encoding, Action callback) {
			string eventName = "_write";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			API.Apply("write", chunk, encoding, item);
		}

		public void write(string chunk, string encoding = null) {
			if (encoding == null) {
				API.Apply("write", chunk);
			} else {
				API.Apply("write", chunk, encoding);
			}
		}

		public void write(Buffer chunk, string encoding = null) {
			if (encoding == null) {
				API.Apply("write", chunk);
			} else {
				API.Apply("write", chunk, encoding);
			}
		}

		public void write(string chunk, Action callback) {
			string eventName = "_write";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			API.Apply("write", chunk, item);
		}

		public void write(Buffer chunk, Action callback) {
			string eventName = "_write";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			API.Apply("write", chunk, item);
		}

		/// <summary>
		/// Sends the last chunk of the request data.
		/// <para>
		/// Subsequent write or end operations will not be allowed.
		/// The finish event is emitted just after the end operation.
		/// </para>
		/// </summary>
		/// <param name="chunk">(optional)</param>
		/// <param name="encoding">(optional)</param>
		/// <param name="callback">(optional)</param>
		public void end(string chunk, string encoding, Action callback) {
			string eventName = "_end";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			API.Apply("end", chunk, encoding, item);
		}

		public void end(Buffer chunk, string encoding, Action callback) {
			string eventName = "_end";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			API.Apply("end", chunk, encoding, item);
		}

		public void end() {
			API.Apply("end");
		}

		public void end(string chunk, string encoding = null) {
			if (encoding == null) {
				API.Apply("end", chunk);
			} else {
				API.Apply("end", chunk, encoding);
			}
		}

		public void end(Buffer chunk, string encoding = null) {
			if (encoding == null) {
				API.Apply("end", chunk);
			} else {
				API.Apply("end", chunk, encoding);
			}
		}

		public void end(string chunk, Action callback) {
			string eventName = "_end";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			API.Apply("end", chunk, item);
		}

		public void end(Buffer chunk, Action callback) {
			string eventName = "_end";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			API.Apply("end", chunk, item);
		}


		/// <summary>
		/// Cancels an ongoing HTTP transaction.
		/// </summary>
		public void abort() {
			API.Apply("abort");
		}

		/// <summary>
		/// Continues any deferred redirection request when the redirection mode is manual.
		/// </summary>
		public void followRedirect() {
			API.Apply("followRedirect");
		}
	}
}
