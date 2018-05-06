namespace Socketron {
	public class ElectronModule : NodeModule {
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

		public ElectronModule() {
			_client = SocketronClient.GetCurrent();
		}

		public void Init() {
			app = new App(
				_client, GetModule("app")
			);
			autoUpdater = new AutoUpdater(
				_client, GetModule("autoUpdater")
			);
			BrowserWindow = new BrowserWindowClass(
				_client, GetModule("BrowserWindow")
			);
			Socketron.BrowserWindow._Class = BrowserWindow;

			clipboard = new Clipboard(
				_client, GetModule("clipboard")
			);
			contentTracing = new ContentTracing(
				_client, GetModule("contentTracing")
			);
			crashReporter = new CrashReporter(
				_client, GetModule("crashReporter")
			);
			dialog = new Dialog(
				_client, GetModule("dialog")
			);
			globalShortcut = new GlobalShortcut(
				_client, GetModule("globalShortcut")
			);
			ipcMain = new IPCMainClass(
				_client, GetModule("ipcMain")
			);
			Menu = new MenuClass(
				_client, GetModule("Menu")
			);
			nativeImage = new NativeImageClass(
				_client, GetModule("nativeImage")
			);
			NativeImage._Class = nativeImage;

			net = new Net(
				_client, GetModule("net")
			);
			Notification = new NotificationClass(
				_client, GetModule("Notification")
			);
			Socketron.Notification._Class = Notification;

			powerMonitor = new PowerMonitor(
				_client, GetModule("powerMonitor")
			);
			powerSaveBlocker = new PowerSaveBlocker(
				_client, GetModule("powerSaveBlocker")
			);
			protocol = new Protocol(
				_client, GetModule("protocol")
			);
			screen = new ScreenClass(
				_client, GetModule("screen")
			);
			shell = new ShellClass(
				_client, GetModule("shell")
			);
			systemPreferences = new SystemPreferences(
				_client, GetModule("systemPreferences")
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
