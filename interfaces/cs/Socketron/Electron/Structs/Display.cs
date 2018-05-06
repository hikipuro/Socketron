namespace Socketron.Electron {
	/// <summary>
	/// The Display object represents a physical display connected to the system.
	/// A fake Display may exist on a headless system,
	/// or a Display may correspond to a remote, virtual display.
	/// </summary>
	public class Display {
		/// <summary>
		/// Unique identifier associated with the display.
		/// </summary>
		public long? id;
		/// <summary>
		/// Can be 0, 90, 180, 270, represents screen rotation in clock-wise degrees.
		/// </summary>
		public double? rotation;
		/// <summary>
		/// Output device's pixel scale factor.
		/// </summary>
		public double? scaleFactor;
		/// <summary>
		/// Can be available, unavailable, unknown.
		/// </summary>
		public string touchSupport;
		/// <summary>
		/// Rectangle.
		/// </summary>
		public Rectangle bounds;
		/// <summary>
		/// Size.
		/// </summary>
		public Size size;
		/// <summary>
		/// Rectangle.
		/// </summary>
		public Rectangle workArea;
		/// <summary>
		/// Size.
		/// </summary>
		public Size workAreaSize;

		public static Display FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new Display() {
				id = json.Int64("id"),
				rotation = json.Double("rotation"),
				scaleFactor = json.Double("scaleFactor"),
				touchSupport = json.String("touchSupport"),
				bounds = Rectangle.FromObject(json["bounds"]),
				size = Size.FromObject(json["size"]),
				workArea = Rectangle.FromObject(json["workArea"]),
				workAreaSize = Size.FromObject(json["workAreaSize"])
			};
		}

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static Display Parse(string text) {
			return JSON.Parse<Display>(text);
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

