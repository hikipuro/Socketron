using System;
using System.Threading;

namespace Socketron {
	/// <summary>
	/// Manage files and URLs using their default applications.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	public class Shell {
		protected Shell() {
		}

		public static bool ShowItemInFolder(Socketron socketron, string fullPath) {
			string[] script = new[] {
				"return electron.shell.showItemInFolder(" + fullPath.Escape() + ");",
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static bool OpenItem(Socketron socketron, string fullPath) {
			string[] script = new[] {
				"return electron.shell.openItem(" + fullPath.Escape() + ");",
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static bool OpenExternal(Socketron socketron, string url) {
			string[] script = new[] {
				"return electron.shell.openExternal(" + url.Escape() + ");",
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static bool MoveItemToTrash(Socketron socketron, string fullPath) {
			string[] script = new[] {
				"return electron.shell.moveItemToTrash(" + fullPath.Escape() + ");",
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static void Beep(Socketron socketron) {
			string[] script = new[] {
				"electron.shell.beep();",
			};
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static bool WriteShortcutLink(Socketron socketron, string shortcutPath) {
			string[] script = new[] {
				"return electron.shell.writeShortcutLink(" + shortcutPath.Escape() + ");",
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static string ReadShortcutLink(Socketron socketron, string shortcutPath) {
			string[] script = new[] {
				"return electron.shell.readShortcutLink(" + shortcutPath.Escape() + ");",
			};
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
		}

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
				Console.Error.WriteLine("error: Shell._ExecuteJavaScriptBlocking");
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
