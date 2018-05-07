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
		/// <param name="client"></param>
		/// <param name="id"></param>
		public ScreenModule(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// The current absolute position of the mouse pointer.
		/// </summary>
		/// <returns></returns>
		public Point getCursorScreenPoint() {
			string script = ScriptBuilder.Build(
				"return {0}.getCursorScreenPoint();",
				Script.GetObject(_id)
			);
			object result = _ExecuteBlocking<object>(script);
			return Point.FromObject(result);
		}

		/// <summary>
		/// *macOS*
		/// Returns Integer - The height of the menu bar in pixels.
		/// </summary>
		/// <returns></returns>
		public int getMenuBarHeight() {
			string script = ScriptBuilder.Build(
				"return {0}.getMenuBarHeight();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<int>(script);
		}

		/// <summary>
		/// Returns Display - The primary display.
		/// </summary>
		/// <returns></returns>
		public Display getPrimaryDisplay() {
			string script = ScriptBuilder.Build(
				"return {0}.getPrimaryDisplay();",
				Script.GetObject(_id)
			);
			object result = _ExecuteBlocking<object>(script);
			return Display.FromObject(result);
		}

		/// <summary>
		/// Returns Display[] - An array of displays that are currently available.
		/// </summary>
		/// <returns></returns>
		public List<Display> getAllDisplays() {
			string script = ScriptBuilder.Build(
				"return {0}.getAllDisplays();",
				Script.GetObject(_id)
			);
			object result = _ExecuteBlocking<object>(script);
			object[] list = result as object[];
			List<Display> displayList = new List<Display>();
			foreach (object item in list) {
				Display display = Display.FromObject(item);
				displayList.Add(display);
			}
			return displayList;
		}

		/// <summary>
		/// Returns Display - The display nearest the specified point.
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public Display getDisplayNearestPoint(Point point) {
			string script = ScriptBuilder.Build(
				"return {0}.getDisplayNearestPoint({1});",
				Script.GetObject(_id),
				point.Stringify()
			);
			object result = _ExecuteBlocking<object>(script);
			return Display.FromObject(result);
		}

		/// <summary>
		/// Returns Display - The display that most closely intersects the provided bounds.
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		public Display getDisplayMatching(Rectangle rect) {
			string script = ScriptBuilder.Build(
				"return {0}.getDisplayMatching({1});",
				Script.GetObject(_id),
				rect.Stringify()
			);
			object result = _ExecuteBlocking<object>(script);
			return Display.FromObject(result);
		}

	}
}
