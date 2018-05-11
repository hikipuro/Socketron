using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Add items to native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class MenuItemModule : JSModule {
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
		public MenuItem Create(MenuItem.Options options) {
			if (options == null) {
				options = new MenuItem.Options();
			}
			return API.ApplyConstructor<MenuItem>(options);
		}

		/// <summary>
		/// Create a new MenuItem instance.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public MenuItem Create(string options) {
			return Create(MenuItem.Options.Parse(options));
		}
	}
}
