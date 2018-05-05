using System;

namespace Socketron {
	/// <summary>
	/// JavaScript color.
	/// </summary>
	public class Color {
		public byte A;
		public byte R;
		public byte G;
		public byte B;

		public static Color FromARGB(string color) {
			byte a = Convert.ToByte(color.Substring(0, 2), 16);
			byte r = Convert.ToByte(color.Substring(2, 2), 16);
			byte g = Convert.ToByte(color.Substring(4, 2), 16);
			byte b = Convert.ToByte(color.Substring(6, 2), 16);
			return new Color(a, r, g, b);
		}

		public static Color FromRGB(string color) {
			byte r = Convert.ToByte(color.Substring(0, 2), 16);
			byte g = Convert.ToByte(color.Substring(2, 2), 16);
			byte b = Convert.ToByte(color.Substring(4, 2), 16);
			return new Color(r, g, b);
		}

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

		public Color(byte a, byte r, byte g, byte b) {
			A = a;
			R = r;
			G = g;
			B = b;
		}

		public Color(byte r = 0, byte g = 0, byte b = 0) {
			R = r;
			G = g;
			B = b;
		}

		public string ToRGB() {
			return RGB(R, G, B);
		}

		public string ToARGB() {
			return ARGB(A, R, G, B);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
