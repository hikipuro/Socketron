namespace Socketron {
	public class Size {
		public int width;
		public int height;

		public static Size FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new Size() {
				width = json.Int32("width"),
				height = json.Int32("height")
			};
		}

		public static Size Parse(string text) {
			return JSON.Parse<Size>(text);
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
