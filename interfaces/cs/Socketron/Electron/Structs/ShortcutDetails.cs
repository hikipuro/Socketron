namespace Socketron.Electron {
	public class ShortcutDetails {
		/// <summary>
		/// The target to launch from this shortcut.
		/// </summary>
		public string target;
		/// <summary>
		/// (optional) The working directory.
		/// Default is empty.
		/// </summary>
		public string cwd;
		/// <summary>
		/// (optional) The arguments to be applied to target when launching from this shortcut.
		/// Default is empty.
		/// </summary>
		public string args;
		/// <summary>
		/// (optional) The description of the shortcut.
		/// Default is empty.
		/// </summary>
		public string description;
		/// <summary>
		/// (optional) The path to the icon, can be a DLL or EXE.
		/// icon and iconIndex have to be set together.
		/// Default is empty, which uses the target's icon.
		/// </summary>
		public string icon;
		/// <summary>
		/// (optional) The resource ID of icon when icon is a DLL or EXE.
		/// Default is 0.
		/// </summary>
		public int? iconIndex;
		/// <summary>
		/// (optional) The Application User Model ID.
		/// Default is empty.
		/// </summary>
		public string appUserModelId;

		public static ShortcutDetails FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new ShortcutDetails() {
				target = json.String("target"),
				cwd = json.String("cwd"),
				args = json.String("args"),
				description = json.String("description"),
				icon = json.String("icon"),
				iconIndex = json.Int32("iconIndex"),
				appUserModelId = json.String("appUserModelId")
			};
		}

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static ShortcutDetails Parse(string text) {
			return JSON.Parse<ShortcutDetails>(text);
		}

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <returns></returns>
		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
