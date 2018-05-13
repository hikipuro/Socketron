using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// WebRequest.onBeforeRequest() parameter.
	/// </summary>
	public class Response {
		public bool? cancel;
		/// <summary>
		/// The original request is prevented from being sent or completed
		/// and is instead redirected to the given URL.
		/// </summary>
		public string redirectURL;
	}

	/// <summary>
	/// WebRequest.onBeforeRedirect() parameter.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class OnBeforeRedirectDetails : JSObject {
		public int id {
			get { return API.GetProperty<int>("id"); }
		}
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public int? webContentsId {
			get { return API.GetProperty<int>("webContentsId"); }
		}
		public string resourceType {
			get { return API.GetProperty<string>("resourceType"); }
		}
		public double timestamp {
			get { return API.GetProperty<double>("timestamp"); }
		}
		public string redirectURL {
			get { return API.GetProperty<string>("redirectURL"); }
		}
		public int statusCode {
			get { return API.GetProperty<int>("statusCode"); }
		}
		/// <summary>
		/// The server IP address that the request was actually sent to.
		/// </summary>
		public string ip {
			get { return API.GetProperty<string>("ip"); }
		}
		public bool fromCache {
			get { return API.GetProperty<bool>("fromCache"); }
		}
		public JsonObject responseHeaders {
			get {
				object result = API.GetProperty<object>("responseHeaders");
				return new JsonObject(result);
			}
		}
	}

	/// <summary>
	/// WebRequest.onBeforeRedirect() options.
	/// </summary>
	public class OnBeforeRedirectFilter {
		/// <summary>
		/// Array of URL patterns that will be used to filter out the requests
		/// that do not match the URL patterns.
		/// </summary>
		public string[] urls;
	}

	/// <summary>
	/// WebRequest.onBeforeRequest() parameter.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class OnBeforeRequestDetails : JSObject {
		public int id {
			get { return API.GetProperty<int>("id"); }
		}
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public int? webContentsId {
			get { return API.GetProperty<int>("webContentsId"); }
		}
		public string resourceType {
			get { return API.GetProperty<string>("resourceType"); }
		}
		public double timestamp {
			get { return API.GetProperty<double>("timestamp"); }
		}
		public UploadData[] uploadData {
			get { return API.GetObjectList<UploadData>("uploadData"); }
		}
	}

	/// <summary>
	/// WebRequest.onBeforeRequest() options.
	/// </summary>
	public class OnBeforeRequestFilter {
		/// <summary>
		/// Array of URL patterns that will be used to filter out the requests
		/// that do not match the URL patterns.
		/// </summary>
		public string[] urls;
	}

	/// <summary>
	/// WebRequest.onBeforeSendHeaders() options.
	/// </summary>
	public class OnBeforeSendHeadersFilter {
		/// <summary>
		/// Array of URL patterns that will be used to filter out the requests
		/// that do not match the URL patterns.
		/// </summary>
		public string[] urls;
	}

	/// <summary>
	/// WebRequest.onCompleted() parameter.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class OnCompletedDetails : JSObject {
		public int id {
			get { return API.GetProperty<int>("id"); }
		}
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public int? webContentsId {
			get { return API.GetProperty<int>("webContentsId"); }
		}
		public string resourceType {
			get { return API.GetProperty<string>("resourceType"); }
		}
		public double timestamp {
			get { return API.GetProperty<double>("timestamp"); }
		}
		public JsonObject responseHeaders {
			get {
				object result = API.GetProperty<object>("responseHeaders");
				return new JsonObject(result);
			}
		}
		public bool fromCache {
			get { return API.GetProperty<bool>("fromCache"); }
		}
		public int statusCode {
			get { return API.GetProperty<int>("statusCode"); }
		}
		public string statusLine {
			get { return API.GetProperty<string>("statusLine"); }
		}
	}

	/// <summary>
	/// WebRequest.onCompleted() options.
	/// </summary>
	public class OnCompletedFilter {
		/// <summary>
		/// Array of URL patterns that will be used to filter out the requests
		/// that do not match the URL patterns.
		/// </summary>
		public string[] urls;
	}

	/// <summary>
	/// WebRequest.onErrorOccurred() parameter.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class OnErrorOccurredDetails : JSObject {
		public int id {
			get { return API.GetProperty<int>("id"); }
		}
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public int? webContentsId {
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
		public string error {
			get { return API.GetProperty<string>("error"); }
		}
	}

	/// <summary>
	/// WebRequest.onErrorOccurred() options.
	/// </summary>
	public class OnErrorOccurredFilter {
		/// <summary>
		/// Array of URL patterns that will be used to filter out the requests
		/// that do not match the URL patterns.
		/// </summary>
		public string[] urls;
	}

	/// <summary>
	/// WebRequest.onHeadersReceived() options.
	/// </summary>
	public class OnHeadersReceivedFilter {
		/// <summary>
		/// Array of URL patterns that will be used to filter out the requests
		/// that do not match the URL patterns.
		/// </summary>
		public string[] urls;
	}

	/// <summary>
	/// WebRequest.onResponseStarted() parameter.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class OnResponseStartedDetails : JSObject {
		public int id {
			get { return API.GetProperty<int>("id"); }
		}
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public int? webContentsId {
			get { return API.GetProperty<int>("webContentsId"); }
		}
		public string resourceType {
			get { return API.GetProperty<string>("resourceType"); }
		}
		public double timestamp {
			get { return API.GetProperty<double>("timestamp"); }
		}
		public JsonObject responseHeaders {
			get {
				object result = API.GetProperty<object>("responseHeaders");
				return new JsonObject(result);
			}
		}
		public bool fromCache {
			get { return API.GetProperty<bool>("fromCache"); }
		}
		public int statusCode {
			get { return API.GetProperty<int>("statusCode"); }
		}
		public string statusLine {
			get { return API.GetProperty<string>("statusLine"); }
		}
	}

	/// <summary>
	/// WebRequest.onResponseStarted() options.
	/// </summary>
	public class OnResponseStartedFilter {
		/// <summary>
		/// Array of URL patterns that will be used to filter out the requests
		/// that do not match the URL patterns.
		/// </summary>
		public string[] urls;
	}

	/// <summary>
	/// WebRequest.onSendHeaders() parameter.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class OnSendHeadersDetails : JSObject {
		public int id {
			get { return API.GetProperty<int>("id"); }
		}
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public int? webContentsId {
			get { return API.GetProperty<int>("webContentsId"); }
		}
		public string resourceType {
			get { return API.GetProperty<string>("resourceType"); }
		}
		public double timestamp {
			get { return API.GetProperty<double>("timestamp"); }
		}
		public JsonObject requestHeaders {
			get {
				object result = API.GetProperty<object>("requestHeaders");
				return new JsonObject(result);
			}
		}
	}

	/// <summary>
	/// WebRequest.onSendHeaders() options.
	/// </summary>
	public class OnSendHeadersFilter {
		/// <summary>
		/// Array of URL patterns that will be used to filter out the requests
		/// that do not match the URL patterns.
		/// </summary>
		public string[] urls;
	}
}
