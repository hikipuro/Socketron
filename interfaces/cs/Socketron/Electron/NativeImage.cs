using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Create tray, dock, and application icons using PNG or JPG files.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class NativeImage : NodeBase, IDisposable {
		public const string Name = "NativeImage";

		public class Options {
			public double scaleFactor;

			public string Stringify() {
				return JSON.Stringify(this);
			}
		}

		public class DPISuffixes {
			public const string x1 = "@1x";
			public const string x1_25 = "@1.25x";
			public const string x1_33 = "@1.33x";
			public const string x1_4 = "@1.4x";
			public const string x1_5 = "@1.5x";
			public const string x1_8 = "@1.8x";
			public const string x2 = "@2x";
			public const string x2_5 = "@2.5x";
			public const string x3 = "@3x";
			public const string x4 = "@4x";
			public const string x5 = "@5x";
		}

		public NativeImage(Socketron socketron) {
			_socketron = socketron;
		}

		public NativeImage(Socketron socketron, int id) {
			_socketron = socketron;
			this.id = id;
		}

		public void Dispose() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"this._removeObjectReference({0});"
				),
				id
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Buffer - A Buffer that contains the image's PNG encoded data.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public LocalBuffer toPNG(Options options = null) {
			string option = string.Empty;
			if (options != null) {
				option = options.Stringify();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.toPNG({1});"
				),
				Script.GetObject(id),
				option
			);
			object result = _ExecuteBlocking<object>(script);
			return LocalBuffer.FromObject(result);
		}

		/// <summary>
		/// Returns Buffer - A Buffer that contains the image's JPEG encoded data.
		/// </summary>
		/// <param name="quality"></param>
		/// <returns></returns>
		public LocalBuffer toJPEG(int quality) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.toJPEG({1});"
				),
				Script.GetObject(id),
				quality
			);
			object result = _ExecuteBlocking<object>(script);
			return LocalBuffer.FromObject(result);
		}

		/// <summary>
		/// Returns Buffer - A Buffer that contains a copy of the image's raw bitmap pixel data.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public LocalBuffer toBitmap(Options options = null) {
			string option = string.Empty;
			if (options != null) {
				option = options.Stringify();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.toBitmap({1});"
				),
				Script.GetObject(id),
				option
			);
			object result = _ExecuteBlocking<object>(script);
			return LocalBuffer.FromObject(result);
		}

		/// <summary>
		/// Returns String - The data URL of the image.
		/// </summary>
		/// <returns></returns>
		public string toDataURL() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.toDataURL();"
				),
				Script.GetObject(id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns Buffer - A Buffer that contains the image's raw bitmap pixel data.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public LocalBuffer getBitmap(Options options) {
			string option = string.Empty;
			if (options != null) {
				option = options.Stringify();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.getBitmap({1});"
				),
				Script.GetObject(id),
				option
			);
			object result = _ExecuteBlocking<object>(script);
			return LocalBuffer.FromObject(result);
		}

		/// <summary>
		/// *macOS*
		/// Returns Buffer - A Buffer that stores C pointer to underlying native handle of the image.
		/// On macOS, a pointer to NSImage instance would be returned.
		/// </summary>
		/// <returns></returns>
		public LocalBuffer getNativeHandle() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.getNativeHandle();"
				),
				Script.GetObject(id)
			);
			object result = _ExecuteBlocking<object>(script);
			return LocalBuffer.FromObject(result);
		}

		/// <summary>
		/// Returns Boolean - Whether the image is empty.
		/// </summary>
		/// <returns></returns>
		public bool isEmpty() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.isEmpty();"
				),
				Script.GetObject(id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Size.
		/// </summary>
		/// <returns></returns>
		public Size getSize() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.getSize();"
				),
				Script.GetObject(id)
			);
			object result = _ExecuteBlocking<object>(script);
			return Size.FromObject(result);
		}

		/// <summary>
		/// Marks the image as a template image.
		/// </summary>
		/// <param name="option"></param>
		public void setTemplateImage(bool option) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.setTemplateImage({1});"
				),
				Script.GetObject(id),
				option.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the image is a template image.
		/// </summary>
		/// <returns></returns>
		public bool isTemplateImage() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.isTemplateImage();"
				),
				Script.GetObject(id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Returns NativeImage - The cropped image.
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		public NativeImage crop(Rectangle rect) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"if (image == null) {{",
						"return null;",
					"}}",
					"image = image.crop({1});",
					"return this._addObjectReference(image);"
				),
				Script.GetObject(id),
				rect.Stringify()
			);
			int result = _ExecuteBlocking<int>(script);
			return new NativeImage(_socketron, result);
		}

		/// <summary>
		/// Returns NativeImage - The resized image.
		/// <para>
		/// If only the height or the width are specified then the current aspect ratio
		/// will be preserved in the resized image.
		/// </para>
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public NativeImage resize(JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"if (image == null) {{",
						"return null;",
					"}}",
					"image = image.resize({1});",
					"return this._addObjectReference(image);"
				),
				Script.GetObject(id),
				options.Stringify()
			);
			int result = _ExecuteBlocking<int>(script);
			return new NativeImage(_socketron, result);
		}

		/// <summary>
		/// Returns Float - The image's aspect ratio.
		/// </summary>
		/// <returns></returns>
		public double getAspectRatio() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.getAspectRatio();"
				),
				Script.GetObject(id)
			);
			return _ExecuteBlocking<double>(script);
		}

		/// <summary>
		/// Add an image representation for a specific scale factor.
		/// This can be used to explicitly add different scale factor representations to an image.
		/// This can be called on empty images.
		/// </summary>
		/// <param name="options"></param>
		public void addRepresentation(JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"if (image == null) {{",
						"return;",
					"}}",
					"image.addRepresentation({1});"
				),
				Script.GetObject(id),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}
	}
}
