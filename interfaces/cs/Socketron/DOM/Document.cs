using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Document : Element {
		public Document() {
			_client = SocketronClient.GetCurrent();
		}

		public string characterSet {
			get { return GetProperty<string>("characterSet"); }
		}

		public string charset {
			get { return GetProperty<string>("charset"); }
		}

		public string compatMode {
			get { return GetProperty<string>("compatMode"); }
		}

		public string contentType {
			get { return GetProperty<string>("contentType"); }
		}

		/*
		public string doctype {
			get { return GetProperty<string>("doctype"); }
		}
		//*/

		/*
		public string documentElement {
			get { return GetProperty<string>("documentElement"); }
		}
		//*/

		public string documentURI {
			get { return GetProperty<string>("documentURI"); }
		}

		/*
		public string domConfig {
			get { return GetProperty<string>("domConfig"); }
		}
		//*/

		[Obsolete]
		public bool fullscreen {
			get { return GetProperty<bool>("fullscreen"); }
		}

		public string hidden {
			get { return GetProperty<string>("hidden"); }
		}

		/*
		public string implementation {
			get { return GetProperty<string>("implementation"); }
		}
		//*/

		[Obsolete]
		public string inputEncoding {
			get { return GetProperty<string>("inputEncoding"); }
		}

		/*
		public string lastStyleSheetSet {
			get { return GetProperty<string>("lastStyleSheetSet"); }
		}
		//*/

		/*
		public string pointerLockElement {
			get { return GetProperty<string>("pointerLockElement"); }
		}
		//*/

		/*
		public string preferredStyleSheetSet {
			get { return GetProperty<string>("preferredStyleSheetSet"); }
		}
		//*/

		/*
		public string scrollingElement {
			get { return GetProperty<string>("scrollingElement"); }
		}
		//*/

		/*
		public string selectedStyleSheetSet {
			get { return GetProperty<string>("selectedStyleSheetSet"); }
		}
		//*/

		/*
		public string styleSheets {
			get { return GetProperty<string>("styleSheets"); }
		}
		//*/

		/*
		public string styleSheetSets {
			get { return GetProperty<string>("styleSheetSets"); }
		}
		//*/

		/*
		public string timeline {
			get { return GetProperty<string>("timeline"); }
		}
		//*/

		/*
		public string undoManager {
			get { return GetProperty<string>("undoManager"); }
		}
		//*/

		public string visibilityState {
			get { return GetProperty<string>("visibilityState"); }
		}

		// ParentNode interface

		/*
		public string children {
			get { return GetProperty<string>("children"); }
		}
		//*/

		/*
		public string firstElementChild {
			get { return GetProperty<string>("firstElementChild"); }
		}
		//*/

		/*
		public string lastElementChild {
			get { return GetProperty<string>("lastElementChild"); }
		}
		//*/

		/*
		public string firstElementChild {
			get { return GetProperty<string>("firstElementChild"); }
		}
		//*/

		/*
		public string childElementCount {
			get { return GetProperty<string>("childElementCount"); }
		}
		//*/

		// HTMLDocument interface

		/*
		public string activeElement {
			get { return GetProperty<string>("activeElement"); }
		}
		//*/

		public string title {
			get { return GetProperty<string>("title"); }
		}

		public string bgColor {
			get { return GetProperty<string>("bgColor"); }
		}

		public Action onclick {
			set {
				if (value == null) {
					string script2 = ScriptBuilder.Build(
						"{0}.onclick = null;",
						Script.GetObject(_id)
					);
					_ExecuteJavaScript(script2);
					return;
				}
				CallbackItem item = null;
				item = _CreateCallbackItem("_onclick", (object[] args) => {
					value?.Invoke();
				});
				string script = ScriptBuilder.Build(
					"{0}.onclick = {1};",
					Script.GetObject(_id),
					Script.GetObject(item.ObjectId)
				);
				_ExecuteJavaScript(script);
			}
		}


		// HTMLDocument interface

		[Obsolete]
		public void clear() {
			string script = ScriptBuilder.Build(
				"{0}.clear();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		public void close() {
			string script = ScriptBuilder.Build(
				"{0}.close();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		public bool execCommand(string aCommandName, bool aShowDefaultUI, string aValueArgument) {
			string script = ScriptBuilder.Build(
				"return {0}.execCommand({1},{2},{3});",
				Script.GetObject(_id),
				aCommandName.Escape(),
				aShowDefaultUI.Escape(),
				aValueArgument.Escape()
			);
			return _ExecuteBlocking<bool>(script);
		}

		/*
		public void getElementsByName(string name) {
			string script = ScriptBuilder.Build(
				"return {0}.getElementsByName({1});",
				Script.GetObject(_id),
				name.Escape()
			);
			_ExecuteBlocking<int>(script);
		}
		//*/

		/*
		public void getSelection() {
			string script = ScriptBuilder.Build(
				"return {0}.getSelection();",
				Script.GetObject(_id)
			);
			_ExecuteBlocking<int>(script);
		}
		//*/

		public bool hasFocus() {
			string script = ScriptBuilder.Build(
				"{0}.hasFocus();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		public bool open() {
			string script = ScriptBuilder.Build(
				"{0}.open();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		public bool queryCommandEnabled(string command) {
			string script = ScriptBuilder.Build(
				"return {0}.queryCommandEnabled({1});",
				Script.GetObject(_id),
				command.Escape()
			);
			return _ExecuteBlocking<bool>(script);
		}

		public bool queryCommandIndeterm(string command) {
			string script = ScriptBuilder.Build(
				"return {0}.queryCommandIndeterm({1});",
				Script.GetObject(_id),
				command.Escape()
			);
			return _ExecuteBlocking<bool>(script);
		}

		public bool queryCommandState(string command) {
			string script = ScriptBuilder.Build(
				"return {0}.queryCommandState({1});",
				Script.GetObject(_id),
				command.Escape()
			);
			return _ExecuteBlocking<bool>(script);
		}

		public bool queryCommandSupported(string command) {
			string script = ScriptBuilder.Build(
				"return {0}.queryCommandSupported({1});",
				Script.GetObject(_id),
				command.Escape()
			);
			return _ExecuteBlocking<bool>(script);
		}

		public bool queryCommandValue(string command) {
			string script = ScriptBuilder.Build(
				"return {0}.queryCommandValue({1});",
				Script.GetObject(_id),
				command.Escape()
			);
			return _ExecuteBlocking<bool>(script);
		}

		public void write(string text) {
			string script = ScriptBuilder.Build(
				"{0}.write({1});",
				Script.GetObject(_id),
				text.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public void writeln(string line) {
			string script = ScriptBuilder.Build(
				"{0}.writeln({1});",
				Script.GetObject(_id),
				line.Escape()
			);
			_ExecuteJavaScript(script);
		}
	}
}
