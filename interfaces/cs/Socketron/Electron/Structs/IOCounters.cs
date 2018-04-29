using System.Web.Script.Serialization;

namespace Socketron {
	public class IOCounters {
		public int? readOperationCount;
		public int? writeOperationCount;
		public int? otherOperationCount;
		public int? readTransferCount;
		public int? writeTransferCount;
		public int? otherTransferCount;

		public static IOCounters Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<IOCounters>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}

