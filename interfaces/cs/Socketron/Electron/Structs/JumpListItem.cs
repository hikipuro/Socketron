using System.Web.Script.Serialization;

namespace Socketron {
	public class JumpListItem {
		public string type;
		public string path;
		public string program;
		public string args;
		public string title;
		public string description;
		public string iconPath;
		public int? iconIndex;

		public static JumpListItem Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<JumpListItem>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}

