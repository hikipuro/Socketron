﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create tray, dock, and application icons using PNG or JPG files.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class NativeImage : JSModule, IDisposable {
		/// <summary>
		/// NativeImage converter options.
		/// </summary>
		public class Options {
			public double scaleFactor;

			/// <summary>
			/// Parse JSON text.
			/// </summary>
			/// <param name="text"></param>
			/// <returns></returns>
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
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the NativeImage constructors,
		/// please use electron.nativeImage.createXxx() method instead.
		/// </para>
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public NativeImage(SocketronClient client, int id) {
			API.client = client;
			API.id = id;
		}

		/// <summary>
		/// Returns Buffer - A Buffer that contains the image's PNG encoded data.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public Buffer toPNG(Options options = null) {
			string option = string.Empty;
			if (options != null) {
				option = options.Stringify();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = {0}.toPNG({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				option,
				Script.AddObject("buf")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new Buffer(API.client, result);
		}

		/// <summary>
		/// Returns Buffer - A Buffer that contains the image's JPEG encoded data.
		/// </summary>
		/// <param name="quality"></param>
		/// <returns></returns>
		public Buffer toJPEG(int quality) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = {0}.toJPEG({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				quality,
				Script.AddObject("buf")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new Buffer(API.client, result);
		}

		/// <summary>
		/// Returns Buffer - A Buffer that contains a copy of the image's raw bitmap pixel data.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public Buffer toBitmap(Options options = null) {
			string option = string.Empty;
			if (options != null) {
				option = options.Stringify();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = {0}.toBitmap({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				option,
				Script.AddObject("buf")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new Buffer(API.client, result);
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
				Script.GetObject(API.id)
			);
			return API._ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns Buffer - A Buffer that contains the image's raw bitmap pixel data.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public Buffer getBitmap(Options options) {
			string option = string.Empty;
			if (options != null) {
				option = options.Stringify();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = {0}.getBitmap({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				option,
				Script.AddObject("buf")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new Buffer(API.client, result);
		}

		/// <summary>
		/// *macOS*
		/// Returns Buffer - A Buffer that stores C pointer to underlying native handle of the image.
		/// On macOS, a pointer to NSImage instance would be returned.
		/// </summary>
		/// <returns></returns>
		public Buffer getNativeHandle() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var buf = {0}.getNativeHandle();",
					"return {1};"
				),
				Script.GetObject(API.id),
				Script.AddObject("buf")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new Buffer(API.client, result);
		}

		/// <summary>
		/// Returns Boolean - Whether the image is empty.
		/// </summary>
		/// <returns></returns>
		public bool isEmpty() {
			return API.Apply<bool>("isEmpty");
		}

		/// <summary>
		/// Returns Size.
		/// </summary>
		/// <returns></returns>
		public Size getSize() {
			object result = API.Apply<object>("getSize");
			return Size.FromObject(result);
		}

		/// <summary>
		/// Marks the image as a template image.
		/// </summary>
		/// <param name="option"></param>
		public void setTemplateImage(bool option) {
			API.Apply("setTemplateImage", option);
		}

		/// <summary>
		/// Returns Boolean - Whether the image is a template image.
		/// </summary>
		/// <returns></returns>
		public bool isTemplateImage() {
			return API.Apply<bool>("isTemplateImage");
		}

		/// <summary>
		/// Returns NativeImage - The cropped image.
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		public NativeImage crop(Rectangle rect) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0}.crop({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				rect.Stringify(),
				Script.AddObject("image")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new NativeImage(API.client, result);
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
					"var image = {0}.resize({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				options.Stringify(),
				Script.AddObject("image")
			);
			int result = API._ExecuteBlocking<int>(script);
			return new NativeImage(API.client, result);
		}

		/// <summary>
		/// Returns Float - The image's aspect ratio.
		/// </summary>
		/// <returns></returns>
		public double getAspectRatio() {
			return API.Apply<double>("getAspectRatio");
		}

		/// <summary>
		/// Add an image representation for a specific scale factor.
		/// This can be used to explicitly add different scale factor representations to an image.
		/// This can be called on empty images.
		/// </summary>
		/// <param name="options"></param>
		public void addRepresentation(JsonObject options) {
			API.Apply("addRepresentation", options);
		}
	}
}
