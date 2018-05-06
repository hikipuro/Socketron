using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Create tray, dock, and application icons using PNG or JPG files.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class NativeImage : NodeModule, IDisposable {
		public const string Name = "NativeImage";
		public static NativeImageClass _Class;

		/// <summary>
		/// NativeImage converter options.
		/// </summary>
		public class Options {
			public double scaleFactor;

			public static Options Parse(string text) {
				return JSON.Parse<Options>(text);
			}

			/// <summary>
			/// Create JSON text.
			/// </summary>
			/// <returns></returns>
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
		
		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public NativeImage(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		public static NativeImage createEmpty() {
			return _Class.createEmpty();
		}

		public static NativeImage createFromPath(string path) {
			return _Class.createFromPath(path);
		}

		public static NativeImage createFromBuffer(LocalBuffer buffer) {
			return _Class.createFromBuffer(buffer);
		}

		public static NativeImage createFromDataURL(string dataURL) {
			return _Class.createFromDataURL(dataURL);
		}

		public static NativeImage createFromNamedImage(string imageName) {
			return _Class.createFromNamedImage(imageName);
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
				Script.GetObject(_id),
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
				Script.GetObject(_id),
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
				Script.GetObject(_id),
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
				Script.GetObject(_id)
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
				Script.GetObject(_id),
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
				Script.GetObject(_id)
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
				Script.GetObject(_id)
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
				Script.GetObject(_id)
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
				Script.GetObject(_id),
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
				Script.GetObject(_id)
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
					"return {2};"
				),
				Script.GetObject(_id),
				rect.Stringify(),
				Script.AddObject("image")
			);
			int result = _ExecuteBlocking<int>(script);
			return new NativeImage(_client, result);
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
					"return {2};"
				),
				Script.GetObject(_id),
				options.Stringify(),
				Script.AddObject("image")
			);
			int result = _ExecuteBlocking<int>(script);
			return new NativeImage(_client, result);
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
				Script.GetObject(_id)
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
				"{0}.addRepresentation({1});",
				Script.GetObject(_id),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}
	}
}
