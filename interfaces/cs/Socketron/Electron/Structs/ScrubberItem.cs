namespace Socketron.Electron {
	public class ScrubberItem {
		/// <summary>
		/// (optional) The text to appear in this item.
		/// </summary>
		public string label;
		/// <summary>
		/// (optional) The image to appear in this item.
		/// </summary>
		public NativeImage icon;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static ScrubberItem Parse(string text) {
			return JSON.Parse<ScrubberItem>(text);
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
