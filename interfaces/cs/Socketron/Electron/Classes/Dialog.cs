using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Display native system dialogs for opening and saving files, alerting, etc.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Dialog : JSObject {
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
		public class OpenDialogProperties {
			/// <summary>
			/// Allow files to be selected.
			/// </summary>
			public const string OpenFile = "openFile";
			/// <summary>
			/// Allow directories to be selected.
			/// </summary>
			public const string OpenDirectory = "openDirectory";
			/// <summary>
			/// Allow multiple paths to be selected.
			/// </summary>
			public const string MultiSelections = "multiSelections";
			/// <summary>
			/// Show hidden files in dialog.
			/// </summary>
			public const string ShowHiddenFiles = "showHiddenFiles";
			/// <summary>
			/// *macOS* Allow creating new directories from dialog.
			/// </summary>
			public const string CreateDirectory = "createDirectory";
			/// <summary>
			/// *Windows* Prompt for creation if the file path entered in the dialog does not exist. 
			/// </summary>
			public const string PromptToCreate = "promptToCreate";
			/// <summary>
			/// *macOS* Disable the automatic alias (symlink) path resolution.
			/// </summary>
			public const string NoResolveAliases = "noResolveAliases";
			/// <summary>
			/// *macOS* Treat packages, such as .app folders, as a directory instead of a file.
			/// </summary>
			public const string TreatPackageAsDirectory = "treatPackageAsDirectory";
		}

		/// <summary>
		/// MessageBoxOptions.type values.
		/// </summary>
		public class MessageBoxType {
			public const string None = "none";
			public const string Info = "info";
			public const string Error = "error";
			public const string Question = "question";
			public const string Warning = "warning";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public Dialog() {
		}

		/// <summary>
		/// Returns String[], an array of file paths chosen by the user,
		/// if the callback is provided it returns undefined.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public string[] showOpenDialog(Dialog.OpenDialogOptions options) {
			return showOpenDialog(null, options);
		}

		public string[] showOpenDialog(BrowserWindow browserWindow, Dialog.OpenDialogOptions options) {
			object[] result = null;
			if (browserWindow == null) {
				result = API.Apply<object[]>("showOpenDialog", options);
			} else {
				result = API.Apply<object[]>("showOpenDialog", browserWindow, options);
			}
			if (result == null) {
				return null;
			}
			return Array.ConvertAll(result, value => Convert.ToString(value));
		}

		public string[] showOpenDialog(Dialog.OpenDialogOptions options, Action<string[], string[]> callback) {
			return showOpenDialog(null, options, callback);
		}

		public string[] showOpenDialog(BrowserWindow browserWindow, Dialog.OpenDialogOptions options,
			Action<string[], string[]> callback) {
			if (callback == null) {
				return showOpenDialog(browserWindow, options);
			}
			string eventName = "_showOpenDialog";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				string[] filePaths = null;
				string[] bookmarks = null;
				if (args[0] != null) {
					JSObject _filesPaths = API.CreateObject<JSObject>(args[0]);
					filePaths = Array.ConvertAll(
						_filesPaths.API.GetValue() as object[],
						value => Convert.ToString(value)
					);
				}
				if (args.Length > 1 && args[1] != null) {
					JSObject _bookmarks = API.CreateObject<JSObject>(args[0]);
					bookmarks = Array.ConvertAll(
						_bookmarks.API.GetValue() as object[],
						value => Convert.ToString(value)
					);
				}
				callback?.Invoke(filePaths, bookmarks);
			});
			if (browserWindow == null) {
				API.Apply("showOpenDialog", options, item);
			} else {
				API.Apply("showOpenDialog", browserWindow, options, item);
			}
			return null;
		}

		/// <summary>
		/// Returns String, the path of the file chosen by the user,
		/// if a callback is provided it returns undefined.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public string showSaveDialog(Dialog.SaveDialogOptions options) {
			return showSaveDialog(null, options);
		}

		public string showSaveDialog(BrowserWindow browserWindow, Dialog.SaveDialogOptions options) {
			if (browserWindow == null) {
				return API.Apply<string>("showSaveDialog", options);
			} else {
				return API.Apply<string>("showSaveDialog", browserWindow, options);
			}
		}

		public string showSaveDialog(Dialog.SaveDialogOptions options, Action<string, string> callback) {
			return showSaveDialog(null, options, callback);
		}

		public string showSaveDialog(BrowserWindow browserWindow, Dialog.SaveDialogOptions options,
			Action<string, string> callback) {
			if (callback == null) {
				return showSaveDialog(browserWindow, options);
			}
			string eventName = "_showSaveDialog";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				string filename = Convert.ToString(args[0]);
				string bookmark = Convert.ToString(args[1]);
				callback?.Invoke(filename, bookmark);
			});
			if (browserWindow == null) {
				API.Apply("showSaveDialog", options, item);
			} else {
				API.Apply("showSaveDialog", browserWindow, options, item);
			}
			return null;
		}

		/// <summary>
		/// Returns Integer, the index of the clicked button,
		/// if a callback is provided it returns undefined.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public int showMessageBox(Dialog.MessageBoxOptions options) {
			return showMessageBox(null, options);
		}

		public int showMessageBox(BrowserWindow browserWindow, Dialog.MessageBoxOptions options) {
			if (browserWindow == null) {
				return API.Apply<int>("showMessageBox", options);
			} else {
				return API.Apply<int>("showMessageBox", browserWindow, options);
			}
		}

		public int showMessageBox(Dialog.MessageBoxOptions options, Action<int, bool> callback) {
			return showMessageBox(null, options, callback);
		}

		public int showMessageBox(BrowserWindow browserWindow, Dialog.MessageBoxOptions options,
			Action<int, bool> callback) {
			if (callback == null) {
				return showMessageBox(browserWindow, options);
			}
			string eventName = "_showMessageBox";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				int response = Convert.ToInt32(args[0]);
				bool checkboxChecked = Convert.ToBoolean(args[1]);
				callback?.Invoke(response, checkboxChecked);
			});
			if (browserWindow == null) {
				API.Apply("showMessageBox", options, item);
			} else {
				API.Apply("showMessageBox", browserWindow, options, item);
			}
			return 0;
		}

		/// <summary>
		/// Displays a modal dialog that shows an error message.
		/// </summary>
		/// <param name="title">The title to display in the error box.</param>
		/// <param name="content">The text content to display in the error box.</param>
		public void showErrorBox(string title, string content) {
			API.Apply("showErrorBox", title, content);
		}

		/// <summary>
		/// *macOS Windows* 
		/// <para>
		/// On macOS, this displays a modal dialog that shows a message and certificate information,
		/// and gives the user the option of trusting/importing the certificate.
		/// If you provide a browserWindow argument the dialog will be attached to the parent window,
		/// making it modal.
		/// </para>
		/// <para>
		/// On Windows the options are more limited, due to the Win32 APIs used:
		/// The message argument is not used, as the OS provides its own confirmation dialog.
		/// The browserWindow argument is ignored since it is not possible to make this confirmation dialog modal.
		/// </para>
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public int showCertificateTrustDialog(Dialog.CertificateTrustDialogOptions options, BrowserWindow browserWindow = null) {
			return showCertificateTrustDialog(null, options);
		}

		public int showCertificateTrustDialog(BrowserWindow browserWindow, Dialog.CertificateTrustDialogOptions options) {
			if (browserWindow == null) {
				return API.Apply<int>("showCertificateTrustDialog", options);
			} else {
				return API.Apply<int>("showCertificateTrustDialog", browserWindow, options);
			}
		}

		public int showCertificateTrustDialog(Dialog.CertificateTrustDialogOptions options, Action callback) {
			return showCertificateTrustDialog(null, options, callback);
		}

		public int showCertificateTrustDialog(BrowserWindow browserWindow, Dialog.CertificateTrustDialogOptions options, Action callback) {
			if (callback == null) {
				return showCertificateTrustDialog(browserWindow, options);
			}
			string eventName = "_showCertificateTrustDialog";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			if (browserWindow == null) {
				API.Apply("showCertificateTrustDialog", options, item);
			} else {
				API.Apply("showCertificateTrustDialog", browserWindow, options, item);
			}
			return 0;
		}
	}
}
