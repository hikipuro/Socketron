using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Extensions to process object.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public static class ProcessModule {
		public static bool isDefaultApp(this Socketron.ProcessModule process) {
			string script = "return process.defaultApp;";
			return process.ExecuteJavaScriptBlocking<bool>(script);
		}

		public static bool isMas(this Socketron.ProcessModule process) {
			string script = ScriptBuilder.Script(
				"if (!process.mas) {",
					"return false;",
				"}",
				"return process.mas;"
			);
			return process.ExecuteJavaScriptBlocking<bool>(script);
		}

		public static bool isNoAsar(this Socketron.ProcessModule process) {
			string script = ScriptBuilder.Script(
				"if (!process.noAsar) {",
					"return false;",
				"}",
				"return process.noAsar;"
			);
			return process.ExecuteJavaScriptBlocking<bool>(script);
		}

		public static string getResourcesPath(this Socketron.ProcessModule process) {
			string script = "return process.resourcesPath;";
			return process.ExecuteJavaScriptBlocking<string>(script);
		}

		public static bool isTraceProcessWarnings(this Socketron.ProcessModule process) {
			string script = ScriptBuilder.Script(
				"if (!process.traceProcessWarnings) {",
					"return false;",
				"}",
				"return process.traceProcessWarnings;"
			);
			return process.ExecuteJavaScriptBlocking<bool>(script);
		}

		public static string getType(this Socketron.ProcessModule process) {
			string script = "return process.type;";
			return process.ExecuteJavaScriptBlocking<string>(script);
		}

		public static string getVersionsChrome(this Socketron.ProcessModule process) {
			string script = "return process.versions.chrome;";
			return process.ExecuteJavaScriptBlocking<string>(script);
		}

		public static string getVersionsElectron(this Socketron.ProcessModule process) {
			string script = "return process.versions.electron;";
			return process.ExecuteJavaScriptBlocking<string>(script);
		}

		public static bool isWindowsStore(this Socketron.ProcessModule process) {
			string script = ScriptBuilder.Script(
				"if (!process.windowsStore) {",
					"return false;",
				"}",
				"return process.windowsStore;"
			);
			return process.ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// Causes the main thread of the current process crash.
		/// </summary>
		/// <param name="process"></param>
		public static void crash(this Socketron.ProcessModule process) {
			string script = "process.crash();";
			process.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns CPUUsage.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static CPUUsage getCPUUsage(this Socketron.ProcessModule process) {
			string script = "return process.getCPUUsage();";
			object result = process.ExecuteJavaScriptBlocking<object>(script);
			return CPUUsage.FromObject(result);
		}

		/// <summary>
		/// Returns IOCounters.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static IOCounters getIOCounters(this Socketron.ProcessModule process) {
			string script = "return process.getIOCounters();";
			object result = process.ExecuteJavaScriptBlocking<object>(script);
			return IOCounters.FromObject(result);
		}

		/// <summary>
		/// Returns an object giving memory usage statistics about the current process.
		/// Note that all statistics are reported in Kilobytes.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static MemoryInfo getProcessMemoryInfo(this Socketron.ProcessModule process) {
			string script = "return process.getProcessMemoryInfo();";
			object result = process.ExecuteJavaScriptBlocking<object>(script);
			return MemoryInfo.FromObject(result);
		}

		/// <summary>
		/// Returns an object giving memory usage statistics about the entire system.
		/// Note that all statistics are reported in Kilobytes.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static JsonObject getSystemMemoryInfo(this Socketron.ProcessModule process) {
			string script = "return process.getSystemMemoryInfo();";
			object result = process.ExecuteJavaScriptBlocking<object>(script);
			return new JsonObject(result);
		}

		/// <summary>
		/// Causes the main thread of the current process hang.
		/// </summary>
		/// <param name="process"></param>
		public static void hang(this Socketron.ProcessModule process) {
			string script = "process.hang();";
			process.ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Linux*
		/// Sets the file descriptor soft limit to maxDescriptors or the OS hard limit,
		/// whichever is lower for the current process.
		/// </summary>
		/// <param name="process"></param>
		/// <param name="maxDescriptors"></param>
		public static void setFdLimit(this Socketron.ProcessModule process, int maxDescriptors) {
			string script = ScriptBuilder.Build(
				"process.setFdLimit({0});",
				maxDescriptors
			);
			process.ExecuteJavaScript(script);
		}

	}
}
