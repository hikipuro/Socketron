using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create TouchBar layouts for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarModule() {
		}

		/// <summary>
		/// *Experimental*
		/// Creates a new touch bar with the specified items.
		/// </summary>
		/// <param name="image"></param>
		public TouchBar Create(TouchBarConstructorOptions options) {
			return API.ApplyConstructor<TouchBar>(options);
		}
	}
}
