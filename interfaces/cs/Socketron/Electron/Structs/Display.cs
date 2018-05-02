namespace Socketron {
	public class Display {
		public long? id;
		public double? rotation;
		public double? scaleFactor;
		public string touchSupport;
		public Rectangle bounds;
		public Size size;
		public Rectangle workArea;
		public Size workAreaSize;

		public static Display Parse(string text) {
			return JSON.Parse<Display>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

