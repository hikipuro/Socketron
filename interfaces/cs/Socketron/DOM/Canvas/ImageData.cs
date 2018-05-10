using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class ImageData : DOMModule {
		public ImageData() {
		}

		/*
		public Uint8ClampedArray data {
			get { return GetObject<Uint8ClampedArray>("data"); }
		}
		//*/

		public uint height {
			get { return API.GetProperty<uint>("height"); }
		}

		public uint width {
			get { return API.GetProperty<uint>("width"); }
		}
	}
}
