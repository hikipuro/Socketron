namespace Socketron.Electron {
	/// <summary>
	/// Note: If a JumpListCategory object has neither the type
	/// nor the name property set then its type is assumed to be tasks.
	/// If the name property is set but the type property is omitted
	/// then the type is assumed to be custom.
	/// </summary>
	public class JumpListCategory {
		/// <summary>
		/// (optional) One of the following:
		/// tasks - Items in this category will be placed into the standard Tasks category.
		///			There can be only one such category, and it will always be displayed
		///			at the bottom of the Jump List.
		/// frequent - Displays a list of files frequently opened by the app,
		///			the name of the category and its items are set by Windows.
		/// recent - Displays a list of files recently opened by the app,
		///			the name of the category and its items are set
		///			by Windows.Items may be added to this category indirectly
		///			using app.addRecentDocument(path).
		/// custom - Displays tasks or file links, name must be set by the app.
		/// </summary>
		public string type;
		/// <summary>
		/// (optional) Must be set if type is custom, otherwise it should be omitted.
		/// </summary>
		public string name;
		/// <summary>
		/// (optional) Array of JumpListItem objects if type is tasks or custom,
		/// otherwise it should be omitted.
		/// </summary>
		public JumpListItem[] items;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static JumpListCategory Parse(string text) {
			return JSON.Parse<JumpListCategory>(text);
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

