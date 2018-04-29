using System.Web.Script.Serialization;

namespace Socketron {
	public class MemoryUsageDetails {
		public int? count;
		public int? size;
		public int? liveSize;

		public static MemoryUsageDetails Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<MemoryUsageDetails>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}


