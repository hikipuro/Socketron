﻿namespace Socketron {
	/// <summary>
	/// Create tray, dock, and application icons using PNG or JPG files.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	public class NativeImageClass : ElectronBase {
		public NativeImageClass(Socketron socketron) {
			_socketron = socketron;
		}

		/// <summary>
		/// Creates an empty NativeImage instance.
		/// </summary>
		/// <returns></returns>
		public NativeImage CreateEmpty() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = electron.nativeImage.createEmpty();",
					"return this._addObjectReference(image);"
				)
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			NativeImage image = new NativeImage(_socketron) {
				ID = result
			};
			return image;
		}

		/// <summary>
		/// Creates a new NativeImage instance from a file located at path.
		/// This method returns an empty image if the path does not exist,
		/// cannot be read, or is not a valid image.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public NativeImage CreateFromPath(string path) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = electron.nativeImage.createFromPath({0});",
					"return this._addObjectReference(image);"
				),
				path.Escape()
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			NativeImage image = new NativeImage(_socketron) {
				ID = result
			};
			return image;
		}

		/// <summary>
		/// Creates a new NativeImage instance from buffer.
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public NativeImage CreateFromBuffer(Buffer buffer) {
			// TODO: add options
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = electron.nativeImage.createFromBuffer({0});",
					"return this._addObjectReference(image);"
				),
				buffer.Stringify()
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			NativeImage image = new NativeImage(_socketron) {
				ID = result
			};
			return image;
		}

		/// <summary>
		/// Creates a new NativeImage instance from dataURL.
		/// </summary>
		/// <param name="dataURL"></param>
		/// <returns></returns>
		public NativeImage CreateFromDataURL(string dataURL) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = electron.nativeImage.createFromDataURL({0});",
					"return this._addObjectReference(image);"
				),
				dataURL.Escape()
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			NativeImage image = new NativeImage(_socketron) {
				ID = result
			};
			return image;
		}

		/// <summary>
		/// *macOS* Creates a new NativeImage instance from the NSImage that maps to the given image name.
		/// See NSImageName for a list of possible values.
		/// </summary>
		/// <param name="imageName"></param>
		/// <returns></returns>
		public NativeImage CreateFromNamedImage(string imageName) {
			// TODO: add hslShift option
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = electron.nativeImage.createFromNamedImage({0});",
					"return this._addObjectReference(image);"
				),
				imageName.Escape()
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			NativeImage image = new NativeImage(_socketron) {
				ID = result
			};
			return image;
		}
	}
}
