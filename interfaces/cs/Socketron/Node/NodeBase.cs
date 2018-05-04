using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Socketron {
	public class NodeBase : IDisposable {
		public int id;
		protected Socketron _socketron;
		protected bool _disposeManually = false;

		~NodeBase() {
			if (id <= 0) {
				return;
			}
			//Console.WriteLine("ElectronBase.Dispose: " + id);
			Dispose();
		}

		public void Dispose() {
			if (_disposeManually) {
				return;
			}
			if (_socketron == null) {
				return;
			}
			_socketron.RemoveObject(id);
			id = 0;
		}

		protected void _ExecuteJavaScript(string script) {
			_socketron.Main.ExecuteJavaScript(script);
		}

		protected void _ExecuteJavaScript(string script, Callback success) {
			_socketron.Main.ExecuteJavaScript(script, success);
		}

		protected void _ExecuteJavaScript(string script, Callback success, Callback error) {
			_socketron.Main.ExecuteJavaScript(script, success, error);
		}

		/*
		protected void _ExecuteJavaScript(string[] script) {
			_socketron.Main.ExecuteJavaScript(script);
		}

		protected void _ExecuteJavaScript(string[] script, Callback callback) {
			_socketron.Main.ExecuteJavaScript(script, callback);
		}

		protected void _ExecuteJavaScript(string[] script, Callback success, Callback error) {
			_socketron.Main.ExecuteJavaScript(script, success, error);
		}
		//*/

		protected T _ExecuteBlocking<T>(string script) {
			ManualResetEvent resetEvent = new ManualResetEvent(false);
			T value = default(T);
#if DEBUG
			StackTrace stackTrace = new StackTrace();
#endif

			_ExecuteJavaScript(script, (result) => {
				if (result == null) {
					if (value != null) {
						Console.Error.WriteLine("error: " + GetType().Name + "._ExecuteJavaScriptBlocking");
						throw new InvalidOperationException(result as string);
					}
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
				Console.Error.WriteLine("error: " + GetType().Name + "._ExecuteJavaScriptBlocking");
#if DEBUG
				Console.Error.WriteLine(stackTrace);
#endif
				throw new InvalidOperationException(result as string);
			});

			resetEvent.WaitOne();
			return value;
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
				Console.Error.WriteLine("error: " + typeof(NodeBase).Name + "._ExecuteJavaScriptBlocking");
				throw new InvalidOperationException(result as string);
			});

			resetEvent.WaitOne();
			return value;
		}

		protected static ScriptHelper Script = new ScriptHelper();
		protected class ScriptHelper {
			public string GetObject(int id) {
				return string.Format("this._objRefs[{0}]", id);
			}
			public string GetObjectList(NodeBase[] list) {
				if (list == null) {
					return "null";
				}
				List<string> result = new List<string>();
				foreach (NodeBase obj in list) {
					result.Add(GetObject(obj.id));
				}
				return string.Join(",", result.ToArray());
			}
			public string GetObjectList(object[] list) {
				if (list == null) {
					return "null";
				}
				List<string> result = new List<string>();
				foreach (object obj in list) {
					result.Add(obj.ToString());
				}
				return string.Join(",", result.ToArray());
			}
			public string AddObject(string name) {
				return string.Format("this._addObjectReference({0})", name);
			}
			public string RemoveObject(int id) {
				return string.Format("this._removeObjectReference({0})", id);
			}
			public string RemoveObject(string name) {
				return string.Format("this._removeObjectReference({0})", name);
			}
		}
	}
}
