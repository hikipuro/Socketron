using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class StreamProtocolResponse : JSObject {
		/// <summary>
		/// The HTTP response code.
		/// </summary>
		public int statusCode {
			get { return API.GetProperty<int>("statusCode"); }
		}
		/// <summary>
		/// An object containing the response headers.
		/// </summary>
		public JsonObject headers {
			get {
				object result = API.GetProperty<object>("statusCode");
				return new JsonObject(result);
			}
		}
		/// <summary>
		/// A Node.js readable stream representing the response body.
		/// </summary>
		public Readable data {
			get { return API.GetObject<Readable>("data"); }
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
