using System;
using System.Collections.Generic;
using System.Threading;

namespace Socketron {
	/// <summary>
	/// Retrieve information about screen size, displays, cursor position, etc.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	public class Screen {
		public class Events {
			public const string DisplayAdded = "display-added";
			public const string DisplayRemoved = "display-removed";
			public const string DisplayMetricsChanged = "display-metrics-changed";
		}

		protected Screen() {
		}

		public static Point GetCursorScreenPoint(Socketron socketron) {
			string[] script = new[] {
				"return electron.screen.getCursorScreenPoint();",
			};
			object result = _ExecuteJavaScriptBlocking<object>(socketron, script);
			JsonObject json = new JsonObject(result);
			Point point = new Point() {
				x = json.Int32("x"),
				y = json.Int32("y")
			};
			return point;
		}

		/// <summary>
		/// macOS
		/// </summary>
		/// <param name="socketron"></param>
		/// <returns></returns>
		public static int GetMenuBarHeight(Socketron socketron) {
			string[] script = new[] {
				"return electron.screen.getMenuBarHeight();",
			};
			return _ExecuteJavaScriptBlocking<int>(socketron, script);
		}

		public static Display GetPrimaryDisplay(Socketron socketron) {
			string[] script = new[] {
				"return electron.screen.getPrimaryDisplay();",
			};
			object result = _ExecuteJavaScriptBlocking<object>(socketron, script);
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

		public static List<Display> GetAllDisplays(Socketron socketron) {
			string[] script = new[] {
				"return electron.screen.getAllDisplays();",
			};
			object result = _ExecuteJavaScriptBlocking<object>(socketron, script);
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

		public static Display GetDisplayNearestPoint(Socketron socketron, Point point) {
			string[] script = new[] {
				"return electron.screen.getDisplayNearestPoint(" + point.Stringify() + ");",
			};
			object result = _ExecuteJavaScriptBlocking<object>(socketron, script);
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

		public static Display GetDisplayMatching(Socketron socketron, Rectangle rect) {
			string[] script = new[] {
				"return electron.screen.getDisplayMatching(" + rect.Stringify() + ");",
			};
			object result = _ExecuteJavaScriptBlocking<object>(socketron, script);
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

		protected static void _ExecuteJavaScript(Socketron socketron, string[] script, Callback success, Callback error) {
			socketron.Main.ExecuteJavaScript(script, success, error);
		}

		protected static T _ExecuteJavaScriptBlocking<T>(Socketron socketron, string[] script) {
			bool done = false;
			T value = default(T);

			_ExecuteJavaScript(socketron, script, (result) => {
				if (result == null) {
					done = true;
					return;
				}
				if (typeof(T) == typeof(double)) {
					//Console.WriteLine(result.GetType());
					if (result.GetType() == typeof(int)) {
						result = (double)(int)result;
					} else if (result.GetType() == typeof(Decimal)) {
						result = (double)(Decimal)result;
					}
				}
				value = (T)result;
				done = true;
			}, (result) => {
				Console.Error.WriteLine("error: Screen._ExecuteJavaScriptBlocking");
				throw new InvalidOperationException(result as string);
				//done = true;
			});

			while (!done) {
				Thread.Sleep(TimeSpan.FromTicks(1));
			}
			return value;
		}
	}
}
