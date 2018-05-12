using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Display native system dialogs for opening and saving files, alerting, etc.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class DialogModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public DialogModule() {
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
