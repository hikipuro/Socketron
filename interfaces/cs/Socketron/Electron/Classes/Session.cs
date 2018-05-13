using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Manage browser sessions, cookies, cache, proxy settings, etc.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Session : EventEmitter {
		/// <summary>
		/// Session instance events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when Electron is about to download item in webContents.
			/// </summary>
			public const string WillDownload = "will-download";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public Session() {
		}

		/// <summary>
		/// A Cookies object for this session.
		/// </summary>
		public Cookies cookies {
			get { return API.GetObject<Cookies>("cookies"); }
		}

		/// <summary>
		/// A WebRequest object for this session.
		/// </summary>
		public WebRequest webRequest {
			get { return API.GetObject<WebRequest>("webRequest"); }
		}

		/// <summary>
		/// A Protocol object for this session.
		/// </summary>
		public Protocol protocol {
			get { return API.GetObject<Protocol>("protocol"); }
		}

		/// <summary>
		/// Callback is invoked with the session's current cache size.
		/// </summary>
		/// <param name="callback"></param>
		public void getCacheSize(Action<int> callback) {
			string eventName = "_getCacheSize";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				int size = Convert.ToInt32(args[0]);
				callback?.Invoke(size);
			});
			API.Apply("getCacheSize", item);
		}

		/// <summary>
		/// Clears the session’s HTTP cache.
		/// </summary>
		/// <param name="callback"></param>
		public void clearCache(Action callback) {
			string eventName = "_clearCache";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				callback?.Invoke();
			});
			API.Apply("clearCache", item);
		}

		/// <summary>
		/// Clears the data of web storages.
		/// </summary>
		public void clearStorageData() {
			API.Apply("clearStorageData");
		}

		/// <summary>
		/// Clears the data of web storages.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="callback"></param>
		public void clearStorageData(ClearStorageDataOptions options, Action callback) {
			string eventName = "_clearStorageData";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				callback?.Invoke();
			});
			API.Apply("clearStorageData", options, item);
		}

		/// <summary>
		/// Writes any unwritten DOMStorage data to disk.
		/// </summary>
		public void flushStorageData() {
			API.Apply("flushStorageData");
		}

		/// <summary>
		/// Sets the proxy settings.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="callback"></param>
		public void setProxy(Config config, Action callback) {
			string eventName = "_setProxy";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				callback?.Invoke();
			});
			API.Apply("setProxy", config, item);
		}

		/// <summary>
		/// Resolves the proxy information for url.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="callback"></param>
		public void resolveProxy(string url, Action<string> callback) {
			string eventName = "_resolveProxy";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				string proxy = Convert.ToString(args[0]);
				callback?.Invoke(proxy);
			});
			API.Apply("resolveProxy", url, item);
		}

		/// <summary>
		/// Sets download saving directory.
		/// By default, the download directory will be the Downloads under the respective app folder.
		/// </summary>
		/// <param name="path">The download location.</param>
		public void setDownloadPath(string path) {
			API.Apply("setDownloadPath", path);
		}

		/// <summary>
		/// Emulates network with the given configuration for the session.
		/// </summary>
		/// <param name="options"></param>
		public void enableNetworkEmulation(EnableNetworkEmulationOptions options) {
			API.Apply("enableNetworkEmulation", options);
		}

		/// <summary>
		/// Disables any network emulation already active for the session.
		/// Resets to the original network configuration.
		/// </summary>
		public void disableNetworkEmulation() {
			API.Apply("disableNetworkEmulation");
		}

		/// <summary>
		/// Sets the certificate verify proc for session,
		/// the proc will be called with proc(request, callback)
		/// whenever a server certificate verification is requested.
		/// Calling callback(0) accepts the certificate, calling callback(-2) rejects it.
		/// </summary>
		public void setCertificateVerifyProc(Action<CertificateVerifyProcRequest, Action<int>> proc) {
			string eventName = "_setCertificateVerifyProc";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				CertificateVerifyProcRequest request = API.CreateObject<CertificateVerifyProcRequest>(args[0]);
				JSObject _callback = API.CreateObject<JSObject>(args[1]);
				Action<int> callback = (verificationResult) => {
					_callback?.API.Invoke(verificationResult);
				};
				proc?.Invoke(request, callback);
			});
			API.Apply("setCertificateVerifyProc", item);
		}

		/// <summary>
		/// Sets the handler which can be used to respond to permission requests for the session.
		/// Calling callback(true) will allow the permission and callback(false) will reject it.
		/// To clear the handler, call setPermissionRequestHandler(null).
		/// </summary>
		public void setPermissionRequestHandler(Action<WebContents, string, Action<bool>, JsonObject> handler) {
			string eventName = "_setPermissionRequestHandler";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				WebContents webContents = API.CreateObject<WebContents>(args[0]);
				string permission = Convert.ToString(args[1]);
				JSObject _callback = API.CreateObject<JSObject>(args[2]);
				JsonObject details = new JsonObject(args[3]);
				Action<bool> callback = (permissionGranted) => {
					_callback?.API.Invoke(permissionGranted);
				};
				handler?.Invoke(webContents, permission, callback, details);
			});
			API.Apply("setPermissionRequestHandler", item);
		}

		/// <summary>
		/// Clears the host resolver cache.
		/// </summary>
		public void clearHostResolverCache() {
			API.Apply("clearHostResolverCache");
		}

		/// <summary>
		/// Clears the host resolver cache.
		/// </summary>
		/// <param name="callback">Called when operation is done.</param>
		public void clearHostResolverCache(Action callback) {
			string eventName = "_clearHostResolverCache";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			API.Apply("clearHostResolverCache", item);
		}

		/// <summary>
		/// Dynamically sets whether to always send credentials for HTTP NTLM or Negotiate authentication.
		/// </summary>
		/// <param name="domains">
		/// A comma-separated list of servers for which integrated authentication is enabled.
		/// </param>
		public void allowNTLMCredentialsForDomains(string domains) {
			API.Apply("allowNTLMCredentialsForDomains", domains);
		}

		/// <summary>
		/// Overrides the userAgent and acceptLanguages for this session.
		/// </summary>
		/// <param name="userAgent"></param>
		/// <param name="acceptLanguages"></param>
		public void setUserAgent(string userAgent, string acceptLanguages = null) {
			if (acceptLanguages == null) {
				API.Apply("setUserAgent", userAgent);
			} else {
				API.Apply("setUserAgent", userAgent, acceptLanguages);
			}
		}

		/// <summary>
		/// Returns String - The user agent for this session.
		/// </summary>
		/// <returns></returns>
		public string getUserAgent() {
			return API.Apply<string>("getUserAgent");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="identifier">Valid UUID.</param>
		/// <param name="callback"></param>
		public void getBlobData(string identifier, Action<Buffer> callback) {
			string eventName = "_getBlobData";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				Buffer result = API.CreateObject<Buffer>(args[0]);
				callback?.Invoke(result);
			});
			API.Apply("getBlobData", identifier, item);
		}

		/// <summary>
		/// Allows resuming cancelled or interrupted downloads from previous Session.
		/// </summary>
		/// <param name="options"></param>
		public void createInterruptedDownload(CreateInterruptedDownloadOptions options) {
			API.Apply("createInterruptedDownload", options);
		}

		/// <summary>
		/// Clears the session’s HTTP authentication cache.
		/// </summary>
		/// <param name="options"></param>
		public void clearAuthCache(RemovePassword options) {
			API.Apply("clearAuthCache", options);
		}

		/// <summary>
		/// Clears the session’s HTTP authentication cache.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="callback">Called when operation is done.</param>
		public void clearAuthCache(RemovePassword options, Action callback) {
			string eventName = "_clearAuthCache";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			API.Apply("clearAuthCache", options, item);
		}

		/// <summary>
		/// Clears the session’s HTTP authentication cache.
		/// </summary>
		/// <param name="options"></param>
		public void clearAuthCache(RemoveClientCertificate options) {
			API.Apply("clearAuthCache", options);
		}

		/// <summary>
		/// Clears the session’s HTTP authentication cache.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="callback">Called when operation is done.</param>
		public void clearAuthCache(RemoveClientCertificate options, Action callback) {
			string eventName = "_clearAuthCache";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			API.Apply("clearAuthCache", options, item);
		}

		/// <summary>
		/// Adds scripts that will be executed on ALL web contents
		/// that are associated with this session just before normal preload scripts run.
		/// </summary>
		/// <param name="preloads">An array of absolute path to preload scripts</param>
		public void setPreloads(string[] preloads) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.setPreloads({1});"
				),
				Script.GetObject(API.id),
				JSON.Stringify(preloads)
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String[] an array of paths to preload scripts that have been registered.
		/// </summary>
		/// <returns></returns>
		public string[] getPreloads() {
			object[] result = API.Apply<object[]>("getPreloads");
			return Array.ConvertAll(result, value => Convert.ToString(value));
		}
	}
}
