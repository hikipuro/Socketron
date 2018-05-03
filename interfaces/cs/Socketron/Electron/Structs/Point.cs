namespace Socketron {
	public class Point {
		public int x;
		public int y;

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
