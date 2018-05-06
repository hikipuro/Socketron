using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Enable apps to automatically update themselves.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class AutoUpdaterModule : NodeModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public AutoUpdaterModule(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// Sets the url and initialize the auto updater.
		/// </summary>
		/// <param name="options"></param>
		public void setFeedURL(JsonObject options) {
			string script = ScriptBuilder.Build(
				"{0}.setFeedURL({1});",
				Script.GetObject(_id),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - The current update feed URL.
		/// </summary>
		/// <returns></returns>
		public string getFeedURL() {
			string script = ScriptBuilder.Build(
				"return {0}.getFeedURL();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Asks the server whether there is an update.
		/// You must call setFeedURL before using this API.
		/// </summary>
		public void checkForUpdates() {
			string script = ScriptBuilder.Build(
				"{0}.checkForUpdates();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Restarts the app and installs the update after it has been downloaded.
		/// It should only be called after update-downloaded has been emitted.
		/// <para>
		/// Under the hood calling autoUpdater.quitAndInstall()
		/// will close all application windows first,
		/// and automatically call app.quit() after all windows have been closed.
		/// </para>
		/// <para>
		/// Note: If the application is quit without calling this API
		/// after the update-downloaded event has been emitted,
		/// the application will still be replaced by the updated one on the next run.
		/// </para>
		/// </summary>
		public void quitAndInstall() {
			string script = ScriptBuilder.Build(
				"{0}.quitAndInstall();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}
	}
}
