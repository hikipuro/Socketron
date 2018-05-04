namespace Socketron {
	/// <summary>
	/// Add items to native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	public class MenuItemClass : NodeBase {
		public MenuItemClass(Socketron socketron) {
			_socketron = socketron;
		}

		public MenuItem Create(MenuItem.Options options = null) {
			if (options == null) {
				options = new MenuItem.Options();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var item = new electron.MenuItem({0});",
					"return {1};"
				),
				options.Stringify(),
				Script.AddObject("item")
			);
			int result = _ExecuteBlocking<int>(script);
			return new MenuItem(_socketron) {
				id = result
			};
		}

	}
}
