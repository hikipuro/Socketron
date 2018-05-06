namespace Socketron.Electron {
	public class MemoryUsageDetails {
		public int? count;
		public int? size;
		public int? liveSize;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static MemoryUsageDetails Parse(string text) {
			return JSON.Parse<MemoryUsageDetails>(text);
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


