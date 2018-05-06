namespace Socketron.Electron {
	public partial class BrowserWindow {
		/// <summary>
		/// BrowserWindow craete options.
		/// </summary>
		public class Options {
			/// <summary>
			/// (optional) Window's width in pixels. Default is 800.
			/// </summary>
			public int? width;
			/// <summary>
			/// (optional) Window's height in pixels. Default is 600.
			/// </summary>
			public int? height;
			/// <summary>
			/// (optional) (required if y is used) Window's left offset from screen.
			/// Default is to center the window.
			/// </summary>
			public int? x;
			/// <summary>
			/// (optional) (required if x is used) Window's top offset from screen.
			/// Default is to center the window.
			/// </summary>
			public int? y;
			/// <summary>
			/// (optional) The width and height would be used as web page's size,
			/// which means the actual window's size will include window frame's size
			/// and be slightly larger. Default is false.
			/// </summary>
			public bool? useContentSize;
			/// <summary>
			/// (optional) Show window in the center of the screen.
			/// </summary>
			public bool? center;
			/// <summary>
			/// (optional) Window's minimum width. Default is 0.
			/// </summary>
			public int? minWidth;
			/// <summary>
			/// (optional) Window's minimum height. Default is 0.
			/// </summary>
			public int? minHeight;
			/// <summary>
			/// (optional) Window's maximum width. Default is no limit.
			/// </summary>
			public int? maxWidth;
			/// <summary>
			/// (optional) Window's maximum height. Default is no limit.
			/// </summary>
			public int? maxHeight;
			/// <summary>
			/// (optional) Whether window is resizable. Default is true.
			/// </summary>
			public bool? resizable;
			/// <summary>
			/// (optional) Whether window is movable.
			/// This is not implemented on Linux. Default is true.
			/// </summary>
			public bool? movable;
			/// <summary>
			/// (optional) Whether window is minimizable.
			/// This is not implemented on Linux. Default is true.
			/// </summary>
			public bool? minimizable;
			/// <summary>
			/// (optional) Whether window is maximizable.
			/// This is not implemented on Linux. Default is true.
			/// </summary>
			public bool? maximizable;
			/// <summary>
			/// (optional) Whether window is closable.
			/// This is not implemented on Linux. Default is true.
			/// </summary>
			public bool? closable;
			/// <summary>
			/// (optional) Whether the window can be focused. Default is true.
			/// On Windows setting focusable: false also implies setting skipTaskbar: true.
			/// On Linux setting focusable: false makes the window stop interacting with wm,
			/// so the window will always stay on top in all workspaces.
			/// </summary>
			public bool? focusable;
			/// <summary>
			/// (optional) Whether the window should always stay on top of other windows. Default is false.
			/// </summary>
			public bool? alwaysOnTop;
			/// <summary>
			/// (optional) Whether the window should show in fullscreen.
			/// When explicitly set to false the fullscreen button will be hidden or disabled on macOS.
			/// Default is false.
			/// </summary>
			public bool? fullscreen;
			/// <summary>
			/// (optional) Whether the window can be put into fullscreen mode.
			/// On macOS, also whether the maximize/zoom button should toggle full screen mode
			/// or maximize window. Default is true.
			/// </summary>
			public bool? fullscreenable;
			/// <summary>
			/// (optional) Use pre-Lion fullscreen on macOS. Default is false.
			/// </summary>
			public bool? simpleFullscreen;
			/// <summary>
			/// (optional) Whether to show the window in taskbar. Default is false.
			/// </summary>
			public bool? skipTaskbar;
			/// <summary>
			/// (optional) The kiosk mode. Default is false.
			/// </summary>
			public bool? kiosk;
			/// <summary>
			/// (optional) Default window title. Default is "Electron".
			/// </summary>
			public string title;
			/// <summary>
			/// (optional) The window icon.
			/// On Windows it is recommended to use ICO icons to get best visual effects,
			/// you can also leave it undefined so the executable's icon will be used.
			/// </summary>
			public NativeImage icon;
			/// <summary>
			/// (optional) Whether window should be shown when created. Default is true.
			/// </summary>
			public bool? show;
			/// <summary>
			/// (optional) Specify false to create a Frameless Window. Default is true.
			/// </summary>
			public bool? frame;
			/// <summary>
			/// (optional) Specify parent window. Default is null.
			/// </summary>
			public BrowserWindow parent;
			/// <summary>
			/// (optional) Whether this is a modal window.
			/// This only works when the window is a child window. Default is false.
			/// </summary>
			public bool? modal;
			/// <summary>
			/// (optional) Whether the web view accepts a single mouse-down event
			/// that simultaneously activates the window. Default is false.
			/// </summary>
			public bool? acceptFirstMouse;
			/// <summary>
			/// (optional) Whether to hide cursor when typing. Default is false.
			/// </summary>
			public bool? disableAutoHideCursor;
			/// <summary>
			/// (optional) Auto hide the menu bar unless the Alt key is pressed. Default is false.
			/// </summary>
			public bool? autoHideMenuBar;
			/// <summary>
			/// (optional) Enable the window to be resized larger than screen. Default is false.
			/// </summary>
			public bool? enableLargerThanScreen;
			/// <summary>
			/// (optional) Window's background color as a hexadecimal value,
			/// like #66CD00 or #FFF or #80FFFFFF (alpha is supported). Default is #FFF (white).
			/// </summary>
			public string backgroundColor;
			/// <summary>
			/// (optional) Whether window should have a shadow.
			/// This is only implemented on macOS. Default is true.
			/// </summary>
			public bool? hasShadow;
			/// <summary>
			/// (optional) Set the initial opacity of the window,
			/// between 0.0 (fully transparent) and 1.0 (fully opaque).
			/// This is only implemented on Windows and macOS.
			/// </summary>
			public double? opacity;
			/// <summary>
			/// (optional) Forces using dark theme for the window,
			/// only works on some GTK+3 desktop environments. Default is false.
			/// </summary>
			public bool? darkTheme;
			/// <summary>
			/// (optional) Makes the window transparent. Default is false.
			/// </summary>
			public bool? transparent;
			/// <summary>
			/// (optional) The type of window, default is normal window.
			/// </summary>
			public string type;
			/// <summary>
			/// (optional) The style of window title bar. Default is default.
			/// </summary>
			public string titleBarStyle;
			/// <summary>
			/// (optional) Shows the title in the title bar in full screen mode
			/// on macOS for all titleBarStyle options. Default is false.
			/// </summary>
			public bool? fullscreenWindowTitle;
			/// <summary>
			/// (optional) Use WS_THICKFRAME style for frameless windows on Windows,
			/// which adds standard window frame. Setting it to false will remove
			/// window shadow and window animations. Default is true.
			/// </summary>
			public bool? thickFrame;
			/// <summary>
			/// (optional) Add a type of vibrancy effect to the window, only on macOS.
			/// Can be appearance-based, light, dark, titlebar, selection, menu, popover,
			/// sidebar, medium-light or ultra-dark.
			/// Please note that using frame: false in combination with a vibrancy value
			/// requires that you use a non-default titleBarStyle as well.
			/// </summary>
			public string vibrancy;
			/// <summary>
			/// (optional) Controls the behavior on macOS when option-clicking
			/// the green stoplight button on the toolbar or by clicking the Window > Zoom menu item.
			/// If true, the window will grow to the preferred width of the web page when zoomed,
			/// false will cause it to zoom to the width of the screen.
			/// This will also affect the behavior when calling maximize() directly. Default is false.
			/// </summary>
			public bool? zoomToPageWidth;
			/// <summary>
			/// (optional) Tab group name, allows opening the window as a native tab on macOS 10.12+.
			/// Windows with the same tabbing identifier will be grouped together.
			/// This also adds a native new tab button to your window's tab bar
			/// and allows your app and window to receive the new-window-for-tab event.
			/// </summary>
			public string tabbingIdentifier;
			/// <summary>
			/// (optional) Settings of web page's features.
			/// </summary>
			public WebPreferences webPreferences = new WebPreferences();

			/// <summary>
			/// Parse JSON text.
			/// </summary>
			/// <param name="text"></param>
			/// <returns></returns>
			public static Options Parse(string text) {
				return JSON.Parse<Options>(text);
			}

			/// <summary>
			/// Create JSON text.
			/// </summary>
			/// <returns></returns>
			public string Stringify() {
				return JSON.Stringify(this);
			}
		}

		/// <summary>
		/// BrowserWindow.Options.type values.
		/// </summary>
		public class Types {
			/// <summary>
			/// *macOS Linux*
			/// </summary>
			public const string Desktop = "desktop";
			/// <summary>
			/// *Linux*
			/// </summary>
			public const string Dock = "dock";
			/// <summary>
			/// *Windows*
			/// </summary>
			public const string Toolbar = "toolbar";
			/// <summary>
			/// *Linux*
			/// </summary>
			public const string Splash = "splash";
			/// <summary>
			/// *Linux*
			/// </summary>
			public const string Notification = "notification";
			/// <summary>
			/// *macOS*
			/// </summary>
			public const string Textured = "textured";
		}

		/// <summary>
		/// BrowserWindow.Options.titleBarStyle values.
		/// </summary>
		public class TitleBarStyles {
			/// <summary>
			/// Results in the standard gray opaque Mac title bar.
			/// </summary>
			public const string Default = "default";
			/// <summary>
			/// Results in a hidden title bar and a full size content window,
			/// yet the title bar still has the standard window controls ("traffic lights") in the top left.
			/// </summary>
			public const string Hidden = "hidden";
			/// <summary>
			/// Results in a hidden title bar with an alternative look where the traffic light buttons
			/// are slightly more inset from the window edge.
			/// </summary>
			public const string HiddenInset = "hiddenInset";
			/// <summary>
			/// Boolean (optional) Draw custom close, minimize,
			/// and full screen buttons on macOS frameless windows.
			/// These buttons will not display unless hovered over in the top left of the window.
			/// These custom buttons prevent issues with mouse events that occur with
			/// the standard window toolbar buttons.
			/// Note: This option is currently experimental.
			/// </summary>
			public const string CustomButtonsOnHover = "customButtonsOnHover";
		}

		/// <summary>
		/// BrowserWindow.Options.defaultFontFamily keys.
		/// </summary>
		public class FontFamilyKeys {
			/// <summary>
			/// Defaults to Times New Roman.
			/// </summary>
			public const string Standard = "standard";
			/// <summary>
			/// Defaults to Times New Roman.
			/// </summary>
			public const string Serif = "serif";
			/// <summary>
			/// Defaults to Arial.
			/// </summary>
			public const string SansSerif = "sansSerif";
			/// <summary>
			/// Defaults to Courier New.
			/// </summary>
			public const string Monospace = "monospace";
			/// <summary>
			/// Defaults to Script.
			/// </summary>
			public const string Cursive = "cursive";
			/// <summary>
			/// Defaults to Impact.
			/// </summary>
			public const string Fantasy = "fantasy";
		}
	}
}
