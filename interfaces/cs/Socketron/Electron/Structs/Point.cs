namespace Socketron {
	public class Point {
		public int x;
		public int y;

		public static Point FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new Point() {
				x = json.Int32("x"),
				y = json.Int32("y")
			};
		}

		public static Point Parse(string text) {
			return JSON.Parse<Point>(text);
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
