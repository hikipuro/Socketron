using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Add items to native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class MenuItem : JSModule {
		/// <summary>
		/// MenuItem constructor options.
		/// </summary>
		public class Options {
			/// <summary>
			/// (optional) Will be called with click(menuItem, browserWindow, event)
			/// when the menu item is clicked.
			/// </summary>
			public Callback click;
			/// <summary>
			/// (optional) Define the action of the menu item,
			/// when specified the click property will be ignored. See roles.
			/// </summary>
			public string role;
			/// <summary>
			/// (optional) Can be normal, separator, submenu, checkbox or radio.
			/// </summary>
			public string type;
			/// <summary>
			/// (optional)
			/// </summary>
			public string label;
			/// <summary>
			/// (optional)
			/// </summary>
			public string sublabel;
			/// <summary>
			/// (optional)
			/// </summary>
			public string accelerator;
			/// <summary>
			/// (optional)
			/// </summary>
			public NativeImage icon;
			/// <summary>
			/// (optional) If false, the menu item will be greyed out and unclickable.
			/// </summary>
			public bool? enabled;
			/// <summary>
			/// (optional) If false, the menu item will be entirely hidden.
			/// </summary>
			public bool? visible;
			/// <summary>
			/// (optional) Should only be specified for checkbox or radio type menu items.
			/// </summary>
			public bool? @checked;
			/// <summary>
			/// (optional) Should be specified for submenu type menu items.
			/// If submenu is specified, the type: 'submenu' can be omitted.
			/// If the value is not a Menu then it will be automatically converted
			/// to one using Menu.buildFromTemplate.
			/// </summary>
			public Options[] submenu;
			/// <summary>
			/// (optional) Unique within a single menu.
			/// If defined then it can be used as a reference to this item by the position attribute.
			/// </summary>
			public string id;
			/// <summary>
			/// (optional) This field allows fine-grained definition of the specific location within a given menu.
			/// </summary>
			public string position;

			/// <summary>
			/// Parse JSON text.
			/// </summary>
			/// <param name="text"></param>
			/// <returns></returns>
			public static Options Parse(string text) {
				return JSON.Parse<Options>(text);
			}

			/// <summary>
			/// Parse JSON text.
			/// </summary>
			/// <param name="text"></param>
			/// <returns></returns>
			public static Options[] ParseArray(string text) {
				return JSON.Parse<Options[]>(text);
			}

			/// <summary>
			/// Create JSON text.
			/// </summary>
			/// <returns></returns>
			public string Stringify() {
				return JSON.Stringify(this);
			}
		}

		/// <summary>
		/// MenuItem.Options.type values.
		/// </summary>
		public class Types {
			public const string normal = "normal";
			public const string separator = "separator";
			public const string submenu = "submenu";
			public const string checkbox = "checkbox";
			public const string radio = "radio";
		}

		/// <summary>
		/// MenuItem.Options.role values.
		/// </summary>
		public class Roles {
			public const string undo = "undo";
			public const string redo = "redo";
			public const string cut = "cut";
			public const string copy = "copy";
			public const string paste = "paste";
			public const string pasteAndMatchStyle = "pasteAndMatchStyle";
			public const string selectAll = "selectAll";
			public const string delete = "delete";

			/// <summary>Minimize current window.</summary>
			public const string minimize = "minimize";
			/// <summary>Close current window.</summary>
			public const string close = "close";
			/// <summary> Quit the application.</summary>
			public const string quit = "quit";
			/// <summary>Reload the current window.</summary>
			public const string reload = "reload";
			/// <summary>Reload the current window ignoring the cache.</summary>
			public const string forceReload = "forceReload";
			/// <summary>Toggle developer tools in the current window.</summary>
			public const string toggleDevTools = "toggleDevTools";
			/// <summary>Toggle full screen mode on the current window.</summary>
			public const string toggleFullScreen = "toggleFullScreen";
			/// <summary>Reset the focused page's zoom level to the original size.</summary>
			public const string resetZoom = "resetZoom";
			/// <summary>Zoom in the focused page by 10%.</summary>
			public const string zoomIn = "zoomIn";
			/// <summary>Zoom out the focused page by 10%.</summary>
			public const string zoomOut = "zoomOut";
			/// <summary>Whole default "Edit" menu (Undo, Copy, etc.).</summary>
			public const string editMenu = "editMenu";
			/// <summary>Whole default "Window" menu (Minimize, Close, etc.).</summary>
			public const string windowMenu = "windowMenu";

			/// <summary>*macOS* Map to the orderFrontStandardAboutPanel action.</summary>
			public const string about = "about";
			/// <summary>*macOS* Map to the hide action.</summary>
			public const string hide = "hide";
			/// <summary>*macOS* Map to the hideOtherApplications action.</summary>
			public const string hideOthers = "hideOthers";
			/// <summary>*macOS* Map to the unhideAllApplications action.</summary>
			public const string unhide = "unhide";
			/// <summary>*macOS* Map to the startSpeaking action.</summary>
			public const string startSpeaking = "startSpeaking";
			/// <summary>*macOS* Map to the stopSpeaking action.</summary>
			public const string stopSpeaking = "stopSpeaking";
			/// <summary>*macOS* Map to the arrangeInFront action.</summary>
			public const string front = "front";
			/// <summary>*macOS* Map to the performZoom action.</summary>
			public const string zoom = "zoom";
			/// <summary>*macOS* Map to the toggleTabBar action.</summary>
			public const string toggleTabBar = "toggleTabBar";
			/// <summary>*macOS* Map to the selectNextTab action.</summary>
			public const string selectNextTab = "selectNextTab";
			/// <summary>*macOS* Map to the selectPreviousTab action.</summary>
			public const string selectPreviousTab = "selectPreviousTab";
			/// <summary>*macOS* Map to the mergeAllWindows action.</summary>
			public const string mergeAllWindows = "mergeAllWindows";
			/// <summary>*macOS* Map to the moveTabToNewWindow action.</summary>
			public const string moveTabToNewWindow = "moveTabToNewWindow";
			/// <summary>*macOS* The submenu is a "Window" menu.</summary>
			public const string window = "window";
			/// <summary>*macOS* The submenu is a "Help" menu.</summary>
			public const string help = "help";
			/// <summary>*macOS* The submenu is a "Services" menu.</summary>
			public const string services = "services";
			/// <summary>*macOS* The submenu is an "Open Recent" menu.</summary>
			public const string recentDocuments = "recentDocuments";
			/// <summary>*macOS* Map to the clearRecentDocuments action.</summary>
			public const string clearRecentDocuments = "clearRecentDocuments";
		}

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
		public JSCallback click {
			set {
				if (value == null) {
					API.SetPropertyNull("click");
					return;
				}
				string eventName = "_click";
				CallbackItem item = API.CreateCallbackItem(eventName, (object[] args) => {
					value?.Invoke(args);
				});
				API.SetProperty("click", item);
			}
		}
	}
}
