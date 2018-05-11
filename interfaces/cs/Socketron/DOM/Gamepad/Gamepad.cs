using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Gamepad : DOMModule {
		int _buttonsCache = 0;

		public Gamepad() {
		}

		public double[] axes {
			get {
				string script = ScriptBuilder.Build(
					"return {0}.axes;",
					Script.GetObject(API.id)
				);
				object[] result = API._ExecuteBlocking<object[]>(script);
				return Array.ConvertAll(
					result, value => Convert.ToDouble(value)
				);
			}
		}

		public GamepadButton[] buttons {
			get {
				if (_buttonsCache == 0) {
					string script = ScriptBuilder.Build(
						ScriptBuilder.Script(
							"var list = [];",
							"var buttons = {0}.buttons;",
							"for (var b of buttons) {{",
								"list.push({{pressed:b.pressed,value:b.value}});",
							"}}",
							"return list;"
						),
						Script.GetObject(API.id)
					);
					_buttonsCache = API.CacheScript(script);
				}
				object[] result = API.ExecuteCachedScript<object[]>(_buttonsCache);
				if (result == null) {
					return new GamepadButton[0];
				}
				GamepadButton[] buttons = new GamepadButton[result.Length];
				for (int i = 0; i < result.Length; i++) {
					object item = result[i];
					JsonObject button = new JsonObject(item);
					buttons[i] = new GamepadButton() {
						pressed = button.Bool("pressed"),
						value = button.Double("value")
					};
				}
				return buttons;
			}
		}

		public bool connected {
			get { return API.GetProperty<bool>("connected"); }
		}

		public int displayId {
			get { return API.GetProperty<int>("displayId"); }
		}

		public string id {
			get { return API.GetProperty<string>("id"); }
		}

		public int index {
			get { return API.GetProperty<int>("index"); }
		}

		public string mapping {
			get { return API.GetProperty<string>("mapping"); }
		}

		public double timestamp {
			get { return API.GetProperty<double>("timestamp"); }
		}
	}
}
