using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Perform copy and paste operations on the system clipboard.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Clipboard : EventEmitter {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public Clipboard() {
		}

		/// <summary>
		/// Returns String - The content in the clipboard as plain text.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string readText(string type = null) {
			if (type == null) {
				return API.Apply<string>("readText");
			} else {
				return API.Apply<string>("readText", type);
			}
		}

		/// <summary>
		/// Writes the text into the clipboard as plain text.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="type"></param>
		public void writeText(string text, string type = null) {
			if (type == null) {
				API.Apply("writeText", text);
			} else {
				API.Apply("writeText", text, type);
			}
		}

		/// <summary>
		/// Returns String - The content in the clipboard as markup.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string readHTML(string type = null) {
			if (type == null) {
				return API.Apply<string>("readHTML");
			} else {
				return API.Apply<string>("readHTML", type);
			}
		}

		/// <summary>
		/// Writes markup to the clipboard.
		/// </summary>
		/// <param name="markup"></param>
		/// <param name="type"></param>
		public void writeHTML(string markup, string type = null) {
			if (type == null) {
				API.Apply("writeHTML", markup);
			} else {
				API.Apply("writeHTML", markup, type);
			}
		}

		/// <summary>
		/// Returns NativeImage - The image content in the clipboard.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public NativeImage readImage(string type = null) {
			if (type == null) {
				return API.ApplyAndGetObject<NativeImage>("readImage");
			} else {
				return API.ApplyAndGetObject<NativeImage>("readImage", type);
			}
		}

		/// <summary>
		/// Writes image to the clipboard.
		/// </summary>
		/// <param name="image"></param>
		/// <param name="type"></param>
		public void writeImage(NativeImage image, string type = null) {
			if (type == null) {
				API.Apply("writeImage", image);
			} else {
				API.Apply("writeImage", image, type);
			}
		}

		/// <summary>
		/// Returns String - The content in the clipboard as RTF.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string readRTF(string type = null) {
			if (type == null) {
				return API.Apply<string>("readRTF");
			} else {
				return API.Apply<string>("readRTF", type);
			}
		}

		/// <summary>
		/// Writes the text into the clipboard in RTF.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="type"></param>
		public void writeRTF(string text, string type = null) {
			if (type == null) {
				API.Apply("writeRTF", text);
			} else {
				API.Apply("writeRTF", text, type);
			}
		}

		/// <summary>
		/// *macOS Windows*
		/// Returns an Object containing title and url keys
		/// representing the bookmark in the clipboard.
		/// The title and url values will be empty strings when the bookmark is unavailable.
		/// </summary>
		/// <returns></returns>
		public ReadBookmark readBookmark() {
			object result = API.Apply("readBookmark");
			return ReadBookmark.FromObject(result);
		}

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
			if (type == null) {
				API.Apply("writeBookmark", title, url);
			} else {
				API.Apply("writeBookmark", title, url, type);
			}
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
			return API.Apply<string>("readFindText");
		}

		/// <summary>
		/// Writes the text into the find pasteboard as plain text.
		/// <para>
		/// This method uses synchronous IPC when called from the renderer process.
		/// </para>
		/// </summary>
		/// <param name="text"></param>
		public void writeFindText(string text) {
			API.Apply("writeFindText", text);
		}

		/// <summary>
		/// Clears the clipboard content.
		/// </summary>
		/// <param name="type"></param>
		public void clear(string type = null) {
			if (type == null) {
				API.Apply("clear");
			} else {
				API.Apply("clear", type);
			}
		}

		/// <summary>
		/// Returns String[] - An array of supported formats for the clipboard type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string[] availableFormats(string type = null) {
			if (type == null) {
				object[] result = API.Apply<object[]>("availableFormats");
				return Array.ConvertAll(result, value => Convert.ToString(value));
			} else {
				object[] result = API.Apply<object[]>("availableFormats", type);
				return Array.ConvertAll(result, value => Convert.ToString(value));
			}
		}

		/// <summary>
		/// *Experimental*
		/// Returns Boolean - Whether the clipboard supports the specified format.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public bool has(string format, string type = null) {
			if (type == null) {
				return API.Apply<bool>("has", format);
			} else {
				return API.Apply<bool>("has", format, type);
			}
		}

		/// <summary>
		/// *Experimental*
		/// Returns String - Reads format type from the clipboard.
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		public string read(string format) {
			return API.Apply<string>("read", format);
		}

		/// <summary>
		/// *Experimental*
		/// Returns Buffer - Reads format type from the clipboard.
		/// </summary>
		/// <param name="format"></param>
		/// <returns></returns>
		public Buffer readBuffer(string format) {
			return API.ApplyAndGetObject<Buffer>("readBuffer", format);
		}

		/// <summary>
		/// *Experimental*
		/// Writes the buffer into the clipboard as format.
		/// </summary>
		/// <param name="format"></param>
		/// <param name="buffer"></param>
		/// <param name="type"></param>
		public void writeBuffer(string format, Buffer buffer, string type = null) {
			if (type == null) {
				API.Apply("writeBuffer", format, buffer);
			} else {
				API.Apply("writeBuffer", format, buffer, type);
			}
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
		public void write(Data data, string type = null) {
			if (type == null) {
				API.Apply("write", data);
			} else {
				API.Apply("write", data, type);
			}
		}
	}
}
