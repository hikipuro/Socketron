namespace Socketron {
	/// <summary>
	/// Note that all statistics are reported in Kilobytes.
	/// </summary>
	public class MemoryInfo {
		/// <summary>
		/// Process id of the process.
		/// </summary>
		//public int? pid;
		/// <summary>
		/// The amount of memory currently pinned to actual physical RAM.
		/// </summary>
		public int? workingSetSize;
		/// <summary>
		/// The maximum amount of memory that has ever been pinned to actual physical RAM.
		/// On macOS its value will always be 0.
		/// </summary>
		public int? peakWorkingSetSize;
		/// <summary>
		/// The amount of memory not shared by other processes,
		/// such as JS heap or HTML content.
		/// </summary>
		public int? privateBytes;
		/// <summary>
		/// The amount of memory shared between processes,
		/// typically memory consumed by the Electron code itself
		/// </summary>
		public int? sharedBytes;

		public static MemoryInfo FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new MemoryInfo() {
				workingSetSize = json.Int32("workingSetSize"),
				peakWorkingSetSize = json.Int32("peakWorkingSetSize"),
				privateBytes = json.Int32("privateBytes"),
				sharedBytes = json.Int32("sharedBytes")
			};
		}

		public static MemoryInfo Parse(string text) {
			return JSON.Parse<MemoryInfo>(text);
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
