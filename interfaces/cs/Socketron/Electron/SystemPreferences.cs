using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Get system preferences.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class SystemPreferences : NodeModule {
		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public SystemPreferences(SocketronClient client) {
			_client = client;
		}

		/// <summary>
		/// SystemPreferences module events.
		/// </summary>
		public class Events {
			/// <summary>*Windows*</summary>
			public const string AccentColorChanged = "accent-color-changed";
			/// <summary>*Windows*</summary>
			public const string ColorChanged = "color-changed";
			/// <summary>*Windows*</summary>
			public const string InvertedColorSchemeChanged = "inverted-color-scheme-changed";
		}

		/// <summary>
		/// SystemPreferences.GetColor color values.
		/// </summary>
		public class WindowsColors {
			/// <summary>Dark shadow for three-dimensional display elements.</summary>
			public const string _3DDarkShadow = "3d-dark-shadow";

			/// <summary>Face color for three-dimensional display elements and for dialog box backgrounds.</summary>
			public const string _3DFace = "3d-face";

			/// <summary>Highlight color for three-dimensional display elements.</summary>
			public const string _3DHighlight = "3d-highlight";

			/// <summary>Light color for three-dimensional display elements.</summary>
			public const string _3DLight = "3d-light";

			/// <summary>Shadow color for three-dimensional display elements.</summary>
			public const string _3Dshadow = "3d-shadow";

			/// <summary>Active window border.</summary>
			public const string ActiveBorder = "active-border";

			/// <summary>Active window title bar. Specifies the left side color in the color gradient of an active window's title bar if the gradient effect is enabled.</summary>
			public const string ActiveCaption = "active-caption";

			/// <summary>Right side color in the color gradient of an active window's title bar.</summary>
			public const string ActiveCaptionGradient = "active-caption-gradient";

			/// <summary>Background color of multiple document interface (MDI) applications.</summary>
			public const string AppWorkspace = "app-workspace";

			/// <summary>Text on push buttons.</summary>
			public const string ButtonText = "button-text";

			/// <summary>Text in caption, size box, and scroll bar arrow box.</summary>
			public const string CaptionText = "caption-text";

			/// <summary>Desktop background color.</summary>
			public const string Desktop = "desktop";

			/// <summary>Grayed (disabled) text.</summary>
			public const string DisabledText = "disabled-text";

			/// <summary>Item(s) selected in a control.</summary>
			public const string Highlight = "highlight";

			/// <summary>Text of item(s) selected in a control.</summary>
			public const string HighlightText = "highlight-text";

			/// <summary>Color for a hyperlink or hot-tracked item.</summary>
			public const string Hotlight = "hotlight";

			/// <summary>Inactive window border.</summary>
			public const string InactiveBorder = "inactive-border";

			/// <summary>Inactive window caption. Specifies the left side color in the color gradient of an inactive window's title bar if the gradient effect is enabled.</summary>
			public const string InactiveCaption = "inactive-caption";

			/// <summary>Right side color in the color gradient of an inactive window's title bar.</summary>
			public const string InactiveCaptionGradient = "inactive-caption-gradient";

			/// <summary>Color of text in an inactive caption.</summary>
			public const string InactiveCaptionText = "inactive-caption-text";

			/// <summary>Background color for tooltip controls.</summary>
			public const string InfoBackground = "info-background";

			/// <summary>Text color for tooltip controls.</summary>
			public const string InfoText = "info-text";

			/// <summary>Menu background.</summary>
			public const string Menu = "menu";

			/// <summary>The color used to highlight menu items when the menu appears as a flat menu.</summary>
			public const string MenuHighlight = "menu-highlight";

			/// <summary>The background color for the menu bar when menus appear as flat menus.</summary>
			public const string Menubar = "menubar";

			/// <summary>Text in menus.</summary>
			public const string MenuText = "menu-text";

			/// <summary>Scroll bar gray area.</summary>
			public const string Scrollbar = "scrollbar";

			/// <summary>Window background.</summary>
			public const string Window = "window";

			/// <summary>Window frame.</summary>
			public const string WindowFrame = "window-frame";

			/// <summary>Text in windows.</summary>
			public const string WindowText = "window-text";
		}

		/// <summary>
		/// *macOS*
		/// Returns Boolean - Whether the system is in Dark Mode.
		/// </summary>
		/// <returns></returns>
		public bool isDarkMode() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.isDarkMode();"
				)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS*
		/// Returns Boolean - Whether the Swipe between pages setting is on.
		/// </summary>
		/// <returns></returns>
		public bool isSwipeTrackingFromScrollEventsEnabled() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.isSwipeTrackingFromScrollEventsEnabled();"
				)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS*
		/// Posts event as native notifications of macOS.
		/// The userInfo is an Object that contains the user information dictionary
		/// sent along with the notification.
		/// </summary>
		/// <param name="event"></param>
		/// <param name="userInfo"></param>
		public void postNotification(string @event, JsonObject userInfo) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.systemPreferences.postNotification({0},{1});"
				),
				@event.Escape(),
				userInfo.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Posts event as native notifications of macOS.
		/// The userInfo is an Object that contains the user information dictionary
		/// sent along with the notification.
		/// </summary>
		/// <param name="event"></param>
		/// <param name="userInfo"></param>
		public void postLocalNotification(string @event, JsonObject userInfo) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.systemPreferences.postLocalNotification({0},{1});"
				),
				@event.Escape(),
				userInfo.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Posts event as native notifications of macOS.
		/// The userInfo is an Object that contains the user information dictionary
		/// sent along with the notification.
		/// </summary>
		/// <param name="event"></param>
		/// <param name="userInfo"></param>
		public void postWorkspaceNotification(string @event, JsonObject userInfo) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.systemPreferences.postWorkspaceNotification({0},{1});"
				),
				@event.Escape(),
				userInfo.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/*
		public void subscribeNotification() {
			// TODO: implement this
			throw new NotImplementedException();
		}
		//*/

		/*
		public void subscribeLocalNotification() {
			// TODO: implement this
			throw new NotImplementedException();
		}
		//*/

		/*
		public void subscribeWorkspaceNotification() {
			// TODO: implement this
			throw new NotImplementedException();
		}
		//*/

		/// <summary>
		/// *macOS*
		/// Removes the subscriber with id.
		/// </summary>
		/// <param name="id"></param>
		public void unsubscribeNotification(int id) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.systemPreferences.unsubscribeNotification({0});"
				),
				id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Same as unsubscribeNotification,
		/// but removes the subscriber from NSNotificationCenter.
		/// </summary>
		/// <param name="id"></param>
		public void unsubscribeLocalNotification(int id) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.systemPreferences.unsubscribeLocalNotification({0});"
				),
				id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Same as unsubscribeNotification,
		/// but removes the subscriber from NSWorkspace.sharedWorkspace.notificationCenter.
		/// </summary>
		/// <param name="id"></param>
		public void unsubscribeWorkspaceNotification(int id) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.systemPreferences.unsubscribeWorkspaceNotification({0});"
				),
				id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Add the specified defaults to your application's NSUserDefaults.
		/// </summary>
		/// <param name="defaults"></param>
		public void registerDefaults(JsonObject defaults) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.registerDefaults({0});"
				),
				defaults.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Returns any - The value of key in NSUserDefaults.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public JsonObject getUserDefault(string key, string type) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.getUserDefault({0},{1});"
				),
				key.Escape(),
				type.Escape()
			);
			object result = _ExecuteBlocking<object>(script);
			return new JsonObject(result);
		}

		/// <summary>
		/// *macOS*
		/// Set the value of key in NSUserDefaults.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="type"></param>
		/// <param name="value"></param>
		public void setUserDefault(string key, string type, string value) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.systemPreferences.setUserDefault({0},{1},{2});"
				),
				key.Escape(),
				type.Escape(),
				value.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Removes the key in NSUserDefaults.
		/// This can be used to restore the default
		/// or global value of a key previously set with setUserDefault.
		/// </summary>
		/// <param name="key"></param>
		public void removeUserDefault(string key) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.systemPreferences.removeUserDefault({0});"
				),
				key.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows*
		/// Returns Boolean - true if DWM composition (Aero Glass) is enabled, and false otherwise.
		/// </summary>
		/// <returns></returns>
		public bool isAeroGlassEnabled() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.isAeroGlassEnabled();"
				)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows*
		/// Returns String - The users current system wide accent color preference in RGBA hexadecimal form.
		/// </summary>
		/// <returns></returns>
		public string getAccentColor() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.getAccentColor();"
				)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// *Windows*
		/// Returns String - The system color setting in RGB hexadecimal form (#ABCDEF).
		/// See the Windows docs for more details.
		/// </summary>
		/// <param name="color">SystemPreferences.WindowsColors</param>
		/// <returns></returns>
		public string getColor(string color) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.getColor({0});"
				),
				color.Escape()
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// *Windows*
		/// Returns Boolean - true if an inverted color scheme,
		/// such as a high contrast theme, is active, false otherwise.
		/// </summary>
		/// <returns></returns>
		public bool isInvertedColorScheme() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.isInvertedColorScheme();"
				)
			);
			return _ExecuteBlocking<bool>(script);
		}
	}
}
