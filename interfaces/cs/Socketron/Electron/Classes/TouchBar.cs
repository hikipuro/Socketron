using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create TouchBar layouts for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBar : JSObject {
		public class Options {
			public TouchBarItem[] items;
			public TouchBarItem escapeItem;
		}

		public TouchBar() {
			// TODO: implement this
		}

		/*
		public TouchBarItem escapeItem {
			get { }
		}
		//*/
	}
}
