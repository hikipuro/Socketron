namespace Socketron {
	public class IOCounters {
		public int? readOperationCount;
		public int? writeOperationCount;
		public int? otherOperationCount;
		public int? readTransferCount;
		public int? writeTransferCount;
		public int? otherTransferCount;

		public static IOCounters Parse(string text) {
			return JSON.Parse<IOCounters>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

