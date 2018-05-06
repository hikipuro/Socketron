namespace Socketron.Electron {
	public class PrinterInfo {
		public string name;
		public string description;
		public int status;
		public bool? isDefault;

		public static PrinterInfo FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new PrinterInfo() {
				name = json.String("name"),
				description = json.String("description"),
				status = json.Int32("status"),
				isDefault = json.Bool("isDefault")
			};
		}

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static PrinterInfo Parse(string text) {
			return JSON.Parse<PrinterInfo>(text);
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
