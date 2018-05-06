using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Perform copy and paste operations on the system clipboard.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Clipboard : NodeModule {
		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public Clipboard(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// Returns String - The content in the clipboard as plain text.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string readText(string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = type.Escape();
			}
			string script = ScriptBuilder.Build(
				"return {0}.readText({1});",
				Script.GetObject(_id),
				option
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Writes the text into the clipboard as plain text.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="type"></param>
		public void writeText(string text, string type = null) {
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
				"{0}.writeText({1});",
				Script.GetObject(_id),
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - The content in the clipboard as markup.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string readHTML(string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = type.Escape();
			}
			string script = ScriptBuilder.Build(
				"return {0}.readHTML({1});",
				Script.GetObject(_id),
				option
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Writes markup to the clipboard.
		/// </summary>
		/// <param name="markup"></param>
		/// <param name="type"></param>
		public void writeHTML(string markup, string type = null) {
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
				"{0}.writeHTML({1});",
				Script.GetObject(_id),
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns NativeImage - The image content in the clipboard.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public NativeImage readImage(string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = type.Escape();
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0}.readImage({1});",
					"return {2};"
				),
				Script.GetObject(_id),
				option,
				Script.AddObject("image")
			);
			int result = _ExecuteBlocking<int>(script);
			return new NativeImage(_client, result);
		}

		/// <summary>
		/// Writes image to the clipboard.
		/// </summary>
		/// <param name="image"></param>
		/// <param name="type"></param>
		public void writeImage(NativeImage image, string type = null) {
			string script = string.Empty;
			if (type != null) {
				script = ScriptBuilder.Build(
					"{0}.writeImage({1},{2});",
					Script.GetObject(_id),
					Script.GetObject(image._id),
					type.Escape()
				);
			} else {
				script = ScriptBuilder.Build(
					"{0}.writeImage({1});",
					Script.GetObject(_id),
					Script.GetObject(image._id)
				);
			}
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - The content in the clipboard as RTF.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string readRTF(string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = type.Escape();
			}
			string script = ScriptBuilder.Build(
				"return {0}.readRTF({1});",
				Script.GetObject(_id),
				option
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Writes the text into the clipboard in RTF.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="type"></param>
		public void writeRTF(string text, string type = null) {
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
				"{0}.writeRTF({1});",
				Script.GetObject(_id),
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Returns an Object containing title and url keys
		/// representing the bookmark in the clipboard.
		/// The title and url values will be empty strings when the bookmark is unavailable.
		/// </summary>
		/// <returns></returns>
		public JsonObject readBookmark() {
			string script = ScriptBuilder.Build(
				"return {0}.readBookmark();",
				Script.GetObject(_id)
			);
			object result = _ExecuteBlocking<object>(script);
			return new JsonObject(result);
		}
		//*/

		/// <summary>
		/// *macOS Windows*
		/// Writes the title and url into the clipboard as a bookmark.
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
		public void writeBookmark(string title, string url, string type = null) {
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
				"{0}.writeBookmark({1});",
				Script.GetObject(_id),
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Returns String - The text on the find pasteboard.
		/// <para>
		/// This method uses synchronous IPC when called from the renderer process.
		/// The cached value is reread from the find pasteboard whenever the application is activated.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public string readFindText() {
			string script = ScriptBuilder.Build(
				"return {0}.readFindText();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Writes the text into the find pasteboard as plain text.
		/// <para>
		/// This method uses synchronous IPC when called from the renderer process.
		/// </para>
		/// </summary>
		/// <param name="text"></param>
		public void writeFindText(string text) {
			string script = ScriptBuilder.Build(
				"{0}.writeFindText({1});",
				Script.GetObject(_id),
				text.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Clears the clipboard content.
		/// </summary>
		/// <param name="type"></param>
		public void clear(string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = type.Escape();
			}
			string script = ScriptBuilder.Build(
				"{0}.clear({1});",
				Script.GetObject(_id),
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String[] - An array of supported formats for the clipboard type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public List<string> availableFormats(string type = null) {
			string option = string.Empty;
			if (type != null) {
				option = type.Escape();
			}
			string script = ScriptBuilder.Build(
				"return {0}.availableFormats({1});",
				Script.GetObject(_id),
				option
			);
			object[] result = _ExecuteBlocking<object[]>(script);
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
		/// *Experimental*
		/// Returns Boolean - Whether the clipboard supports the specified format.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public bool has(string format, string type = null) {
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
				"return {0}.has({1});",
				Script.GetObject(_id),
				option
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *Experimental*
		/// Returns String - Reads format type from the clipboard.
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		public string read(string format) {
			string script = ScriptBuilder.Build(
				"return {0}.read({1});",
				Script.GetObject(_id),
				format.Escape()
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// *Experimental*
		/// Returns Buffer - Reads format type from the clipboard.
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		public LocalBuffer readBuffer(string format) {
			string script = ScriptBuilder.Build(
				"return {0}.readBuffer({1});",
				Script.GetObject(_id),
				format.Escape()
			);
			object result = _ExecuteBlocking<object>(script);
			JsonObject json = new JsonObject(result);
			return LocalBuffer.FromJson(json);
		}

		/// <summary>
		/// *Experimental*
		/// Writes the buffer into the clipboard as format.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="buffer"></param>
		/// <param name="type"></param>
		public void writeBuffer(string format, LocalBuffer buffer, string type = null) {
			string script = string.Empty;
			if (type != null) {
				script = ScriptBuilder.Build(
					"{0}.writeBuffer({1},{2},{3});",
					Script.GetObject(_id),
					format.Escape(),
					buffer.Stringify(),
					type.Escape()
				);
			} else {
				script = ScriptBuilder.Build(
					"{0}.writeBuffer({1},{2});",
					Script.GetObject(_id),
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
		public void write(JsonObject data, string type = null) {
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
				"{0}.write({1});",
				Script.GetObject(_id),
				option
			);
			_ExecuteJavaScript(script);
		}
	}
}
