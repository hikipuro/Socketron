using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Create native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class MenuClass : NodeBase {
		public MenuClass(Socketron socketron) {
			_socketron = socketron;
		}

		public void setApplicationMenu(Menu menu) {
			if (menu == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var menu = {0};",
					"electron.Menu.setApplicationMenu(menu);"
				),
				Script.GetObject(menu.id)
			);
			_ExecuteJavaScript(script);
		}

		public Menu getApplicationMenu() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var menu = electron.Menu.getApplicationMenu();",
					"return this._addObjectReference(menu);"
				)
			);
			int result = _ExecuteBlocking<int>(script);
			if (result <= 0) {
				return null;
			}
			Menu menu = new Menu(_socketron) {
				id = result
			};
			return menu;
		}

		/// <summary>
		/// *macOS*
		/// 
		/// </summary>
		/// <param name="action"></param>
		public void sendActionToFirstResponder(string action) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.Menu.sendActionToFirstResponder({0});"
				),
				action.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public Menu buildFromTemplate(MenuItem.Options[] template) {
			string templateText = JSON.Stringify(template);

			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var menu = electron.Menu.buildFromTemplate({0});",
					"return this._addObjectReference(menu);"
				),
				templateText
			);
			int result = _ExecuteBlocking<int>(script);
			if (result <= 0) {
				return null;
			}
			Menu menu = new Menu(_socketron) {
				id = result
			};
			return menu;
		}

		public Menu buildFromTemplate(string template) {
			MenuItem.Options[] options = JSON.Parse<MenuItem.Options[]>(template);
			return buildFromTemplate(options);
		}
	}
}
