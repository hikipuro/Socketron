using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class ElectronModule : JSObject {
		/// <summary>
		/// Control your application's event lifecycle.
		/// <para>Process: Main</para>
		/// </summary>
		public App app {
			get { return API.GetObject<App>("app"); }
		}
		/// <summary>
		/// Enable apps to automatically update themselves.
		/// <para>Process: Main</para>
		/// </summary>
		public AutoUpdater autoUpdater {
			get { return API.GetObject<AutoUpdater>("autoUpdater"); }
		}
		/// <summary>
		/// Create and control views.
		/// <para>Process: Main</para>
		/// </summary>
		public BrowserViewModule BrowserView {
			get { return API.GetObject<BrowserViewModule>("BrowserView"); }
		}
		/// <summary>
		/// Create and control browser windows.
		/// <para>Process: Main</para>
		/// </summary>
		public BrowserWindowModule BrowserWindow {
			get { return API.GetObject<BrowserWindowModule>("BrowserWindow"); }
		}
		/// <summary>
		/// Manipulate the child browser window.
		/// <para>Process: Renderer</para>
		/// </summary>
		public BrowserWindowProxy BrowserWindowProxy {
			get { return API.GetObject<BrowserWindowProxy>("BrowserWindowProxy"); }
		}
		/// <summary>
		/// Make HTTP/HTTPS requests.
		/// <para>Process: Main</para>
		/// </summary>
		public ClientRequest ClientRequest {
			get { return API.GetObject<ClientRequest>("ClientRequest"); }
		}
		/// <summary>
		/// Perform copy and paste operations on the system clipboard.
		/// <para>Process: Main, Renderer</para>
		/// </summary>
		public Clipboard clipboard {
			get { return API.GetObject<Clipboard>("clipboard"); }
		}
		/// <summary>
		/// Collect tracing data from Chromium's content module
		/// for finding performance bottlenecks and slow operations.
		/// <para>Process: Main</para>
		/// </summary>
		public ContentTracing contentTracing {
			get { return API.GetObject<ContentTracing>("contentTracing"); }
		}
		/// <summary>
		/// Query and modify a session's cookies.
		/// <para>Process: Main</para>
		/// </summary>
		public Cookies Cookies {
			get { return API.GetObject<Cookies>("Cookies"); }
		}
		/// <summary>
		/// Submit crash reports to a remote server.
		/// <para>Process: Main, Renderer</para>
		/// </summary>
		public CrashReporter crashReporter {
			get { return API.GetObject<CrashReporter>("crashReporter"); }
		}
		/// <summary>
		/// An alternate transport for Chrome's remote debugging protocol.
		/// <para>Process: Main</para>
		/// </summary>
		public Debugger Debugger {
			get { return API.GetObject<Debugger>("Debugger"); }
		}
		/// <summary>
		/// Access information about media sources that can be used
		/// to capture audio and video from the desktop using
		/// the navigator.mediaDevices.getUserMedia API.
		/// <para>Process: Renderer</para>
		/// </summary>
		public DesktopCapturer desktopCapturer {
			get { return API.GetObject<DesktopCapturer>("desktopCapturer"); }
		}
		/// <summary>
		/// Display native system dialogs for opening and saving files, alerting, etc.
		/// <para>Process: Main</para>
		/// </summary>
		public Dialog dialog {
			get { return API.GetObject<Dialog>("dialog"); }
		}
		/// <summary>
		/// Control file downloads from remote sources.
		/// <para>Process: Main</para>
		/// </summary>
		public DownloadItem DownloadItem {
			get { return API.GetObject<DownloadItem>("DownloadItem"); }
		}
		/// <summary>
		/// Detect keyboard events when the application does not have keyboard focus.
		/// <para>Process: Main</para>
		/// </summary>
		public GlobalShortcut globalShortcut {
			get { return API.GetObject<GlobalShortcut>("globalShortcut"); }
		}
		/// <summary>
		/// Handle responses to HTTP/HTTPS requests.
		/// <para>Process: Main</para>
		/// </summary>
		public IncomingMessage IncomingMessage {
			get { return API.GetObject<IncomingMessage>("IncomingMessage"); }
		}
		/// <summary>
		/// Communicate asynchronously from the main process to renderer processes.
		/// <para>Process: Main</para>
		/// </summary>
		public IPCMain ipcMain {
			get { return API.GetObject<IPCMain>("ipcMain"); }
		}
		/// <summary>
		/// Communicate asynchronously from a renderer process to the main process.
		/// <para>Process: Renderer</para>
		/// </summary>
		public IPCRenderer ipcRenderer {
			get { return API.GetObject<IPCRenderer>("ipcRenderer"); }
		}
		/// <summary>
		/// Create native application menus and context menus.
		/// <para>Process: Main</para>
		/// </summary>
		public MenuModule Menu {
			get { return API.GetObject<MenuModule>("Menu"); }
		}
		/// <summary>
		/// Add items to native application menus and context menus.
		/// <para>Process: Main</para>
		/// </summary>
		public MenuItemModule MenuItem {
			get { return API.GetObject<MenuItemModule>("MenuItem"); }
		}
		/// <summary>
		/// Create tray, dock, and application icons using PNG or JPG files.
		/// <para>Process: Main, Renderer</para>
		/// </summary>
		public NativeImageModule nativeImage {
			get { return API.GetObject<NativeImageModule>("nativeImage"); }
		}
		/// <summary>
		/// Issue HTTP/HTTPS requests using Chromium's native networking library.
		/// <para>Process: Main</para>
		/// </summary>
		public Net net {
			get { return API.GetObject<Net>("net"); }
		}
		/// <summary>
		/// Create OS desktop notifications.
		/// <para>Process: Main</para>
		/// </summary>
		public NotificationModule Notification {
			get { return API.GetObject<NotificationModule>("Notification"); }
		}
		/// <summary>
		/// Monitor power state changes.
		/// <para>Process: Main</para>
		/// </summary>
		public PowerMonitor powerMonitor {
			get { return API.GetObject<PowerMonitor>("powerMonitor"); }
		}
		/// <summary>
		/// Block the system from entering low-power (sleep) mode.
		/// <para>Process: Main</para>
		/// </summary>
		public PowerSaveBlocker powerSaveBlocker {
			get { return API.GetObject<PowerSaveBlocker>("powerSaveBlocker"); }
		}
		/// <summary>
		/// Register a custom protocol and intercept existing protocol requests.
		/// <para>Process: Main</para>
		/// </summary>
		public Protocol protocol {
			get { return API.GetObject<Protocol>("protocol"); }
		}
		/// <summary>
		/// Use main process modules from the renderer process.
		/// <para>Process: Renderer</para>
		/// </summary>
		public Remote remote {
			get { return API.GetObject<Remote>("remote"); }
		}
		/// <summary>
		/// Retrieve information about screen size, displays, cursor position, etc.
		/// <para>Process: Main, Renderer</para>
		/// </summary>
		public Screen screen {
			get { return API.GetObject<Screen>("screen"); }
		}
		/// <summary>
		/// Manage browser sessions, cookies, cache, proxy settings, etc.
		/// <para>Process: Main</para>
		/// </summary>
		public SessionModule session {
			get { return API.GetObject<SessionModule>("session"); }
		}
		/// <summary>
		/// Manage files and URLs using their default applications.
		/// <para>Process: Main, Renderer</para>
		/// </summary>
		public Shell shell {
			get { return API.GetObject<Shell>("shell"); }
		}
		/// <summary>
		/// Get system preferences.
		/// <para>Process: Main</para>
		/// </summary>
		public SystemPreferences systemPreferences {
			get { return API.GetObject<SystemPreferences>("systemPreferences"); }
		}
		/// <summary>
		/// Create a button in the touch bar for native macOS applications.
		/// <para>Process: Main</para>
		/// </summary>
		public TouchBarButtonModule TouchBarButton {
			get { return API.GetObject<TouchBarButtonModule>("TouchBarButton"); }
		}
		/// <summary>
		/// Create a color picker in the touch bar for native macOS applications.
		/// <para>Process: Main</para>
		/// </summary>
		public TouchBarColorPickerModule TouchBarColorPicker {
			get { return API.GetObject<TouchBarColorPickerModule>("TouchBarColorPicker"); }
		}
		/// <summary>
		/// Create a group in the touch bar for native macOS applications.
		/// <para>Process: Main</para>
		/// </summary>
		public TouchBarGroupModule TouchBarGroup {
			get { return API.GetObject<TouchBarGroupModule>("TouchBarGroup"); }
		}
		/// <summary>
		/// Create a label in the touch bar for native macOS applications.
		/// <para>Process: Main</para>
		/// </summary>
		public TouchBarLabelModule TouchBarLabel {
			get { return API.GetObject<TouchBarLabelModule>("TouchBarLabel"); }
		}
		/// <summary>
		/// Create a popover in the touch bar for native macOS applications.
		/// <para>Process: Main</para>
		/// </summary>
		public TouchBarPopoverModule TouchBarPopover {
			get { return API.GetObject<TouchBarPopoverModule>("TouchBarPopover"); }
		}
		/// <summary>
		/// Create a scrubber (a scrollable selector).
		/// <para>Process: Main</para>
		/// </summary>
		public TouchBarScrubberModule TouchBarScrubber {
			get { return API.GetObject<TouchBarScrubberModule>("TouchBarScrubber"); }
		}
		/// <summary>
		/// Create a segmented control (a button group) where one button has a selected state.
		/// <para>Process: Main</para>
		/// </summary>
		public TouchBarSegmentedControlModule TouchBarSegmentedControl {
			get { return API.GetObject<TouchBarSegmentedControlModule>("TouchBarSegmentedControl"); }
		}
		/// <summary>
		/// Create a slider in the touch bar for native macOS applications.
		/// <para>Process: Main</para>
		/// </summary>
		public TouchBarSliderModule TouchBarSlider {
			get { return API.GetObject<TouchBarSliderModule>("TouchBarSlider"); }
		}
		/// <summary>
		/// Create a spacer between two items in the touch bar for native macOS applications.
		/// <para>Process: Main</para>
		/// </summary>
		public TouchBarSpacerModule TouchBarSpacer {
			get { return API.GetObject<TouchBarSpacerModule>("TouchBarSpacer"); }
		}
		/// <summary>
		/// Create TouchBar layouts for native macOS applications.
		/// <para>Process: Main</para>
		/// </summary>
		public TouchBarModule TouchBar {
			get { return API.GetObject<TouchBarModule>("TouchBar"); }
		}
		/// <summary>
		/// Add icons and context menus to the system's notification area.
		/// <para>Process: Main</para>
		/// </summary>
		public TrayModule Tray {
			get { return API.GetObject<TrayModule>("Tray"); }
		}
		/// <summary>
		/// Render and control web pages.
		/// <para>Process: Main</para>
		/// </summary>
		public WebContentsModule webContents {
			get { return API.GetObject<WebContentsModule>("webContents"); }
		}
		/// <summary>
		/// Customize the rendering of the current web page.
		/// <para>Process: Renderer</para>
		/// </summary>
		public WebFrame webFrame {
			get { return API.GetObject<WebFrame>("webFrame"); }
		}

		public ElectronModule() {
		}
	}
}
