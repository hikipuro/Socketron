using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Script.Serialization;

namespace Socketron {
	/// <summary>
	/// Create tray, dock, and application icons using PNG or JPG files.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	public class NativeImage : IDisposable {
		public const string Name = "NativeImage";
		public int ID;
		protected Socketron _socketron;

		public class PNGOptions {
			public double scaleFactor;

			public string Stringify() {
				var serializer = new JavaScriptSerializer();
				serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
				return serializer.Serialize(this);
			}
		}

		protected NativeImage() {
		}

		public static NativeImage CreateEmpty(Socketron socketron) {
			string[] script = new[] {
				"var image = electron.nativeImage.createEmpty();",
				"return this._addObjectReference(image);"
			};
			int result = _ExecuteJavaScriptBlocking<int>(socketron, script);
			NativeImage image = new NativeImage() {
				_socketron = socketron,
				ID = result
			};
			return image;
		}

		public static NativeImage CreateFromPath(Socketron socketron, string path) {
			string[] script = new[] {
				"var image = electron.nativeImage.createFromPath(" + path.Escape() + ");",
				"return this._addObjectReference(image);"
			};
			int result = _ExecuteJavaScriptBlocking<int>(socketron, script);
			NativeImage image = new NativeImage() {
				_socketron = socketron,
				ID = result
			};
			return image;
		}

		public static NativeImage CreateFromBuffer(Socketron socketron, Buffer buffer) {
			string[] script = new[] {
				"var image = electron.nativeImage.createFromBuffer(" + buffer.Stringify() + ");",
				"return this._addObjectReference(image);"
			};
			int result = _ExecuteJavaScriptBlocking<int>(socketron, script);
			NativeImage image = new NativeImage() {
				_socketron = socketron,
				ID = result
			};
			return image;
		}

		public static NativeImage CreateFromDataURL(Socketron socketron, string dataURL) {
			string[] script = new[] {
				"var image = electron.nativeImage.createFromDataURL(" + dataURL.Escape() + ");",
				"return this._addObjectReference(image);"
			};
			int result = _ExecuteJavaScriptBlocking<int>(socketron, script);
			NativeImage image = new NativeImage() {
				_socketron = socketron,
				ID = result
			};
			return image;
		}

		public static NativeImage CreateFromNamedImage(Socketron socketron, string imageName) {
			string[] script = new[] {
				"var image = electron.nativeImage.createFromNamedImage(" + imageName.Escape() + ");",
				"return this._addObjectReference(image);"
			};
			int result = _ExecuteJavaScriptBlocking<int>(socketron, script);
			NativeImage image = new NativeImage() {
				_socketron = socketron,
				ID = result
			};
			return image;
		}

		public void Dispose() {
			string[] script = new[] {
				"this._removeObjectReference(" + ID + ");"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public Buffer ToPNG(PNGOptions options = null) {
			List<string> script = new List<string> {
				"var image = this._objRefs[" + ID + "];",
				"if (image == null) {",
					"return;",
				"}"
			};
			if (options == null) {
				script.Add("return image.toPNG();");
			} else {
				script.Add("return image.toPNG(" + options.Stringify() + ");");
			}
			object result = _ExecuteJavaScriptBlocking<object>(_socketron, script.ToArray());
			JsonObject json = new JsonObject(result);
			return Buffer.FromJson(json);
		}

		public Buffer ToJPEG(int quality) {
			string[] script = new[] {
				"var image = this._objRefs[" + ID + "];",
				"if (image == null) {",
					"return;",
				"}",
				"return image.toJPEG(" + quality + ");"
			};
			object result = _ExecuteJavaScriptBlocking<object>(_socketron, script);
			JsonObject json = new JsonObject(result);
			return Buffer.FromJson(json);
		}

		public Buffer ToBitmap(int quality) {
			string[] script = new[] {
				"var image = this._objRefs[" + ID + "];",
				"if (image == null) {",
					"return;",
				"}",
				"return image.toBitmap(" + quality + ");"
			};
			object result = _ExecuteJavaScriptBlocking<object>(_socketron, script);
			JsonObject json = new JsonObject(result);
			return Buffer.FromJson(json);
		}

		public string ToDataURL() {
			string[] script = new[] {
				"var image = this._objRefs[" + ID + "];",
				"if (image == null) {",
					"return;",
				"}",
				"return image.toDataURL();"
			};
			return _ExecuteJavaScriptBlocking<string>(_socketron, script);
		}

		/*
		public Buffer GetBitmap(int quality) {
			string[] script = new[] {
				"var image = this._objRefs[" + _ID + "];",
				"if (image == null) {",
					"return;",
				"}",
				"return image.getBitmap(" + quality + ");"
			};
			object result = _ExecuteJavaScriptBlocking<object>(_socketron, script);
			JsonObject json = new JsonObject(result);
			return Buffer.FromJson(json);
		}
		//*/

		public Buffer GetNativeHandle() {
			string[] script = new[] {
				"var image = this._objRefs[" + ID + "];",
				"if (image == null) {",
					"return;",
				"}",
				"return image.getNativeHandle();"
			};
			object result = _ExecuteJavaScriptBlocking<object>(_socketron, script);
			JsonObject json = new JsonObject(result);
			return Buffer.FromJson(json);
		}

		public bool IsEmpty() {
			string[] script = new[] {
				"var image = this._objRefs[" + ID + "];",
				"if (image == null) {",
					"return;",
				"}",
				"return image.isEmpty();"
			};
			return _ExecuteJavaScriptBlocking<bool>(_socketron, script);
		}

		public Size GetSize() {
			string[] script = new[] {
				"var image = this._objRefs[" + ID + "];",
				"if (image == null) {",
					"return;",
				"}",
				"return image.getSize();"
			};
			object result = _ExecuteJavaScriptBlocking<object>(_socketron, script);
			JsonObject json = new JsonObject(result);
			Size size = new Size();
			size.width = (int)json["width"];
			size.height = (int)json["height"];
			return size;
		}

		public void SetTemplateImage(bool option) {
			string[] script = new[] {
				"var image = this._objRefs[" + ID + "];",
				"if (image == null) {",
					"return;",
				"}",
				"return image.setTemplateImage(" + option.Escape() + ");"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public bool IsTemplateImage() {
			string[] script = new[] {
				"var image = this._objRefs[" + ID + "];",
				"if (image == null) {",
					"return;",
				"}",
				"return image.isTemplateImage();"
			};
			return _ExecuteJavaScriptBlocking<bool>(_socketron, script);
		}

		/*
		public NativeImage Crop(Rectangle rect) {
			string[] script = new[] {
				"var image = this._objRefs[" + _ID + "];",
				"if (image == null) {",
					"return;",
				"}",
				"return image.crop(" + rect.Stringify() + ");"
			};
			return _ExecuteJavaScriptBlocking<object>(_socketron, script);
		}
		//*/

		/*
		public NativeImage Resize(Rectangle rect) {
			string[] script = new[] {
				"var image = this._objRefs[" + _ID + "];",
				"if (image == null) {",
					"return;",
				"}",
				"return image.resize(" + rect.Stringify() + ");"
			};
			return _ExecuteJavaScriptBlocking<object>(_socketron, script);
		}
		//*/

		public double GetAspectRatio() {
			string[] script = new[] {
				"var image = this._objRefs[" + ID + "];",
				"if (image == null) {",
					"return;",
				"}",
				"return image.getAspectRatio();"
			};
			return _ExecuteJavaScriptBlocking<double>(_socketron, script);
		}

		/*
		public void AddRepresentation(double scaleFactor, int width, int height, Buffer buffer, string dataURL) {
			string[] script = new[] {
				"var image = this._objRefs[" + _ID + "];",
				"if (image == null) {",
					"return;",
				"}",
				"return image.addRepresentation(" + option.Escape() + ");"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}
		//*/

		protected static void _ExecuteJavaScript(Socketron socketron, string[] script, Callback success, Callback error) {
			socketron.Main.ExecuteJavaScript(script, success, error);
		}

		protected static T _ExecuteJavaScriptBlocking<T>(Socketron socketron, string[] script) {
			bool done = false;
			T value = default(T);

			_ExecuteJavaScript(socketron, script, (result) => {
				if (result == null) {
					done = true;
					return;
				}
				if (typeof(T) == typeof(double)) {
					//Console.WriteLine(result.GetType());
					if (result.GetType() == typeof(int)) {
						result = (double)(int)result;
					} else if (result.GetType() == typeof(Decimal)) {
						result = (double)(Decimal)result;
					}
				}
				value = (T)result;
				done = true;
			}, (result) => {
				Console.Error.WriteLine("error: NativeImage._ExecuteJavaScriptBlocking");
				throw new InvalidOperationException(result as string);
				//done = true;
			});

			while (!done) {
				Thread.Sleep(TimeSpan.FromTicks(1));
			}
			return value;
		}
	}
}
