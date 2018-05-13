using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Detect keyboard events when the application does not have keyboard focus.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class GlobalShortcut : EventEmitter {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public GlobalShortcut() {
		}

		/// <summary>
		/// Registers a global shortcut of accelerator.
		/// The callback is called when the registered shortcut is pressed by the user.
		/// </summary>
		/// <param name="accelerator"></param>
		/// <param name="callback"></param>
		public void register(string accelerator, Action callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_register";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				callback?.Invoke();
			});
			API.Apply("register", accelerator, item);
		}

		/// <summary>
		/// Returns Boolean - Whether this application has registered accelerator.
		/// </summary>
		/// <param name="accelerator"></param>
		/// <returns></returns>
		public bool isRegistered(string accelerator) {
			return API.Apply<bool>("isRegistered", accelerator);
		}

		/// <summary>
		/// Unregisters the global shortcut of accelerator.
		/// </summary>
		/// <param name="accelerator"></param>
		public void unregister(string accelerator) {
			API.Apply("unregister", accelerator);
		}

		/// <summary>
		/// Unregisters all of the global shortcuts.
		/// </summary>
		public void unregisterAll() {
			API.Apply("unregisterAll");
		}
	}
}
