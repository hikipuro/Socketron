namespace Socketron.Electron {
	public class IOCounters {
		/// <summary>
		/// The number of I/O read operations.
		/// </summary>
		public int? readOperationCount;
		/// <summary>
		/// The number of I/O write operations.
		/// </summary>
		public int? writeOperationCount;
		/// <summary>
		/// The number of I/O other operations.
		/// </summary>
		public int? otherOperationCount;
		/// <summary>
		/// The number of I/O read transfers.
		/// </summary>
		public int? readTransferCount;
		/// <summary>
		/// The number of I/O write transfers.
		/// </summary>
		public int? writeTransferCount;
		/// <summary>
		/// The number of I/O other transfers.
		/// </summary>
		public int? otherTransferCount;

		public static IOCounters FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new IOCounters() {
				readOperationCount = json.Int32("readOperationCount"),
				writeOperationCount = json.Int32("writeOperationCount"),
				otherOperationCount = json.Int32("otherOperationCount"),
				readTransferCount = json.Int32("readTransferCount"),
				writeTransferCount = json.Int32("writeTransferCount"),
				otherTransferCount = json.Int32("otherTransferCount")
			};
		}

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static IOCounters Parse(string text) {
			return JSON.Parse<IOCounters>(text);
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

