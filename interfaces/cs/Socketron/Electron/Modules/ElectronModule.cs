namespace Socketron.Electron {
	public class ElectronModule : JSModule {
		/// <summary>
		/// Control your application's event lifecycle.
		/// <para>Process: Main</para>
		/// </summary>
		public AppModule app;
		/// <summary>
		/// Enable apps to automatically update themselves.
		/// <para>Process: Main</para>
		/// </summary>
		public AutoUpdaterModule autoUpdater;
		/// <summary>
		/// Create and control views.
		/// <para>Process: Main</para>
		/// </summary>
		public BrowserViewModule BrowserView;
		/// <summary>
		/// Create and control browser windows.
		/// <para>Process: Main</para>
		/// </summary>
		public BrowserWindowModule BrowserWindow;
		/// <summary>
		/// Perform copy and paste operations on the system clipboard.
		/// <para>Process: Main, Renderer</para>
		/// </summary>
		public ClipboardModule clipboard;
		/// <summary>
		/// Collect tracing data from Chromium's content module
		/// for finding performance bottlenecks and slow operations.
		/// <para>Process: Main</para>
		/// </summary>
		public ContentTracingModule contentTracing;
		/// <summary>
		/// Submit crash reports to a remote server.
		/// <para>Process: Main, Renderer</para>
		/// </summary>
		public CrashReporterModule crashReporter;
		/// <summary>
		/// Display native system dialogs for opening and saving files, alerting, etc.
		/// <para>Process: Main</para>
		/// </summary>
		public DialogModule dialog;
		/// <summary>
		/// Detect keyboard events when the application does not have keyboard focus.
		/// <para>Process: Main</para>
		/// </summary>
		public GlobalShortcutModule globalShortcut;
		/// <summary>
		/// Communicate asynchronously from the main process to renderer processes.
		/// <para>Process: Main</para>
		/// </summary>
		public IPCMainModule ipcMain;
		/// <summary>
		/// Create native application menus and context menus.
		/// <para>Process: Main</para>
		/// </summary>
		public MenuModule Menu;
		/// <summary>
		/// Add items to native application menus and context menus.
		/// <para>Process: Main</para>
		/// </summary>
		public MenuItemModule MenuItem;
		/// <summary>
		/// Create tray, dock, and application icons using PNG or JPG files.
		/// <para>Process: Main, Renderer</para>
		/// </summary>
		public NativeImageModule nativeImage;
		/// <summary>
		/// Issue HTTP/HTTPS requests using Chromium's native networking library.
		/// <para>Process: Main</para>
		/// </summary>
		public NetModule net;
		/// <summary>
		/// Create OS desktop notifications.
		/// <para>Process: Main</para>
		/// </summary>
		public NotificationModule Notification;
		/// <summary>
		/// Monitor power state changes.
		/// <para>Process: Main</para>
		/// </summary>
		public PowerMonitorModule powerMonitor;
		/// <summary>
		/// Block the system from entering low-power (sleep) mode.
		/// <para>Process: Main</para>
		/// </summary>
		public PowerSaveBlockerModule powerSaveBlocker;
		/// <summary>
		/// Register a custom protocol and intercept existing protocol requests.
		/// <para>Process: Main</para>
		/// </summary>
		public ProtocolModule protocol;
		/// <summary>
		/// Retrieve information about screen size, displays, cursor position, etc.
		/// <para>Process: Main, Renderer</para>
		/// </summary>
		public ScreenModule screen;
		/// <summary>
		/// Manage browser sessions, cookies, cache, proxy settings, etc.
		/// <para>Process: Main</para>
		/// </summary>
		public SessionModule session;
		/// <summary>
		/// Manage files and URLs using their default applications.
		/// <para>Process: Main, Renderer</para>
		/// </summary>
		public ShellModule shell;
		/// <summary>
		/// Get system preferences.
		/// <para>Process: Main</para>
		/// </summary>
		public SystemPreferencesModule systemPreferences;
		/// <summary>
		/// Add icons and context menus to the system's notification area.
		/// <para>Process: Main</para>
		/// </summary>
		public TrayModule Tray;
		/// <summary>
		/// Render and control web pages.
		/// <para>Process: Main</para>
		/// </summary>
		public WebContentsModule webContents;

		public ElectronModule() {
			_client = SocketronClient.GetCurrent();
		}

		public void Init() {
			app = new AppModule(
				_client, GetModule("app")
			);
			autoUpdater = new AutoUpdaterModule(
				_client, GetModule("autoUpdater")
			);
			BrowserView = new BrowserViewModule(
				_client, GetModule("BrowserView")
			);
			BrowserWindow = new BrowserWindowModule(
				_client, GetModule("BrowserWindow")
			);
			clipboard = new ClipboardModule(
				_client, GetModule("clipboard")
			);
			contentTracing = new ContentTracingModule(
				_client, GetModule("contentTracing")
			);
			crashReporter = new CrashReporterModule(
				_client, GetModule("crashReporter")
			);
			dialog = new DialogModule(
				_client, GetModule("dialog")
			);
			globalShortcut = new GlobalShortcutModule(
				_client, GetModule("globalShortcut")
			);
			ipcMain = new IPCMainModule(
				_client, GetModule("ipcMain")
			);
			Menu = new MenuModule(
				_client, GetModule("Menu")
			);
			MenuItem = new MenuItemModule(
				_client, GetModule("MenuItem")
			);
			nativeImage = new NativeImageModule(
				_client, GetModule("nativeImage")
			);
			net = new NetModule(
				_client, GetModule("net")
			);
			Notification = new NotificationModule(
				_client, GetModule("Notification")
			);
			powerMonitor = new PowerMonitorModule(
				_client, GetModule("powerMonitor")
			);
			powerSaveBlocker = new PowerSaveBlockerModule(
				_client, GetModule("powerSaveBlocker")
			);
			protocol = new ProtocolModule(
				_client, GetModule("protocol")
			);
			screen = new ScreenModule(
				_client, GetModule("screen")
			);
			session = new SessionModule(
				_client, GetModule("session")
			);
			shell = new ShellModule(
				_client, GetModule("shell")
			);
			systemPreferences = new SystemPreferencesModule(
				_client, GetModule("systemPreferences")
			);
			Tray = new TrayModule(
				_client, GetModule("Tray")
			);
			webContents = new WebContentsModule(
				_client, GetModule("webContents")
			);
		}

		protected int GetModule(string name) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var module = {0}.{1};",
					"return {2};"
				),
				Script.GetObject(_id),
				name,
				Script.AddObject("module")
			);
			return _ExecuteBlocking<int>(script);
		}
	}
}
