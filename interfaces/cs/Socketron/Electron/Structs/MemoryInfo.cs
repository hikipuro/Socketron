namespace Socketron {
	public class MemoryInfo {
		//public int? pid;
		public int? workingSetSize;
		public int? peakWorkingSetSize;
		public int? privateBytes;
		public int? sharedBytes;

		public static MemoryInfo Parse(string text) {
			return JSON.Parse<MemoryInfo>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
