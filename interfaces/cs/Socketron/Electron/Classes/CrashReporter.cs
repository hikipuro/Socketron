using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Submit crash reports to a remote server.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class CrashReporter : EventEmitter {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public CrashReporter() {
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
			API.Apply("start", options);
		}

		/// <summary>
		/// Returns the date and ID of the last crash report.
		/// If no crash reports have been sent or the crash reporter
		/// has not been started, null is returned.
		/// </summary>
		/// <returns></returns>
		public CrashReport getLastCrashReport() {
			object result = API.Apply("getLastCrashReport");
			return CrashReport.FromObject(result);
		}

		/// <summary>
		/// Returns all uploaded crash reports.
		/// Each report contains the date and uploaded ID.
		/// </summary>
		/// <returns></returns>
		public CrashReport[] getUploadedReports() {
			object[] result = API.Apply<object[]>("getUploadedReports");
			return Array.ConvertAll(
				result, value => CrashReport.FromObject(value)
			);
		}

		/// <summary>
		/// *Linux macOS*
		/// Returns Boolean - Whether reports should be submitted to the server.
		/// Set through the start method or setUploadToServer.
		/// </summary>
		/// <returns></returns>
		public bool getUploadToServer() {
			return API.Apply<bool>("getUploadToServer");
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
			API.Apply("setUploadToServer", uploadToServer);
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
			API.Apply("addExtraParameter", key, value);
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
			API.Apply("removeExtraParameter", key);
		}

		/// <summary>
		/// See all of the current parameters being passed to the crash reporter.
		/// </summary>
		/// <returns></returns>
		public JsonObject getParameters() {
			object result = API.Apply("getParameters");
			return new JsonObject(result);
		}
	}
}
