using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create tray, dock, and application icons using PNG or JPG files.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class NativeImageModule : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public NativeImageModule(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// Creates an empty NativeImage instance.
		/// </summary>
		/// <returns></returns>
		public NativeImage createEmpty() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0}.createEmpty();",
					"return {1};"
				),
				Script.GetObject(_id),
				Script.AddObject("image")
			);
			int result = SocketronClient.ExecuteBlocking<int>(script);
			return new NativeImage(_client, result);
		}

		/// <summary>
		/// Creates a new NativeImage instance from a file located at path.
		/// This method returns an empty image if the path does not exist,
		/// cannot be read, or is not a valid image.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public NativeImage createFromPath(string path) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0}.createFromPath({1});",
					"return {2};"
				),
				Script.GetObject(_id),
				path.Escape(),
				Script.AddObject("image")
			);
			int result = SocketronClient.ExecuteBlocking<int>(script);
			return new NativeImage(_client, result);
		}

		/// <summary>
		/// Creates a new NativeImage instance from buffer.
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public NativeImage createFromBuffer(Buffer buffer) {
			// TODO: add options
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0}.createFromBuffer({1});",
					"return {2};"
				),
				Script.GetObject(_id),
				Script.GetObject(buffer._id),
				Script.AddObject("image")
			);
			int result = SocketronClient.ExecuteBlocking<int>(script);
			return new NativeImage(_client, result);
		}

		/// <summary>
		/// Creates a new NativeImage instance from dataURL.
		/// </summary>
		/// <param name="dataURL"></param>
		/// <returns></returns>
		public NativeImage createFromDataURL(string dataURL) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0}.createFromDataURL({1});",
					"return {2};"
				),
				Script.GetObject(_id),
				dataURL.Escape(),
				Script.AddObject("image")
			);
			int result = SocketronClient.ExecuteBlocking<int>(script);
			return new NativeImage(_client, result);
		}

		/// <summary>
		/// *macOS*
		/// Creates a new NativeImage instance from the NSImage that maps to the given image name.
		/// See NSImageName for a list of possible values.
		/// </summary>
		/// <param name="imageName"></param>
		/// <returns></returns>
		public NativeImage createFromNamedImage(string imageName) {
			// TODO: add hslShift option
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0}.createFromNamedImage({1});",
					"return {2};"
				),
				Script.GetObject(_id),
				imageName.Escape(),
				Script.AddObject("image")
			);
			int result = SocketronClient.ExecuteBlocking<int>(script);
			return new NativeImage(_client, result);
		}
	}
}
