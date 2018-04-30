using System;
using System.Web.Script.Serialization;

namespace Socketron {
	/// <summary>
	/// Add items to native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	public class MenuItem {
		public class Options {
			public Action click;
			public string role;
			public string type;
			public string label;
			public string sublabel;
			public string accelerator;
			public NativeImage icon;
			public bool? enabled;
			public bool? visible;
			public bool? @checked;
			public Options[] submenu;
			public string id;
			public string position;

			public static Options Parse(string text) {
				var serializer = new JavaScriptSerializer();
				return serializer.Deserialize<Options>(text);
			}

			public static Options[] ParseArray(string text) {
				var serializer = new JavaScriptSerializer();
				return serializer.Deserialize<Options[]>(text);
			}

			public string Stringify() {
				var serializer = new JavaScriptSerializer();
				serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
				return serializer.Serialize(this);
			}
		}

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

			// macOS

			/// <summary>Map to the orderFrontStandardAboutPanel action.</summary>
			public const string about = "about";

			/// <summary>Map to the hide action.</summary>
			public const string hide = "hide";

			/// <summary>Map to the hideOtherApplications action.</summary>
			public const string hideOthers = "hideOthers";

			/// <summary>Map to the unhideAllApplications action.</summary>
			public const string unhide = "unhide";

			/// <summary>Map to the startSpeaking action.</summary>
			public const string startSpeaking = "startSpeaking";

			/// <summary>Map to the stopSpeaking action.</summary>
			public const string stopSpeaking = "stopSpeaking";

			/// <summary>Map to the arrangeInFront action.</summary>
			public const string front = "front";

			/// <summary>Map to the performZoom action.</summary>
			public const string zoom = "zoom";

			/// <summary>Map to the toggleTabBar action.</summary>
			public const string toggleTabBar = "toggleTabBar";

			/// <summary>Map to the selectNextTab action.</summary>
			public const string selectNextTab = "selectNextTab";

			/// <summary>Map to the selectPreviousTab action.</summary>
			public const string selectPreviousTab = "selectPreviousTab";

			/// <summary>Map to the mergeAllWindows action.</summary>
			public const string mergeAllWindows = "mergeAllWindows";

			/// <summary>Map to the moveTabToNewWindow action.</summary>
			public const string moveTabToNewWindow = "moveTabToNewWindow";

			/// <summary>The submenu is a "Window" menu.</summary>
			public const string window = "window";

			/// <summary>The submenu is a "Help" menu.</summary>
			public const string help = "help";

			/// <summary>The submenu is a "Services" menu.</summary>
			public const string services = "services";

			/// <summary>The submenu is an "Open Recent" menu.</summary>
			public const string recentDocuments = "recentDocuments";

			/// <summary>Map to the clearRecentDocuments action.</summary>
			public const string clearRecentDocuments = "clearRecentDocuments";
		}

		public MenuItem() {
		}

		public bool enabled;
		public bool visible;
		public bool @checked;
		public string label;
		public Action click;
	}
}
