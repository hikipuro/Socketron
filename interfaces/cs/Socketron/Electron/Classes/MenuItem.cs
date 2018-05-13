using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Add items to native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class MenuItem : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public MenuItem() {
		}

		/// <summary>
		/// A Boolean indicating whether the item is enabled,
		/// this property can be dynamically changed.
		/// </summary>
		public bool enabled {
			get { return API.GetProperty<bool>("enabled"); }
			set { API.SetProperty("enabled", value); }
		}

		/// <summary>
		/// A Boolean indicating whether the item is visible,
		/// this property can be dynamically changed.
		/// </summary>
		public bool visible {
			get { return API.GetProperty<bool>("visible"); }
			set { API.SetProperty("visible", value); }
		}

		/// <summary>
		/// A Boolean indicating whether the item is checked,
		/// this property can be dynamically changed.
		/// </summary>
		public bool @checked {
			get { return API.GetProperty<bool>("checked"); }
			set { API.SetProperty("checked", value); }
		}

		/// <summary>
		/// A String representing the menu items visible label.
		/// </summary>
		public string label {
			get { return API.GetProperty<string>("label"); }
			set { API.SetProperty("label", value); }
		}

		/// <summary>
		/// A Function that is fired when the MenuItem receives a click event.
		/// </summary>
		public Action<MenuItem, BrowserWindow, Event> click {
			set {
				string eventName = "_MenuItem_click";
				if (value == null) {
					if (_click != null) {
						API.RemoveCallbackItem(eventName, _click);
					}
					API.SetPropertyNull("click");
					return;
				}
				_click = (args) => {
					MenuItem menuItem = API.CreateObject<MenuItem>(args[0]);
					BrowserWindow browserWindow = API.CreateObject<BrowserWindow>(args[1]);
					Event @event = API.CreateObject<Event>(args[2]);
					value?.Invoke(menuItem, browserWindow, @event);
				};
				CallbackItem item = API.CreateCallbackItem(eventName, _click);
				API.SetProperty("click", item);
			}
		}

		public Menu submenu {
			get { return API.GetObject<Menu>("submenu"); }
		}

		protected JSCallback _click;
	}
}
