namespace Socketron.Electron {
	/// <summary>
	/// ClientRequest.getHeader() return value.
	/// </summary>
	public class Header {
		/// <summary>
		/// Specify an extra header name.
		/// </summary>
		public string name;

		public static Header FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new Header() {
				name = json.String("name")
			};
		}
	}
}
