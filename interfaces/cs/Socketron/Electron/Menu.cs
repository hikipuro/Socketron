using System;
using System.Threading;
using System.Web.Script.Serialization;

namespace Socketron {
	/// <summary>
	/// Create native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	public class Menu {
		public const string Name = "Menu";
		public int ID;
		protected Socketron _socketron;

		public class Events {
			public const string MenuWillShow = "menu-will-show";
			public const string MenuWillClose = "menu-will-close";
		}

		protected Menu() {
		}

		public static void SetApplicationMenu(Socketron socketron, Menu menu) {
			if (menu == null) {
				return;
			}
			string[] script = new[] {
				"var menu = this._objRefs[" + menu.ID + "];",
				"electron.Menu.setApplicationMenu(menu);",
			};
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static Menu GetApplicationMenu(Socketron socketron) {
			string[] script = new[] {
				"var menu = electron.Menu.getApplicationMenu();",
				"return this._addObjectReference(menu);"
			};
			int result = _ExecuteJavaScriptBlocking<int>(socketron, script);
			if (result <= 0) {
				return null;
			}
			Menu menu = new Menu() {
				_socketron = socketron,
				ID = result
			};
			return menu;
		}

		/// <summary>
		/// macOS
		/// </summary>
		/// <param name="socketron"></param>
		/// <param name="action"></param>
		public static void SendActionToFirstResponder(Socketron socketron, string action) {
			string[] script = new[] {
				"electron.Menu.sendActionToFirstResponder(" + action.Escape() + ");",
			};
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static Menu BuildFromTemplate(Socketron socketron, MenuItem.Options[] template) {
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			serializer.RegisterConverters(
				new JavaScriptConverter[] {
					new NullPropertiesConverter()
				}
			);
			string templateText = serializer.Serialize(template);

			string[] script = new[] {
				"var menu = electron.Menu.buildFromTemplate(" + templateText + ");",
				"return this._addObjectReference(menu);"
			};
			int result = _ExecuteJavaScriptBlocking<int>(socketron, script);
			if (result <= 0) {
				return null;
			}
			Menu menu = new Menu() {
				_socketron = socketron,
				ID = result
			};
			return menu;
		}

		public static Menu BuildFromTemplate(Socketron socketron, string template) {
			string[] script = new[] {
				"var menu = electron.Menu.buildFromTemplate(" + template + ");",
				"return this._addObjectReference(menu);"
			};
			int result = _ExecuteJavaScriptBlocking<int>(socketron, script);
			if (result <= 0) {
				return null;
			}
			Menu menu = new Menu() {
				_socketron = socketron,
				ID = result
			};
			return menu;
		}

		/*
		public void Popup(string options) {
			string[] script = new[] {
				"electron.Menu.popup(" + options.Escape() + ");",
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}
		//*/

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
				Console.Error.WriteLine("error: Menu._ExecuteJavaScriptBlocking");
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
