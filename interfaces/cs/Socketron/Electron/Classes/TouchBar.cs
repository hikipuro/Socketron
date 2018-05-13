using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create TouchBar layouts for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBar : EventEmitter {
		/// <summary>
		/// TouchBar constructor options.
		/// </summary>
		public class Options {
			public TouchBarItem[] items;
			public TouchBarItem escapeItem;
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBar() {
		}

		/// <summary>
		/// A TouchBarItem that will replace the "esc" button on the touch bar when set.
		/// Setting to null restores the default "esc" button. Changing this value
		/// immediately updates the escape item in the touch bar.
		/// </summary>
		public TouchBarItem escapeItem {
			get { return API.GetObject<TouchBarItem>("escapeItem"); }
			set { API.SetObject("escapeItem", value); }
		}
	}
}
