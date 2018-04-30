using System;
using System.Collections.Generic;
using System.Threading;

namespace Socketron {
	/// <summary>
	/// Detect keyboard events when the application does not have keyboard focus.
	/// <para>Process: Main</para>
	/// </summary>
	public class GlobalShortcut {
		public const string Name = "GlobalShortcut";

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		protected GlobalShortcut() {
		}

		public static Callback GetCallbackFromId(ushort id) {
			if (!_callbackList.ContainsKey(id)) {
				return null;
			}
			return _callbackList[id];
		}

		public static void Register(Socketron socketron, string accelerator, Callback callback) {
			if (callback == null) {
				return;
			}
			_callbackList.Add(_callbackListId, callback);
			string[] script = new[] {
				"var listener = () => {",
					"emit('__event'," + Name.Escape() + "," + _callbackListId + ");",
				"};",
				"this._addClientEventListener(" + Name.Escape() + "," + _callbackListId + ",listener);",
				"electron.globalShortcut.register(" + accelerator.Escape() + ",listener);",
			};
			_callbackListId++;
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static bool IsRegistered(Socketron socketron, string accelerator) {
			string[] script = new[] {
				"return electron.globalShortcut.isRegistered(" + accelerator.Escape() + ");",
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static void Unregister(Socketron socketron, string accelerator) {
			string[] script = new[] {
				"electron.globalShortcut.unregister(" + accelerator.Escape() + ");",
			};
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static void UnregisterAll(Socketron socketron) {
			string[] script = new[] {
				"electron.globalShortcut.unregisterAll();",
			};
			_ExecuteJavaScript(socketron, script, null, null);
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
				Console.Error.WriteLine("error: GlobalShortcut._ExecuteJavaScriptBlocking");
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
