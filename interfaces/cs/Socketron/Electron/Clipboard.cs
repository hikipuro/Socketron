using System;
using System.Collections.Generic;
using System.Threading;

namespace Socketron {
	/// <summary>
	/// Perform copy and paste operations on the system clipboard.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	public class Clipboard {
		public const string Name = "Clipboard";

		public static string ReadText(Socketron socketron, string type = null) {
			string[] script = new[] {
				"return electron.clipboard.readText();"
			};
			if (type != null) {
				script = new[] {
					"return electron.clipboard.readText(" + type.Escape() + ");"
				};
			}
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
		}

		public static void WriteText(Socketron socketron, string text, string type = null) {
			string[] script = new[] {
				"electron.clipboard.writeText(" + text.Escape() + ");"
			};
			if (type != null) {
				script = new[] {
					"electron.clipboard.writeText(" + text.Escape() + "," + type.Escape() + ");"
				};
			}
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static string ReadHTML(Socketron socketron, string type = null) {
			string[] script = new[] {
				"return electron.clipboard.readHTML();"
			};
			if (type != null) {
				script = new[] {
					"return electron.clipboard.readHTML(" + type.Escape() + ");"
				};
			}
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
		}

		public static void WriteHTML(Socketron socketron, string markup, string type = null) {
			string[] script = new[] {
				"electron.clipboard.writeHTML(" + markup.Escape() + ");"
			};
			if (type != null) {
				script = new[] {
					"electron.clipboard.writeHTML(" + markup.Escape() + "," + type.Escape() + ");"
				};
			}
			_ExecuteJavaScript(socketron, script, null, null);
		}

		/*
		public static string ReadImage(Socketron socketron, string type = null) {
			string[] script = new[] {
				"return electron.clipboard.readImage();"
			};
			if (type != null) {
				script = new[] {
					"return electron.clipboard.readImage(" + type.Escape() + ");"
				};
			}
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
		}
		//*/

		/*
		public static void WriteImage(Socketron socketron, string markup, string type = null) {
			string[] script = new[] {
				"electron.clipboard.writeImage(" + markup.Escape() + ");"
			};
			if (type != null) {
				script = new[] {
					"electron.clipboard.writeImage(" + markup.Escape() + "," + type.Escape() + ");"
				};
			}
			_ExecuteJavaScript(socketron, script, null, null);
		}
		//*/

		public static string ReadRTF(Socketron socketron, string type = null) {
			string[] script = new[] {
				"return electron.clipboard.readRTF();"
			};
			if (type != null) {
				script = new[] {
					"return electron.clipboard.readRTF(" + type.Escape() + ");"
				};
			}
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
		}

		public static void WriteRTF(Socketron socketron, string markup, string type = null) {
			string[] script = new[] {
				"electron.clipboard.writeRTF(" + markup.Escape() + ");"
			};
			if (type != null) {
				script = new[] {
					"electron.clipboard.writeRTF(" + markup.Escape() + "," + type.Escape() + ");"
				};
			}
			_ExecuteJavaScript(socketron, script, null, null);
		}

		/*
		public static string ReadBookmark(Socketron socketron, string type = null) {
			string[] script = new[] {
				"return electron.clipboard.readBookmark();"
			};
			if (type != null) {
				script = new[] {
					"return electron.clipboard.readBookmark(" + type.Escape() + ");"
				};
			}
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
		}
		//*/

		public static void WriteBookmark(Socketron socketron, string title, string url, string type = null) {
			string[] script = new[] {
				"electron.clipboard.writeBookmark(" + title.Escape() + "," + url.Escape() + ");"
			};
			if (type != null) {
				script = new[] {
					"electron.clipboard.writeBookmark(" + title.Escape() + "," + url.Escape() + "," + type.Escape() + ");"
				};
			}
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static string ReadFindText(Socketron socketron, string type = null) {
			string[] script = new[] {
				"return electron.clipboard.readFindText();"
			};
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
		}

		public static void WriteFindText(Socketron socketron, string text) {
			string[] script = new[] {
				"electron.clipboard.writeFindText(" + text.Escape() + ");"
			};
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static void Clear(Socketron socketron, string type = null) {
			string[] script = new[] {
				"electron.clipboard.clear();"
			};
			if (type != null) {
				script = new[] {
					"electron.clipboard.clear(" + type.Escape() + ");"
				};
			}
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static List<string> AvailableFormats(Socketron socketron, string type = null) {
			string[] script = new[] {
				"return electron.clipboard.availableFormats();"
			};
			if (type != null) {
				script = new[] {
					"return electron.clipboard.availableFormats(" + type.Escape() + ");"
				};
			}
			object[] result = _ExecuteJavaScriptBlocking<object[]>(socketron, script);
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

		public static bool Has(Socketron socketron, string format, string type = null) {
			string[] script = new[] {
				"return electron.clipboard.has(" + format.Escape() + ");"
			};
			if (type != null) {
				script = new[] {
					"return electron.clipboard.has(" + format.Escape() + "," + type.Escape() + ");"
				};
			}
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static string Read(Socketron socketron, string format) {
			string[] script = new[] {
				"return electron.clipboard.read(" + format.Escape() + ");"
			};
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
		}

		/*
		public static void WriteBuffer(Socketron socketron, string format, Buffer buffer, string type = null) {
			string[] script = new[] {
				"electron.clipboard.writeBuffer(" + format.Escape() + "," + buffer.xxx() + ");"
			};
			if (type != null) {
				script = new[] {
					"electron.clipboard.writeBuffer(" + format.Escape() + "," + buffer.xxx() + "," + type.Escape() + ");"
				};
			}
			_ExecuteJavaScript(socketron, script, null, null);
		}
		//*/

		public static void Write(Socketron socketron, JsonObject data, string type = null) {
			string[] script = new[] {
				"electron.clipboard.write(" + data.Stringify() + ");"
			};
			if (type != null) {
				script = new[] {
					"electron.clipboard.write(" + data.Stringify() + "," + type.Escape() + ");"
				};
			}
			_ExecuteJavaScript(socketron, script, null, null);
		}

		/*
		public static string ReadBuffer(Socketron socketron, string format) {
			string[] script = new[] {
				"return electron.clipboard.readBuffer(" + format.Escape() + ");"
			};
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
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
				Console.Error.WriteLine("error: Clipboard._ExecuteJavaScriptBlocking");
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
