using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Retrieve information about screen size, displays, cursor position, etc.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class ScreenClass : NodeBase {
		public ScreenClass(Socketron socketron) {
			_socketron = socketron;
		}

		/// <summary>
		/// The current absolute position of the mouse pointer.
		/// </summary>
		/// <returns></returns>
		public Point getCursorScreenPoint() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.screen.getCursorScreenPoint();"
				)
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
				ScriptBuilder.Script(
					"return electron.screen.getMenuBarHeight();"
				)
			);
			return _ExecuteBlocking<int>(script);
		}

		/// <summary>
		/// Returns Display - The primary display.
		/// </summary>
		/// <returns></returns>
		public Display getPrimaryDisplay() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.screen.getPrimaryDisplay();"
				)
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
				ScriptBuilder.Script(
					"return electron.screen.getAllDisplays();"
				)
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
				ScriptBuilder.Script(
					"return electron.screen.getDisplayNearestPoint({0});"
				),
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
				ScriptBuilder.Script(
					"return electron.screen.getDisplayMatching({0});"
				),
				rect.Stringify()
			);
			object result = _ExecuteBlocking<object>(script);
			return Display.FromObject(result);
		}

	}
}
