using System.Web.Script.Serialization;

namespace Socketron {
	public class StreamProtocolResponse {
		public int? statusCode;
		public object headers;
		//public string data;

		public static StreamProtocolResponse Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<StreamProtocolResponse>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
