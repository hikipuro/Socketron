using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Document : Element {
		public Document() {
		}

		public string characterSet {
			get { return API.GetProperty<string>("characterSet"); }
		}

		public string charset {
			get { return API.GetProperty<string>("charset"); }
		}

		public string compatMode {
			get { return API.GetProperty<string>("compatMode"); }
		}

		public string contentType {
			get { return API.GetProperty<string>("contentType"); }
		}

		/*
		public string doctype {
			get { return API.GetProperty<string>("doctype"); }
		}
		//*/

		/*
		public string documentElement {
			get { return API.GetProperty<string>("documentElement"); }
		}
		//*/

		public string documentURI {
			get { return API.GetProperty<string>("documentURI"); }
		}

		/*
		public string domConfig {
			get { return API.GetProperty<string>("domConfig"); }
		}
		//*/

		[Obsolete]
		public bool fullscreen {
			get { return API.GetProperty<bool>("fullscreen"); }
		}

		public string hidden {
			get { return API.GetProperty<string>("hidden"); }
		}

		/*
		public string implementation {
			get { return API.GetProperty<string>("implementation"); }
		}
		//*/

		[Obsolete]
		public string inputEncoding {
			get { return API.GetProperty<string>("inputEncoding"); }
		}

		/*
		public string lastStyleSheetSet {
			get { return API.GetProperty<string>("lastStyleSheetSet"); }
		}
		//*/

		/*
		public string pointerLockElement {
			get { return API.GetProperty<string>("pointerLockElement"); }
		}
		//*/

		/*
		public string preferredStyleSheetSet {
			get { return API.GetProperty<string>("preferredStyleSheetSet"); }
		}
		//*/

		/*
		public string scrollingElement {
			get { return API.GetProperty<string>("scrollingElement"); }
		}
		//*/

		/*
		public string selectedStyleSheetSet {
			get { return API.GetProperty<string>("selectedStyleSheetSet"); }
		}
		//*/

		/*
		public string styleSheets {
			get { return API.GetProperty<string>("styleSheets"); }
		}
		//*/

		/*
		public string styleSheetSets {
			get { return API.GetProperty<string>("styleSheetSets"); }
		}
		//*/

		/*
		public string timeline {
			get { return API.GetProperty<string>("timeline"); }
		}
		//*/

		/*
		public string undoManager {
			get { return API.GetProperty<string>("undoManager"); }
		}
		//*/

		public string visibilityState {
			get { return API.GetProperty<string>("visibilityState"); }
		}

		// ParentNode interface

		/*
		public string children {
			get { return API.GetProperty<string>("children"); }
		}
		//*/

		/*
		public string firstElementChild {
			get { return API.GetProperty<string>("firstElementChild"); }
		}
		//*/

		/*
		public string lastElementChild {
			get { return API.GetProperty<string>("lastElementChild"); }
		}
		//*/

		/*
		public string firstElementChild {
			get { return API.GetProperty<string>("firstElementChild"); }
		}
		//*/

		/*
		public string childElementCount {
			get { return API.GetProperty<string>("childElementCount"); }
		}
		//*/

		// HTMLDocument interface

		/*
		public string activeElement {
			get { return API.GetProperty<string>("activeElement"); }
		}
		//*/

		public Element body {
			get { return API.GetObject<Element>("body"); }
		}

		public Element head {
			get { return API.GetObject<Element>("head"); }
		}

		public string title {
			get { return API.GetProperty<string>("title"); }
		}

		public string URL {
			get { return API.GetProperty<string>("URL"); }
		}

		public string bgColor {
			get { return API.GetProperty<string>("bgColor"); }
		}

		public Action onclick {
			set {
				if (value == null) {
					string script2 = ScriptBuilder.Build(
						"{0}.onclick = null;",
						Script.GetObject(API.id)
					);
					API.ExecuteJavaScript(script2);
					return;
				}
				CallbackItem item = null;
				item = API.CreateCallbackItem("_onclick", (object[] args) => {
					value?.Invoke();
				});
				string script = ScriptBuilder.Build(
					"{0}.onclick = {1};",
					Script.GetObject(API.id),
					Script.GetObject(item.ObjectId)
				);
				API.ExecuteJavaScript(script);
			}
		}

		public Element createElement(string tagName) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var element = {0}.createElement({1});",
					"return {2}"
				),
				Script.GetObject(API.id),
				tagName.Escape(),
				Script.AddObject("element")
			);
			int id = API._ExecuteBlocking<int>(script);
			if (tagName == "canvas") {
				return API.CreateObject<HTMLCanvasElement>(id);
			}
			return API.CreateObject<Element>(id);
		}

		public Element querySelector(string selectors) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var element = {0}.querySelector({1});",
					"return {2}"
				),
				Script.GetObject(API.id),
				selectors.Escape(),
				Script.AddObject("element")
			);
			int id = API._ExecuteBlocking<int>(script);
			return API.CreateObject<Element>(id);
		}


		// HTMLDocument interface

		[Obsolete]
		public void clear() {
			string script = ScriptBuilder.Build(
				"{0}.clear();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public void close() {
			string script = ScriptBuilder.Build(
				"{0}.close();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public bool execCommand(string aCommandName, bool aShowDefaultUI, string aValueArgument) {
			string script = ScriptBuilder.Build(
				"return {0}.execCommand({1},{2},{3});",
				Script.GetObject(API.id),
				aCommandName.Escape(),
				aShowDefaultUI.Escape(),
				aValueArgument.Escape()
			);
			return API._ExecuteBlocking<bool>(script);
		}

		/*
		public void getElementsByName(string name) {
			string script = ScriptBuilder.Build(
				"return {0}.getElementsByName({1});",
				Script.GetObject(API.id),
				name.Escape()
			);
			API._ExecuteBlocking<int>(script);
		}
		//*/

		/*
		public void getSelection() {
			string script = ScriptBuilder.Build(
				"return {0}.getSelection();",
				Script.GetObject(API.id)
			);
			API._ExecuteBlocking<int>(script);
		}
		//*/

		public bool hasFocus() {
			string script = ScriptBuilder.Build(
				"{0}.hasFocus();",
				Script.GetObject(API.id)
			);
			return API._ExecuteBlocking<bool>(script);
		}

		public bool open() {
			string script = ScriptBuilder.Build(
				"{0}.open();",
				Script.GetObject(API.id)
			);
			return API._ExecuteBlocking<bool>(script);
		}

		public bool queryCommandEnabled(string command) {
			string script = ScriptBuilder.Build(
				"return {0}.queryCommandEnabled({1});",
				Script.GetObject(API.id),
				command.Escape()
			);
			return API._ExecuteBlocking<bool>(script);
		}

		public bool queryCommandIndeterm(string command) {
			string script = ScriptBuilder.Build(
				"return {0}.queryCommandIndeterm({1});",
				Script.GetObject(API.id),
				command.Escape()
			);
			return API._ExecuteBlocking<bool>(script);
		}

		public bool queryCommandState(string command) {
			string script = ScriptBuilder.Build(
				"return {0}.queryCommandState({1});",
				Script.GetObject(API.id),
				command.Escape()
			);
			return API._ExecuteBlocking<bool>(script);
		}

		public bool queryCommandSupported(string command) {
			string script = ScriptBuilder.Build(
				"return {0}.queryCommandSupported({1});",
				Script.GetObject(API.id),
				command.Escape()
			);
			return API._ExecuteBlocking<bool>(script);
		}

		public bool queryCommandValue(string command) {
			string script = ScriptBuilder.Build(
				"return {0}.queryCommandValue({1});",
				Script.GetObject(API.id),
				command.Escape()
			);
			return API._ExecuteBlocking<bool>(script);
		}

		public void write(string text) {
			string script = ScriptBuilder.Build(
				"{0}.write({1});",
				Script.GetObject(API.id),
				text.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		public void writeln(string line) {
			string script = ScriptBuilder.Build(
				"{0}.writeln({1});",
				Script.GetObject(API.id),
				line.Escape()
			);
			API.ExecuteJavaScript(script);
		}
	}
}
