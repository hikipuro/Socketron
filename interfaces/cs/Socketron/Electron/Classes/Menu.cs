using System.Collections.Generic;
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
		/// <param name="client"></param>
		/// <param name="id"></param>
		public Menu(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/*
		public void setApplicationMenu(Menu menu) {
			_Class.setApplicationMenu(menu);
		}

		public Menu getApplicationMenu() {
			return _Class.getApplicationMenu();
		}

		public void sendActionToFirstResponder(string action) {
			_Class.sendActionToFirstResponder(action);
		}

		public static Menu buildFromTemplate(MenuItem.Options[] template) {
			return _Class.buildFromTemplate(template);
		}

		public static Menu buildFromTemplate(string template) {
			return _Class.buildFromTemplate(template);
		}
		//*/

		/// <summary>
		/// A MenuItem[] array containing the menu's items.
		/// </summary>
		public List<MenuItem> items {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var result = [];",
						"for (var item of {0}.items) {{",
							"result.push({1});",
						"}}",
						"return result;"
					),
					Script.GetObject(_id),
					Script.AddObject("item")
				);
				object[] result = _ExecuteBlocking<object[]>(script);
				List<MenuItem> items = new List<MenuItem>();
				foreach (object item in result) {
					int itemId = (int)item;
					MenuItem menuItem = new MenuItem(_client, itemId);
					items.Add(menuItem);
				}
				return items;
			}
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
			string script = ScriptBuilder.Build(
				"{0}.popup({1});",
				Script.GetObject(_id),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Closes the context menu in the browserWindow.
		/// </summary>
		/// <param name="browserWindow">Default is the focused window.</param>
		public void closePopup(BrowserWindow browserWindow = null) {
			string script = string.Empty;
			if (browserWindow == null) {
				script = ScriptBuilder.Build(
					"{0}.closePopup();",
					Script.GetObject(_id)
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"{1}.closePopup(window);"
					),
					browserWindow._id,
					Script.GetObject(_id)
				);
			}
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Appends the menuItem to the menu.
		/// </summary>
		/// <param name="menuItem"></param>
		public void append(MenuItem menuItem) {
			string script = ScriptBuilder.Build(
				"{0}.append({1});",
				Script.GetObject(_id),
				Script.GetObject(menuItem._id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns MenuItem the item with the specified id.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public MenuItem getMenuItemById(string id) {
			string script = ScriptBuilder.Build(
				"return {0}.getMenuItemById({1});",
				Script.GetObject(_id),
				id.Escape()
			);
			int result = _ExecuteBlocking<int>(script);
			return new MenuItem(_client, result);
		}

		/// <summary>
		/// Inserts the menuItem to the pos position of the menu.
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="menuItem"></param>
		public void insert(int pos, MenuItem menuItem) {
			string script = ScriptBuilder.Build(
				"{0}.insert({1},{2});",
				Script.GetObject(_id),
				pos,
				Script.GetObject(menuItem._id)
			);
			_ExecuteJavaScript(script);
		}
	}
}
