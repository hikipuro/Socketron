using System;
using System.Threading;

namespace Socketron {
	/// <summary>
	/// Extensions to process object.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	public class Process {
		public class Events {
			public const string Loaded = "loaded";
		}

		protected Process() {
		}

		public static string GetPlatform(Socketron socketron) {
			string[] script = new[] {
				"return process.platform;",
			};
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
		}

		public static bool GetDefaultApp(Socketron socketron) {
			string[] script = new[] {
				"return process.defaultApp;",
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static bool GetMas(Socketron socketron) {
			string[] script = new[] {
				"return process.mas;",
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static bool GetNoAsar(Socketron socketron) {
			string[] script = new[] {
				"return process.noAsar;",
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static bool GetNoDeprecation(Socketron socketron) {
			string[] script = new[] {
				"return process.noDeprecation;",
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static string GetResourcesPath(Socketron socketron) {
			string[] script = new[] {
				"return process.resourcesPath;",
			};
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
		}

		public static bool GetThrowDeprecation(Socketron socketron) {
			string[] script = new[] {
				"return process.throwDeprecation;",
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static bool GetTraceDeprecation(Socketron socketron) {
			string[] script = new[] {
				"return process.traceDeprecation;",
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static bool GetTraceProcessWarnings(Socketron socketron) {
			string[] script = new[] {
				"return process.traceProcessWarnings;",
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static string GetType(Socketron socketron) {
			string[] script = new[] {
				"return process.type;",
			};
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
		}

		public class Versions {
			public static string GetChrome(Socketron socketron) {
				string[] script = new[] {
				"return process.versions.chrome;",
			};
				return _ExecuteJavaScriptBlocking<string>(socketron, script);
			}

			public static string GetElectron(Socketron socketron) {
				string[] script = new[] {
				"return process.versions.electron;",
			};
				return _ExecuteJavaScriptBlocking<string>(socketron, script);
			}
		}

		public static bool GetWindowsStore(Socketron socketron) {
			string[] script = new[] {
				"if (process.windowsStore) {",
					"return true;",
				"} else {",
					"return false;",
				"}"
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public static void Crash(Socketron socketron) {
			string[] script = new[] {
				"process.crash();",
			};
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static CPUUsage GetCPUUsage(Socketron socketron) {
			string[] script = new[] {
				"return process.getCPUUsage();",
			};
			object result = _ExecuteJavaScriptBlocking<object>(socketron, script);
			JsonObject json = new JsonObject(result);
			CPUUsage usage = new CPUUsage();
			usage.percentCPUUsage = json.Double("percentCPUUsage");
			usage.idleWakeupsPerSecond = json.Double("idleWakeupsPerSecond");
			return usage;
		}

		public static IOCounters GetIOCounters(Socketron socketron) {
			string[] script = new[] {
				"return process.getIOCounters();",
			};
			object result = _ExecuteJavaScriptBlocking<object>(socketron, script);
			JsonObject json = new JsonObject(result);
			IOCounters counters = new IOCounters();
			counters.readOperationCount = json.Int32("readOperationCount");
			counters.writeOperationCount = json.Int32("writeOperationCount");
			counters.otherOperationCount = json.Int32("otherOperationCount");
			counters.readTransferCount = json.Int32("readTransferCount");
			counters.writeTransferCount = json.Int32("writeTransferCount");
			counters.otherTransferCount = json.Int32("otherTransferCount");
			return counters;
		}

		public static MemoryInfo GetProcessMemoryInfo(Socketron socketron) {
			string[] script = new[] {
				"return process.getProcessMemoryInfo();",
			};
			object result = _ExecuteJavaScriptBlocking<object>(socketron, script);
			JsonObject json = new JsonObject(result);
			MemoryInfo info = new MemoryInfo();
			//info.pid = json.Int32("pid");
			info.workingSetSize = json.Int32("workingSetSize");
			info.peakWorkingSetSize = json.Int32("peakWorkingSetSize");
			info.privateBytes = json.Int32("privateBytes");
			info.sharedBytes = json.Int32("sharedBytes");
			return info;
		}

		public static JsonObject GetSystemMemoryInfo(Socketron socketron) {
			string[] script = new[] {
				"return process.getSystemMemoryInfo();",
			};
			object result = _ExecuteJavaScriptBlocking<object>(socketron, script);
			JsonObject json = new JsonObject(result);
			return json;
		}

		public static void Hang(Socketron socketron) {
			string[] script = new[] {
				"process.hang();",
			};
			_ExecuteJavaScript(socketron, script, null, null);
		}

		/// <summary>
		/// macOS Linux
		/// </summary>
		/// <param name="socketron"></param>
		/// <param name="maxDescriptors"></param>
		public static void SetFdLimit(Socketron socketron, int maxDescriptors) {
			string[] script = new[] {
				"process.setFdLimit(" + maxDescriptors + ");",
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
				Console.Error.WriteLine("error: Process._ExecuteJavaScriptBlocking");
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
