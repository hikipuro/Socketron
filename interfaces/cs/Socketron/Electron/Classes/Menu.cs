using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Menu : JSModule {
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

		/// <summary>
		/// Pops up this menu as a context menu in the BrowserWindow.
		/// </summary>
		/// <param name="options">
		/// <list type="bullet">
		/// <item><description>"window" BrowserWindow (optional) - Default is the focused window.</description></item>
		/// <item><description>"x" Number (optional) - Default is the current mouse cursor position. Must be declared if y is declared.</description></item>
		/// <item><description>"y" Number (optional) - Default is the current mouse cursor position. Must be declared if x is declared.</description></item>
		/// <item><description>"positioningItem" Number (optional) *macOS* - The index of the menu item to be positioned under the mouse cursor at the specified coordinates. Default is -1.)</description></item>
		/// <item><description>"callback" Function (optional) - Called when menu is closed.</description></item>
		/// </list>
		/// </param>
		public void popup(JsonObject options) {
			API.Apply("popup", options);
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
