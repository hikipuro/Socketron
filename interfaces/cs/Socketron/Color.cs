namespace Socketron {
	/// <summary>
	/// JavaScript color.
	/// </summary>
	public class Color {
		public static string RGB(byte r, byte g, byte b) {
			return string.Format(
				"#{0:X2}{1:X2}{2:X2}",
				r, g, b
			);
		}

		public static string RGB(int r, int g, int b) {
			return RGB((byte)r, (byte)g, (byte)b);
		}

		public static string ARGB(byte a, byte r, byte g, byte b) {
			return string.Format(
				"#{0:X2}{1:X2}{2:X2}{3:X2}",
				a, r, g, b
			);
		}

		public static string ARGB(int a, int r, int g, int b) {
			return ARGB((byte)a, (byte)r, (byte)g, (byte)b);
		}
	}
}
