namespace Socketron.Electron {
	public class ElectronModule : NodeModule {
		public AppModule app;
		public AutoUpdaterModule autoUpdater;
		public BrowserViewModule BrowserView;
		public BrowserWindowModule BrowserWindow;
		public ClipboardModule clipboard;
		public ContentTracingModule contentTracing;
		public CrashReporterModule crashReporter;
		public DialogModule dialog;
		public GlobalShortcutModule globalShortcut;
		public IPCMainModule ipcMain;
		public MenuModule Menu;
		public MenuItemModule MenuItem;
		public NativeImageModule nativeImage;
		public NetModule net;
		public NotificationModule Notification;
		public PowerMonitorModule powerMonitor;
		public PowerSaveBlockerModule powerSaveBlocker;
		public ProtocolModule protocol;
		public ScreenModule screen;
		public SessionModule session;
		public ShellModule shell;
		public SystemPreferencesModule systemPreferences;
		public TrayModule Tray;
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
