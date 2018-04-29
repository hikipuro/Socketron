using System.Web.Script.Serialization;

namespace Socketron {
	public class WebSource {
		public string code;
		public string url;
		public int? startLine;

		public static WebSource Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<WebSource>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
