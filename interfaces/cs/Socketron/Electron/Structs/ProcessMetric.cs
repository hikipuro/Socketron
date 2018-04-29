using System.Web.Script.Serialization;

namespace Socketron {
	public class ProcessMetric {
		public int? pid;
		public string type;
		public MemoryInfo memory;
		public CPUUsage cpu;

		public static ProcessMetric Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<ProcessMetric>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
