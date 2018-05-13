namespace Socketron.Electron {
	public class MemoryUsageDetails {
		public int? count;
		public int? size;
		public int? liveSize;

		public static MemoryUsageDetails FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new MemoryUsageDetails() {
				count = json.Int32("count"),
				size = json.Int32("size"),
				liveSize = json.Int32("liveSize")
			};
		}

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


