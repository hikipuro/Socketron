namespace Socketron {
	/// <summary>
	/// Add icons and context menus to the system's notification area.
	/// <para>Process: Main</para>
	/// </summary>
	public class TrayClass : ElectronBase {
		public TrayClass(Socketron socketron) {
			_socketron = socketron;
		}

		public Tray Create(NativeImage image) {
			if (image == null) {
				return null;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"var tray = new electron.Tray(image);",
					"return " + Script.AddObject("tray") + ";"
				),
				Script.GetObject(image.ID)
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			Tray tray = new Tray(_socketron) {
				ID = result
			};
			return tray;
		}

		public Tray Create(string image) {
			if (image == null) {
				return null;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = new electron.Tray({0});",
					"return " + Script.AddObject("tray") + ";"
				),
				image.Escape()
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			Tray tray = new Tray(_socketron) {
				ID = result
			};
			return tray;
		}
	}
}
