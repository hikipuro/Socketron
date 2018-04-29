using System.Web.Script.Serialization;

namespace Socketron {
	public class ShortcutDetails {
		public string target;
		public string cwd;
		public string args;
		public string description;
		public string icon;
		public int? iconIndex;
		public string appUserModelId;

		public static ShortcutDetails Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<ShortcutDetails>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
