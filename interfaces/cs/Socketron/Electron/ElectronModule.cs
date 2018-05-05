namespace Socketron {
	public class ElectronModule {
		public App app;
		public AutoUpdater autoUpdater;
		public BrowserWindowClass BrowserWindow;
		public Clipboard clipboard;
		public ContentTracing contentTracing;
		public CrashReporter crashReporter;
		public Dialog dialog;
		public GlobalShortcut globalShortcut;
		public IPCMainClass ipcMain;
		public MenuClass Menu;
		public NativeImageClass nativeImage;
		public Net net;
		public NotificationClass Notification;
		public PowerMonitor powerMonitor;
		public PowerSaveBlocker powerSaveBlocker;
		public Protocol protocol;
		public ScreenClass screen;
		public ShellClass shell;
		public SystemPreferences systemPreferences;

		public ElectronModule(SocketronClient client) {
			app = new App(client);
			autoUpdater = new AutoUpdater(client);
			BrowserWindow = new BrowserWindowClass();
			clipboard = new Clipboard(client);
			contentTracing = new ContentTracing(client);
			crashReporter = new CrashReporter(client);
			dialog = new Dialog();
			globalShortcut = new GlobalShortcut(client);
			ipcMain = new IPCMainClass(client);
			Menu = new MenuClass();
			nativeImage = new NativeImageClass();
			net = new Net(client);
			Notification = new NotificationClass(client);
			powerMonitor = new PowerMonitor(client);
			powerSaveBlocker = new PowerSaveBlocker(client);
			protocol = new Protocol(client);
			screen = new ScreenClass(client);
			shell = new ShellClass(client);
			systemPreferences = new SystemPreferences(client);
		}
	}
}
