namespace Socketron.Electron {
	/// <summary>
	/// Menu.popup() options.
	/// </summary>
	public class PopupOptions {
		/// <summary>
		/// Default is the current mouse cursor position. Must be declared if y is declared.
		/// </summary>
		public double? x;
		/// <summary>
		/// Default is the current mouse cursor position. Must be declared if x is declared.
		/// </summary>
		public double? y;
		/// <summary>
		/// Set to true to have this method return immediately called,
		/// false to return after the menu has been selected or closed.
		/// Defaults to false.
		/// </summary>
		public bool? async;
		/// <summary>
		/// The index of the menu item to be positioned under the mouse cursor
		/// at the specified coordinates. Default is -1.
		/// </summary>
		public int? positioningItem;
	}
}
