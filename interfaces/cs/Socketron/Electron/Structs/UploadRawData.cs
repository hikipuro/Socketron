using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class UploadRawData : JSObject {
		/// <summary>
		/// rawData.
		/// </summary>
		public string type {
			get { return API.GetProperty<string>("type"); }
		}
		/// <summary>
		/// Data to be uploaded.
		/// </summary>
		public Buffer bytes {
			get { return API.GetObject<Buffer>("bytes"); }
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
