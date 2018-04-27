using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Socketron {
	public class WebPreferences {
		public bool? devTools;
		public bool? nodeIntegration;
		public bool? nodeIntegrationInWorker;
		public string preload;
		public bool? sandbox;
		//public Session session;
		public string partition;
		public string affinity;
		public double? zoomFactor;
		public bool? javascript;
		public bool? webSecurity;
		public bool? allowRunningInsecureContent;
		public bool? images;
		public bool? textAreasAreResizable;
		public bool? webgl;
		public bool? webaudio;
		public bool? plugins;
		public bool? experimentalFeatures;
		public bool? experimentalCanvasFeatures;
		public bool? scrollBounce;
		public string blinkFeatures;
		public string disableBlinkFeatures;
		public Dictionary<string, string> defaultFontFamily;
		public int? defaultFontSize;
		public int? defaultMonospaceFontSize;
		public int? minimumFontSize;
		public string defaultEncoding;
		public bool? backgroundThrottling;
		public bool? offscreen;
		public bool? contextIsolation;
		public bool? nativeWindowOpen;
		public bool? webviewTag;
		public string[] additionalArguments;
		public bool? safeDialogs;
		public string safeDialogsMessage;
		public bool? navigateOnDragDrop;
	}

	public class BrowserWindowOptions {
		public int? width;
		public int? height;
		public int? x;
		public int? y;
		public bool? useContentSize;
		public bool? center;
		public int? minWidth;
		public int? minHeight;
		public int? maxWidth;
		public int? maxHeight;
		public bool? resizable;
		public bool? movable;
		public bool? minimizable;
		public bool? maximizable;
		public bool? closable;
		public bool? focusable;
		public bool? alwaysOnTop;
		public bool? fullscreen;
		public bool? fullscreenable;
		public bool? simpleFullscreen;
		public bool? skipTaskbar;
		public bool? kiosk;
		public string title;
		//public string icon;
		public bool? show;
		public bool? frame;
		//public BrowserWindow parent;
		public bool? modal;
		public bool? acceptFirstMouse;
		public bool? disableAutoHideCursor;
		public bool? autoHideMenuBar;
		public bool? enableLargerThanScreen;
		public string backgroundColor;
		public bool? hasShadow;
		public double? opacity;
		public bool? darkTheme;
		public bool? transparent;
		public string type;
		public string titleBarStyle;
		public bool? fullscreenWindowTitle;
		public bool? thickFrame;
		public string vibrancy;
		public bool? zoomToPageWidth;
		public string tabbingIdentifier;
		public WebPreferences webPreferences = new WebPreferences();

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
			return serializer.Serialize(this);
		}
	}
}
