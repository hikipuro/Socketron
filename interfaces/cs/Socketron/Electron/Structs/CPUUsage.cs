namespace Socketron {
	public class CPUUsage {
		/// <summary>
		/// Percentage of CPU used since the last call to getCPUUsage.
		/// First call returns 0.
		/// </summary>
		public double percentCPUUsage;
		/// <summary>
		/// The number of average idle cpu wakeups per second since the last call to getCPUUsage.
		/// First call returns 0. Will always return 0 on Windows.
		/// </summary>
		public double idleWakeupsPerSecond;

		public static CPUUsage FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new CPUUsage() {
				percentCPUUsage = json.Double("percentCPUUsage"),
				idleWakeupsPerSecond = json.Double("idleWakeupsPerSecond")
			};
		}

		public static CPUUsage Parse(string text) {
			return JSON.Parse<CPUUsage>(text);
		}

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <returns></returns>
		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

