using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Add icons and context menus to the system's notification area.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TrayModule : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public TrayModule(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// Creates a new tray icon associated with the image.
		/// </summary>
		/// <param name="image"></param>
		public Tray Create(NativeImage image) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var Tray = {0};",
					"var tray = new Tray({1});",
					"return {2};"
				),
				Script.GetObject(_id),
				Script.GetObject(image._id),
				Script.AddObject("tray")
			);
			int result = _ExecuteBlocking<int>(script);
			return new Tray(_client, result);
		}

		/// <summary>
		/// Creates a new tray icon associated with the image.
		/// </summary>
		/// <param name="image"></param>
		public Tray Create(string image) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var Tray = {0};",
					"var tray = new Tray({1});",
					"return {2};"
				),
				Script.GetObject(_id),
				image.Escape(),
				Script.AddObject("tray")
			);
			int result = _ExecuteBlocking<int>(script);
			return new Tray(_client, result);
		}
	}
}
