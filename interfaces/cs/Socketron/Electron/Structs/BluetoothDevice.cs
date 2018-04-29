using System.Web.Script.Serialization;

namespace Socketron {
	public class BluetoothDevice {
		public string deviceName;
		public string deviceId;

		public static BluetoothDevice Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<BluetoothDevice>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
