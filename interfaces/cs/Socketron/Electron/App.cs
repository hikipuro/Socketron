namespace Socketron {
	/// <summary>
	/// Control your application's event lifecycle.
	/// <para>Process: Main</para>
	/// </summary>
	public class App {
		/// <summary>
		/// App object events.
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
		public class Paths {
			/// <summary>
			/// User's home directory.
			/// </summary>
			public const string home = "home";
			/// <summary>
			/// Per-user application data directory, which by default points to:
			/// - %APPDATA% on Windows
			/// - $XDG_CONFIG_HOME or ~/.config on Linux
			/// - ~/Library/Application Support on macOS
			/// </summary>
			public const string appData = "appData";
			/// <summary>
			/// The directory for storing your app's configuration files,
			/// which by default it is the appData directory appended with your app's name.
			/// </summary>
			public const string userData = "userData";
			/// <summary>
			/// Temporary directory.
			/// </summary>
			public const string temp = "temp";
			/// <summary>
			/// The current executable file.
			/// </summary>
			public const string exe = "exe";
			/// <summary>
			/// The libchromiumcontent library.
			/// </summary>
			public const string module = "module";
			/// <summary>
			/// The current user's Desktop directory.
			/// </summary>
			public const string desktop = "desktop";
			/// <summary>
			/// Directory for a user's "My Documents".
			/// </summary>
			public const string documents = "documents";
			/// <summary>
			/// Directory for a user's downloads.
			/// </summary>
			public const string downloads = "downloads";
			/// <summary>
			/// Directory for a user's music.
			/// </summary>
			public const string music = "music";
			/// <summary>
			/// Directory for a user's pictures.
			/// </summary>
			public const string pictures = "pictures";
			/// <summary>
			/// Directory for a user's videos.
			/// </summary>
			public const string videos = "videos";
			/// <summary>
			/// Directory for your app's log folder.
			/// </summary>
			public const string logs = "logs";
			/// <summary>
			/// Full path to the system version of the Pepper Flash plugin.
			/// </summary>
			public const string pepperFlashSystemPlugin = "pepperFlashSystemPlugin";
		}
	}
}
