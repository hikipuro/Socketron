using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Script.Serialization;

namespace Socketron {
	public class Dialog {
		public class Properties {
			public const string openFile = "openFile";
			public const string openDirectory = "openDirectory";
			public const string multiSelections = "multiSelections";
			public const string showHiddenFiles = "showHiddenFiles";
			public const string createDirectory = "createDirectory";
			public const string promptToCreate = "promptToCreate";
			public const string noResolveAliases = "noResolveAliases";
			public const string treatPackageAsDirectory = "treatPackageAsDirectory";
		}

		public class Type {
			public const string none = "none";
			public const string info = "info";
			public const string error = "error";
			public const string question = "question";
			public const string warning = "warning";
		}

		public class OpenDialogOptions {
			public string title;
			public string defaultPath;
			public string buttonLabel;
			//public FileFilter[] filters;
			public string[] properties;
			public string message;
			public bool? securityScopedBookmarks;

			public string Stringify() {
				var serializer = new JavaScriptSerializer();
				serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
				return serializer.Serialize(this);
			}
		}

		public class SaveDialogOptions {
			public string title;
			public string defaultPath;
			public string buttonLabel;
			//public FileFilter[] filters;
			public string[] properties;
			public string message;
			public string nameFieldLabel;
			public bool? showsTagField;
			public bool? securityScopedBookmarks;

			public string Stringify() {
				var serializer = new JavaScriptSerializer();
				serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
				return serializer.Serialize(this);
			}
		}

		public class MessageBoxOptions {
			public string type;
			public string[] buttons;
			public int? defaultId;
			public string title;
			public string message;
			public string detail;
			public string checkboxLabel;
			public bool? checkboxChecked;
			//public NativeImage icon;
			public int? cancelId;
			public bool? noLink;
			public bool? normalizeAccessKeys;

			public string Stringify() {
				var serializer = new JavaScriptSerializer();
				serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
				return serializer.Serialize(this);
			}
		}

		public class CertificateTrustDialogOptions {
			//public Certificate certificate;
			public string message;

			public string Stringify() {
				var serializer = new JavaScriptSerializer();
				serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
				return serializer.Serialize(this);
			}
		}

		public static List<string> ShowOpenDialog(Socketron socketron, OpenDialogOptions options, BrowserWindow browserWindow = null) {
			string optionsText = options.Stringify();
			string[] script = new[] {
				"return electron.dialog.showOpenDialog(" + optionsText + ");"
			};
			if (browserWindow != null) {
				script = new[] {
					"var window = electron.BrowserWindow.fromId(" + browserWindow.ID + ");",
					"return electron.dialog.showOpenDialog(window," + optionsText + ");"
				};
			}
			object[] result = _ExecuteJavaScriptBlocking<object[]>(socketron, script);
			if (result == null) {
				return null;
			}
			List<string> paths = new List<string>();
			foreach (object item in result) {
				paths.Add(item as string);
			}
			return paths;
		}

		public static string ShowSaveDialog(Socketron socketron, SaveDialogOptions options, BrowserWindow browserWindow = null) {
			string optionsText = options.Stringify();
			string[] script = new[] {
				"return electron.dialog.showSaveDialog(" + optionsText + ");"
			};
			if (browserWindow != null) {
				script = new[] {
					"var window = electron.BrowserWindow.fromId(" + browserWindow.ID + ");",
					"return electron.dialog.showSaveDialog(window," + optionsText + ");"
				};
			}
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
		}

		public static int ShowMessageBox(Socketron socketron, MessageBoxOptions options, BrowserWindow browserWindow = null) {
			string optionsText = options.Stringify();
			string[] script = new[] {
				"return electron.dialog.showMessageBox(" + optionsText + ");"
			};
			if (browserWindow != null) {
				script = new[] {
					"var window = electron.BrowserWindow.fromId(" + browserWindow.ID + ");",
					"return electron.dialog.showMessageBox(window," + optionsText + ");"
				};
			}
			return _ExecuteJavaScriptBlocking<int>(socketron, script);
		}

		public static void ShowErrorBox(Socketron socketron, string title, string content) {
			string[] script = new[] {
				"return electron.dialog.showErrorBox(" + title.Escape() + "," + content.Escape() + ");"
			};
			_ExecuteJavaScript(socketron, script, null, null);
		}

		public static int ShowCertificateTrustDialog(Socketron socketron, CertificateTrustDialogOptions options, BrowserWindow browserWindow = null) {
			string optionsText = options.Stringify();
			string[] script = new[] {
				"return electron.dialog.showCertificateTrustDialog(" + optionsText + ");"
			};
			if (browserWindow != null) {
				script = new[] {
					"var window = electron.BrowserWindow.fromId(" + browserWindow.ID + ");",
					"return electron.dialog.showCertificateTrustDialog(window," + optionsText + ");"
				};
			}
			return _ExecuteJavaScriptBlocking<int>(socketron, script);
		}

		protected static void _ExecuteJavaScript(Socketron socketron, string[] script, Callback success, Callback error) {
			socketron.Main.ExecuteJavaScript(script, success, error);
		}

		protected static T _ExecuteJavaScriptBlocking<T>(Socketron socketron, string[] script) {
			bool done = false;
			T value = default(T);

			_ExecuteJavaScript(socketron, script, (result) => {
				if (result == null) {
					done = true;
					return;
				}
				if (typeof(T) == typeof(double)) {
					//Console.WriteLine(result.GetType());
					if (result.GetType() == typeof(int)) {
						result = (double)(int)result;
					} else if (result.GetType() == typeof(Decimal)) {
						result = (double)(Decimal)result;
					}
				}
				value = (T)result;
				done = true;
			}, (result) => {
				Console.Error.WriteLine("error: Dialog._ExecuteJavaScriptBlocking");
				throw new InvalidOperationException(result as string);
				//done = true;
			});

			while (!done) {
				Thread.Sleep(10);
			}
			return value;
		}
	}
}
