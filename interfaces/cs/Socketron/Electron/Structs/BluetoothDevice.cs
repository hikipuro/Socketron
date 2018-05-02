namespace Socketron {
	public class BluetoothDevice {
		public string deviceName;
		public string deviceId;

		public static BluetoothDevice Parse(string text) {
			return JSON.Parse<BluetoothDevice>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
