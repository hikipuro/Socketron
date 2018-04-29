using System.Web.Script.Serialization;

namespace Socketron {
	public class MemoryInfo {
		public int? pid;
		public int? workingSetSize;
		public int? peakWorkingSetSize;
		public int? privateBytes;
		public int? sharedBytes;

		public static MemoryInfo Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<MemoryInfo>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
