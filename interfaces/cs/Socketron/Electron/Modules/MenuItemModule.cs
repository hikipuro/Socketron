using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Add items to native application menus and context menus.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class MenuItemModule : NodeModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public MenuItemModule(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// Create a new MenuItem instance.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public MenuItem Create(MenuItem.Options options) {
			if (options == null) {
				options = new MenuItem.Options();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var item = new {0}({1});",
					"return {2};"
				),
				Script.GetObject(_id),
				options.Stringify(),
				Script.AddObject("item")
			);
			int result = _ExecuteBlocking<int>(script);
			return new MenuItem(_client, result);
		}

		/// <summary>
		/// Create a new MenuItem instance.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public MenuItem Create(string options) {
			return Create(MenuItem.Options.Parse(options));
		}
	}
}
