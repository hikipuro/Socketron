using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class MenuModule : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public MenuModule(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// Sets menu as the application menu on macOS.
		/// On Windows and Linux, the menu will be set as each window's top menu.
		/// <para>
		/// Passing null will remove the menu bar on Windows and Linux but has no effect on macOS.
		/// </para>
		/// <para>
		/// Note: This API has to be called after the ready event of app module.
		/// </para>
		/// </summary>
		/// <param name="menu"></param>
		public void setApplicationMenu(Menu menu) {
			if (menu == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				"{0}.setApplicationMenu({1});",
				Script.GetObject(_id),
				Script.GetObject(menu._id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Menu | null - The application menu, if set, or null, if not set.
		/// <para>
		/// Note: The returned Menu instance doesn't support dynamic addition
		/// or removal of menu items. Instance properties can still be dynamically modified.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public Menu getApplicationMenu() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var menu = {0}.getApplicationMenu();",
					"return {1};"
				),
				Script.GetObject(_id),
				Script.AddObject("menu")
			);
			int result = _ExecuteBlocking<int>(script);
			if (result <= 0) {
				return null;
			}
			return new Menu(_client, result);
		}

		/// <summary>
		/// *macOS*
		/// Sends the action to the first responder of application.
		/// <para>
		/// This is used for emulating default macOS menu behaviors.
		/// Usually you would just use the role property of a MenuItem.
		/// </para>
		/// <para>
		/// See the macOS Cocoa Event Handling Guide for more information on macOS' native actions.
		/// </para>
		/// </summary>
		/// <param name="action"></param>
		public void sendActionToFirstResponder(string action) {
			string script = ScriptBuilder.Build(
				"{0}.sendActionToFirstResponder({1});",
				Script.GetObject(_id),
				action.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Generally, the template is just an array of options for constructing a MenuItem.
		/// <para>
		/// You can also attach other fields to the element of the template
		/// and they will become properties of the constructed menu items.
		/// </para>
		/// </summary>
		/// <param name="template"></param>
		/// <returns></returns>
		public Menu buildFromTemplate(MenuItem.Options[] template) {
			string templateText = JSON.Stringify(template);

			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var menu = {0}.buildFromTemplate({1});",
					"return {2};"
				),
				Script.GetObject(_id),
				templateText,
				Script.AddObject("menu")
			);
			int result = _ExecuteBlocking<int>(script);
			if (result <= 0) {
				return null;
			}
			return new Menu(_client, result);
		}

		/// <summary>
		/// Generally, the template is just an array of options for constructing a MenuItem.
		/// <para>
		/// You can also attach other fields to the element of the template
		/// and they will become properties of the constructed menu items.
		/// </para>
		/// </summary>
		/// <param name="template"></param>
		/// <returns></returns>
		public Menu buildFromTemplate(string template) {
			MenuItem.Options[] options = JSON.Parse<MenuItem.Options[]>(template);
			return buildFromTemplate(options);
		}
	}
}
