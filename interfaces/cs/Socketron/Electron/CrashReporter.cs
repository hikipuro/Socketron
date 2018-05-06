using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Submit crash reports to a remote server.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class CrashReporter : NodeModule {
		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public CrashReporter(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// You are required to call this method before using any other
		/// crashReporter APIs and in each process (main/renderer)
		/// from which you want to collect crash reports.
		/// You can pass different options to crashReporter.start
		/// when calling from different processes.
		/// </summary>
		/// <param name="options"></param>
		public void start(JsonObject options) {
			string script = ScriptBuilder.Build(
				"{0}.start({1});",
				Script.GetObject(_id),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns the date and ID of the last crash report.
		/// If no crash reports have been sent or the crash reporter
		/// has not been started, null is returned.
		/// </summary>
		/// <returns></returns>
		public CrashReport getLastCrashReport() {
			string script = ScriptBuilder.Build(
				"return {0}.getLastCrashReport();",
				Script.GetObject(_id)
			);
			object result = _ExecuteBlocking<object>(script);
			return CrashReport.FromObject(result);
		}

		/// <summary>
		/// Returns all uploaded crash reports.
		/// Each report contains the date and uploaded ID.
		/// </summary>
		/// <returns></returns>
		public List<CrashReport> getUploadedReports() {
			string script = ScriptBuilder.Build(
				"return {0}.getUploadedReports();",
				Script.GetObject(_id)
			);
			object[] result = _ExecuteBlocking<object[]>(script);
			List<CrashReport> reports = new List<CrashReport>();
			foreach (object item in result) {
				reports.Add(CrashReport.FromObject(item));
			}
			return reports;
		}

		/// <summary>
		/// *Linux macOS*
		/// Returns Boolean - Whether reports should be submitted to the server.
		/// Set through the start method or setUploadToServer.
		/// </summary>
		/// <returns></returns>
		public bool getUploadToServer() {
			string script = ScriptBuilder.Build(
				"return {0}.getUploadToServer();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *Linux macOS*
		/// This would normally be controlled by user preferences.
		/// This has no effect if called before start is called.
		/// </summary>
		/// <param name="uploadToServer">
		/// *macOS* Whether reports should be submitted to the server.
		/// </param>
		public void setUploadToServer(bool uploadToServer) {
			string script = ScriptBuilder.Build(
				"{0}.setUploadToServer({1});",
				Script.GetObject(_id),
				uploadToServer.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Set an extra parameter to be sent with the crash report.
		/// </summary>
		/// <param name="key">
		/// Parameter key, must be less than 64 characters long.
		/// </param>
		/// <param name="value">
		/// Parameter value, must be less than 64 characters long.
		/// </param>
		public void addExtraParameter(string key, string value) {
			string script = ScriptBuilder.Build(
				"{0}.addExtraParameter({1},{2});",
				Script.GetObject(_id),
				key.Escape(),
				value.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Remove a extra parameter from the current set of parameters
		/// so that it will not be sent with the crash report.
		/// </summary>
		/// <param name="key">
		/// Parameter key, must be less than 64 characters long.
		/// </param>
		public void removeExtraParameter(string key) {
			string script = ScriptBuilder.Build(
				"{0}.removeExtraParameter({1});",
				Script.GetObject(_id),
				key.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// See all of the current parameters being passed to the crash reporter.
		/// </summary>
		/// <returns></returns>
		public JsonObject getParameters() {
			string script = ScriptBuilder.Build(
				"return {0}.getParameters();",
				Script.GetObject(_id)
			);
			object result = _ExecuteBlocking<object>(script);
			return new JsonObject(result);
		}
	}
}
