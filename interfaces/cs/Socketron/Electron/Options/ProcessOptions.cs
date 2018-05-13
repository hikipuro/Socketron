namespace Socketron.Electron {
	/// <summary>
	/// Process.getProcessMemoryInfo() return value.
	/// </summary>
	public class ProcessMemoryInfo {
		/// <summary>
		/// The amount of memory currently pinned to actual physical RAM.
		/// </summary>
		public long workingSetSize;
		/// <summary>
		/// The maximum amount of memory that has ever been pinned to actual physical RAM.
		/// </summary>
		public long peakWorkingSetSize;
		/// <summary>
		/// The amount of memory not shared by other processes, such as JS heap or HTML content.
		/// </summary>
		public long privateBytes;
		/// <summary>
		/// The amount of memory shared between processes, typically memory consumed by the Electron code itself
		/// </summary>
		public long sharedBytes;

		public static ProcessMemoryInfo FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new ProcessMemoryInfo() {
				workingSetSize = json.Int64("workingSetSize"),
				peakWorkingSetSize = json.Int64("peakWorkingSetSize"),
				privateBytes = json.Int64("privateBytes"),
				sharedBytes = json.Int64("sharedBytes")
			};
		}
	}

	/// <summary>
	/// Process.getSystemMemoryInfo() return value.
	/// </summary>
	public class SystemMemoryInfo {
		/// <summary>
		/// The total amount of physical memory in Kilobytes available to the system.
		/// </summary>
		public long total;
		/// <summary>
		/// The total amount of memory not being used by applications or disk cache.
		/// </summary>
		public long free;
		/// <summary>
		/// The total amount of swap memory in Kilobytes available to the system.
		/// </summary>
		public long swapTotal;
		/// <summary>
		/// The free amount of swap memory in Kilobytes available to the system.
		/// </summary>
		public long swapFree;

		public static SystemMemoryInfo FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new SystemMemoryInfo() {
				total = json.Int64("total"),
				free = json.Int64("free"),
				swapTotal = json.Int64("swapTotal"),
				swapFree = json.Int64("swapFree")
			};
		}
	}
}
