using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Control file downloads from remote sources.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class DownloadItem: JSModule {
		/// <summary>
		/// DownloadItem instance events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when the download has been updated and is not done.
			/// </summary>
			public const string Updated = "updated";
			/// <summary>
			/// Emitted when the download is in a terminal state.
			/// </summary>
			public const string Done = "done";
		}

		/// <summary>
		/// The API is only available in session's will-download callback function.
		/// <para>
		/// If user doesn't set the save path via the API,
		/// Electron will use the original routine to determine
		/// the save path (Usually prompts a save dialog).
		/// </para>
		/// </summary>
		/// <param name="path"></param>
		public void setSavePath(string path) {
			API.Apply("setSavePath", path);
		}

		/// <summary>
		/// Returns String - The save path of the download item.
		/// <para>
		/// This will be either the path set via downloadItem.setSavePath(path)
		/// or the path selected from the shown save dialog.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public string getSavePath() {
			return API.Apply<string>("getSavePath");
		}

		/// <summary>
		/// Pauses the download.
		/// </summary>
		public void pause() {
			API.Apply("pause");
		}

		/// <summary>
		/// Returns Boolean - Whether the download is paused.
		/// </summary>
		/// <returns></returns>
		public bool isPaused() {
			return API.Apply<bool>("isPaused");
		}

		/// <summary>
		/// Resumes the download that has been paused.
		/// </summary>
		public void resume() {
			API.Apply("resume");
		}

		/// <summary>
		/// Returns Boolean - Whether the download can resume.
		/// </summary>
		/// <returns></returns>
		public bool canResume() {
			return API.Apply<bool>("canResume");
		}

		/// <summary>
		/// Cancels the download operation.
		/// </summary>
		public void cancel() {
			API.Apply("cancel");
		}

		/// <summary>
		/// Returns String - The origin url where the item is downloaded from.
		/// </summary>
		/// <returns></returns>
		public string getURL() {
			return API.Apply<string>("getURL");
		}

		/// <summary>
		/// Returns String - The files mime type.
		/// </summary>
		/// <returns></returns>
		public string getMimeType() {
			return API.Apply<string>("getMimeType");
		}

		/// <summary>
		/// Returns Boolean - Whether the download has user gesture.
		/// </summary>
		/// <returns></returns>
		public bool hasUserGesture() {
			return API.Apply<bool>("hasUserGesture");
		}

		/// <summary>
		/// Returns String - The file name of the download item.
		/// </summary>
		/// <returns></returns>
		public string getFilename() {
			return API.Apply<string>("getFilename");
		}

		/// <summary>
		/// Returns Integer - The total size in bytes of the download item.
		/// If the size is unknown, it returns 0.
		/// </summary>
		/// <returns></returns>
		public long getTotalBytes() {
			return API.Apply<long>("getTotalBytes");
		}

		/// <summary>
		/// Returns Integer - The received bytes of the download item.
		/// </summary>
		/// <returns></returns>
		public long getReceivedBytes() {
			return API.Apply<long>("getReceivedBytes");
		}

		/// <summary>
		/// Returns String - The Content-Disposition field from the response header.
		/// </summary>
		/// <returns></returns>
		public string getContentDisposition() {
			return API.Apply<string>("getContentDisposition");
		}

		/// <summary>
		/// Returns String - The current state.
		/// Can be progressing, completed, cancelled or interrupted.
		/// </summary>
		/// <returns></returns>
		public string getState() {
			return API.Apply<string>("getState");
		}

		/// <summary>
		/// Returns String[] - The complete url chain of the item including any redirects.
		/// </summary>
		/// <returns></returns>
		public string[] getURLChain() {
			string script = ScriptBuilder.Build(
				"return {0}.getURLChain();",
				Script.GetObject(API.id)
			);
			object[] result = API._ExecuteBlocking<object[]>(script);
			return Array.ConvertAll(result, value => Convert.ToString(value));
		}

		/// <summary>
		/// Returns String - Last-Modified header value.
		/// </summary>
		/// <returns></returns>
		public string getLastModifiedTime() {
			return API.Apply<string>("getLastModifiedTime");
		}

		/// <summary>
		/// Returns String - ETag header value.
		/// </summary>
		/// <returns></returns>
		public string getETag() {
			return API.Apply<string>("getETag");
		}

		/// <summary>
		/// Returns Double - Number of seconds since the UNIX epoch when the download was started.
		/// </summary>
		/// <returns></returns>
		public double getStartTime() {
			return API.Apply<double>("getStartTime");
		}
	}
}
