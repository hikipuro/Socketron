using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Socketron.Electron {
	/// <summary>
	/// Create native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class MenuModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public MenuModule() {
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
			API.Apply("setApplicationMenu", menu);
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
			return API.ApplyAndGetObject<Menu>("getApplicationMenu");
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
			API.Apply("sendActionToFirstResponder", action);
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
			string param = _CreateTemplateString(template);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var menu = {0}.buildFromTemplate({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				param,
				Script.AddObject("menu")
			);
			int result = API._ExecuteBlocking<int>(script);
			return API.CreateObject<Menu>(result);
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

		protected void _AddTemplateProperty(List<string> list, string name, string value) {
			if (value == null) {
				return;
			}
			list.Add(string.Format("{0}:{1}", name, value.Escape()));
		}

		protected void _AddTemplateProperty(List<string> list, string name, bool? value) {
			if (value == null) {
				return;
			}
			list.Add(string.Format("{0}:{1}", name, value.Escape()));
		}

		protected string _CreateTemplateString(MenuItem.Options[] template) {
			if (template == null) {
				return string.Empty;
			}
			List<string> list = new List<string>();
			foreach (MenuItem.Options item in template) {
				List<string> itemValues = new List<string>();
				_AddTemplateProperty(itemValues, "role", item.role);
				_AddTemplateProperty(itemValues, "type", item.type);
				_AddTemplateProperty(itemValues, "label", item.label);
				_AddTemplateProperty(itemValues, "sublabel", item.sublabel);
				_AddTemplateProperty(itemValues, "accelerator", item.accelerator);
				if (item.icon != null) {
					itemValues.Add(string.Format(
						"icon:{0}", Script.GetObject(item.icon.API.id)
					));
				}
				_AddTemplateProperty(itemValues, "enabled", item.enabled);
				_AddTemplateProperty(itemValues, "visible", item.visible);
				_AddTemplateProperty(itemValues, "checked", item.@checked);
				_AddTemplateProperty(itemValues, "id", item.id);
				_AddTemplateProperty(itemValues, "position", item.position);
				if (item.click != null) {
					string eventName = "_MenuItem_click";
					CallbackItem callbackItem = API.CreateCallbackItem(eventName, (args) => {
						MenuItem menuItem = API.CreateObject<MenuItem>(args[0]);
						BrowserWindow browserWindow = API.CreateObject<BrowserWindow>(args[1]);
						Event @event = API.CreateObject<Event>(args[2]);
						item.click?.Invoke(menuItem, browserWindow, @event);
					});
					itemValues.Add(string.Format(
						"click:{0}", Script.GetObject(callbackItem.ObjectId)
					));
				}
				if (item.submenu != null) {
					itemValues.Add(string.Format(
						"submenu:{0}", _CreateTemplateString(item.submenu)
					));
				}
				list.Add(string.Format(
					"{{{0}}}", string.Join(",", itemValues.ToArray())
				));
			}
			return "[" + string.Join(",", list.ToArray()) + "]";
		}
	}
}
