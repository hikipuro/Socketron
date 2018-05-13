using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Session.setCertificateVerifyProc() parameter.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class CertificateVerifyProcRequest : JSObject {
		public string hostname {
			get { return API.GetProperty<string>("hostname"); }
		}
		public Certificate certificate {
			get {
				// TODO: implement this
				throw new NotImplementedException();
				//object result = API.GetProperty<object>("certificate");
				//return Certificate.FromObject(result);
			}
		}
		/// <summary>
		/// Verification result from chromium.
		/// </summary>
		public string verificationResult {
			get { return API.GetProperty<string>("verificationResult"); }
		}
		/// <summary>
		/// Error code.
		/// </summary>
		public int errorCode {
			get { return API.GetProperty<int>("errorCode"); }
		}
	}

	/// <summary>
	/// Session.clearStorageData() options.
	/// </summary>
	public class ClearStorageDataOptions {
		/// <summary>
		/// Should follow window.location.origin’s representation scheme://host:port.
		/// </summary>
		public string origin;
		/// <summary>
		/// The types of storages to clear, can contain: appcache, cookies, filesystem,
		/// indexdb, localstorage, shadercache, websql, serviceworkers
		/// </summary>
		public string[] storages;
		/// <summary>
		/// The types of quotas to clear, can contain: temporary, persistent, syncable.
		/// </summary>
		public string[] quotas;
	}

	/// <summary>
	/// Session.setProxy() options.
	/// </summary>
	public class Config {
		/// <summary>
		/// The URL associated with the PAC file.
		/// </summary>
		public string pacScript;
		/// <summary>
		/// Rules indicating which proxies to use.
		/// </summary>
		public string proxyRules;
		/// <summary>
		/// Rules indicating which URLs should bypass the proxy settings.
		/// </summary>
		public string proxyBypassRules;
	}

	/// <summary>
	/// Session.createInterruptedDownload() options.
	/// </summary>
	public class CreateInterruptedDownloadOptions {
		/// <summary>
		/// Absolute path of the download.
		/// </summary>
		public string path;
		/// <summary>
		/// Complete URL chain for the download.
		/// </summary>
		public string[] urlChain;
		public string mimeType;
		/// <summary>
		/// Start range for the download.
		/// </summary>
		public int offset;
		/// <summary>
		/// Total length of the download.
		/// </summary>
		public int length;
		/// <summary>
		/// Last-Modified header value.
		/// </summary>
		public string lastModified;
		/// <summary>
		/// ETag header value.
		/// </summary>
		public string eTag;
		/// <summary>
		/// Time when download was started in number of seconds since UNIX epoch.
		/// </summary>
		public long startTime;
	}

	/// <summary>
	/// Session.enableNetworkEmulation() options.
	/// </summary>
	public class EnableNetworkEmulationOptions {
		/// <summary>
		/// Whether to emulate network outage. Defaults to false.
		/// </summary>
		public bool? offline;
		/// <summary>
		/// RTT in ms. Defaults to 0 which will disable latency throttling.
		/// </summary>
		public int? latency;
		/// <summary>
		/// Download rate in Bps. Defaults to 0 which will disable download throttling.
		/// </summary>
		public int? downloadThroughput;
		/// <summary>
		/// Upload rate in Bps. Defaults to 0 which will disable upload throttling.
		/// </summary>
		public int? uploadThroughput;
	}

	/// <summary>
	/// Session.fromPartition() options.
	/// </summary>
	public class FromPartitionOptions {
		/// <summary>
		/// Whether to enable cache.
		/// </summary>
		public bool cache;
	}
}
