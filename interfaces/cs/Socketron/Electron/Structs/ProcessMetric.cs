namespace Socketron {
	public class ProcessMetric {
		public int? pid;
		public string type;
		public MemoryInfo memory;
		public CPUUsage cpu;

		public static ProcessMetric Parse(string text) {
			return JSON.Parse<ProcessMetric>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
