using System.Web.Script.Serialization;

namespace Socketron {
	public class Task {
		public string program;
		public string arguments;
		public string title;
		public string description;
		public string iconPath;
		public int? iconIndex;

		public static Task Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<Task>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
