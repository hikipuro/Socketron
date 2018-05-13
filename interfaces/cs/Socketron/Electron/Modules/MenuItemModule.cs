using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Add items to native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class MenuItemModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public MenuItemModule() {
		}

		/// <summary>
		/// Create a new MenuItem instance.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public MenuItem Create(MenuItemConstructorOptions options) {
			if (options == null) {
				options = new MenuItemConstructorOptions();
			}
			return API.ApplyConstructor<MenuItem>(options);
		}

		/// <summary>
		/// Create a new MenuItem instance.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public MenuItem Create(string options) {
			return Create(MenuItemConstructorOptions.Parse(options));
		}
	}
}
