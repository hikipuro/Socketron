using System.Collections.Generic;

namespace Socketron {
	/// <summary>
	/// Perform copy and paste operations on the system clipboard.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	public class ClipboardClass : ElectronBase {
		public const string Name = "Clipboard";

		public ClipboardClass(Socketron socketron) {
			_socketron = socketron;
		}

		/// <summary>
		/// Returns String - The content in the clipboard as plain text.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string ReadText(string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = type.Escape();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.clipboard.readText({0});"
				),
				option
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// Writes the text into the clipboard as plain text.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="type"></param>
		public void WriteText(string text, string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = ScriptBuilder.Params(
					text.Escape(),
					type.Escape()
				);
			} else {
				option = text.Escape();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.clipboard.writeText({0});"
				),
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - The content in the clipboard as markup.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string ReadHTML(string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = type.Escape();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.clipboard.readHTML({0});"
				),
				option
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// Writes markup to the clipboard.
		/// </summary>
		/// <param name="markup"></param>
		/// <param name="type"></param>
		public void WriteHTML(string markup, string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = ScriptBuilder.Params(
					markup.Escape(),
					type.Escape()
				);
			} else {
				option = markup.Escape();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.clipboard.writeHTML({0});"
				),
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns NativeImage - The image content in the clipboard.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public NativeImage ReadImage(string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = type.Escape();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = electron.clipboard.readImage({0});",
					"return this._addObjectReference(image);"
				),
				option
			);
			int result = _ExecuteJavaScriptBlocking<int>(script);
			return new NativeImage(_socketron) {
				ID = result
			};
		}

		/// <summary>
		/// Writes image to the clipboard.
		/// </summary>
		/// <param name="image"></param>
		/// <param name="type"></param>
		public void WriteImage(NativeImage image, string type = null) {
			string script = string.Empty;
			if (type != null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var image = {0};",
						"electron.clipboard.writeImage(image,{1});"
					),
					Script.GetObject(image.ID),
					type.Escape()
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var image = {0};",
						"electron.clipboard.writeImage(image);"
					),
					Script.GetObject(image.ID)
				);
			}
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - The content in the clipboard as RTF.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string ReadRTF(string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = type.Escape();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.clipboard.readRTF({0});"
				),
				option
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// Writes the text into the clipboard in RTF.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="type"></param>
		public void WriteRTF(string text, string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = ScriptBuilder.Params(
					text.Escape(),
					type.Escape()
				);
			} else {
				option = text.Escape();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.clipboard.writeRTF({0});"
				),
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows* Returns an Object containing title and url keys
		/// representing the bookmark in the clipboard.
		/// The title and url values will be empty strings when the bookmark is unavailable.
		/// </summary>
		/// <returns></returns>
		public JsonObject ReadBookmark() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.clipboard.readBookmark({0});"
				)
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return new JsonObject(result);
		}
		//*/

		/// <summary>
		/// *macOS Windows* Writes the title and url into the clipboard as a bookmark.
		/// <para>
		/// Note: Most apps on Windows don't support pasting bookmarks into them
		/// so you can use clipboard.write to write both a bookmark and fallback text to the clipboard.
		/// </para>
		/// </summary>
		/// <example>
		/// <code>
		/// clipboard.write({
		///		text: 'https://electronjs.org',
		///		bookmark: 'Electron Homepage'
		/// })
		/// </code>
		/// </example>
		/// <param name="title"></param>
		/// <param name="url"></param>
		/// <param name="type"></param>
		public void WriteBookmark(string title, string url, string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = ScriptBuilder.Params(
					url.Escape(),
					type.Escape()
				);
			} else {
				option = url.Escape();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.clipboard.writeBookmark({0});"
				),
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS* Returns String - The text on the find pasteboard.
		/// <para>
		/// This method uses synchronous IPC when called from the renderer process.
		/// The cached value is reread from the find pasteboard whenever the application is activated.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public string ReadFindText() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.clipboard.readFindText();"
				)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// Writes the text into the find pasteboard as plain text.
		/// <para>
		/// This method uses synchronous IPC when called from the renderer process.
		/// </para>
		/// </summary>
		/// <param name="text"></param>
		public void WriteFindText(string text) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.clipboard.writeFindText({0});"
				),
				text.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Clears the clipboard content.
		/// </summary>
		/// <param name="type"></param>
		public void Clear(string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = type.Escape();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.clipboard.clear({0});"
				),
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String[] - An array of supported formats for the clipboard type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public List<string> AvailableFormats(string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = type.Escape();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.clipboard.availableFormats({0});"
				),
				option
			);
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			List<string> formats = new List<string>();
			foreach (object item in result) {
				string format = item as string;
				if (format == null) {
					continue;
				}
				formats.Add(format);
			}
			return formats;
		}

		/// <summary>
		/// *Experimental* Returns Boolean - Whether the clipboard supports the specified format.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public bool Has(string format, string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = ScriptBuilder.Params(
					format.Escape(),
					type.Escape()
				);
			} else {
				option = format.Escape();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.clipboard.has({0});"
				),
				option
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *Experimental* Returns String - Reads format type from the clipboard.
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		public string Read(string format) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.clipboard.read({0});"
				),
				format.Escape()
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// *Experimental* Returns Buffer - Reads format type from the clipboard.
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		public Buffer ReadBuffer(string format) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.clipboard.readBuffer({0});"
				),
				format.Escape()
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			JsonObject json = new JsonObject(result);
			return Buffer.FromJson(json);
		}

		/// <summary>
		/// *Experimental* Writes the buffer into the clipboard as format.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="buffer"></param>
		/// <param name="type"></param>
		public void WriteBuffer(string format, Buffer buffer, string type = null) {
			string script = string.Empty;
			if (type != null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return electron.clipboard.writeBuffer({0},{1},{2});"
					),
					format.Escape(),
					buffer.Stringify(),
					type.Escape()
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return electron.clipboard.writeBuffer({0},{1});"
					),
					format.Escape(),
					buffer.Stringify()
				);
			}
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Writes data to the clipboard.
		/// </summary>
		/// <param name="data">
		/// <list type="bullet">
		/// <item><description>"text" String (optional)</description></item>
		/// <item><description>"html" String (optional)</description></item>
		/// <item><description>"image" NativeImage (optional)</description></item>
		/// <item><description>"rtf" String (optional)</description></item>
		/// <item><description>"bookmark" String (optional) - The title of the url at text.</description></item>
		/// </list>
		/// </param>
		/// <param name="type"></param>
		/// <example>
		/// <code>
		/// const {clipboard} = require('electron')
		/// clipboard.write({text: 'test', html: '&lt;b&gt;test&lt;/b&gt;'})
		/// </code>
		/// </example>
		public void Write(JsonObject data, string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = ScriptBuilder.Params(
					data.Stringify(),
					type.Escape()
				);
			} else {
				option = data.Stringify();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.clipboard.write({0});"
				),
				option
			);
			_ExecuteJavaScript(script);
		}
	}
}
