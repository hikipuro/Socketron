﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Socketron.Electron {
	/// <summary>
	/// Display native system dialogs for opening and saving files, alerting, etc.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class DialogModule : NodeModule {
		public const string Name = "Dialog";
		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public DialogModule(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// This method is used for internally by the library.
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
		/// Returns String[], an array of file paths chosen by the user,
		/// if the callback is provided it returns undefined.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="browserWindow"></param>
		/// <returns></returns>
		public List<string> showOpenDialog(Dialog.OpenDialogOptions options, BrowserWindow browserWindow = null) {
			string script = string.Empty;
			if (browserWindow != null) {
				script = ScriptBuilder.Build(
					"return {0}.showOpenDialog({1},{2});",
					Script.GetObject(_id),
					Script.GetObject(browserWindow._id),
					options.Stringify()
				);
			} else {
				script = ScriptBuilder.Build(
					"return {0}.showOpenDialog({1});",
					Script.GetObject(_id),
					options.Stringify()
				);
			}
			object[] result = _ExecuteBlocking<object[]>(script);
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
			Dialog.OpenDialogOptions options,
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
						"return {2}.showOpenDialog({3},{4},callback);"
					),
					Name.Escape(),
					_callbackListId,
					Script.GetObject(_id),
					Script.GetObject(browserWindow._id),
					options.Stringify()
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var callback = (filePaths, bookmarks) => {{",
							"emit('__event',{0},{1},filePaths,bookmarks);",
						"}};",
						"return {2}.showOpenDialog({3},callback);"
					),
					Name.Escape(),
					_callbackListId,
					Script.GetObject(_id),
					options.Stringify()
				);
			}
			_callbackListId++;
			_ExecuteJavaScript(script);
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
			string script = string.Empty;
			if (browserWindow != null) {
				script = ScriptBuilder.Build(
					"return {0}.showSaveDialog({1},{2});",
					Script.GetObject(_id),
					Script.GetObject(browserWindow._id),
					options.Stringify()
				);
			} else {
				script = ScriptBuilder.Build(
					"return {0}.showSaveDialog({1});",
					Script.GetObject(_id),
					options.Stringify()
				);
			}
			return _ExecuteBlocking<string>(script);
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
			string script = string.Empty;
			if (browserWindow != null) {
				script = ScriptBuilder.Build(
					"return {0}.showMessageBox({1},{2});",
					Script.GetObject(_id),
					Script.GetObject(browserWindow._id),
					options.Stringify()
				);
			} else {
				script = ScriptBuilder.Build(
					"return {0}.showMessageBox({1});",
					Script.GetObject(_id),
					options.Stringify()
				);
			}
			return _ExecuteBlocking<int>(script);
		}

		/// <summary>
		/// Displays a modal dialog that shows an error message.
		/// </summary>
		/// <param name="title">The title to display in the error box.</param>
		/// <param name="content">The text content to display in the error box.</param>
		public void showErrorBox(string title, string content) {
			string script = ScriptBuilder.Build(
				"{0}.showErrorBox({1},{2});",
				Script.GetObject(_id),
				title.Escape(),
				content.Escape()
			);
			_ExecuteJavaScript(script);
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
			string script = string.Empty;
			if (browserWindow != null) {
				script = ScriptBuilder.Build(
					"return {0}.showCertificateTrustDialog({1},{2});",
					Script.GetObject(_id),
					Script.GetObject(browserWindow._id),
					options.Stringify()
				);
			} else {
				script = ScriptBuilder.Build(
					"return {0}.showCertificateTrustDialog({1});",
					Script.GetObject(_id),
					options.Stringify()
				);
			}
			return _ExecuteBlocking<int>(script);
		}
	}
}
