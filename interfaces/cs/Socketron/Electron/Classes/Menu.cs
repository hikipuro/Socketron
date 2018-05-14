using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Menu : JSObject {
		/// <summary>
		/// Menu instance events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when menu.popup() is called.
			/// </summary>
			public const string MenuWillShow = "menu-will-show";
			/// <summary>
			/// Emitted when a popup is closed either manually or with menu.closePopup().
			/// </summary>
			public const string MenuWillClose = "menu-will-close";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the Menu constructors,
		/// please use electron.Menu.buildFromTemplate() method instead.
		/// </para>
		/// </summary>
		public Menu() {
		}

		/// <summary>
		/// A MenuItem[] array containing the menu's items.
		/// </summary>
		public MenuItem[] items {
			get { return API.GetObjectList<MenuItem>("items"); }
		}

		public EventEmitter on(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.on(eventName, listener);
		}

		public EventEmitter once(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.once(eventName, listener);
		}

		public EventEmitter addListener(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.addListener(eventName, listener);
		}

		public EventEmitter removeListener(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.removeListener(eventName, listener);
		}

		public EventEmitter removeAllListeners(string eventName) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.removeAllListeners(eventName);
		}

		/// <summary>
		/// Pops up this menu as a context menu in the browserWindow.
		/// </summary>
		public void popup() {
			API.Apply("popup");
		}

		/// <summary>
		/// Pops up this menu as a context menu in the BrowserWindow.
		/// </summary>
		/// <param name="browserWindow"></param>
		/// <param name="options"></param>
		public void popup(BrowserWindow browserWindow, PopupOptions options) {
			API.Apply("popup", browserWindow, options);
		}

		/// <summary>
		/// Closes the context menu in the browserWindow.
		/// </summary>
		/// <param name="browserWindow">Default is the focused window.</param>
		public void closePopup(BrowserWindow browserWindow = null) {
			if (browserWindow == null) {
				API.Apply("closePopup");
			} else {
				API.Apply("closePopup", browserWindow);
			}
		}

		/// <summary>
		/// Appends the menuItem to the menu.
		/// </summary>
		/// <param name="menuItem"></param>
		public void append(MenuItem menuItem) {
			API.Apply("append", menuItem);
		}

		/// <summary>
		/// Returns MenuItem the item with the specified id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public MenuItem getMenuItemById(string id) {
			return API.ApplyAndGetObject<MenuItem>("getMenuItemById", id);
		}

		/// <summary>
		/// Inserts the menuItem to the pos position of the menu.
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="menuItem"></param>
		public void insert(int pos, MenuItem menuItem) {
			API.Apply("insert", pos, menuItem);
		}
	}
}
