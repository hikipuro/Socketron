using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Control your application's event lifecycle.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class App : JSObject {
		/// <summary>
		/// App module events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when the application has finished basic startup. 
			/// </summary>
			public const string WillFinishLaunching = "will-finish-launching";
			/// <summary>
			/// Emitted when Electron has finished initializing. 
			/// </summary>
			public const string Ready = "ready";
			/// <summary>
			/// Emitted when all windows have been closed.
			/// </summary>
			public const string WindowAllClosed = "window-all-closed";
			/// <summary>
			/// Emitted before the application starts closing its windows.
			/// </summary>
			public const string BeforeQuit = "before-quit";
			/// <summary>
			/// Emitted when all windows have been closed and the application will quit.
			/// </summary>
			public const string WillQuit = "will-quit";
			/// <summary>
			/// Emitted when the application is quitting.
			/// </summary>
			public const string Quit = "quit";
			/// <summary>
			/// *macOS*
			/// Emitted when the user wants to open a file with the application.
			/// </summary>
			public const string OpenFile = "open-file";
			/// <summary>
			/// *macOS*
			/// Emitted when the user wants to open a URL with the application.
			/// </summary>
			public const string OpenUrl = "open-url";
			/// <summary>
			/// *macOS*
			/// Emitted when the application is activated.
			/// </summary>
			public const string Activate = "activate";
			/// <summary>
			/// *macOS*
			/// Emitted during Handoff when an activity from a different device wants to be resumed.
			/// </summary>
			public const string ContinueActivity = "continue-activity";
			/// <summary>
			/// *macOS*
			/// Emitted during Handoff before an activity from a different device wants to be resumed.
			/// </summary>
			public const string WillContinueActivity = "will-continue-activity";
			/// <summary>
			/// *macOS*
			/// Emitted during Handoff when an activity from a different device fails to be resumed.
			/// </summary>
			public const string ContinueActivityError = "continue-activity-error";
			/// <summary>
			/// *macOS*
			/// Emitted during Handoff after an activity from this device was successfully resumed on another one.
			/// </summary>
			public const string ActivityWasContinued = "activity-was-continued";
			/// <summary>
			/// *macOS*
			/// Emitted when Handoff is about to be resumed on another device.
			/// </summary>
			public const string UpdateActivityState = "update-activity-state";
			/// <summary>
			/// *macOS*
			/// Emitted when the user clicks the native macOS new tab button.
			/// </summary>
			public const string NewWindowForTab = "new-window-for-tab";
			/// <summary>
			/// Emitted when a browserWindow gets blurred.
			/// </summary>
			public const string BrowserWindowBlur = "browser-window-blur";
			/// <summary>
			/// Emitted when a browserWindow gets focused.
			/// </summary>
			public const string BrowserWindowFocus = "browser-window-focus";
			/// <summary>
			/// Emitted when a new browserWindow is created.
			/// </summary>
			public const string BrowserWindowCreated = "browser-window-created";
			/// <summary>
			/// Emitted when a new webContents is created.
			/// </summary>
			public const string WebContentsCreated = "web-contents-created";
			/// <summary>
			/// Emitted when failed to verify the certificate for url,
			/// to trust the certificate you should prevent the default behavior
			/// with event.preventDefault() and call callback(true).
			/// </summary>
			public const string CertificateError = "certificate-error";
			/// <summary>
			/// Emitted when a client certificate is requested.
			/// </summary>
			public const string SelectClientCertificate = "select-client-certificate";
			/// <summary>
			/// Emitted when webContents wants to do basic auth.
			/// </summary>
			public const string Login = "login";
			/// <summary>
			/// Emitted when the gpu process crashes or is killed.
			/// </summary>
			public const string GpuProcessCrashed = "gpu-process-crashed";
			/// <summary>
			/// *macOS Windows*
			/// Emitted when Chrome's accessibility support changes.
			/// </summary>
			public const string AccessibilitySupportChanged = "accessibility-support-changed";
			/// <summary>
			/// Emitted when Electron has created a new session.
			/// </summary>
			public const string SessionCreated = "session-created";
		}

		/// <summary>
		/// app.getPath() paths.
		/// </summary>
		public class Path {
			/// <summary>
			/// User's home directory.
			/// </summary>
			public const string Home = "home";
			/// <summary>
			/// Per-user application data directory, which by default points to:
			/// - %APPDATA% on Windows
			/// - $XDG_CONFIG_HOME or ~/.config on Linux
			/// - ~/Library/Application Support on macOS
			/// </summary>
			public const string AppData = "appData";
			/// <summary>
			/// The directory for storing your app's configuration files,
			/// which by default it is the appData directory appended with your app's name.
			/// </summary>
			public const string UserData = "userData";
			/// <summary>
			/// Temporary directory.
			/// </summary>
			public const string Temp = "temp";
			/// <summary>
			/// The current executable file.
			/// </summary>
			public const string Exe = "exe";
			/// <summary>
			/// The libchromiumcontent library.
			/// </summary>
			public const string Module = "module";
			/// <summary>
			/// The current user's Desktop directory.
			/// </summary>
			public const string Desktop = "desktop";
			/// <summary>
			/// Directory for a user's "My Documents".
			/// </summary>
			public const string Documents = "documents";
			/// <summary>
			/// Directory for a user's downloads.
			/// </summary>
			public const string Downloads = "downloads";
			/// <summary>
			/// Directory for a user's music.
			/// </summary>
			public const string Music = "music";
			/// <summary>
			/// Directory for a user's pictures.
			/// </summary>
			public const string Pictures = "pictures";
			/// <summary>
			/// Directory for a user's videos.
			/// </summary>
			public const string Videos = "videos";
			/// <summary>
			/// Directory for your app's log folder.
			/// </summary>
			public const string Logs = "logs";
			/// <summary>
			/// Full path to the system version of the Pepper Flash plugin.
			/// </summary>
			public const string PepperFlashSystemPlugin = "pepperFlashSystemPlugin";
		}
		public class CommandLine : JSObject {
			/// <summary>
			/// This constructor is used for internally by the library.
			/// </summary>
			public CommandLine() {
			}

			/// <summary>
			/// Append a switch (with optional value) to Chromium's command line.
			/// <para>
			/// Note: This will not affect process.argv,
			/// and is mainly used by developers to control some low-level Chromium behaviors.
			/// </para>
			/// </summary>
			/// <param name="switch">A command-line switch.</param>
			/// <param name="value"> A value for the given switch.</param>
			public void appendSwitch(string @switch, string value = null) {
				if (value == null) {
					API.Apply("appendSwitch", @switch);
				} else {
					API.Apply("appendSwitch", @switch, value);
				}
			}

			/// <summary>
			/// Append an argument to Chromium's command line. The argument will be quoted correctly.
			/// <para>
			/// Note: This will not affect process.argv.
			/// </para>
			/// </summary>
			/// <param name="value">The argument to append to the command line.</param>
			public void appendArgument(string value) {
				API.Apply("appendArgument", value);
			}
		}

		public class Dock : JSObject {
			/// <summary>
			/// This constructor is used for internally by the library.
			/// </summary>
			public Dock() {
			}

			/// <summary>
			/// *macOS*
			/// When critical is passed, the dock icon will bounce until either
			/// the application becomes active or the request is canceled.
			/// <para>
			/// When informational is passed, the dock icon will bounce for one second.However,
			/// the request remains active until either the application becomes active
			/// or the request is canceled.
			/// </para>
			/// <para>
			/// Returns Integer an ID representing the request.
			/// </para>
			/// </summary>
			/// <param name="type">
			/// Can be critical or informational.
			/// The default is informational.
			/// </param>
			public void bounce(string type = null) {
				if (type == null) {
					API.Apply("bounce");
				} else {
					API.Apply("bounce", type);
				}
			}

			/// <summary>
			/// *macOS*
			/// Cancel the bounce of id.
			/// </summary>
			/// <param name="id"></param>
			public void cancelBounce(int id) {
				API.Apply("cancelBounce", id);
			}

			/// <summary>
			/// *macOS*
			/// Bounces the Downloads stack if the filePath is inside the Downloads folder.
			/// </summary>
			/// <param name="filePath"></param>
			public void downloadFinished(string filePath) {
				API.Apply("downloadFinished", filePath);
			}

			/// <summary>
			/// *macOS*
			/// Sets the string to be displayed in the dock’s badging area.
			/// </summary>
			/// <param name="text"></param>
			public void setBadge(string text) {
				API.Apply("setBadge", text);
			}

			/// <summary>
			/// *macOS*
			/// Returns String - The badge string of the dock.
			/// </summary>
			/// <returns></returns>
			public string getBadge() {
				return API.Apply<string>("getBadge");
			}

			/// <summary>
			/// *macOS*
			/// Hides the dock icon.
			/// </summary>
			public void hide() {
				API.Apply("hide");
			}

			/// <summary>
			/// *macOS*
			/// Shows the dock icon.
			/// </summary>
			public void show() {
				API.Apply("show");
			}

			/// <summary>
			/// *macOS*
			/// Returns Boolean - Whether the dock icon is visible.
			/// <para>
			/// The app.dock.show() call is asynchronous so this method
			/// might not return true immediately after that call.
			/// </para>
			/// </summary>
			/// <returns></returns>
			public bool isVisible() {
				return API.Apply<bool>("isVisible");
			}

			/// <summary>
			/// *macOS*
			/// Sets the application's dock menu.
			/// </summary>
			/// <param name="menu"></param>
			public void setMenu(Menu menu) {
				API.Apply("setMenu", menu);
			}

			/// <summary>
			/// *macOS*
			/// Sets the image associated with this dock icon.
			/// </summary>
			/// <param name="image"></param>
			public void setIcon(NativeImage image) {
				API.Apply("setIcon", image);
			}
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public App() {
		}

		public CommandLine commandLine {
			get { return API.GetObject<CommandLine>("commandLine"); }
		}

		/// <summary>*macOS*</summary>
		public Dock dock {
			get { return API.GetObject<Dock>("dock"); }
		}

		/// <summary>
		/// A Boolean property that returns true if the app is packaged, false otherwise.
		/// For many apps, this property can be used to distinguish development and production environments.
		/// </summary>
		public bool isPackaged {
			get { return API.GetProperty<bool>("isPackaged"); }
		}

		/// <summary>
		/// Try to close all windows.
		/// <para>
		/// The before-quit event will be emitted first.
		/// If all windows are successfully closed,
		/// the will-quit event will be emitted and by default the application will terminate.
		/// </para>
		/// <para>
		/// This method guarantees that all beforeunload and unload event handlers are correctly executed.
		/// It is possible that a window cancels the quitting by returning false
		/// in the beforeunload event handler.
		/// </para>
		/// </summary>
		public void quit() {
			API.Apply("quit");
		}

		/// <summary>
		/// Exits immediately with exitCode.
		/// exitCode defaults to 0.
		/// <para>
		/// All windows will be closed immediately without asking user 
		/// and the before-quit and will-quit events will not be emitted.
		/// </para>
		/// </summary>
		/// <param name="exitCode">(optional)</param>
		public void exit(int exitCode = 0) {
			API.Apply("exit", exitCode);
		}

		/// <summary>
		/// Relaunches the app when current instance exits.
		/// </summary>
		/// <param name="options">(optional)</param>
		public void relaunch(JsonObject options = null) {
			if (options == null) {
				API.Apply("relaunch");
			} else {
				API.Apply("relaunch", options);
			}
		}

		/// <summary>
		/// Returns Boolean - true if Electron has finished initializing, false otherwise.
		/// </summary>
		/// <returns></returns>
		public bool isReady() {
			return API.Apply<bool>("isReady");
		}

		/// <summary>
		/// Returns Promise - fulfilled when Electron is initialized.
		/// <para>
		/// May be used as a convenient alternative to checking app.isReady()
		/// and subscribing to the ready event if the app is not ready yet.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public Promise whenReady() {
			return API.ApplyAndGetObject<Promise>("whenReady");
		}

		/// <summary>
		/// On Linux, focuses on the first visible window.
		/// On macOS, makes the application the active app.
		/// On Windows, focuses on the application's first window.
		/// </summary>
		public void focus() {
			API.Apply("focus");
		}

		/// <summary>
		/// *macOS*
		/// Hides all application windows without minimizing them.
		/// </summary>
		public void hide() {
			API.Apply("hide");
		}

		/// <summary>
		/// *macOS*
		/// Shows application windows after they were hidden. Does not automatically focus them.
		/// </summary>
		public void show() {
			API.Apply("show");
		}

		/// <summary>
		/// Returns String - The current application directory.
		/// </summary>
		/// <returns></returns>
		public string getAppPath() {
			return API.Apply<string>("getAppPath");
		}

		/// <summary>
		/// Returns String - A path to a special directory or file associated with name.
		/// On failure an Error is thrown.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string getPath(string name) {
			return API.Apply<string>("getPath", name);
		}

		/// <summary>
		/// Fetches a path's associated icon.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="callback"></param>
		public void getFileIcon(string path, Action<Error, NativeImage> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_getFileIcon";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				Error error = API.CreateObject<Error>(args[0]);
				NativeImage image = API.CreateObject<NativeImage>(args[1]);
				callback?.Invoke(error, image);
			});
			API.Apply("getFileIcon", path, item);
		}

		/// <summary>
		/// Fetches a path's associated icon.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="options"></param>
		/// <param name="callback"></param>
		public void getFileIcon(string path, JsonObject options, Action<Error, NativeImage> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_getFileIcon";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				Error error = API.CreateObject<Error>(args[0]);
				NativeImage image = API.CreateObject<NativeImage>(args[1]);
				callback?.Invoke(error, image);
			});
			API.Apply("getFileIcon", path, options, item);
		}

		/// <summary>
		/// Overrides the path to a special directory or file associated with name.
		/// <para>
		/// If the path specifies a directory that does not exist,
		/// the directory will be created by this method.
		/// On failure an Error is thrown.
		/// </para>
		/// </summary>
		/// <param name="name"></param>
		/// <param name="path"></param>
		public void setPath(string name, string path) {
			API.Apply("setPath", name, path);
		}

		/// <summary>
		/// Returns String - The version of the loaded application.
		/// <para>
		/// If no version is found in the application's package.json file,
		/// the version of the current bundle or executable is returned.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public string getVersion() {
			return API.Apply<string>("getVersion");
		}

		/// <summary>
		/// Returns String - The current application's name,
		/// which is the name in the application's package.json file.
		/// </summary>
		/// <returns></returns>
		public string getName() {
			return API.Apply<string>("getName");
		}

		/// <summary>
		/// Overrides the current application's name.
		/// </summary>
		/// <param name="name"></param>
		public void setName(string name) {
			API.Apply("setName", name);
		}

		/// <summary>
		/// Returns String - The current application locale.
		/// <para>
		/// Note: When distributing your packaged app, you have to also ship the locales folder.
		/// </para>
		/// <para>
		/// Note: On Windows you have to call it after the ready events gets emitted.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public string getLocale() {
			return API.Apply<string>("getLocale");
		}

		/// <summary>
		/// *macOS Windows*
		/// Adds path to the recent documents list.
		/// <para>
		/// This list is managed by the OS.
		/// On Windows you can visit the list from the task bar,
		/// and on macOS you can visit it from dock menu.
		/// </para>
		/// </summary>
		/// <param name="path"></param>
		public void addRecentDocument(string path) {
			API.Apply("addRecentDocument", path);
		}

		/// <summary>
		/// *macOS Windows*
		/// Clears the recent documents list.
		/// </summary>
		public void clearRecentDocuments() {
			API.Apply("clearRecentDocuments");
		}

		/// <summary>
		/// This method sets the current executable
		/// as the default handler for a protocol (aka URI scheme).
		/// </summary>
		/// <param name="protocol">
		/// The name of your protocol, without ://.
		/// If you want your app to handle electron:// links,
		/// call this method with electron as the parameter.
		/// </param>
		/// <returns>Whether the call succeeded.</returns>
		public bool setAsDefaultProtocolClient(string protocol) {
			return API.Apply<bool>("setAsDefaultProtocolClient", protocol);
		}

		/// <summary>
		/// This method sets the current executable
		/// as the default handler for a protocol (aka URI scheme).
		/// </summary>
		/// <param name="protocol">
		/// The name of your protocol, without ://.
		/// If you want your app to handle electron:// links,
		/// call this method with electron as the parameter.
		/// </param>
		/// <param name="path">*Windows* Defaults to process.execPath</param>
		/// <param name="args">*Windows* Defaults to an empty array</param>
		/// <returns>Whether the call succeeded.</returns>
		public bool setAsDefaultProtocolClient(string protocol, string path, string[] args) {
			return API.Apply<bool>("setAsDefaultProtocolClient", protocol, path, args);
		}

		/// <summary>
		/// Returns Boolean - Whether the call succeeded.
		/// <para>
		/// This method checks if the current executable
		/// as the default handler for a protocol (aka URI scheme).
		/// If so, it will remove the app as the default handler.
		/// </para>
		/// </summary>
		/// <param name="protocol">The name of your protocol, without ://</param>
		/// <returns></returns>
		public bool removeAsDefaultProtocolClient(string protocol) {
			return API.Apply<bool>("removeAsDefaultProtocolClient", protocol);
		}

		/// <summary>
		/// Returns Boolean - Whether the call succeeded.
		/// </summary>
		/// <param name="protocol">The name of your protocol, without ://</param>
		/// <param name="path">*Windows* Defaults to process.execPath</param>
		/// <returns></returns>
		public bool removeAsDefaultProtocolClient(string protocol, string path) {
			return API.Apply<bool>("removeAsDefaultProtocolClient", protocol, path);
		}

		/// <summary>
		/// Returns Boolean - Whether the call succeeded.
		/// </summary>
		/// <param name="protocol">The name of your protocol, without ://</param>
		/// <param name="path">*Windows* Defaults to process.execPath</param>
		/// <param name="args">*Windows* Defaults to an empty array</param>
		/// <returns></returns>
		public bool removeAsDefaultProtocolClient(string protocol, string path, string[] args) {
			return API.Apply<bool>("removeAsDefaultProtocolClient", protocol, path, args);
		}

		/// <summary>
		/// This method checks if the current executable
		/// is the default handler for a protocol (aka URI scheme).
		/// If so, it will return true. Otherwise, it will return false.
		/// </summary>
		/// <param name="protocol">The name of your protocol, without ://</param>
		/// <returns></returns>
		public bool isDefaultProtocolClient(string protocol) {
			return API.Apply<bool>("isDefaultProtocolClient", protocol);
		}

		/// <summary>
		/// This method checks if the current executable
		/// is the default handler for a protocol (aka URI scheme).
		/// If so, it will return true. Otherwise, it will return false.
		/// </summary>
		/// <param name="protocol">The name of your protocol, without ://</param>
		/// <param name="path">*Windows* Defaults to process.execPath</param>
		/// <returns></returns>
		public bool isDefaultProtocolClient(string protocol, string path) {
			return API.Apply<bool>("isDefaultProtocolClient", protocol, path);
		}

		/// <summary>
		/// This method checks if the current executable
		/// is the default handler for a protocol (aka URI scheme).
		/// If so, it will return true. Otherwise, it will return false.
		/// </summary>
		/// <param name="protocol">The name of your protocol, without ://</param>
		/// <param name="path">*Windows* Defaults to process.execPath</param>
		/// <param name="args">*Windows* Defaults to an empty array</param>
		/// <returns></returns>
		public bool isDefaultProtocolClient(string protocol, string path, string[] args) {
			return API.Apply<bool>("isDefaultProtocolClient", protocol, path, args);
		}

		/// <summary>
		/// *Windows*
		/// Adds tasks to the Tasks category of the JumpList on Windows.
		/// <para>
		/// Note: If you'd like to customize the Jump List
		/// even more use app.setJumpList(categories) instead.
		/// </para>
		/// </summary>
		/// <param name="tasks">Array of Task objects.</param>
		/// <returns>Whether the call succeeded.</returns>
		public bool setUserTasks(TaskObject[] tasks) {
			return API.Apply<bool>("setUserTasks", tasks as object);
		}

		/// <summary>
		/// *Windows*
		/// Returns Object:
		/// </summary>
		/// <returns></returns>
		public JsonObject getJumpListSettings() {
			object result = API.Apply("getJumpListSettings");
			return new JsonObject(result);

		}

		/// <summary>
		/// *Windows*
		/// Sets or removes a custom Jump List for the application.
		/// </summary>
		/// <param name="categories"></param>
		public void setJumpList(JumpListCategory[] categories) {
			API.Apply("setJumpList", categories as object);
		}

		/// <summary>
		/// This method makes your application a Single Instance Application
		/// - instead of allowing multiple instances of your app to run,
		/// this will ensure that only a single instance of your app is running,
		/// and other instances signal this instance and exit.
		/// </summary>
		/// <param name="callback"></param>
		/// <returns></returns>
		public bool makeSingleInstance(Action<string[], string> callback) {
			if (callback == null) {
				return false;
			}
			string eventName = "_makeSingleInstance";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				// TODO: check commandLine
				API.RemoveCallbackItem(eventName, item);
				string[] commandLine = null;
				string workingDirectory = Convert.ToString(args[1]);
				if (args[0] != null) {
					JSObject _commandLine = API.CreateObject<JSObject>(args[0]);
					commandLine = Array.ConvertAll(
						_commandLine.API.GetValue() as object[],
						value => Convert.ToString(value)
					);
				}
				callback?.Invoke(commandLine, workingDirectory);
			});
			return API.Apply<bool>("makeSingleInstance", item);
		}

		/// <summary>
		/// Releases all locks that were created by makeSingleInstance.
		/// This will allow multiple instances of the application to once again run side by side.
		/// </summary>
		public void releaseSingleInstance() {
			API.Apply("releaseSingleInstance");
		}

		/// <summary>
		/// *macOS* 
		/// Creates an NSUserActivity and sets it as the current activity.
		/// The activity is eligible for Handoff to another device afterward.
		/// </summary>
		/// <param name="type">
		/// Uniquely identifies the activity. Maps to NSUserActivity.activityType.
		/// </param>
		/// <param name="userInfo">
		/// App-specific state to store for use by another device.
		/// </param>
		/// <param name="webpageURL">
		/// (optional) - The webpage to load in a browser if no suitable app
		/// is installed on the resuming device. The scheme must be http or https.
		/// </param>
		public void setUserActivity(string type, JsonObject userInfo, string webpageURL = null) {
			if (webpageURL == null) {
				API.Apply("setUserActivity", type, userInfo);
			} else {
				API.Apply("setUserActivity", type, userInfo, webpageURL);
			}
		}

		/// <summary>
		/// *macOS*
		/// Returns String - The type of the currently running activity.
		/// </summary>
		/// <returns></returns>
		public string getCurrentActivityType() {
			return API.Apply<string>("getCurrentActivityType");
		}

		/// <summary>
		/// *macOS*
		/// Invalidates the current Handoff user activity.
		/// </summary>
		public void invalidateCurrentActivity() {
			API.Apply("invalidateCurrentActivity");
		}

		/// <summary>
		/// *macOS*
		/// Updates the current activity if its type matches type,
		/// merging the entries from userInfo into its current userInfo dictionary.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="userInfo"></param>
		public void updateCurrentActivity(string type, JsonObject userInfo) {
			API.Apply("updateCurrentActivity", type, userInfo);
		}

		/// <summary>
		/// *Windows*
		/// Changes the Application User Model ID to id.
		/// </summary>
		/// <param name="id"></param>
		public void setAppUserModelId(string id) {
			API.Apply("setAppUserModelId", id);
		}

		/// <summary>
		/// *Linux*
		/// Imports the certificate in pkcs12 format into the platform certificate store.
		/// callback is called with the result of import operation,
		/// a value of 0 indicates success while any other value indicates failure
		/// according to chromium net_error_list.
		/// </summary>
		/// <param name="options">
		/// "certificate" String - Path for the pkcs12 file.
		/// "password" String - Passphrase for the certificate.
		/// </param>
		/// <param name="callback">
		/// "result" Integer - Result of import.
		/// </param>
		public void importCertificate(JsonObject options, Action<int> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_importCertificate";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				int result = Convert.ToInt32(args[0]);
				callback?.Invoke(result);
			});
			API.Apply("importCertificate", options, item);
		}

		/// <summary>
		/// Disables hardware acceleration for current app.
		/// <para>
		/// This method can only be called before app is ready.
		/// </para>
		/// </summary>
		public void disableHardwareAcceleration() {
			API.Apply("disableHardwareAcceleration");
		}

		/// <summary>
		/// By default, Chromium disables 3D APIs (e.g. WebGL) until restart
		/// on a per domain basis if the GPU processes crashes too frequently.
		/// This function disables that behaviour.
		/// <para>
		/// This method can only be called before app is ready.
		/// </para>
		/// </summary>
		public void disableDomainBlockingFor3DAPIs() {
			API.Apply("disableDomainBlockingFor3DAPIs");
		}

		/// <summary>
		/// Returns ProcessMetric[]: Array of ProcessMetric objects
		/// that correspond to memory and cpu usage statistics
		/// of all the processes associated with the app.
		/// </summary>
		/// <returns></returns>
		public ProcessMetric[] getAppMetrics() {
			object[] result = API.Apply<object[]>("getAppMetrics");
			return Array.ConvertAll(
				result, value => ProcessMetric.FromObject(value)
			);
		}

		/// <summary>
		/// Returns GPUFeatureStatus - The Graphics Feature Status from chrome://gpu/.
		/// </summary>
		/// <returns></returns>
		public GPUFeatureStatus getGPUFeatureStatus() {
			object result = API.Apply("getGPUFeatureStatus");
			return GPUFeatureStatus.FromObject(result);
		}

		/// <summary>
		/// *Linux macOS*
		/// Returns Boolean - Whether the call succeeded.
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public bool setBadgeCount(int count) {
			return API.Apply<bool>("setBadgeCount", count);
		}

		/// <summary>
		/// *Linux macOS*
		/// Returns Integer - The current value displayed in the counter badge.
		/// </summary>
		/// <returns></returns>
		public int getBadgeCount() {
			return API.Apply<int>("getBadgeCount");
		}

		/// <summary>
		/// *Linux*
		/// Returns Boolean - Whether the current desktop environment is Unity launcher.
		/// </summary>
		/// <returns></returns>
		public bool isUnityRunning() {
			return API.Apply<bool>("isUnityRunning");
		}

		/// <summary>
		/// *macOS Windows*
		/// If you provided path and args options to app.setLoginItemSettings
		/// then you need to pass the same arguments here for openAtLogin to be set correctly.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public JsonObject getLoginItemSettings(JsonObject options = null) {
			if (options == null) {
				object result = API.Apply("getLoginItemSettings");
				return new JsonObject(result);
			} else {
				object result = API.Apply("getLoginItemSettings", options);
				return new JsonObject(result);
			}
		}

		/// <summary>
		/// *macOS Windows*
		/// Set the app's login item settings.
		/// </summary>
		/// <param name="settings"></param>
		public void setLoginItemSettings(JsonObject settings) {
			API.Apply("setLoginItemSettings", settings);
		}

		/// <summary>
		/// *macOS Windows*
		/// Returns Boolean - true if Chrome's accessibility support is enabled, false otherwise.
		/// <para>
		/// This API will return true if the use of assistive technologies,
		/// such as screen readers, has been detected.
		/// See https://www.chromium.org/developers/design-documents/accessibility for more details.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public bool isAccessibilitySupportEnabled() {
			return API.Apply<bool>("isAccessibilitySupportEnabled");
		}

		/// <summary>
		/// *macOS Windows*
		/// Manually enables Chrome's accessibility support,
		/// allowing to expose accessibility switch to users in application settings. 
		/// </summary>
		/// <param name="enabled"></param>
		public void setAccessibilitySupportEnabled(bool enabled) {
			API.Apply("setAccessibilitySupportEnabled", enabled);
		}

		/// <summary>
		/// *macOS*
		/// Set the about panel options. This will override the values
		/// defined in the app's .plist file. See the Apple docs for more details.
		/// </summary>
		/// <param name="options"></param>
		public void setAboutPanelOptions(JsonObject options) {
			API.Apply("setAboutPanelOptions", options);
		}

		/// <summary>
		/// *macOS (mas)*
		/// Returns Function - This function must be called once you have finished
		/// accessing the security scoped file. 
		/// </summary>
		/// <param name="bookmarkData"></param>
		public void startAccessingSecurityScopedResource(string bookmarkData) {
			API.Apply("startAccessingSecurityScopedResource", bookmarkData);
		}

		/// <summary>
		/// *Experimental macOS Windows*
		/// Enables mixed sandbox mode on the app.
		/// This method can only be called before app is ready.
		/// </summary>
		public void enableMixedSandbox() {
			API.Apply("enableMixedSandbox");
		}

		/// <summary>
		/// *macOS*
		/// Returns Boolean - Whether the application is currently running
		/// from the systems Application folder.
		/// Use in combination with app.moveToApplicationsFolder()
		/// </summary>
		/// <returns></returns>
		public bool isInApplicationsFolder() {
			return API.Apply<bool>("isInApplicationsFolder");
		}

		/// <summary>
		/// *macOS*
		/// Returns Boolean - Whether the move was successful.
		/// Please note that if the move is successful your application will quit and relaunch.
		/// </summary>
		/// <returns></returns>
		public bool moveToApplicationsFolder() {
			return API.Apply<bool>("moveToApplicationsFolder");
		}
	}
}
