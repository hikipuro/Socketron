namespace Socketron.Electron {
	public class ProcessMetric {
		/// <summary>
		/// Process id of the process.
		/// </summary>
		public int? pid;
		/// <summary>
		/// Process type (Browser or Tab or GPU etc).
		/// </summary>
		public string type;
		/// <summary>
		/// Memory information for the process.
		/// </summary>
		public MemoryInfo memory;
		/// <summary>
		/// CPU usage of the process.
		/// </summary>
		public CPUUsage cpu;

		public static ProcessMetric FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new ProcessMetric() {
				pid = json.Int32("pid"),
				type = json.String("type"),
				memory = MemoryInfo.FromObject(json["memory"]),
				cpu = CPUUsage.FromObject(json["cpu"])
			};
		}

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static ProcessMetric Parse(string text) {
			return JSON.Parse<ProcessMetric>(text);
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
