using System.Web.Script.Serialization;

namespace Socketron {
	public class CPUUsage {
		public double percentCPUUsage;
		public double idleWakeupsPerSecond;

		public static CPUUsage Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<CPUUsage>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}

