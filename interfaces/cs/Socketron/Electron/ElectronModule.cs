namespace Socketron {
	public class ElectronModule {
		public App app;
		public AutoUpdater autoUpdater;
		public BrowserWindowClass BrowserWindow;
		public Clipboard clipboard;
		public Dialog dialog;
		public GlobalShortcut globalShortcut;
		public IPCMainClass ipcMain;
		public MenuClass Menu;
		public NativeImageClass nativeImage;
		public Net net;
		public NotificationClass Notification;
		public ScreenClass screen;
		public ShellClass shell;
		public SystemPreferences systemPreferences;

		public ElectronModule(SocketronClient client) {
			app = new App(client);
			autoUpdater = new AutoUpdater(client);
			BrowserWindow = new BrowserWindowClass();
			clipboard = new Clipboard(client);
			dialog = new Dialog();
			globalShortcut = new GlobalShortcut(client);
			ipcMain = new IPCMainClass(client);
			Menu = new MenuClass();
			nativeImage = new NativeImageClass();
			net = new Net();
			Notification = new NotificationClass(client);
			screen = new ScreenClass(client);
			shell = new ShellClass(client);
			systemPreferences = new SystemPreferences(client);
		}
	}
}
