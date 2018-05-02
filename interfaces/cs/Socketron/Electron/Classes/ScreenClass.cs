using System.Collections.Generic;

namespace Socketron {
	/// <summary>
	/// Retrieve information about screen size, displays, cursor position, etc.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	public class ScreenClass : ElectronBase {
		public ScreenClass(Socketron socketron) {
			_socketron = socketron;
		}

		/// <summary>
		/// The current absolute position of the mouse pointer.
		/// </summary>
		/// <returns></returns>
		public Point GetCursorScreenPoint() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.screen.getCursorScreenPoint();"
				)
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			JsonObject json = new JsonObject(result);
			Point point = new Point() {
				x = json.Int32("x"),
				y = json.Int32("y")
			};
			return point;
		}

		/// <summary>
		/// *macOS* Returns Integer - The height of the menu bar in pixels.
		/// </summary>
		/// <returns></returns>
		public int GetMenuBarHeight() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.screen.getMenuBarHeight();"
				)
			);
			return _ExecuteJavaScriptBlocking<int>(script);
		}

		/// <summary>
		/// Returns Display - The primary display.
		/// </summary>
		/// <returns></returns>
		public Display GetPrimaryDisplay() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.screen.getPrimaryDisplay();"
				)
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			JsonObject json = new JsonObject(result);
			Display display = new Display() {
				id = json.Int64("id"),
				rotation = json.Double("rotation"),
				scaleFactor = json.Double("scaleFactor"),
				touchSupport = json.String("touchSupport"),
				bounds = Rectangle.FromObject(json["bounds"]),
				size = Size.FromObject(json["size"]),
				workArea = Rectangle.FromObject(json["workArea"]),
				workAreaSize = Size.FromObject(json["workAreaSize"])
			};
			return display;
		}

		/// <summary>
		/// Returns Display[] - An array of displays that are currently available.
		/// </summary>
		/// <returns></returns>
		public List<Display> GetAllDisplays() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.screen.getAllDisplays();"
				)
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			object[] list = result as object[];
			List<Display> displayList = new List<Display>();
			foreach (object item in list) {
				JsonObject json = new JsonObject(item);
				Display display = new Display() {
					id = json.Int64("id"),
					rotation = json.Double("rotation"),
					scaleFactor = json.Double("scaleFactor"),
					touchSupport = json.String("touchSupport"),
					bounds = Rectangle.FromObject(json["bounds"]),
					size = Size.FromObject(json["size"]),
					workArea = Rectangle.FromObject(json["workArea"]),
					workAreaSize = Size.FromObject(json["workAreaSize"])
				};
				displayList.Add(display);
			}
			return displayList;
		}

		/// <summary>
		/// Returns Display - The display nearest the specified point.
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public Display GetDisplayNearestPoint(Point point) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.screen.getDisplayNearestPoint({0});"
				),
				point.Stringify()
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			JsonObject json = new JsonObject(result);
			Display display = new Display() {
				id = json.Int64("id"),
				rotation = json.Double("rotation"),
				scaleFactor = json.Double("scaleFactor"),
				touchSupport = json.String("touchSupport"),
				bounds = Rectangle.FromObject(json["bounds"]),
				size = Size.FromObject(json["size"]),
				workArea = Rectangle.FromObject(json["workArea"]),
				workAreaSize = Size.FromObject(json["workAreaSize"])
			};
			return display;
		}

		/// <summary>
		/// Returns Display - The display that most closely intersects the provided bounds.
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		public Display GetDisplayMatching(Rectangle rect) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.screen.getDisplayMatching({0});"
				),
				rect.Stringify()
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			JsonObject json = new JsonObject(result);
			Display display = new Display() {
				id = json.Int64("id"),
				rotation = json.Double("rotation"),
				scaleFactor = json.Double("scaleFactor"),
				touchSupport = json.String("touchSupport"),
				bounds = Rectangle.FromObject(json["bounds"]),
				size = Size.FromObject(json["size"]),
				workArea = Rectangle.FromObject(json["workArea"]),
				workAreaSize = Size.FromObject(json["workAreaSize"])
			};
			return display;
		}

	}
}
