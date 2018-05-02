namespace Socketron {
	public class Electron {
		public BrowserWindowClass BrowserWindow;
		public ClipboardClass Clipboard;
		public DialogClass Dialog;
		public NativeImageClass NativeImage;
		public MenuClass Menu;
		public IPCMainClass IPCMain;
		public NotificationClass Notification;
		public ScreenClass Screen;

		public Electron(Socketron socketron) {
			BrowserWindow = new BrowserWindowClass(socketron);
			Clipboard = new ClipboardClass(socketron);
			Dialog = new DialogClass(socketron);
			NativeImage = new NativeImageClass(socketron);
			Menu = new MenuClass(socketron);
			IPCMain = new IPCMainClass(socketron);
			Notification = new NotificationClass(socketron);
			Screen = new ScreenClass(socketron);
		}
	}
}
