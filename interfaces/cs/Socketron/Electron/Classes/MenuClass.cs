namespace Socketron {
	/// <summary>
	/// Create native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	public class MenuClass : ElectronBase {
		public MenuClass(Socketron socketron) {
			_socketron = socketron;
		}

		public void SetApplicationMenu(Menu menu) {
			if (menu == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var menu = this._objRefs[{0}];",
					"electron.Menu.setApplicationMenu(menu);"
				),
				menu.ID
			);
			_ExecuteJavaScript(script);
		}

		public Menu GetApplicationMenu() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var menu = electron.Menu.getApplicationMenu();",
					"return this._addObjectReference(menu);"
				)
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			if (result <= 0) {
				return null;
			}
			Menu menu = new Menu(_socketron) {
				ID = result
			};
			return menu;
		}

		/// <summary>
		/// macOS
		/// </summary>
		/// <param name="action"></param>
		public void SendActionToFirstResponder(string action) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.Menu.sendActionToFirstResponder({0});"
				),
				action.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public Menu BuildFromTemplate(MenuItem.Options[] template) {
			string templateText = JSON.Stringify(template);

			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var menu = electron.Menu.buildFromTemplate({0});",
					"return this._addObjectReference(menu);"
				),
				templateText
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			if (result <= 0) {
				return null;
			}
			Menu menu = new Menu(_socketron) {
				ID = result
			};
			return menu;
		}

		public Menu BuildFromTemplate(string template) {
			MenuItem.Options[] options = JSON.Parse<MenuItem.Options[]>(template);
			return BuildFromTemplate(options);
		}
	}
}
