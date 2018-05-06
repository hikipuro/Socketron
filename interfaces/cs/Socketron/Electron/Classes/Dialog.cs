namespace Socketron.Electron {
	/// <summary>
	/// Display native system dialogs for opening and saving files, alerting, etc.
	/// <para>Process: Main</para>
	/// </summary>
	public class Dialog {
		/// <summary>
		/// ShowOpenDialog options.
		/// </summary>
		public class OpenDialogOptions {
			/// <summary>(optional)</summary>
			public string title;
			/// <summary>(optional)</summary>
			public string defaultPath;
			/// <summary>
			/// (optional) Custom label for the confirmation button,
			/// when left empty the default label will be used.
			/// </summary>
			public string buttonLabel;
			/// <summary>(optional)</summary>
			public FileFilter[] filters;
			/// <summary>
			/// (optional) Contains which features the dialog should use.
			/// </summary>
			public string[] properties;
			/// <summary>
			/// (optional) *macOS* Message to display above input boxes.
			/// </summary>
			public string message;
			/// <summary>
			/// (optional) *masOS* Create security scoped bookmarks
			/// when packaged for the Mac App Store.
			/// </summary>
			public bool? securityScopedBookmarks;

			/// <summary>
			/// Parse JSON text.
			/// </summary>
			/// <param name="text"></param>
			/// <returns></returns>
			public static OpenDialogOptions Parse(string text) {
				return JSON.Parse<OpenDialogOptions>(text);
			}

			/// <summary>
			/// Create JSON text.
			/// </summary>
			/// <returns></returns>
			public string Stringify() {
				return JSON.Stringify(this);
			}
		}

		/// <summary>
		/// ShowSaveDialog options.
		/// </summary>
		public class SaveDialogOptions {
			/// <summary>(optional)</summary>
			public string title;
			/// <summary>
			/// (optional) Absolute directory path, absolute file path,
			/// or file name to use by default.
			/// </summary>
			public string defaultPath;
			/// <summary>
			/// (optional) Custom label for the confirmation button,
			/// when left empty the default label will be used.
			/// </summary>
			public string buttonLabel;
			/// <summary>(optional)</summary>
			public FileFilter[] filters;
			/// <summary>
			/// (optional) *macOS* Message to display above text fields.
			/// </summary>
			public string message;
			/// <summary>
			/// (optional) *macOS* Custom label for the text displayed
			/// in front of the filename text field.
			/// </summary>
			public string nameFieldLabel;
			/// <summary>
			/// (optional) *macOS* Show the tags input box, defaults to true.
			/// </summary>
			public bool? showsTagField;
			/// <summary>
			/// (optional) *macOS* Create a security scoped bookmark
			/// when packaged for the Mac App Store. 
			/// </summary>
			public bool? securityScopedBookmarks;

			/// <summary>
			/// Parse JSON text.
			/// </summary>
			/// <param name="text"></param>
			/// <returns></returns>
			public static SaveDialogOptions Parse(string text) {
				return JSON.Parse<SaveDialogOptions>(text);
			}

			/// <summary>
			/// Create JSON text.
			/// </summary>
			/// <returns></returns>
			public string Stringify() {
				return JSON.Stringify(this);
			}
		}

		/// <summary>
		/// ShowMessageBox options.
		/// </summary>
		public class MessageBoxOptions {
			/// <summary>
			/// (optional) Can be "none", "info", "error", "question" or "warning".
			/// On Windows, "question" displays the same icon as "info",
			/// unless you set an icon using the "icon" option.
			/// On macOS, both "warning" and "error" display the same warning icon.
			/// </summary>
			public string type;
			/// <summary>
			/// (optional) Array of texts for buttons.
			/// On Windows, an empty array will result in one button labeled "OK".
			/// </summary>
			public string[] buttons;
			/// <summary>
			/// (optional) Index of the button in the buttons array
			/// which will be selected by default when the message box opens.
			/// </summary>
			public int? defaultId;
			/// <summary>
			/// (optional) Title of the message box, some platforms will not show it.
			/// </summary>
			public string title;
			/// <summary>
			/// Content of the message box.
			/// </summary>
			public string message;
			/// <summary>
			/// (optional) Extra information of the message.
			/// </summary>
			public string detail;
			/// <summary>
			/// (optional) If provided, the message box will include a checkbox with the given label.
			/// The checkbox state can be inspected only when using callback.
			/// </summary>
			public string checkboxLabel;
			/// <summary>
			/// (optional) Initial checked state of the checkbox. false by default.
			/// </summary>
			public bool? checkboxChecked;
			/// <summary>(optional)</summary>
			public NativeImage icon;
			/// <summary>
			/// (optional) The index of the button to be used to cancel the dialog, via the Esc key.
			/// </summary>
			public int? cancelId;
			/// <summary>
			///  (optional) On Windows Electron will try to figure out which one of the buttons
			///  are common buttons (like "Cancel" or "Yes"), and show the others as command links in the dialog.
			///  This can make the dialog appear in the style of modern Windows apps.
			///  If you don't like this behavior, you can set noLink to true.
			/// </summary>
			public bool? noLink;
			/// <summary>
			/// (optional) Normalize the keyboard access keys across platforms. 
			/// </summary>
			public bool? normalizeAccessKeys;

			/// <summary>
			/// Parse JSON text.
			/// </summary>
			/// <param name="text"></param>
			/// <returns></returns>
			public static MessageBoxOptions Parse(string text) {
				return JSON.Parse<MessageBoxOptions>(text);
			}

			/// <summary>
			/// Create JSON text.
			/// </summary>
			/// <returns></returns>
			public string Stringify() {
				return JSON.Stringify(this);
			}
		}

		/// <summary>
		/// ShowCertificateTrustDialog options.
		/// </summary>
		public class CertificateTrustDialogOptions {
			/// <summary>
			/// The certificate to trust/import.
			/// </summary>
			public Certificate certificate;
			/// <summary>
			/// The message to display to the user.
			/// </summary>
			public string message;

			/// <summary>
			/// Parse JSON text.
			/// </summary>
			/// <param name="text"></param>
			/// <returns></returns>
			public static CertificateTrustDialogOptions Parse(string text) {
				return JSON.Parse<CertificateTrustDialogOptions>(text);
			}

			/// <summary>
			/// Create JSON text.
			/// </summary>
			/// <returns></returns>
			public string Stringify() {
				return JSON.Stringify(this);
			}
		}

		/// <summary>
		/// OpenDialogOptions.properties values.
		/// </summary>
		public class Properties {
			/// <summary>
			/// Allow files to be selected.
			/// </summary>
			public const string openFile = "openFile";
			/// <summary>
			/// Allow directories to be selected.
			/// </summary>
			public const string openDirectory = "openDirectory";
			/// <summary>
			/// Allow multiple paths to be selected.
			/// </summary>
			public const string multiSelections = "multiSelections";
			/// <summary>
			/// Show hidden files in dialog.
			/// </summary>
			public const string showHiddenFiles = "showHiddenFiles";
			/// <summary>
			/// *macOS* Allow creating new directories from dialog.
			/// </summary>
			public const string createDirectory = "createDirectory";
			/// <summary>
			/// *Windows* Prompt for creation if the file path entered in the dialog does not exist. 
			/// </summary>
			public const string promptToCreate = "promptToCreate";
			/// <summary>
			/// *macOS* Disable the automatic alias (symlink) path resolution.
			/// </summary>
			public const string noResolveAliases = "noResolveAliases";
			/// <summary>
			/// *macOS* Treat packages, such as .app folders, as a directory instead of a file.
			/// </summary>
			public const string treatPackageAsDirectory = "treatPackageAsDirectory";
		}

		/// <summary>
		/// MessageBoxOptions.type values.
		/// </summary>
		public class Types {
			public const string none = "none";
			public const string info = "info";
			public const string error = "error";
			public const string question = "question";
			public const string warning = "warning";
		}
	}
}
