using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Socketron {
	/// <summary>
	/// Display native system dialogs for opening and saving files, alerting, etc.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Dialog : NodeModule {
		public const string Name = "Dialog";
		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Callback GetCallbackFromId(ushort id) {
			if (!_callbackList.ContainsKey(id)) {
				return null;
			}
			return _callbackList[id];
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
		public class Type {
			public const string none = "none";
			public const string info = "info";
			public const string error = "error";
			public const string question = "question";
			public const string warning = "warning";
		}

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
		/// Returns String[], an array of file paths chosen by the user,
		/// if the callback is provided it returns undefined.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="browserWindow"></param>
		/// <returns></returns>
		public List<string> showOpenDialog(OpenDialogOptions options, BrowserWindow browserWindow = null) {
			string script = string.Empty;
			if (browserWindow != null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"return electron.dialog.showOpenDialog(window,{1});"
					),
					browserWindow._id,
					options.Stringify()
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return electron.dialog.showOpenDialog({0});"
					),
					options.Stringify()
				);
			}
			object[] result = SocketronClient.ExecuteBlocking<object[]>(script);
			if (result == null) {
				return null;
			}
			List<string> paths = new List<string>();
			foreach (object item in result) {
				paths.Add(item as string);
			}
			return paths;
		}

		public List<string> showOpenDialog(
			OpenDialogOptions options,
			Action<string[], string[]> callback,
			BrowserWindow browserWindow = null) {
			if (callback == null) {
				return null;
			}
			ushort callbackId = _callbackListId;
			_callbackList.Add(_callbackListId, (object args) => {
				_callbackList.Remove(callbackId);
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				string[] filePaths = null;
				string[] bookmarks = null;
				if (argsList[0] != null) {
					filePaths = (argsList[0] as object[]).Cast<string>().ToArray();
				}
				if (argsList[1] != null) {
					bookmarks = (argsList[1] as object[]).Cast<string>().ToArray();
				}
				callback?.Invoke(filePaths, bookmarks);
			});
			string script = string.Empty;
			if (browserWindow != null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var callback = (filePaths, bookmarks) => {{",
							"emit('__event',{0},{1},filePaths,bookmarks);",
						"}};",
						"var window = electron.BrowserWindow.fromId({2});",
						"return electron.dialog.showOpenDialog(window,{3},callback);"
					),
					Name.Escape(),
					_callbackListId,
					browserWindow._id,
					options.Stringify()
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var callback = (filePaths, bookmarks) => {{",
							"emit('__event',{0},{1},filePaths,bookmarks);",
						"}};",
						"return electron.dialog.showOpenDialog({2},callback);"
					),
					Name.Escape(),
					_callbackListId,
					options.Stringify()
				);
			}
			_callbackListId++;
			SocketronClient.Execute(script);
			return null;
		}

		/// <summary>
		/// Returns String, the path of the file chosen by the user,
		/// if a callback is provided it returns undefined.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="browserWindow"></param>
		/// <returns></returns>
		public string showSaveDialog(SaveDialogOptions options, BrowserWindow browserWindow = null) {
			// TODO: add callback option
			string script = string.Empty;
			if (browserWindow != null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"return electron.dialog.showSaveDialog(window,{1});"
					),
					browserWindow._id,
					options.Stringify()
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return electron.dialog.showSaveDialog({0});"
					),
					options.Stringify()
				);
			}
			return SocketronClient.ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns Integer, the index of the clicked button,
		/// if a callback is provided it returns undefined.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="browserWindow"></param>
		/// <returns></returns>
		public int showMessageBox(MessageBoxOptions options, BrowserWindow browserWindow = null) {
			// TODO: add callback option
			string script = string.Empty;
			if (browserWindow != null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"return electron.dialog.showMessageBox(window,{1});"
					),
					browserWindow._id,
					options.Stringify()
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return electron.dialog.showMessageBox({0});"
					),
					options.Stringify()
				);
			}
			return SocketronClient.ExecuteBlocking<int>(script);
		}

		/// <summary>
		/// Displays a modal dialog that shows an error message.
		/// </summary>
		/// <param name="title">The title to display in the error box.</param>
		/// <param name="content">The text content to display in the error box.</param>
		public void showErrorBox(string title, string content) {
			string script = ScriptBuilder.Build(
				"electron.dialog.showErrorBox({0},{1});",
				title.Escape(),
				content.Escape()
			);
			SocketronClient.Execute(script);
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
		/// <param name="browserWindow"></param>
		/// <returns></returns>
		public int showCertificateTrustDialog(CertificateTrustDialogOptions options, BrowserWindow browserWindow = null) {
			// TODO: add callback option
			string script = string.Empty;
			if (browserWindow != null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"return electron.dialog.showCertificateTrustDialog(window,{1});"
					),
					browserWindow._id,
					options.Stringify()
				);
			} else {
				script = ScriptBuilder.Build(
					"return electron.dialog.showCertificateTrustDialog({0});",
					options.Stringify()
				);
			}
			return SocketronClient.ExecuteBlocking<int>(script);
		}
	}
}
