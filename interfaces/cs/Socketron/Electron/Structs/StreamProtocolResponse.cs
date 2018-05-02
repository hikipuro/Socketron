namespace Socketron {
	public class StreamProtocolResponse {
		public int? statusCode;
		public object headers;
		//public string data;

		public static StreamProtocolResponse Parse(string text) {
			return JSON.Parse<StreamProtocolResponse>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
