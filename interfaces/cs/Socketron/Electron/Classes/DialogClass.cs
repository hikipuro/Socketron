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
	public class DialogClass : NodeBase {
		public const string Name = "DialogClass";
		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		public DialogClass(Socketron socketron) {
			_socketron = socketron;
		}

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
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"return electron.dialog.showOpenDialog(window,{1});"
					),
					browserWindow.id,
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
			BrowserWindow browserWindow = null)
		{
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
					browserWindow.id,
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
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"return electron.dialog.showSaveDialog(window,{1});"
					),
					browserWindow.id,
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
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"return electron.dialog.showMessageBox(window,{1});"
					),
					browserWindow.id,
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
			return _ExecuteBlocking<int>(script);
		}

		/// <summary>
		/// Displays a modal dialog that shows an error message.
		/// </summary>
		/// <param name="title">The title to display in the error box.</param>
		/// <param name="content">The text content to display in the error box.</param>
		public void showErrorBox(string title, string content) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.dialog.showErrorBox({0},{1});"
				),
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
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"return electron.dialog.showCertificateTrustDialog(window,{1});"
					),
					browserWindow.id,
					options.Stringify()
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return electron.dialog.showCertificateTrustDialog({0});"
					),
					options.Stringify()
				);
			}
			return _ExecuteBlocking<int>(script);
		}
	}
}
