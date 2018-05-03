using System.Collections.Generic;

namespace Socketron {
	/// <summary>
	/// Display native system dialogs for opening and saving files, alerting, etc.
	/// <para>Process: Main</para>
	/// </summary>
	public class DialogClass : ElectronBase {
		public DialogClass(Socketron socketron) {
			_socketron = socketron;
		}

		/// <summary>
		/// Returns String[], an array of file paths chosen by the user,
		/// if the callback is provided it returns undefined.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="browserWindow"></param>
		/// <returns></returns>
		public List<string> ShowOpenDialog(Dialog.OpenDialogOptions options, BrowserWindow browserWindow = null) {
			// TODO: add callback option
			string script = string.Empty;
			if (browserWindow != null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"return electron.dialog.showOpenDialog(window,{1});"
					),
					browserWindow.ID,
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
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
			if (result == null) {
				return null;
			}
			List<string> paths = new List<string>();
			foreach (object item in result) {
				paths.Add(item as string);
			}
			return paths;
		}

		/// <summary>
		/// Returns String, the path of the file chosen by the user,
		/// if a callback is provided it returns undefined.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="browserWindow"></param>
		/// <returns></returns>
		public string ShowSaveDialog(Dialog.SaveDialogOptions options, BrowserWindow browserWindow = null) {
			// TODO: add callback option
			string script = string.Empty;
			if (browserWindow != null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"return electron.dialog.showSaveDialog(window,{1});"
					),
					browserWindow.ID,
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
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// Returns Integer, the index of the clicked button,
		/// if a callback is provided it returns undefined.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="browserWindow"></param>
		/// <returns></returns>
		public int ShowMessageBox(Dialog.MessageBoxOptions options, BrowserWindow browserWindow = null) {
			// TODO: add callback option
			string script = string.Empty;
			if (browserWindow != null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"return electron.dialog.showMessageBox(window,{1});"
					),
					browserWindow.ID,
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
			return _ExecuteJavaScriptBlocking<int>(script);
		}

		/// <summary>
		/// Displays a modal dialog that shows an error message.
		/// </summary>
		/// <param name="title">The title to display in the error box.</param>
		/// <param name="content">The text content to display in the error box.</param>
		public void ShowErrorBox(string title, string content) {
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
		public int ShowCertificateTrustDialog(Dialog.CertificateTrustDialogOptions options, BrowserWindow browserWindow = null) {
			// TODO: add callback option
			string script = string.Empty;
			if (browserWindow != null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var window = electron.BrowserWindow.fromId({0});",
						"return electron.dialog.showCertificateTrustDialog(window,{1});"
					),
					browserWindow.ID,
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
			return _ExecuteJavaScriptBlocking<int>(script);
		}
	}
}
