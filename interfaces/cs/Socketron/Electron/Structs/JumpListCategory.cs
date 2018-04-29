using System.Web.Script.Serialization;

namespace Socketron {
	public class JumpListCategory {
		public string type;
		public string name;
		public JumpListItem[] items;

		public static JumpListCategory Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<JumpListCategory>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}

