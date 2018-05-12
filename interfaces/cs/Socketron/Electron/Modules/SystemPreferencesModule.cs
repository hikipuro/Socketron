using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Get system preferences.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class SystemPreferencesModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public SystemPreferencesModule() {
		}

		public EventEmitter on(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.on(eventName, listener);
		}

		public EventEmitter once(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.once(eventName, listener);
		}

		/// <summary>
		/// *macOS*
		/// Returns Boolean - Whether the system is in Dark Mode.
		/// </summary>
		/// <returns></returns>
		public bool isDarkMode() {
			return API.Apply<bool>("isDarkMode");
		}

		/// <summary>
		/// *macOS*
		/// Returns Boolean - Whether the Swipe between pages setting is on.
		/// </summary>
		/// <returns></returns>
		public bool isSwipeTrackingFromScrollEventsEnabled() {
			return API.Apply<bool>("isSwipeTrackingFromScrollEventsEnabled");
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
			API.Apply("postNotification", @event, userInfo);
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
			API.Apply("postLocalNotification", @event, userInfo);
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
			API.Apply("postWorkspaceNotification", @event, userInfo);
		}

		/// <summary>
		/// *macOS*
		/// Subscribes to native notifications of macOS.
		/// </summary>
		/// <param name="event"></param>
		/// <param name="callback"></param>
		public void subscribeNotification(string @event, Action<string, JsonObject> callback) {
			string eventName = "_subscribeNotification";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				string eventParam = Convert.ToString(args[0]);
				JsonObject userInfo = new JsonObject(args[1]);
				callback?.Invoke(eventParam, userInfo);
			});
			API.Apply("subscribeNotification", @event, item);
		}

		/// <summary>
		/// *macOS*
		/// Same as subscribeNotification, but uses NSNotificationCenter for local defaults.
		/// </summary>
		/// <param name="event"></param>
		/// <param name="callback"></param>
		public void subscribeLocalNotification(string @event, Action<string, JsonObject> callback) {
			string eventName = "_subscribeLocalNotification";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				string eventParam = Convert.ToString(args[0]);
				JsonObject userInfo = new JsonObject(args[1]);
				callback?.Invoke(eventParam, userInfo);
			});
			API.Apply("subscribeLocalNotification", @event, item);
		}

		/// <summary>
		/// *macOS*
		/// Same as subscribeNotification, but uses NSWorkspace.sharedWorkspace.notificationCenter.
		/// </summary>
		/// <param name="event"></param>
		/// <param name="callback"></param>
		public void subscribeWorkspaceNotification(string @event, Action<string, JsonObject> callback) {
			string eventName = "_subscribeWorkspaceNotification";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				string eventParam = Convert.ToString(args[0]);
				JsonObject userInfo = new JsonObject(args[1]);
				callback?.Invoke(eventParam, userInfo);
			});
			API.Apply("subscribeWorkspaceNotification", @event, item);
		}

		/// <summary>
		/// *macOS*
		/// Removes the subscriber with id.
		/// </summary>
		/// <param name="id"></param>
		public void unsubscribeNotification(int id) {
			API.Apply("unsubscribeNotification", id);
		}

		/// <summary>
		/// *macOS*
		/// Same as unsubscribeNotification,
		/// but removes the subscriber from NSNotificationCenter.
		/// </summary>
		/// <param name="id"></param>
		public void unsubscribeLocalNotification(int id) {
			API.Apply("unsubscribeLocalNotification", id);
		}

		/// <summary>
		/// *macOS*
		/// Same as unsubscribeNotification,
		/// but removes the subscriber from NSWorkspace.sharedWorkspace.notificationCenter.
		/// </summary>
		/// <param name="id"></param>
		public void unsubscribeWorkspaceNotification(int id) {
			API.Apply("unsubscribeWorkspaceNotification", id);
		}

		/// <summary>
		/// *macOS*
		/// Add the specified defaults to your application's NSUserDefaults.
		/// </summary>
		/// <param name="defaults"></param>
		public void registerDefaults(JsonObject defaults) {
			API.Apply("registerDefaults", defaults);
		}

		/// <summary>
		/// *macOS*
		/// Returns any - The value of key in NSUserDefaults.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public JsonObject getUserDefault(string key, string type) {
			object result = API.Apply("getUserDefault", key, type);
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
			API.Apply("setUserDefault", key, type, value);
		}

		/// <summary>
		/// *macOS*
		/// Removes the key in NSUserDefaults.
		/// This can be used to restore the default
		/// or global value of a key previously set with setUserDefault.
		/// </summary>
		/// <param name="key"></param>
		public void removeUserDefault(string key) {
			API.Apply("removeUserDefault", key);
		}

		/// <summary>
		/// *Windows*
		/// Returns Boolean - true if DWM composition (Aero Glass) is enabled, and false otherwise.
		/// </summary>
		/// <returns></returns>
		public bool isAeroGlassEnabled() {
			return API.Apply<bool>("isAeroGlassEnabled");
		}

		/// <summary>
		/// *Windows*
		/// Returns String - The users current system wide accent color preference in RGBA hexadecimal form.
		/// </summary>
		/// <returns></returns>
		public string getAccentColor() {
			return API.Apply<string>("getAccentColor");
		}

		/// <summary>
		/// *Windows*
		/// Returns String - The system color setting in RGB hexadecimal form (#ABCDEF).
		/// See the Windows docs for more details.
		/// </summary>
		/// <param name="color">SystemPreferences.WindowsColors</param>
		/// <returns></returns>
		public string getColor(string color) {
			return API.Apply<string>("getColor", color);
		}

		/// <summary>
		/// *Windows*
		/// Returns Boolean - true if an inverted color scheme,
		/// such as a high contrast theme, is active, false otherwise.
		/// </summary>
		/// <returns></returns>
		public bool isInvertedColorScheme() {
			return API.Apply<bool>("isInvertedColorScheme");
		}
	}
}
