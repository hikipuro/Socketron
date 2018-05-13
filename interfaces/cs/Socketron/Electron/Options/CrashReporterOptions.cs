namespace Socketron.Electron {
	/// <summary>
	/// CrashReporter.start() options.
	/// </summary>
	public class CrashReporterStartOptions {
		public string companyName;
		/// <summary>
		/// URL that crash reports will be sent to as POST.
		/// </summary>
		public string submitURL;
		/// <summary>
		/// Defaults to app.getName().
		/// </summary>
		public string productName;
		/// <summary>
		/// Whether crash reports should be sent to the server Default is true.
		/// </summary>
		public bool? uploadToServer;
		/// <summary>
		/// Default is false.
		/// </summary>
		public bool? ignoreSystemCrashHandler;
		/// <summary>
		/// An object you can define that will be sent along with the report.
		/// Only string properties are sent correctly.
		/// Nested objects are not supported and the property names
		/// and values must be less than 64 characters long.
		/// </summary>
		public JsonObject extra;
		/// <summary>
		/// Directory to store the crashreports temporarily
		/// (only used when the crash reporter is started via process.crashReporter.start)
		/// </summary>
		public string crashesDirectory;
	}
}
