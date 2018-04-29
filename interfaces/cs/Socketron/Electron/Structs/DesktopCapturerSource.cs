using System.Web.Script.Serialization;

namespace Socketron {
	public class DesktopCapturerSource {
		public string id;
		public string name;
		//public NativeImage thumbnail;
		public string display_id;

		public static DesktopCapturerSource Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<DesktopCapturerSource>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}

