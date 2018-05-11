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
		/// Make HTTP/HTTPS requests.
		/// <para>Process: Main</para>
		/// </summary>
		public ClientRequest ClientRequest;
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
		}

		public void Init() {
			app = API.GetObject<AppModule>("app");
			autoUpdater = API.GetObject<AutoUpdaterModule>("autoUpdater");
			BrowserView = API.GetObject<BrowserViewModule>("BrowserView");
			BrowserWindow = API.GetObject<BrowserWindowModule>("BrowserWindow");
			ClientRequest = API.GetObject<ClientRequest>("ClientRequest");
			clipboard = API.GetObject<ClipboardModule>("clipboard");
			contentTracing = API.GetObject<ContentTracingModule>("contentTracing");
			crashReporter = API.GetObject<CrashReporterModule>("crashReporter");
			dialog = API.GetObject<DialogModule>("dialog");
			globalShortcut = API.GetObject<GlobalShortcutModule>("globalShortcut");
			ipcMain = API.GetObject<IPCMainModule>("ipcMain");
			Menu = API.GetObject<MenuModule>("Menu");
			MenuItem = API.GetObject<MenuItemModule>("MenuItem");
			nativeImage = API.GetObject<NativeImageModule>("nativeImage");
			net = API.GetObject<NetModule>("net");
			Notification = API.GetObject<NotificationModule>("Notification");
			powerMonitor = API.GetObject<PowerMonitorModule>("powerMonitor");
			powerSaveBlocker = API.GetObject<PowerSaveBlockerModule>("powerSaveBlocker");
			protocol = API.GetObject<ProtocolModule>("protocol");
			screen = API.GetObject<ScreenModule>("screen");
			session = API.GetObject<SessionModule>("session");
			shell = API.GetObject<ShellModule>("shell");
			systemPreferences = API.GetObject<SystemPreferencesModule>("systemPreferences");
			Tray = API.GetObject<TrayModule>("Tray");
			webContents = API.GetObject<WebContentsModule>("webContents");
		}
	}
}
