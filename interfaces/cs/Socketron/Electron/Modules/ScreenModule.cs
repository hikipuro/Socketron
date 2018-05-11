using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Retrieve information about screen size, displays, cursor position, etc.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class ScreenModule : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public ScreenModule() {
		}

		/// <summary>
		/// The current absolute position of the mouse pointer.
		/// </summary>
		/// <returns></returns>
		public Point getCursorScreenPoint() {
			object result = API.Apply("getCursorScreenPoint");
			return Point.FromObject(result);
		}

		/// <summary>
		/// *macOS*
		/// Returns Integer - The height of the menu bar in pixels.
		/// </summary>
		/// <returns></returns>
		public int getMenuBarHeight() {
			return API.Apply<int>("getMenuBarHeight");
		}

		/// <summary>
		/// Returns Display - The primary display.
		/// </summary>
		/// <returns></returns>
		public Display getPrimaryDisplay() {
			object result = API.Apply("getPrimaryDisplay");
			return Display.FromObject(result);
		}

		/// <summary>
		/// Returns Display[] - An array of displays that are currently available.
		/// </summary>
		/// <returns></returns>
		public Display[] getAllDisplays() {
			object[] result = API.Apply<object[]>("getAllDisplays");
			return Array.ConvertAll(
				result, value => Display.FromObject(value)
			);
		}

		/// <summary>
		/// Returns Display - The display nearest the specified point.
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public Display getDisplayNearestPoint(Point point) {
			object result = API.Apply("getDisplayNearestPoint", point);
			return Display.FromObject(result);
		}

		/// <summary>
		/// Returns Display - The display that most closely intersects the provided bounds.
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		public Display getDisplayMatching(Rectangle rect) {
			object result = API.Apply("getDisplayMatching", rect);
			return Display.FromObject(result);
		}
	}
}
