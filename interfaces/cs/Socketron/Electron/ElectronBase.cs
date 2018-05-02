using System;
using System.Diagnostics;
using System.Threading;

namespace Socketron {
	public class ElectronBase {
		protected Socketron _socketron;

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

		protected T _ExecuteJavaScriptBlocking<T>(string script) {
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
				Console.Error.WriteLine("error: " + typeof(ElectronBase).Name + "._ExecuteJavaScriptBlocking");
				throw new InvalidOperationException(result as string);
			});

			resetEvent.WaitOne();
			return value;
		}
	}
}
