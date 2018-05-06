namespace Socketron.Electron {
	/// <summary>
	/// Settings of web page's features.
	/// </summary>
	public class WebPreferences {
		/// <summary>
		/// (optional) Whether to enable DevTools.
		/// If it is set to false, can not use BrowserWindow.webContents.openDevTools()
		/// to open DevTools. Default is true.
		/// </summary>
		public bool? devTools;
		/// <summary>
		/// (optional) Whether node integration is enabled. Default is true.
		/// </summary>
		public bool? nodeIntegration;
		/// <summary>
		/// (optional) Whether node integration is enabled in web workers.
		/// Default is false. More about this can be found in Multithreading.
		/// </summary>
		public bool? nodeIntegrationInWorker;
		/// <summary>
		/// (optional) Specifies a script that will be loaded before other scripts run in the page.
		/// This script will always have access to node APIs no matter whether node integration is
		/// turned on or off. The value should be the absolute file path to the script.
		/// When node integration is turned off, the preload script can reintroduce
		/// Node global symbols back to the global scope. See example here.
		/// </summary>
		public string preload;
		/// <summary>
		/// (optional) If set, this will sandbox the renderer associated with the window,
		/// making it compatible with the Chromium OS-level sandbox and disabling the Node.js engine.
		/// This is not the same as the nodeIntegration option and the APIs available to the preload
		/// script are more limited. Read more about the option here. Note: This option is currently
		/// experimental and may change or be removed in future Electron releases.
		/// </summary>
		public bool? sandbox;
		/// <summary>
		/// (optional) Sets the session used by the page.
		/// Instead of passing the Session object directly,
		/// you can also choose to use the partition option instead,
		/// which accepts a partition string.
		/// When both session and partition are provided, session will be preferred.
		/// Default is the default session.
		/// </summary>
		public Session session;
		/// <summary>
		/// (optional) Sets the session used by the page according to the session's partition string.
		/// If partition starts with persist:, the page will use a persistent session available
		/// to all pages in the app with the same partition.
		/// If there is no persist: prefix, the page will use an in-memory session.
		/// By assigning the same partition, multiple pages can share the same session.
		/// Default is the default session.
		/// </summary>
		public string partition;
		/// <summary>
		///  (optional) When specified, web pages with the same affinity will run
		///  in the same renderer process. Note that due to reusing the renderer process,
		///  certain webPreferences options will also be shared between the web pages
		///  even when you specified different values for them,
		///  including but not limited to preload, sandbox and nodeIntegration.
		///  So it is suggested to use exact same webPreferences for web pages with the same affinity.
		/// </summary>
		public string affinity;
		/// <summary>
		/// (optional) The default zoom factor of the page, 3.0 represents 300%. Default is 1.0.
		/// </summary>
		public double? zoomFactor;
		/// <summary>
		/// (optional) Enables JavaScript support. Default is true.
		/// </summary>
		public bool? javascript;
		/// <summary>
		/// (optional) When false, it will disable the same-origin policy (usually using testing websites by people),
		/// and set allowRunningInsecureContent to true if this options has not been set by user.
		/// Default is true.
		/// </summary>
		public bool? webSecurity;
		/// <summary>
		/// (optional) Allow an https page to run JavaScript, CSS or plugins from http URLs. Default is false.
		/// </summary>
		public bool? allowRunningInsecureContent;
		/// <summary>
		/// (optional) Enables image support. Default is true.
		/// </summary>
		public bool? images;
		/// <summary>
		/// (optional) Make TextArea elements resizable. Default is true.
		/// </summary>
		public bool? textAreasAreResizable;
		/// <summary>
		/// (optional) Enables WebGL support. Default is true.
		/// </summary>
		public bool? webgl;
		/// <summary>
		/// (optional) Enables WebAudio support. Default is true.
		/// </summary>
		public bool? webaudio;
		/// <summary>
		/// (optional) Whether plugins should be enabled. Default is false.
		/// </summary>
		public bool? plugins;
		/// <summary>
		/// (optional) Enables Chromium's experimental features. Default is false.
		/// </summary>
		public bool? experimentalFeatures;
		/// <summary>
		/// (optional) Enables Chromium's experimental canvas features. Default is false.
		/// </summary>
		public bool? experimentalCanvasFeatures;
		/// <summary>
		/// (optional) Enables scroll bounce (rubber banding) effect on macOS. Default is false.
		/// </summary>
		public bool? scrollBounce;
		/// <summary>
		///  (optional) A list of feature strings separated by ",",
		///  like CSSVariables,KeyboardEventKey to enable.
		///  The full list of supported feature strings can be found
		///  in the RuntimeEnabledFeatures.json5 file.
		/// </summary>
		public string blinkFeatures;
		/// <summary>
		/// (optional) A list of feature strings separated by ",",
		/// like CSSVariables,KeyboardEventKey to disable.
		/// The full list of supported feature strings can be found
		/// in the RuntimeEnabledFeatures.json5 file.
		/// </summary>
		public string disableBlinkFeatures;
		/// <summary>
		/// (optional) Sets the default font for the font-family.
		/// </summary>
		public JsonObject defaultFontFamily;
		/// <summary>
		/// (optional) Defaults to 16.
		/// </summary>
		public int? defaultFontSize;
		/// <summary>
		/// (optional) Defaults to 13.
		/// </summary>
		public int? defaultMonospaceFontSize;
		/// <summary>
		/// (optional) Defaults to 0.
		/// </summary>
		public int? minimumFontSize;
		/// <summary>
		/// (optional) Defaults to ISO-8859-1.
		/// </summary>
		public string defaultEncoding;
		/// <summary>
		/// (optional) Whether to throttle animations and timers when the page becomes background.
		/// This also affects the Page Visibility API. Defaults to true.
		/// </summary>
		public bool? backgroundThrottling;
		/// <summary>
		/// (optional) Whether to enable offscreen rendering for the browser window.
		/// Defaults to false. See the offscreen rendering tutorial for more details.
		/// </summary>
		public bool? offscreen;
		/// <summary>
		/// (optional) Whether to run Electron APIs and the specified preload script
		/// in a separate JavaScript context. Defaults to false.
		/// The context that the preload script runs in will still have full access
		/// to the document and window globals but it will use its own set of
		/// JavaScript builtins (Array, Object, JSON, etc.) and will be isolated
		/// from any changes made to the global environment by the loaded page.
		/// The Electron API will only be available in the preload script and not the loaded page.
		/// This option should be used when loading potentially untrusted remote content
		/// to ensure the loaded content cannot tamper with the preload script
		/// and any Electron APIs being used.
		/// This option uses the same technique used by Chrome Content Scripts.
		/// You can access this context in the dev tools by selecting
		/// the 'Electron Isolated Context' entry in the combo box at the top of the Console tab.
		/// Note: This option is currently experimental and may change or be removed in
		/// future Electron releases.
		/// </summary>
		public bool? contextIsolation;
		/// <summary>
		/// (optional) Whether to use native window.open().
		/// Defaults to false. Note: This option is currently experimental.
		/// </summary>
		public bool? nativeWindowOpen;
		/// <summary>
		/// (optional) Whether to enable the &lt;webview&gt; tag.
		/// Defaults to the value of the nodeIntegration option.
		/// Note: The preload script configured for the &lt;webview&gt; will have
		/// node integration enabled when it is executed so you should ensure
		/// remote/untrusted content is not able to create a &lt;webview&gt; tag
		/// with a possibly malicious preload script.
		/// You can use the will-attach-webview event on webContents to strip away
		/// the preload script and to validate or alter the &lt;webview&gt;'s initial settings.
		/// </summary>
		public bool? webviewTag;
		/// <summary>
		/// (optional) A list of strings that will be appended to process.argv
		/// in the renderer process of this app.
		/// Useful for passing small bits of data down to renderer process preload scripts.
		/// </summary>
		public string[] additionalArguments;
		/// <summary>
		/// (optional) Whether to enable browser style consecutive dialog protection.
		/// Default is false.
		/// </summary>
		public bool? safeDialogs;
		/// <summary>
		/// (optional) The message to display when consecutive dialog protection is triggered.
		/// If not defined the default message would be used, note that currently
		/// the default message is in English and not localized.
		/// </summary>
		public string safeDialogsMessage;
		/// <summary>
		/// (optional) Whether dragging and dropping a file or link onto the page causes a navigation.
		/// Default is false.
		/// </summary>
		public bool? navigateOnDragDrop;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static WebPreferences Parse(string text) {
			return JSON.Parse<WebPreferences>(text);
		}

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <returns></returns>
		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
