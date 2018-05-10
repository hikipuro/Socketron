using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Extensions to process object.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public static class ProcessModule {
		/// <summary>
		/// When app is started by being passed as parameter to the default app,
		/// this property is true in the main process, otherwise it is undefined.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static bool isDefaultApp(this NodeModules.Process process) {
			return process.API.GetProperty<bool>("defaultApp");
		}

		/// <summary>
		/// For Mac App Store build, this property is true,
		/// for other builds it is undefined.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static bool isMas(this NodeModules.Process process) {
			string script = ScriptBuilder.Script(
				"if (!self.mas) {",
					"return false;",
				"}",
				"return self.mas;"
			);
			return process.API.ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// A Boolean that controls ASAR support inside your application.
		/// Setting this to true will disable the support for asar archives
		/// in Node's built-in modules.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static bool isNoAsar(this NodeModules.Process process) {
			string script = ScriptBuilder.Script(
				"if (!self.noAsar) {",
					"return false;",
				"}",
				"return self.noAsar;"
			);
			return process.API.ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// A String representing the path to the resources directory.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static string getResourcesPath(this NodeModules.Process process) {
			return process.API.GetProperty<string>("resourcesPath");
		}

		/// <summary>
		/// A Boolean that controls whether or not deprecations printed
		/// to stderr include their stack trace.
		/// Setting this to true will print stack traces for deprecations.
		/// This property is instead of the --trace-deprecation command line flag.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static bool isTraceProcessWarnings(this NodeModules.Process process) {
			string script = ScriptBuilder.Script(
				"if (!self.traceProcessWarnings) {",
					"return false;",
				"}",
				"return self.traceProcessWarnings;"
			);
			return process.API.ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// A String representing the current process's type,
		/// can be "browser" (i.e. main process) or "renderer".
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static string getType(this NodeModules.Process process) {
			return process.API.GetProperty<string>("type");
		}

		/// <summary>
		/// A String representing Chrome's version string.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static string getVersionsChrome(this NodeModules.Process process) {
			return process.API.GetProperty<string>("versions.chrome");
		}

		/// <summary>
		/// A String representing Electron's version string.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static string getVersionsElectron(this NodeModules.Process process) {
			return process.API.GetProperty<string>("versions.electron");
		}

		/// <summary>
		/// If the app is running as a Windows Store app (appx),
		/// this property is true, for otherwise it is undefined.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static bool isWindowsStore(this NodeModules.Process process) {
			string script = ScriptBuilder.Script(
				"if (!self.windowsStore) {",
					"return false;",
				"}",
				"return self.windowsStore;"
			);
			return process.API.ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Causes the main thread of the current process crash.
		/// </summary>
		/// <param name="process"></param>
		public static void crash(this NodeModules.Process process) {
			string script = "self.crash();";
			process.API.Execute(script);
		}

		/// <summary>
		/// Returns CPUUsage.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static CPUUsage getCPUUsage(this NodeModules.Process process) {
			string script = "return self.getCPUUsage();";
			object result = process.API.ExecuteBlocking<object>(script);
			return CPUUsage.FromObject(result);
		}

		/// <summary>
		/// Returns IOCounters.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static IOCounters getIOCounters(this NodeModules.Process process) {
			string script = "return self.getIOCounters();";
			object result = process.API.ExecuteBlocking<object>(script);
			return IOCounters.FromObject(result);
		}

		/// <summary>
		/// Returns an object giving memory usage statistics about the current process.
		/// Note that all statistics are reported in Kilobytes.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static MemoryInfo getProcessMemoryInfo(this NodeModules.Process process) {
			string script = "return self.getProcessMemoryInfo();";
			object result = process.API.ExecuteBlocking<object>(script);
			return MemoryInfo.FromObject(result);
		}

		/// <summary>
		/// Returns an object giving memory usage statistics about the entire system.
		/// Note that all statistics are reported in Kilobytes.
		/// </summary>
		/// <param name="process"></param>
		/// <returns></returns>
		public static JsonObject getSystemMemoryInfo(this NodeModules.Process process) {
			string script = "return self.getSystemMemoryInfo();";
			object result = process.API.ExecuteBlocking<object>(script);
			return new JsonObject(result);
		}

		/// <summary>
		/// Causes the main thread of the current process hang.
		/// </summary>
		/// <param name="process"></param>
		public static void hang(this NodeModules.Process process) {
			string script = "self.hang();";
			process.API.Execute(script);
		}

		/// <summary>
		/// *macOS Linux*
		/// Sets the file descriptor soft limit to maxDescriptors or the OS hard limit,
		/// whichever is lower for the current process.
		/// </summary>
		/// <param name="process"></param>
		/// <param name="maxDescriptors"></param>
		public static void setFdLimit(this NodeModules.Process process, int maxDescriptors) {
			string script = ScriptBuilder.Build(
				"self.setFdLimit({0});",
				maxDescriptors
			);
			process.API.Execute(script);
		}

	}
}
