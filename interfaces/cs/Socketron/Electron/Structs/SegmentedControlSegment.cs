using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class SegmentedControlSegment : JSObject {
		/// <summary>
		/// (optional) The text to appear in this segment.
		/// </summary>
		public string label {
			get { return API.GetProperty<string>("label"); }
		}
		/// <summary>
		/// (optional) The image to appear in this segment.
		/// </summary>
		public NativeImage icon {
			get { return API.GetObject<NativeImage>("icon"); }
		}
		/// <summary>
		/// (optional) Whether this segment is selectable. Default: true.
		/// </summary>
		public bool enabled {
			get { return API.GetProperty<bool>("enabled"); }
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
