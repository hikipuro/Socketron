namespace Socketron {
	public class CPUUsage {
		public double percentCPUUsage;
		public double idleWakeupsPerSecond;

		public static CPUUsage Parse(string text) {
			return JSON.Parse<CPUUsage>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

