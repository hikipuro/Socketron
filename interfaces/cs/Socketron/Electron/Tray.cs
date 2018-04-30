using System;
using System.Threading;

namespace Socketron {
	/// <summary>
	/// Add icons and context menus to the system's notification area.
	/// <para>Process: Main</para>
	/// </summary>
	public class Tray {
		public const string Name = "Tray";
		public int ID;
		protected Socketron _socketron;

		protected Tray() {
		}

		public static Tray Create(Socketron socketron, NativeImage image) {
			string[] script = new[] {
				"var image = this._objRefs[" + image.ID + "];",
				"var tray = new electron.Tray(image);",
				"return this._addObjectReference(tray);"
			};
			int result = _ExecuteJavaScriptBlocking<int>(socketron, script);
			Tray tray = new Tray() {
				_socketron = socketron,
				ID = result
			};
			return tray;
		}

		public void Destroy() {
			string[] script = new[] {
				"var tray = this._objRefs[" + ID + "];",
				"tray.destroy();",
				"this._removeObjectReference(" + ID + ");"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public void SetImage(NativeImage image) {
			string[] script = new[] {
				"var image = this._objRefs[" + image.ID + "];",
				"var tray = this._objRefs[" + ID + "];",
				"tray.setImage(image);"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public void SetPressedImage(NativeImage image) {
			string[] script = new[] {
				"var image = this._objRefs[" + image.ID + "];",
				"var tray = this._objRefs[" + ID + "];",
				"tray.setPressedImage(image);"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public void SetToolTip(string toolTip) {
			string[] script = new[] {
				"var tray = this._objRefs[" + ID + "];",
				"tray.setToolTip(" + toolTip.Escape() + ");"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public void SetTitle(string title) {
			string[] script = new[] {
				"var tray = this._objRefs[" + ID + "];",
				"tray.setTitle(" + title.Escape() + ");"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public void SetHighlightMode(string mode) {
			string[] script = new[] {
				"var tray = this._objRefs[" + ID + "];",
				"tray.setHighlightMode(" + mode.Escape() + ");"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		/*
		public void DisplayBalloon(string mode) {
			string[] script = new[] {
				"var tray = this._objRefs[" + ID + "];",
				"tray.displayBalloon(" + mode.Escape() + ");"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}
		//*/

		/*
		public void PopUpContextMenu(string mode) {
			string[] script = new[] {
				"var tray = this._objRefs[" + ID + "];",
				"tray.popUpContextMenu(" + mode.Escape() + ");"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}
		//*/

		public void SetContextMenu(Menu menu) {
			string[] script = new[] {
				"var menu = this._objRefs[" + menu.ID + "];",
				"var tray = this._objRefs[" + ID + "];",
				"tray.setContextMenu(menu);"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public Rectangle GetBounds(string mode) {
			string[] script = new[] {
				"var tray = this._objRefs[" + ID + "];",
				"return tray.getBounds();"
			};
			object result = _ExecuteJavaScriptBlocking<object>(_socketron, script);
			JsonObject json = new JsonObject(result);
			Rectangle rect = new Rectangle() {
				x = (int)json["x"],
				y = (int)json["y"],
				width = (int)json["width"],
				height = (int)json["height"]
			};
			return rect;
		}

		public bool IsDestroyed(Action<bool> callback) {
			string[] script = new[] {
				"var tray = this._objRefs[" + ID + "];",
				"return window.isDestroyed();"
			};
			return _ExecuteJavaScriptBlocking<bool>(_socketron, script);
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
				Console.Error.WriteLine("error: Tray._ExecuteJavaScriptBlocking");
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
