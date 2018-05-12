using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class ScrubberItem : JSObject {
		/// <summary>
		/// (optional) The text to appear in this item.
		/// </summary>
		public string label {
			get { return API.GetProperty<string>("label"); }
			set { API.SetProperty("label", value); }
		}
		/// <summary>
		/// (optional) The image to appear in this item.
		/// </summary>
		public NativeImage icon {
			get { return API.GetObject<NativeImage>("icon"); }
			set { API.SetObject("icon", value); }
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
