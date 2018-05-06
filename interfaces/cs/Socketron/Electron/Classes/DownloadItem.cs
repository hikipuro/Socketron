using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Socketron.Electron {
	/// <summary>
	/// Control file downloads from remote sources.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class DownloadItem : NodeModule {
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
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.setSavePath({1});"
				),
				Script.GetObject(_id),
				path.Escape()
			);
			_ExecuteJavaScript(script);
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
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.getSavePath();"
				),
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Pauses the download.
		/// </summary>
		public void pause() {
			string script = ScriptBuilder.Build(
				"{0}.pause();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the download is paused.
		/// </summary>
		/// <returns></returns>
		public bool isPaused() {
			string script = ScriptBuilder.Build(
				"return {0}.isPaused();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Resumes the download that has been paused.
		/// </summary>
		public void resume() {
			string script = ScriptBuilder.Build(
				"{0}.resume();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the download can resume.
		/// </summary>
		/// <returns></returns>
		public bool canResume() {
			string script = ScriptBuilder.Build(
				"return {0}.canResume();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Cancels the download operation.
		/// </summary>
		public void cancel() {
			string script = ScriptBuilder.Build(
				"{0}.cancel();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - The origin url where the item is downloaded from.
		/// </summary>
		/// <returns></returns>
		public string getURL() {
			string script = ScriptBuilder.Build(
				"return {0}.getURL();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns String - The files mime type.
		/// </summary>
		/// <returns></returns>
		public string getMimeType() {
			string script = ScriptBuilder.Build(
				"return {0}.getMimeType();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the download has user gesture.
		/// </summary>
		/// <returns></returns>
		public bool hasUserGesture() {
			string script = ScriptBuilder.Build(
				"return {0}.hasUserGesture();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Returns String - The file name of the download item.
		/// </summary>
		/// <returns></returns>
		public string getFilename() {
			string script = ScriptBuilder.Build(
				"return {0}.getFilename();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns Integer - The total size in bytes of the download item.
		/// If the size is unknown, it returns 0.
		/// </summary>
		/// <returns></returns>
		public int getTotalBytes() {
			string script = ScriptBuilder.Build(
				"return {0}.getTotalBytes();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<int>(script);
		}

		/// <summary>
		/// Returns Integer - The received bytes of the download item.
		/// </summary>
		/// <returns></returns>
		public int getReceivedBytes() {
			string script = ScriptBuilder.Build(
				"return {0}.getReceivedBytes();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<int>(script);
		}

		/// <summary>
		/// Returns String - The Content-Disposition field from the response header.
		/// </summary>
		/// <returns></returns>
		public string getContentDisposition() {
			string script = ScriptBuilder.Build(
				"return {0}.getContentDisposition();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns String - The current state.
		/// Can be progressing, completed, cancelled or interrupted.
		/// </summary>
		/// <returns></returns>
		public string getState() {
			string script = ScriptBuilder.Build(
				"return {0}.getState();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns String[] - The complete url chain of the item including any redirects.
		/// </summary>
		/// <returns></returns>
		public string[] getURLChain() {
			string script = ScriptBuilder.Build(
				"return {0}.getURLChain();",
				Script.GetObject(_id)
			);
			object[] result = _ExecuteBlocking<object[]>(script);
			return result.Cast<string>().ToArray();
		}

		/// <summary>
		/// Returns String - Last-Modified header value.
		/// </summary>
		/// <returns></returns>
		public string getLastModifiedTime() {
			string script = ScriptBuilder.Build(
				"return {0}.getLastModifiedTime();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns String - ETag header value.
		/// </summary>
		/// <returns></returns>
		public string getETag() {
			string script = ScriptBuilder.Build(
				"return {0}.getETag();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns Double - Number of seconds since the UNIX epoch when the download was started.
		/// </summary>
		/// <returns></returns>
		public double getStartTime() {
			string script = ScriptBuilder.Build(
				"return {0}.getStartTime();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<double>(script);
		}
	}
}
