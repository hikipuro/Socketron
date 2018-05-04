namespace Socketron {
	/// <summary>
	/// Create native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	public class Menu : NodeBase {
		public const string Name = "Menu";

		public class Events {
			public const string MenuWillShow = "menu-will-show";
			public const string MenuWillClose = "menu-will-close";
		}

		public Menu(Socketron socketron) {
			_socketron = socketron;
		}

		/*
		public void Popup(string options) {
			string[] script = new[] {
				"electron.Menu.popup(" + options.Escape() + ");",
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}
		//*/

		public void ClosePopup() {
			// TODO: implement this
		}

		public void Append() {
			// TODO: implement this
		}

		public void GetMenuItemById() {
			// TODO: implement this
		}

		public void Insert() {
			// TODO: implement this
		}

		public void GetItems() {
			// TODO: implement this
		}
	}
}
