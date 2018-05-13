using System;

namespace Socketron.Electron {
	/// <summary>
	/// WebFrame.setSpellCheckProvider() options.
	/// </summary>
	public class Provider {
		/// <summary>
		/// Returns Boolean
		/// </summary>
		public Action<string> spellCheck;
	}

	/// <summary>
	/// WebFrame.registerURLSchemeAsPrivileged() options.
	/// </summary>
	public class RegisterURLSchemeAsPrivilegedOptions {
		/// <summary>
		/// Default true.
		/// </summary>
		public bool? secure;
		/// <summary>
		/// Default true.
		/// </summary>
		public bool? bypassCSP;
		/// <summary>
		/// Default true.
		/// </summary>
		public bool? allowServiceWorkers;
		/// <summary>
		/// Default true.
		/// </summary>
		public bool? supportFetchAPI;
		/// <summary>
		/// Default true.
		/// </summary>
		public bool? corsEnabled;
	}

	/// <summary>
	/// WebFrame.getResourceUsage() return value.
	/// </summary>
	public class ResourceUsage {
		public MemoryUsageDetails images;
		public MemoryUsageDetails cssStyleSheets;
		public MemoryUsageDetails xslStyleSheets;
		public MemoryUsageDetails fonts;
		public MemoryUsageDetails other;

		public static ResourceUsage FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new ResourceUsage() {
				images = MemoryUsageDetails.FromObject(json["images"]),
				cssStyleSheets = MemoryUsageDetails.FromObject(json["cssStyleSheets"]),
				xslStyleSheets = MemoryUsageDetails.FromObject(json["xslStyleSheets"]),
				fonts = MemoryUsageDetails.FromObject(json["fonts"]),
				other = MemoryUsageDetails.FromObject(json["other"])
			};
		}
	}
}
