using System;
using System.Linq;

namespace Socketron {
	/// <summary>
	/// Manage browser sessions, cookies, cache, proxy settings, etc.
	/// <para>Process: Main</para>
	/// </summary>
	public class Session : ElectronBase {
		/// <summary>
		/// Session instance events.
		/// </summary>
		public class Events {
			public const string WillDownload = "will-download";
		}

		public int id;

		public Session(Socketron socketron) {
			_socketron = socketron;
		}

		public Cookies cookies {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = {0};",
						"var cookies = session.cookies;",
						"return {1};"
					),
					Script.GetObject(id),
					Script.AddObject("cookies")
				);
				int result = _ExecuteJavaScriptBlocking<int>(script);
				return new Cookies(_socketron) {
					id = result
				};
			}
		}

		public WebRequest webRequest {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = {0};",
						"var webRequest = session.webRequest;",
						"return {1};"
					),
					Script.GetObject(id),
					Script.AddObject("webRequest")
				);
				int result = _ExecuteJavaScriptBlocking<int>(script);
				return new WebRequest(_socketron) {
					id = result
				};
			}
		}

		public Protocol protocol {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = {0};",
						"var protocol = session.protocol;",
						"return {1};"
					),
					Script.GetObject(id),
					Script.AddObject("protocol")
				);
				int result = _ExecuteJavaScriptBlocking<int>(script);
				return new Protocol(_socketron) {
					id = result
				};
			}
		}

		public void GetCacheSize() {
			throw new NotImplementedException();
		}

		public void ClearCache() {
			throw new NotImplementedException();
		}

		public void ClearStorageData() {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Writes any unwritten DOMStorage data to disk.
		/// </summary>
		public void FlushStorageData() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"session.flushStorageData();"
				),
				Script.GetObject(id)
			);
			_ExecuteJavaScript(script);
		}

		public void SetProxy() {
			throw new NotImplementedException();
		}

		public void ResolveProxy() {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Sets download saving directory.
		/// By default, the download directory will be the Downloads under the respective app folder.
		/// </summary>
		/// <param name="path">The download location.</param>
		public void SetDownloadPath(string path) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"session.setDownloadPath({1});"
				),
				Script.GetObject(id),
				path.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Emulates network with the given configuration for the session.
		/// </summary>
		/// <param name="options"></param>
		public void EnableNetworkEmulation(JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"session.enableNetworkEmulation({1});"
				),
				Script.GetObject(id),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Disables any network emulation already active for the session.
		/// Resets to the original network configuration.
		/// </summary>
		public void DisableNetworkEmulation() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"session.disableNetworkEmulation();"
				),
				Script.GetObject(id)
			);
			_ExecuteJavaScript(script);
		}

		public void SetCertificateVerifyProc() {
			throw new NotImplementedException();
		}

		public void SetPermissionRequestHandler() {
			throw new NotImplementedException();
		}

		public void ClearHostResolverCache() {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Dynamically sets whether to always send credentials for HTTP NTLM or Negotiate authentication.
		/// </summary>
		/// <param name="domains">
		/// A comma-separated list of servers for which integrated authentication is enabled.
		/// </param>
		public void AllowNTLMCredentialsForDomains(string domains) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"session.allowNTLMCredentialsForDomains({1});"
				),
				Script.GetObject(id),
				domains.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Overrides the userAgent and acceptLanguages for this session.
		/// </summary>
		/// <param name="userAgent"></param>
		/// <param name="acceptLanguages"></param>
		public void SetUserAgent(string userAgent, string acceptLanguages = null) {
			string script = string.Empty;
			if (acceptLanguages == null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = {0};",
						"session.setUserAgent({1});"
					),
					Script.GetObject(id),
					userAgent.Escape()
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = {0};",
						"session.setUserAgent({1},{2});"
					),
					Script.GetObject(id),
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
		public string GetUserAgent() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"return session.getUserAgent();"
				),
				Script.GetObject(id)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		public void GetBlobData() {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Allows resuming cancelled or interrupted downloads from previous Session.
		/// </summary>
		/// <param name="options"></param>
		public void CreateInterruptedDownload(JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"session.createInterruptedDownload({1});"
				),
				Script.GetObject(id),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		public void ClearAuthCache() {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Adds scripts that will be executed on ALL web contents
		/// that are associated with this session just before normal preload scripts run.
		/// </summary>
		/// <param name="preloads">An array of absolute path to preload scripts</param>
		public void SetPreloads(string[] preloads) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"session.setPreloads({1});"
				),
				Script.GetObject(id),
				JSON.Stringify(preloads)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String[] an array of paths to preload scripts that have been registered.
		/// </summary>
		/// <returns></returns>
		public string[] GetPreloads() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var session = {0};",
					"return session.getPreloads();"
				),
				Script.GetObject(id)
			);
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			return result.Cast<string>().ToArray();
		}
	}
}
