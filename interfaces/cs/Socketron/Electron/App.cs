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
			public const string WillFinishLaunching = "will-finish-launching";
			public const string Ready = "ready";
			public const string WindowAllClosed = "window-all-closed";
			public const string BeforeQuit = "before-quit";
			public const string WillQuit = "will-quit";
			public const string Quit = "quit";
			/// <summary>*macOS*</summary>
			public const string OpenFile = "open-file";
			/// <summary>*macOS*</summary>
			public const string OpenUrl = "open-url";
			/// <summary>*macOS*</summary>
			public const string Activate = "activate";
			/// <summary>*macOS*</summary>
			public const string ContinueActivity = "continue-activity";
			/// <summary>*macOS*</summary>
			public const string WillContinueActivity = "will-continue-activity";
			/// <summary>*macOS*</summary>
			public const string ContinueActivityError = "continue-activity-error";
			/// <summary>*macOS*</summary>
			public const string ActivityWasContinued = "activity-was-continued";
			/// <summary>*macOS*</summary>
			public const string UpdateActivityState = "update-activity-state";
			/// <summary>*macOS*</summary>
			public const string NewWindowForTab = "new-window-for-tab";
			public const string BrowserWindowBlur = "browser-window-blur";
			public const string BrowserWindowFocus = "browser-window-focus";
			public const string BrowserWindowCreated = "browser-window-created";
			public const string WebContentsCreated = "web-contents-created";
			public const string CertificateError = "certificate-error";
			public const string SelectClientCertificate = "select-client-certificate";
			public const string Login = "login";
			public const string GpuProcessCrashed = "gpu-process-crashed";
			/// <summary>*macOS Windows*</summary>
			public const string AccessibilitySupportChanged = "accessibility-support-changed";
			public const string SessionCreated = "session-created";
		}
	}
}
