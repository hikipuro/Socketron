using System;

namespace Socketron {
	/// <summary>
	/// Create tray, dock, and application icons using PNG or JPG files.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	public class NativeImage : ElectronBase, IDisposable {
		public const string Name = "NativeImage";
		public int ID;

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

		public void Dispose() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"this._removeObjectReference({0});"
				),
				ID
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Buffer - A Buffer that contains the image's PNG encoded data.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public Buffer ToPNG(Options options = null) {
			string option = string.Empty;
			if (options != null) {
				option = options.Stringify();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = this._objRefs[{0}];",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.toPNG({1});"
				),
				ID,
				option
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			JsonObject json = new JsonObject(result);
			return Buffer.FromJson(json);
		}

		/// <summary>
		/// Returns Buffer - A Buffer that contains the image's JPEG encoded data.
		/// </summary>
		/// <param name="quality"></param>
		/// <returns></returns>
		public Buffer ToJPEG(int quality) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = this._objRefs[{0}];",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.toJPEG({1});"
				),
				ID,
				quality
			);
			object result = _ExecuteJavaScriptBlocking<object>(_socketron, script);
			JsonObject json = new JsonObject(result);
			return Buffer.FromJson(json);
		}

		/// <summary>
		/// Returns Buffer - A Buffer that contains a copy of the image's raw bitmap pixel data.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public Buffer ToBitmap(Options options = null) {
			string option = string.Empty;
			if (options != null) {
				option = options.Stringify();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = this._objRefs[{0}];",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.toBitmap({1});"
				),
				ID,
				option
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			JsonObject json = new JsonObject(result);
			return Buffer.FromJson(json);
		}

		/// <summary>
		/// Returns String - The data URL of the image.
		/// </summary>
		/// <returns></returns>
		public string ToDataURL() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = this._objRefs[{0}];",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.toDataURL();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// Returns Buffer - A Buffer that contains the image's raw bitmap pixel data.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public Buffer GetBitmap(Options options) {
			string option = string.Empty;
			if (options != null) {
				option = options.Stringify();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = this._objRefs[{0}];",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.getBitmap({1});"
				),
				ID,
				option
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			JsonObject json = new JsonObject(result);
			return Buffer.FromJson(json);
		}

		/// <summary>
		/// *macOS* Returns Buffer - A Buffer that stores C pointer to underlying
		/// native handle of the image. On macOS, a pointer to NSImage instance would be returned.
		/// </summary>
		/// <returns></returns>
		public Buffer GetNativeHandle() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = this._objRefs[{0}];",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.getNativeHandle();"
				),
				ID
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			JsonObject json = new JsonObject(result);
			return Buffer.FromJson(json);
		}

		/// <summary>
		/// Returns Boolean - Whether the image is empty.
		/// </summary>
		/// <returns></returns>
		public bool IsEmpty() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = this._objRefs[{0}];",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.isEmpty();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Size.
		/// </summary>
		/// <returns></returns>
		public Size GetSize() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = this._objRefs[{0}];",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.getSize();"
				),
				ID
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return Size.FromObject(result);
		}

		/// <summary>
		/// Marks the image as a template image.
		/// </summary>
		/// <param name="option"></param>
		public void SetTemplateImage(bool option) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = this._objRefs[{0}];",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.setTemplateImage({1});"
				),
				ID,
				option.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the image is a template image.
		/// </summary>
		/// <returns></returns>
		public bool IsTemplateImage() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = this._objRefs[{0}];",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.isTemplateImage();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Returns NativeImage - The cropped image.
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		public NativeImage Crop(Rectangle rect) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = this._objRefs[{0}];",
					"if (image == null) {{",
						"return null;",
					"}}",
					"image = image.crop({1});",
					"return this._addObjectReference(image);"
				),
				ID,
				rect.Stringify()
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			NativeImage image = new NativeImage(_socketron) {
				ID = result
			};
			return image;
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
		public NativeImage Resize(JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = this._objRefs[{0}];",
					"if (image == null) {{",
						"return null;",
					"}}",
					"image = image.resize({1});",
					"return this._addObjectReference(image);"
				),
				ID,
				options.Stringify()
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			NativeImage image = new NativeImage(_socketron) {
				ID = result
			};
			return image;
		}

		/// <summary>
		/// Returns Float - The image's aspect ratio.
		/// </summary>
		/// <returns></returns>
		public double GetAspectRatio() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = this._objRefs[{0}];",
					"if (image == null) {{",
						"return null;",
					"}}",
					"return image.getAspectRatio();"
				),
				ID
			);
			return _ExecuteJavaScriptBlocking<double>(script);
		}

		/// <summary>
		/// Add an image representation for a specific scale factor.
		/// This can be used to explicitly add different scale factor representations to an image.
		/// This can be called on empty images.
		/// </summary>
		/// <param name="options"></param>
		public void AddRepresentation(JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = this._objRefs[{0}];",
					"if (image == null) {{",
						"return;",
					"}}",
					"image.addRepresentation({1});"
				),
				ID,
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}
	}
}
