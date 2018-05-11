using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Add icons and context menus to the system's notification area.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TrayModule : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TrayModule() {
		}

		/// <summary>
		/// Creates a new tray icon associated with the image.
		/// </summary>
		/// <param name="image"></param>
		public Tray Create(NativeImage image) {
			return API.ApplyConstructor<Tray>(image);
		}

		/// <summary>
		/// Creates a new tray icon associated with the image.
		/// </summary>
		/// <param name="image"></param>
		public Tray Create(string image) {
			return API.ApplyConstructor<Tray>(image);
		}
	}
}
