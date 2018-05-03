namespace Socketron {
	/// <summary>
	/// Get system preferences.
	/// <para>Process: Main</para>
	/// </summary>
	public class SystemPreferencesClass : ElectronBase {
		public SystemPreferencesClass(Socketron socketron) {
			_socketron = socketron;
		}

		/// <summary>
		/// *macOS*
		/// Returns Boolean - Whether the system is in Dark Mode.
		/// </summary>
		/// <returns></returns>
		public bool IsDarkMode() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.isDarkMode();"
				)
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS*
		/// Returns Boolean - Whether the Swipe between pages setting is on.
		/// </summary>
		/// <returns></returns>
		public bool IsSwipeTrackingFromScrollEventsEnabled() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.isSwipeTrackingFromScrollEventsEnabled();"
				)
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS*
		/// Posts event as native notifications of macOS.
		/// The userInfo is an Object that contains the user information dictionary
		/// sent along with the notification.
		/// </summary>
		/// <param name="event"></param>
		/// <param name="userInfo"></param>
		public void PostNotification(string @event, JsonObject userInfo) {
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
		public void PostLocalNotification(string @event, JsonObject userInfo) {
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
		public void PostWorkspaceNotification(string @event, JsonObject userInfo) {
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
		public void SubscribeNotification() {
			// TODO: implement this
			throw new NotImplementedException();
		}
		//*/

		/*
		public void SubscribeLocalNotification() {
			// TODO: implement this
			throw new NotImplementedException();
		}
		//*/

		/*
		public void SubscribeWorkspaceNotification() {
			// TODO: implement this
			throw new NotImplementedException();
		}
		//*/

		/// <summary>
		/// *macOS*
		/// Removes the subscriber with id.
		/// </summary>
		/// <param name="id"></param>
		public void UnsubscribeNotification(int id) {
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
		public void UnsubscribeLocalNotification(int id) {
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
		public void UnsubscribeWorkspaceNotification(int id) {
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
		public void RegisterDefaults(JsonObject defaults) {
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
		public object GetUserDefault(string key, string type) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.getUserDefault({0},{1});"
				),
				key.Escape(),
				type.Escape()
			);
			return _ExecuteJavaScriptBlocking<object>(script);
		}

		/// <summary>
		/// *macOS*
		/// Set the value of key in NSUserDefaults.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="type"></param>
		/// <param name="value"></param>
		public void SetUserDefault(string key, string type, string value) {
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
		public void RemoveUserDefault(string key) {
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
		public bool IsAeroGlassEnabled() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.isAeroGlassEnabled();"
				)
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows*
		/// Returns String - The users current system wide accent color preference in RGBA hexadecimal form.
		/// </summary>
		/// <returns></returns>
		public string GetAccentColor() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.getAccentColor();"
				)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// *Windows*
		/// Returns String - The system color setting in RGB hexadecimal form (#ABCDEF).
		/// See the Windows docs for more details.
		/// </summary>
		/// <param name="color">SystemPreferences.WindowsColors</param>
		/// <returns></returns>
		public string GetColor(string color) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.getColor({0});"
				),
				color.Escape()
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// *Windows*
		/// Returns Boolean - true if an inverted color scheme,
		/// such as a high contrast theme, is active, false otherwise.
		/// </summary>
		/// <returns></returns>
		public bool IsInvertedColorScheme() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.systemPreferences.isInvertedColorScheme();"
				)
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}
	}
}
