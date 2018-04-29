using System.Web.Script.Serialization;

namespace Socketron {
	public class PrinterInfo {
		public string name;
		public string description;
		public int status;
		public bool? isDefault;

		public static PrinterInfo Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<PrinterInfo>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
