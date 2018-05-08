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

		/// <summary>
		/// Create color object from a JavaScript color string.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static Color FromARGB(string color) {
			byte a = Convert.ToByte(color.Substring(0, 2), 16);
			byte r = Convert.ToByte(color.Substring(2, 2), 16);
			byte g = Convert.ToByte(color.Substring(4, 2), 16);
			byte b = Convert.ToByte(color.Substring(6, 2), 16);
			return new Color(a, r, g, b);
		}

		/// <summary>
		/// Create color object from a JavaScript color string.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static Color FromRGB(string color) {
			byte r = Convert.ToByte(color.Substring(0, 2), 16);
			byte g = Convert.ToByte(color.Substring(2, 2), 16);
			byte b = Convert.ToByte(color.Substring(4, 2), 16);
			return new Color(r, g, b);
		}

		/// <summary>
		/// Create color object from rgb values.
		/// </summary>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static string RGB(byte r, byte g, byte b) {
			return string.Format(
				"#{0:X2}{1:X2}{2:X2}",
				r, g, b
			);
		}

		/// <summary>
		/// Create color object from rgb values.
		/// </summary>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static string RGB(int r, int g, int b) {
			return RGB((byte)r, (byte)g, (byte)b);
		}

		/// <summary>
		/// Create color object from argb values.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static string ARGB(byte a, byte r, byte g, byte b) {
			return string.Format(
				"#{0:X2}{1:X2}{2:X2}{3:X2}",
				a, r, g, b
			);
		}

		/// <summary>
		/// Create color object from argb values.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static string ARGB(int a, int r, int g, int b) {
			return ARGB((byte)a, (byte)r, (byte)g, (byte)b);
		}

		/// <summary>
		/// Create color object from argb values.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		public Color(byte a, byte r, byte g, byte b) {
			A = a;
			R = r;
			G = g;
			B = b;
		}

		/// <summary>
		/// Create color object from rgb values.
		/// </summary>
		/// <param name="r"></param>
		/// <param name="g"></param>
		/// <param name="b"></param>
		public Color(byte r = 0, byte g = 0, byte b = 0) {
			R = r;
			G = g;
			B = b;
		}

		/// <summary>
		/// Convert to hex color string.
		/// not include "#" prefix.
		/// </summary>
		/// <returns></returns>
		public string ToRGB() {
			return RGB(R, G, B);
		}

		/// <summary>
		/// Convert to hex color string.
		/// not include "#" prefix.
		/// </summary>
		/// <returns></returns>
		public string ToARGB() {
			return ARGB(A, R, G, B);
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
