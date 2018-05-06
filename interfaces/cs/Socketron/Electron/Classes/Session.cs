using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Socketron.Electron {
	/// <summary>
	/// Manage browser sessions, cookies, cache, proxy settings, etc.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Session : NodeModule {
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
		/// <param name="client"></param>
		/// <param name="id"></param>
		public Session(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// A Cookies object for this session.
		/// </summary>
		public Cookies cookies {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = {0};",
						"var cookies = session.cookies;",
						"return {1};"
					),
					Script.GetObject(_id),
					Script.AddObject("cookies")
				);
				int result = _ExecuteBlocking<int>(script);
				return new Cookies(_client) {
					_id = result
				};
			}
		}

		/// <summary>
		/// A WebRequest object for this session.
		/// </summary>
		public WebRequest webRequest {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = {0};",
						"var webRequest = session.webRequest;",
						"return {1};"
					),
					Script.GetObject(_id),
					Script.AddObject("webRequest")
				);
				int result = _ExecuteBlocking<int>(script);
				return new WebRequest(_client) {
					_id = result
				};
			}
		}

		/// <summary>
		/// A Protocol object for this session.
		/// </summary>
		public ProtocolModule protocol {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = {0};",
						"var protocol = session.protocol;",
						"return {1};"
					),
					Script.GetObject(_id),
					Script.AddObject("protocol")
				);
				int result = _ExecuteBlocking<int>(script);
				return new ProtocolModule(_client, result);
			}
		}

		/// <summary>
		/// Callback is invoked with the session's current cache size.
		/// </summary>
		/// <param name="callback"></param>
		public void getCacheSize(Action<int> callback) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Clears the session’s HTTP cache.
		/// </summary>
		/// <param name="callback"></param>
		public void clearCache(Action callback) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Clears the data of web storages.
		/// </summary>
		public void clearStorageData() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Writes any unwritten DOMStorage data to disk.
		/// </summary>
		public void flushStorageData() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"session.flushStorageData();"
				),
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets the proxy settings.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="callback"></param>
		public void setProxy(JsonObject config, Action callback) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Resolves the proxy information for url.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="callback"></param>
		public void resolveProxy(string url, Action<string> callback) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Sets download saving directory.
		/// By default, the download directory will be the Downloads under the respective app folder.
		/// </summary>
		/// <param name="path">The download location.</param>
		public void setDownloadPath(string path) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"session.setDownloadPath({1});"
				),
				Script.GetObject(_id),
				path.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Emulates network with the given configuration for the session.
		/// </summary>
		/// <param name="options"></param>
		public void enableNetworkEmulation(JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"session.enableNetworkEmulation({1});"
				),
				Script.GetObject(_id),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Disables any network emulation already active for the session.
		/// Resets to the original network configuration.
		/// </summary>
		public void disableNetworkEmulation() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"session.disableNetworkEmulation();"
				),
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets the certificate verify proc for session,
		/// the proc will be called with proc(request, callback)
		/// whenever a server certificate verification is requested.
		/// Calling callback(0) accepts the certificate, calling callback(-2) rejects it.
		/// </summary>
		public void setCertificateVerifyProc() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Sets the handler which can be used to respond to permission requests for the session.
		/// Calling callback(true) will allow the permission and callback(false) will reject it.
		/// To clear the handler, call setPermissionRequestHandler(null).
		/// </summary>
		public void setPermissionRequestHandler() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Clears the host resolver cache.
		/// </summary>
		public void clearHostResolverCache() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Dynamically sets whether to always send credentials for HTTP NTLM or Negotiate authentication.
		/// </summary>
		/// <param name="domains">
		/// A comma-separated list of servers for which integrated authentication is enabled.
		/// </param>
		public void allowNTLMCredentialsForDomains(string domains) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"session.allowNTLMCredentialsForDomains({1});"
				),
				Script.GetObject(_id),
				domains.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Overrides the userAgent and acceptLanguages for this session.
		/// </summary>
		/// <param name="userAgent"></param>
		/// <param name="acceptLanguages"></param>
		public void setUserAgent(string userAgent, string acceptLanguages = null) {
			string script = string.Empty;
			if (acceptLanguages == null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = {0};",
						"session.setUserAgent({1});"
					),
					Script.GetObject(_id),
					userAgent.Escape()
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = {0};",
						"session.setUserAgent({1},{2});"
					),
					Script.GetObject(_id),
					userAgent.Escape(),
					acceptLanguages.Escape()
				);
			}
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - The user agent for this session.
		/// </summary>
		/// <returns></returns>
		public string getUserAgent() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"return session.getUserAgent();"
				),
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="identifier">Valid UUID.</param>
		/// <param name="callback"></param>
		public void getBlobData(string identifier, Action<Buffer> callback) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Allows resuming cancelled or interrupted downloads from previous Session.
		/// </summary>
		/// <param name="options"></param>
		public void createInterruptedDownload(JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"session.createInterruptedDownload({1});"
				),
				Script.GetObject(_id),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Clears the session’s HTTP authentication cache.
		/// </summary>
		/// <param name="options"></param>
		public void clearAuthCache(RemovePassword options) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds scripts that will be executed on ALL web contents
		/// that are associated with this session just before normal preload scripts run.
		/// </summary>
		/// <param name="preloads">An array of absolute path to preload scripts</param>
		public void setPreloads(string[] preloads) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"session.setPreloads({1});"
				),
				Script.GetObject(_id),
				JSON.Stringify(preloads)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String[] an array of paths to preload scripts that have been registered.
		/// </summary>
		/// <returns></returns>
		public string[] getPreloads() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"return session.getPreloads();"
				),
				Script.GetObject(_id)
			);
			object[] result = _ExecuteBlocking<object[]>(script);
			return result.Cast<string>().ToArray();
		}
	}
}
