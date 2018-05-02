namespace Socketron {
	public class Point {
		public int x;
		public int y;

		public static Point Parse(string text) {
			return JSON.Parse<Point>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
