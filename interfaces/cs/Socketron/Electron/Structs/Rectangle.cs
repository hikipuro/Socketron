namespace Socketron.Electron {
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

		public Rectangle() {
		}

		public Rectangle(int x, int y, int width, int height) {
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public Rectangle(int width, int height) {
			this.width = width;
			this.height = height;
		}

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

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
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
