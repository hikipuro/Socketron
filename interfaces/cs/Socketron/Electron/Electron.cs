namespace Socketron {
	public class Electron {
		public AppClass app;
		public BrowserWindowClass BrowserWindow;
		public ClipboardClass clipboard;
		public DialogClass dialog;
		public NativeImageClass nativeImage;
		public MenuClass Menu;
		public MenuItemClass MenuItem;
		public IPCMainClass ipcMain;
		public NotificationClass Notification;
		public ScreenClass screen;
		public ShellClass shell;
		public SystemPreferencesClass systemPreferences;
		public GlobalShortcutClass globalShortcut;
		public TrayClass Tray;

		public Electron(Socketron socketron) {
			app = new AppClass(socketron);
			BrowserWindow = new BrowserWindowClass(socketron);
			clipboard = new ClipboardClass(socketron);
			dialog = new DialogClass(socketron);
			nativeImage = new NativeImageClass(socketron);
			Menu = new MenuClass(socketron);
			MenuItem = new MenuItemClass(socketron);
			ipcMain = new IPCMainClass(socketron);
			Notification = new NotificationClass(socketron);
			screen = new ScreenClass(socketron);
			shell = new ShellClass(socketron);
			systemPreferences = new SystemPreferencesClass(socketron);
			globalShortcut = new GlobalShortcutClass(socketron);
			Tray = new TrayClass(socketron);
		}
	}
}
