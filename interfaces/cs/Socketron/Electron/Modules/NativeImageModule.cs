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
		public NativeImageModule() {
		}

		/// <summary>
		/// Creates an empty NativeImage instance.
		/// </summary>
		/// <returns></returns>
		public NativeImage createEmpty() {
			return API.ApplyAndGetObject<NativeImage>("createEmpty");
		}

		/// <summary>
		/// Creates a new NativeImage instance from a file located at path.
		/// This method returns an empty image if the path does not exist,
		/// cannot be read, or is not a valid image.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public NativeImage createFromPath(string path) {
			return API.ApplyAndGetObject<NativeImage>("createFromPath", path);
		}

		/// <summary>
		/// Creates a new NativeImage instance from buffer.
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public NativeImage createFromBuffer(Buffer buffer) {
			return API.ApplyAndGetObject<NativeImage>("createFromBuffer", buffer);
		}

		/// <summary>
		/// Creates a new NativeImage instance from dataURL.
		/// </summary>
		/// <param name="dataURL"></param>
		/// <returns></returns>
		public NativeImage createFromDataURL(string dataURL) {
			return API.ApplyAndGetObject<NativeImage>("createFromDataURL", dataURL);
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
			return API.ApplyAndGetObject<NativeImage>("createFromNamedImage", imageName);
		}
	}
}
