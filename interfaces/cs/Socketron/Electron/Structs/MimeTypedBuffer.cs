using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class MimeTypedBuffer : JSObject {
		/// <summary>
		/// The mimeType of the Buffer that you are sending.
		/// </summary>
		public string mimeType {
			get { return API.GetProperty<string>("mimeType"); }
		}
		/// <summary>
		/// The actual Buffer content.
		/// </summary>
		public Buffer data {
			get { return API.GetObject<Buffer>("data"); }
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
