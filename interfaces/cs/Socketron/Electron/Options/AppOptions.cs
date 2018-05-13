namespace Socketron.Electron {
	/// <summary>
	/// App.relaunch() options.
	/// </summary>
	public class RelaunchOptions {
		public string[] args;
		public string execPath;
	}

	/// <summary>
	/// App.setLoginItemSettings() options.
	/// </summary>
	public class Settings {
		/// <summary>
		/// true to open the app at login, false to remove the app as a login item.
		/// Defaults to false.
		/// </summary>
		public bool? openAtLogin;
		/// <summary>
		/// true to open the app as hidden. Defaults to false.
		/// The user can edit this setting from the System Preferences so
		/// app.getLoginItemStatus().wasOpenedAsHidden should be checked when the app is
		/// opened to know the current value.This setting is only supported on macOS.
		/// </summary>
		public bool? openAsHidden;
		/// <summary>
		/// The executable to launch at login. Defaults to process.execPath.
		/// </summary>
		public string path;
		/// <summary>
		/// The command-line arguments to pass to the executable.
		/// Defaults to an empty array.
		/// Take care to wrap paths in quotes.
		/// </summary>
		public string[] args;
	}

	/// <summary>
	/// App.setAboutPanelOptions() options.
	/// </summary>
	public class AboutPanelOptionsOptions {
		/// <summary>
		/// The app's name.
		/// </summary>
		public string applicationName;
		/// <summary>
		/// The app's version.
		/// </summary>
		public string applicationVersion;
		/// <summary>
		/// Copyright information.
		/// </summary>
		public string copyright;
		/// <summary>
		/// Credit information.
		/// </summary>
		public string credits;
		/// <summary>
		/// The app's build version number.
		/// </summary>
		public string version;
	}

	/// <summary>
	/// App.getFileIcon() options.
	/// </summary>
	public class FileIconOptions {
		public string size;

		public class Size {
			public const string Small = "small";
			public const string Normal = "normal";
			public const string Large = "large";
		}
	}

	/// <summary>
	/// App.importCertificate() options.
	/// </summary>
	public class ImportCertificateOptions {
		/// <summary>
		/// Path for the pkcs12 file.
		/// </summary>
		public string certificate;
		/// <summary>
		/// Passphrase for the certificate.
		/// </summary>
		public string password;
	}

	/// <summary>
	/// App.getJumpListSettings() options.
	/// </summary>
	public class JumpListSettings {
		/// <summary>
		/// The minimum number of items that will be shown in the Jump List.
		/// </summary>
		public int minItems;
		/// <summary>
		/// Array of JumpListItem objects that correspond to items that the user has
		/// explicitly removed from custom categories in the Jump List. These items must not
		/// be re-added to the Jump List in the call to app.setJumpList(), Windows will not
		/// display any custom category that contains any of the removed items.
		/// </summary>
		public JumpListItem[] removedItems;

		public static JumpListSettings FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			// TODO: fix removedItems
			JsonObject json = new JsonObject(obj);
			return new JumpListSettings() {
				minItems = json.Int32("minItems"),
				//y = json.Int32("y")
			};
		}
	}

	/// <summary>
	/// App.getLoginItemSettings() return value.
	/// </summary>
	public class LoginItemSettings {
		public JsonObject options;
		/// <summary>
		/// true if the app is set to open at login.
		/// </summary>
		public bool openAtLogin;
		/// <summary>
		/// true if the app is set to open as hidden at login.
		/// This setting is only supported on macOS.
		/// </summary>
		public bool openAsHidden;
		/// <summary>
		/// true if the app was opened at login automatically.
		/// This setting is only supported on macOS.
		/// </summary>
		public bool wasOpenedAtLogin;
		/// <summary>
		/// true if the app was opened as a hidden login item.
		/// This indicates that the app should not open any windows at startup.
		/// This setting is only supported on macOS.
		/// </summary>
		public bool wasOpenedAsHidden;
		/// <summary>
		/// true if the app was opened as a login item that should restore the state from
		/// the previous session.This indicates that the app should restore the windows
		/// that were open the last time the app was closed.This setting is only supported on macOS.
		/// </summary>
		public bool restoreState;

		public static LoginItemSettings FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new LoginItemSettings() {
				options = new JsonObject(json["options"]),
				openAtLogin = json.Bool("openAtLogin"),
				openAsHidden = json.Bool("openAsHidden"),
				wasOpenedAtLogin = json.Bool("wasOpenedAtLogin"),
				wasOpenedAsHidden = json.Bool("wasOpenedAsHidden"),
				restoreState = json.Bool("restoreState")
			};
		}
	}

	/// <summary>
	/// App.getLoginItemSettings() options.
	/// </summary>
	public class LoginItemSettingsOptions {
		/// <summary>
		/// The executable path to compare against. Defaults to process.execPath.
		/// </summary>
		public string path;
		/// <summary>
		/// The command-line arguments to compare against. Defaults to an empty array.
		/// </summary>
		public string[] args;
	}
}
