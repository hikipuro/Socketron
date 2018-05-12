using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Add icons and context menus to the system's notification area.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Tray : JSObject {
		/// <summary>
		/// Tray instance events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when the tray icon is clicked.
			/// </summary>
			public const string Click = "click";
			/// <summary>
			/// *macOS Windows*
			/// Emitted when the tray icon is right clicked.
			/// </summary>
			public const string RightClick = "right-click";
			/// <summary>
			/// *macOS Windows*
			/// Emitted when the tray icon is double clicked.
			/// </summary>
			public const string DoubleClick = "double-click";
			/// <summary>
			/// *Windows*
			/// Emitted when the tray balloon shows.
			/// </summary>
			public const string BalloonShow = "balloon-show";
			/// <summary>
			/// *Windows*
			/// Emitted when the tray balloon is clicked.
			/// </summary>
			public const string BalloonClick = "balloon-click";
			/// <summary>
			/// *Windows*
			/// Emitted when the tray balloon is closed
			/// because of timeout or user manually closes it.
			/// </summary>
			public const string BalloonClosed = "balloon-closed";
			/// <summary>
			/// *macOS*
			/// Emitted when any dragged items are dropped on the tray icon.
			/// </summary>
			public const string Drop = "drop";
			/// <summary>
			/// *macOS*
			/// Emitted when dragged files are dropped in the tray icon.
			/// </summary>
			public const string DropFiles = "drop-files";
			/// <summary>
			/// *macOS*
			/// Emitted when dragged text is dropped in the tray icon.
			/// </summary>
			public const string DropText = "drop-text";
			/// <summary>
			/// *macOS*
			/// Emitted when a drag operation enters the tray icon.
			/// </summary>
			public const string DragEnter = "drag-enter";
			/// <summary>
			/// *macOS*
			/// Emitted when a drag operation exits the tray icon.
			/// </summary>
			public const string DragLeave = "drag-leave";
			/// <summary>
			/// *macOS*
			/// Emitted when a drag operation ends on the tray or ends at another location.
			/// </summary>
			public const string DragEnd = "drag-end";
			/// <summary>
			/// *macOS*
			/// Emitted when the mouse enters the tray icon.
			/// </summary>
			public const string MouseEnter = "mouse-enter";
			/// <summary>
			/// *macOS*
			/// Emitted when the mouse exits the tray icon.
			/// </summary>
			public const string MouseLeave = "mouse-leave";
			/// <summary>
			/// *macOS*
			/// Emitted when the mouse moves in the tray icon.
			/// </summary>
			public const string MouseMove = "mouse-move";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the Tray constructors,
		/// please use electron.Tray.Create() method instead.
		/// </para>
		/// </summary>
		public Tray() {
		}

		public EventEmitter on(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.on(eventName, listener);
		}

		public EventEmitter once(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.once(eventName, listener);
		}

		/// <summary>
		/// Destroys the tray icon immediately.
		/// </summary>
		public void destroy() {
			API.Apply("destroy");
		}

		/// <summary>
		/// Sets the image associated with this tray icon.
		/// </summary>
		/// <param name="image"></param>
		public void setImage(NativeImage image) {
			API.Apply("setImage", image);
		}

		/// <summary>
		/// Sets the image associated with this tray icon.
		/// </summary>
		/// <param name="image"></param>
		public void setImage(string image) {
			API.Apply("setImage", image);
		}

		/// <summary>
		/// *macOS*
		/// Sets the image associated with this tray icon when pressed on macOS.
		/// </summary>
		/// <param name="image"></param>
		public void setPressedImage(NativeImage image) {
			API.Apply("setPressedImage", image);
		}

		/// <summary>
		/// Sets the hover text for this tray icon.
		/// </summary>
		/// <param name="toolTip"></param>
		public void setToolTip(string toolTip) {
			API.Apply("setToolTip", toolTip);
		}

		/// <summary>
		/// *macOS*
		/// Sets the title displayed aside of the tray icon in the status bar (Support ANSI colors).
		/// </summary>
		/// <param name="title"></param>
		public void setTitle(string title) {
			API.Apply("setTitle", title);
		}

		/// <summary>
		/// *macOS*
		/// Sets when the tray's icon background becomes highlighted (in blue).
		/// </summary>
		/// <param name="mode"></param>
		public void setHighlightMode(string mode) {
			API.Apply("setHighlightMode", mode);
		}

		/// <summary>
		/// *Windows*
		/// Displays a tray balloon.
		/// </summary>
		/// <param name="mode"></param>
		public void displayBalloon(JsonObject options) {
			API.Apply("displayBalloon", options);
		}

		/// <summary>
		/// *macOS Windows*
		/// Pops up the context menu of the tray icon.
		/// When menu is passed, the menu will be shown instead of the tray icon's context menu.
		/// <para>
		/// The position is only available on Windows, and it is (0, 0) by default.
		/// </para>
		/// </summary>
		public void popUpContextMenu() {
			API.Apply("popUpContextMenu");
		}

		/// <summary>
		/// *macOS Windows*
		/// Pops up the context menu of the tray icon.
		/// When menu is passed, the menu will be shown instead of the tray icon's context menu.
		/// <para>
		/// The position is only available on Windows, and it is (0, 0) by default.
		/// </para>
		/// </summary>
		/// <param name="menu"></param>
		public void popUpContextMenu(Menu menu) {
			API.Apply("popUpContextMenu", menu);
		}

		/// <summary>
		/// *macOS Windows*
		/// Pops up the context menu of the tray icon.
		/// When menu is passed, the menu will be shown instead of the tray icon's context menu.
		/// <para>
		/// The position is only available on Windows, and it is (0, 0) by default.
		/// </para>
		/// </summary>
		/// <param name="menu"></param>
		/// <param name="position"></param>
		public void popUpContextMenu(Menu menu, Point position) {
			API.Apply("popUpContextMenu", menu, position);
		}

		/// <summary>
		/// Sets the context menu for this icon.
		/// </summary>
		/// <param name="menu"></param>
		public void setContextMenu(Menu menu) {
			API.Apply("setContextMenu", menu);
		}

		/// <summary>
		/// *macOS Windows*
		/// The bounds of this tray icon as Object.
		/// </summary>
		/// <returns></returns>
		public Rectangle getBounds() {
			object result = API.Apply<object>("getBounds");
			return Rectangle.FromObject(result);
		}

		/// <summary>
		/// Returns Boolean - Whether the tray icon is destroyed.
		/// </summary>
		/// <returns></returns>
		public bool isDestroyed() {
			return API.Apply<bool>("isDestroyed");
		}
	}
}
