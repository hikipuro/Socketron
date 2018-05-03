namespace Socketron {
	public class BluetoothDevice {
		public string deviceName;
		public string deviceId;

		public static BluetoothDevice Parse(string text) {
			return JSON.Parse<BluetoothDevice>(text);
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
