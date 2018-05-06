namespace Socketron.Electron {
	public class BluetoothDevice {
		public string deviceName;
		public string deviceId;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
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
