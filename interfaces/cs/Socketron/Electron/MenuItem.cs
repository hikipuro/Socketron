using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Add items to native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class MenuItem : NodeModule {
		public const string Name = "MenuItem";
		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Callback GetCallbackFromId(ushort id) {
			if (!_callbackList.ContainsKey(id)) {
				return null;
			}
			return _callbackList[id];
		}

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

			public static Options Parse(string text) {
				return JSON.Parse<Options>(text);
			}

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
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public MenuItem(SocketronClient client) {
			_client = client;
		}

		public MenuItem(Options options) {
			SocketronClient client = SocketronClient.GetCurrent();
			if (options == null) {
				options = new Options();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var item = new electron.MenuItem({0});",
					"return {1};"
				),
				options.Stringify(),
				Script.AddObject("item")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			_client = client;
			_id = result;
		}

		public MenuItem(string options) : this(Options.Parse(options)) {
		}

		/// <summary>
		/// A Boolean indicating whether the item is enabled,
		/// this property can be dynamically changed.
		/// </summary>
		public bool enabled {
			get {
				string script = ScriptBuilder.Build(
					"return {0}.enabled;",
					Script.GetObject(_id)
				);
				return _ExecuteBlocking<bool>(script);
			}
			set {
				string script = ScriptBuilder.Build(
					"{0}.enabled = {1};",
					Script.GetObject(_id),
					value.Escape()
				);
				_ExecuteJavaScript(script);
			}
		}

		/// <summary>
		/// A Boolean indicating whether the item is visible,
		/// this property can be dynamically changed.
		/// </summary>
		public bool visible {
			get {
				string script = ScriptBuilder.Build(
					"return {0}.visible;",
					Script.GetObject(_id)
				);
				return _ExecuteBlocking<bool>(script);
			}
			set {
				string script = ScriptBuilder.Build(
					"{0}.visible = {1};",
					Script.GetObject(_id),
					value.Escape()
				);
				_ExecuteJavaScript(script);
			}
		}

		/// <summary>
		/// A Boolean indicating whether the item is checked,
		/// this property can be dynamically changed.
		/// </summary>
		public bool @checked {
			get {
				string script = ScriptBuilder.Build(
					"return {0}.checked;",
					Script.GetObject(_id)
				);
				return _ExecuteBlocking<bool>(script);
			}
			set {
				string script = ScriptBuilder.Build(
					"{0}.checked = {1};",
					Script.GetObject(_id),
					value.Escape()
				);
				_ExecuteJavaScript(script);
			}
		}

		/// <summary>
		/// A String representing the menu items visible label.
		/// </summary>
		public string label {
			get {
				string script = ScriptBuilder.Build(
					"return {0}.label;",
					Script.GetObject(_id)
				);
				return _ExecuteBlocking<string>(script);
			}
			set {
				string script = ScriptBuilder.Build(
					"{0}.label = {1};",
					Script.GetObject(_id),
					value.Escape()
				);
				_ExecuteJavaScript(script);
			}
		}

		/// <summary>
		/// A Function that is fired when the MenuItem receives a click event.
		/// </summary>
		public Callback click {
			set {
				_callbackList.Add(_callbackListId, value);
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var item = {0};",
						"var listener = () => {{",
							"emit('__event',{1},{2});",
						"}};",
						"this._addClientEventListener({1},{2},listener);",
						"item.click = listener;"
					),
					Script.GetObject(_id),
					Name.Escape(),
					_callbackListId
				);
				_callbackListId++;
				_ExecuteJavaScript(script);
			}
		}
	}
}
