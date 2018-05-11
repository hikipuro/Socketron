﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Display native system dialogs for opening and saving files, alerting, etc.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class DialogModule : JSModule {
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
		/// <param name="browserWindow"></param>
		/// <returns></returns>
		public string[] showOpenDialog(Dialog.OpenDialogOptions options, BrowserWindow browserWindow = null) {
			object[] result;
			if (browserWindow == null) {
				result = API.Apply<object[]>("showOpenDialog", options);
			} else {
				result = API.Apply<object[]>("showOpenDialog", options, browserWindow);
			}
			if (result == null) {
				return null;
			}
			return Array.ConvertAll(result, value => Convert.ToString(value));
		}

		public string[] showOpenDialog(Dialog.OpenDialogOptions options,
			Action<string[], string[]> callback, BrowserWindow browserWindow = null) {
			if (callback == null) {
				return null;
			}
			string eventName = "_showOpenDialog";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				string[] filePaths = null;
				string[] bookmarks = null;
				if (args[0] != null) {
					filePaths = Array.ConvertAll(
						args[0] as object[],
						value => Convert.ToString(value)
					);
				}
				if (args[1] != null) {
					bookmarks = Array.ConvertAll(
						args[1] as object[],
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
		/// <param name="browserWindow"></param>
		/// <returns></returns>
		public string showSaveDialog(Dialog.SaveDialogOptions options, BrowserWindow browserWindow = null) {
			// TODO: add callback option
			if (browserWindow == null) {
				return API.Apply<string>("showSaveDialog", options);
			} else {
				return API.Apply<string>("showSaveDialog", options, browserWindow);
			}
		}

		/// <summary>
		/// Returns Integer, the index of the clicked button,
		/// if a callback is provided it returns undefined.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="browserWindow"></param>
		/// <returns></returns>
		public int showMessageBox(Dialog.MessageBoxOptions options, BrowserWindow browserWindow = null) {
			// TODO: add callback option
			if (browserWindow == null) {
				return API.Apply<int>("showMessageBox", options);
			} else {
				return API.Apply<int>("showMessageBox", options, browserWindow);
			}
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
		/// <param name="browserWindow"></param>
		/// <returns></returns>
		public int showCertificateTrustDialog(Dialog.CertificateTrustDialogOptions options, BrowserWindow browserWindow = null) {
			// TODO: add callback option
			if (browserWindow == null) {
				return API.Apply<int>("showCertificateTrustDialog", options);
			} else {
				return API.Apply<int>("showCertificateTrustDialog", options, browserWindow);
			}
		}
	}
}
