namespace Socketron {
	public class PrinterInfo {
		public string name;
		public string description;
		public int status;
		public bool? isDefault;

		public static PrinterInfo Parse(string text) {
			return JSON.Parse<PrinterInfo>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
