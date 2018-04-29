using System.Web.Script.Serialization;

namespace Socketron {
	public class FileFilter {
		public string name;
		public string[] extensions;

		public static FileFilter Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<FileFilter>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}

