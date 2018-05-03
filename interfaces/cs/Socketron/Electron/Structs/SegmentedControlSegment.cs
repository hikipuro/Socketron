﻿namespace Socketron {
	public class SegmentedControlSegment {
		/// <summary>
		/// (optional) The text to appear in this segment.
		/// </summary>
		public string label;
		/// <summary>
		/// (optional) The image to appear in this segment.
		/// </summary>
		public NativeImage icon;
		/// <summary>
		/// (optional) Whether this segment is selectable. Default: true.
		/// </summary>
		public bool? enabled;

		public static SegmentedControlSegment Parse(string text) {
			return JSON.Parse<SegmentedControlSegment>(text);
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
