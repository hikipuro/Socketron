﻿namespace Socketron {
	public class MemoryUsageDetails {
		public int? count;
		public int? size;
		public int? liveSize;

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


