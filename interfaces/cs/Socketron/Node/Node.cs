using System;
using System.Collections.Generic;
using System.Threading;

namespace Socketron {
	public class Node {
		public const string Name = "Node";

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		public static Callback GetCallbackFromId(ushort id) {
			if (!_callbackList.ContainsKey(id)) {
				return null;
			}
			return _callbackList[id];
		}

		/*
		public static string GetDirname(Socketron socketron) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return global.__dirname;"
				)
			);
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
		}
		//*/

		public static int SetTimeout(Socketron socketron, Callback callback, int delay) {
			if (callback == null) {
				return -1;
			}
			_callbackList.Add(_callbackListId, callback);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"this._removeObjectReference(timer);",
						"console.log('test timer', timer);",
						"emit('__event',{1},{2});",
					"}}",
					"var timer = setTimeout(callback,{0});",
					"return this._addObjectReference(timer);"
				),
				delay,
				Name.Escape(),
				_callbackListId
			);
			_callbackListId++;
			return _ExecuteJavaScriptBlocking<int>(socketron, script);
		}

		public static void ClearTimeout(Socketron socketron, int timeoutObject) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var timer = this._objRefs[{0}];",
					"clearTimeout(timer);",
					"this._removeObjectReference(timer);"
				),
				timeoutObject
			);
			_ExecuteJavaScript(socketron, script, null, null);
		}

		protected static void _ExecuteJavaScript(Socketron socketron, string script, Callback success, Callback error) {
			socketron.Main.ExecuteJavaScript(script, success, error);
		}

		protected static T _ExecuteJavaScriptBlocking<T>(Socketron socketron, string script) {
			ManualResetEvent resetEvent = new ManualResetEvent(false);
			T value = default(T);

			_ExecuteJavaScript(socketron, script, (result) => {
				if (result == null) {
					resetEvent.Set();
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
				resetEvent.Set();
			}, (result) => {
				Console.Error.WriteLine("error: Node._ExecuteJavaScriptBlocking");
				throw new InvalidOperationException(result as string);
				//done = true;
			});

			resetEvent.WaitOne();
			return value;
		}
	}
}
