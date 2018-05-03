namespace Socketron {
	public class StreamProtocolResponse {
		/// <summary>
		/// The HTTP response code.
		/// </summary>
		public int? statusCode;
		/// <summary>
		/// An object containing the response headers.
		/// </summary>
		public object headers;
		/// <summary>
		/// A Node.js readable stream representing the response body.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		//public string data;

		public static StreamProtocolResponse Parse(string text) {
			return JSON.Parse<StreamProtocolResponse>(text);
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
