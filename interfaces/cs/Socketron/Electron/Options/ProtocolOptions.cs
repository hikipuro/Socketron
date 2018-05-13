using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Protocol.interceptHttpProtocol(),
	/// Protocol.registerHttpProtocol() parameter.
	/// </summary>
	public class RedirectRequest {
		public string url;
		public string method;
		public Session session;
		public UploadData uploadData;
	}

	/// <summary>
	/// Protocol.registerBufferProtocol() parameter.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class RegisterBufferProtocolRequest : JSObject {
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public string referrer {
			get { return API.GetProperty<string>("referrer"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public UploadData[] uploadData {
			get { return API.GetObjectList<UploadData>("uploadData"); }
		}
	}

	/// <summary>
	/// Protocol.registerFileProtocol() parameter.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class RegisterFileProtocolRequest : JSObject {
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public string referrer {
			get { return API.GetProperty<string>("referrer"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public UploadData[] uploadData {
			get { return API.GetObjectList<UploadData>("uploadData"); }
		}
	}

	/// <summary>
	/// Protocol.registerHttpProtocol() parameter.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class RegisterHttpProtocolRequest : JSObject {
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public string referrer {
			get { return API.GetProperty<string>("referrer"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public UploadData[] uploadData {
			get { return API.GetObjectList<UploadData>("uploadData"); }
		}
	}

	/// <summary>
	/// Protocol.registerStandardSchemes() options.
	/// </summary>
	public class RegisterStandardSchemesOptions {
		/// <summary>
		/// true to register the scheme as secure. Default false.
		/// </summary>
		public bool? secure;
	}

	/// <summary>
	/// Protocol.registerStreamProtocol() options.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class RegisterStreamProtocolRequest : JSObject {
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public JsonObject headers {
			get {
				object result = API.GetProperty<object>("headers");
				return new JsonObject(result);
			}
		}
		public string referrer {
			get { return API.GetProperty<string>("referrer"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public UploadData[] uploadData {
			get { return API.GetObjectList<UploadData>("uploadData"); }
		}
	}

	/// <summary>
	/// Protocol.registerStringProtocol() options.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class RegisterStringProtocolRequest : JSObject {
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public string referrer {
			get { return API.GetProperty<string>("referrer"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public UploadData[] uploadData {
			get { return API.GetObjectList<UploadData>("uploadData"); }
		}
	}

	/// <summary>
	/// Protocol.interceptBufferProtocol() options.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class InterceptBufferProtocolRequest : JSObject {
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public string referrer {
			get { return API.GetProperty<string>("referrer"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public UploadData[] uploadData {
			get { return API.GetObjectList<UploadData>("uploadData"); }
		}
	}

	/// <summary>
	/// Protocol.interceptBufferProtocol() options.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class InterceptFileProtocolRequest : JSObject {
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public string referrer {
			get { return API.GetProperty<string>("referrer"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public UploadData[] uploadData {
			get { return API.GetObjectList<UploadData>("uploadData"); }
		}
	}

	/// <summary>
	/// Protocol.interceptBufferProtocol() options.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class InterceptHttpProtocolRequest : JSObject {
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public string referrer {
			get { return API.GetProperty<string>("referrer"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public UploadData[] uploadData {
			get { return API.GetObjectList<UploadData>("uploadData"); }
		}
	}

	/// <summary>
	/// Protocol.interceptStreamProtocol() options.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class InterceptStreamProtocolRequest : JSObject {
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public JsonObject headers {
			get {
				object result = API.GetProperty<object>("headers");
				return new JsonObject(result);
			}
		}
		public string referrer {
			get { return API.GetProperty<string>("referrer"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public UploadData[] uploadData {
			get { return API.GetObjectList<UploadData>("uploadData"); }
		}
	}

	/// <summary>
	/// Protocol.interceptBufferProtocol() options.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class InterceptStringProtocolRequest : JSObject {
		public string url {
			get { return API.GetProperty<string>("url"); }
		}
		public string referrer {
			get { return API.GetProperty<string>("referrer"); }
		}
		public string method {
			get { return API.GetProperty<string>("method"); }
		}
		public UploadData[] uploadData {
			get { return API.GetObjectList<UploadData>("uploadData"); }
		}
	}
}
