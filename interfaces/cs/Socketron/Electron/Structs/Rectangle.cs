namespace Socketron {
	public class Rectangle {
		/// <summary>
		/// The x coordinate of the origin of the rectangle (must be an integer).
		/// </summary>
		public int x;
		/// <summary>
		/// The y coordinate of the origin of the rectangle (must be an integer).
		/// </summary>
		public int y;
		/// <summary>
		/// The width of the rectangle (must be an integer).
		/// </summary>
		public int width;
		/// <summary>
		/// The height of the rectangle (must be an integer).
		/// </summary>
		public int height;

		public static Rectangle FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new Rectangle() {
				x = json.Int32("x"),
				y = json.Int32("y"),
				width = json.Int32("width"),
				height = json.Int32("height")
			};
		}

		public static Rectangle Parse(string text) {
			return JSON.Parse<Rectangle>(text);
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
