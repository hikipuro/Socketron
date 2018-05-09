using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Window : EventTarget {
		public Window() {
			_client = SocketronClient.GetCurrent();
		}

		public Window this[int i] {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var w = {0};",
						"return w[{1}];"
					),
					Script.GetObject(_id), i
				);
				int id = _ExecuteBlocking<int>(script);
				return new Window() {
					_id = id,
					_webContentsId = _webContentsId
				};
			}
		}

		/*
		public int applicationCache {
			get { return GetProperty<int>("applicationCache"); }
		}
		//*/

		public bool closed {
			get { return GetProperty<bool>("closed"); }
		}

		/*
		public bool console {
			get { return GetProperty<bool>("console"); }
		}
		//*/

		/*
		public bool content {
			get { return GetProperty<bool>("content"); }
		}
		//*/

		/*
		public bool crypto {
			get { return GetProperty<bool>("crypto"); }
		}
		//*/

		public double devicePixelRatio {
			get { return GetProperty<double>("devicePixelRatio"); }
		}

		/*
		public bool dialogArguments {
			get { return GetProperty<bool>("dialogArguments"); }
		}
		//*/
		
		public Document document {
			get { return GetObject<Document>("document"); }
		}

		/*
		public bool frameElement {
			get { return GetProperty<bool>("frameElement"); }
		}
		//*/

		/*
		public bool frames {
			get { return GetProperty<bool>("frames"); }
		}
		//*/

		public bool fullScreen {
			get { return GetProperty<bool>("fullScreen"); }
		}

		/*
		public bool history {
			get { return GetProperty<bool>("history"); }
		}
		//*/

		public int innerHeight {
			get { return GetProperty<int>("innerHeight"); }
		}

		public int innerWidth {
			get { return GetProperty<int>("innerWidth"); }
		}

		public bool isSecureContext {
			get { return GetProperty<bool>("isSecureContext"); }
		}

		public int length {
			get { return GetProperty<int>("length"); }
		}

		/*
		public string location {
			get { return GetProperty<string>("location"); }
		}
		//*/

		/*
		public string locationbar {
			get { return GetProperty<string>("locationbar"); }
		}
		//*/

		/*
		public string localStorage {
			get { return GetProperty<string>("localStorage"); }
		}
		//*/

		/*
		public string menubar {
			get { return GetProperty<string>("menubar"); }
		}
		//*/

		/*
		public string messageManager {
			get { return GetProperty<string>("messageManager"); }
		}
		//*/

		public string name {
			get { return GetProperty<string>("name"); }
		}

		public Navigator navigator {
			get { return GetObject<Navigator>("navigator"); }
		}

		/*
		public string opener {
			get { return GetProperty<string>("opener"); }
		}
		//*/

		public int orientation {
			get { return GetProperty<int>("orientation"); }
		}

		public int outerHeight {
			get { return GetProperty<int>("outerHeight"); }
		}

		public int outerWidth {
			get { return GetProperty<int>("outerWidth"); }
		}

		public int pageXOffset {
			get { return GetProperty<int>("pageXOffset"); }
		}

		public int pageYOffset {
			get { return GetProperty<int>("pageYOffset"); }
		}

		/*
		public string sessionStorage {
			get { return GetProperty<string>("sessionStorage"); }
		}
		//*/

		/*
		public string parent {
			get { return GetProperty<string>("parent"); }
		}
		//*/

		/*
		public string performance {
			get { return GetProperty<string>("performance"); }
		}
		//*/

		/*
		public string personalbar {
			get { return GetProperty<string>("personalbar"); }
		}
		//*/

		/*
		public string returnValue {
			get { return GetProperty<string>("returnValue"); }
		}
		//*/

		/*
		public string screen {
			get { return GetProperty<string>("screen"); }
		}
		//*/

		public int screenX {
			get { return GetProperty<int>("screenX"); }
		}

		public int screenY {
			get { return GetProperty<int>("screenY"); }
		}

		/*
		public string scrollbars {
			get { return GetProperty<string>("scrollbars"); }
		}
		//*/

		public int scrollX {
			get { return GetProperty<int>("scrollX"); }
		}

		public int scrollY {
			get { return GetProperty<int>("scrollY"); }
		}

		public Window self {
			get { return this; }
		}

		/*
		public string sessionStorage {
			get { return GetProperty<string>("sessionStorage"); }
		}
		//*/

		/*
		public string speechSynthesis {
			get { return GetProperty<string>("speechSynthesis"); }
		}
		//*/

		/*
		public string status {
			get { return GetProperty<string>("status"); }
		}
		//*/

		/*
		public string statusbar {
			get { return GetProperty<string>("statusbar"); }
		}
		//*/

		/*
		public string toolbar {
			get { return GetProperty<string>("toolbar"); }
		}
		//*/

		public int top {
			get { return GetProperty<int>("top"); }
		}

		public Window window {
			get { return this; }
		}

		/*
		public string caches {
			get { return GetProperty<string>("caches"); }
		}
		//*/

		/*
		public string indexedDB {
			get { return GetProperty<string>("indexedDB"); }
		}
		//*/

		/*
		public string origin {
			get { return GetProperty<string>("origin"); }
		}
		//*/

		public void alert(string message) {
			string script = ScriptBuilder.Build(
				"{0}.alert({1});",
				Script.GetObject(_id),
				message.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public void blur() {
			string script = ScriptBuilder.Build(
				"{0}.blur();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		public void cancelAnimationFrame(int requestID) {
			string script = ScriptBuilder.Build(
				"{0}.cancelAnimationFrame({1});",
				Script.GetObject(_id),
				requestID
			);
			_ExecuteJavaScript(script);
		}

		public void cancelIdleCallback(int handle) {
			string script = ScriptBuilder.Build(
				"{0}.cancelIdleCallback({1});",
				Script.GetObject(_id),
				handle
			);
			_ExecuteJavaScript(script);
		}

		[Obsolete]
		public void captureEvents(string eventType) {
			string script = ScriptBuilder.Build(
				"{0}.captureEvents({1});",
				Script.GetObject(_id),
				eventType.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public void clearImmediate(int immediateID) {
			string script = ScriptBuilder.Build(
				"{0}.clearImmediate({1});",
				Script.GetObject(_id),
				immediateID
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

		public bool confirm(string message) {
			string script = ScriptBuilder.Build(
				"return {0}.confirm({1});",
				Script.GetObject(_id),
				message.Escape()
			);
			return _ExecuteBlocking<bool>(script);
		}

		public void focus() {
			string script = ScriptBuilder.Build(
				"{0}.focus();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/*
		public void getComputedStyle() {
			string script = ScriptBuilder.Build(
				"{0}.getComputedStyle();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void getSelection() {
			string script = ScriptBuilder.Build(
				"{0}.getSelection();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}
		//*/

		/*
		public void matchMedia() {
			string script = ScriptBuilder.Build(
				"{0}.matchMedia();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}
		//*/

		public void moveBy(int deltaX, int deltaY) {
			string script = ScriptBuilder.Build(
				"{0}.moveBy({1},{2});",
				Script.GetObject(_id),
				deltaX, deltaY
			);
			_ExecuteJavaScript(script);
		}

		public void moveTo(int x, int y) {
			string script = ScriptBuilder.Build(
				"{0}.moveTo({1},{2});",
				Script.GetObject(_id),
				x, y
			);
			_ExecuteJavaScript(script);
		}

		public void open(string url, string windowName) {
			string script = ScriptBuilder.Build(
				"{0}.open({1},{2});",
				Script.GetObject(_id),
				url.Escape(),
				windowName.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public void postMessage(string message, string targetOrigin) {
			string script = ScriptBuilder.Build(
				"{0}.postMessage({1},{2});",
				Script.GetObject(_id),
				message.Escape(),
				targetOrigin.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public void print() {
			string script = ScriptBuilder.Build(
				"{0}.print();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		public string prompt(string text, string value) {
			string script = ScriptBuilder.Build(
				"return {0}.prompt({1});",
				Script.GetObject(_id),
				text.Escape(),
				value.Escape()
			);
			return _ExecuteBlocking<string>(script);
		}

		[Obsolete]
		public void releaseEvents(string eventType) {
			string script = ScriptBuilder.Build(
				"{0}.releaseEvents({1});",
				Script.GetObject(_id),
				eventType.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public int requestAnimationFrame(Action callback) {
			string eventName = "_requestAnimationFrame";
			CallbackItem item = null;
			item = _CreateCallbackItem(eventName, (object[] args) => {
				_client.Callbacks.RemoveItem(_id, eventName, item.CallbackId);
				callback?.Invoke();
			});
			string script = ScriptBuilder.Build(
				"{0}.requestAnimationFrame({1});",
				Script.GetObject(_id),
				Script.GetObject(item.ObjectId)
			);
			return _ExecuteBlocking<int>(script);
		}

		public int requestIdleCallback(Action callback) {
			string eventName = "_requestIdleCallback";
			CallbackItem item = null;
			item = _CreateCallbackItem(eventName, (object[] args) => {
				_client.Callbacks.RemoveItem(_id, eventName, item.CallbackId);
				callback?.Invoke();
			});
			string script = ScriptBuilder.Build(
				"{0}.requestIdleCallback({1});",
				Script.GetObject(_id),
				Script.GetObject(item.ObjectId)
			);
			return _ExecuteBlocking<int>(script);
		}

		public void resizeBy(int xDelta, int yDelta) {
			string script = ScriptBuilder.Build(
				"{0}.resizeBy({1},{2});",
				Script.GetObject(_id),
				xDelta, yDelta
			);
			_ExecuteJavaScript(script);
		}

		public void resizeTo(int iWidth, int iHeight) {
			string script = ScriptBuilder.Build(
				"{0}.resizeTo({1},{2});",
				Script.GetObject(_id),
				iWidth, iHeight
			);
			_ExecuteJavaScript(script);
		}

		public void scroll(int xCoord, int yCoord) {
			string script = ScriptBuilder.Build(
				"{0}.scroll({1},{2});",
				Script.GetObject(_id),
				xCoord, yCoord
			);
			_ExecuteJavaScript(script);
		}

		public void scrollBy(int x, int y) {
			string script = ScriptBuilder.Build(
				"{0}.scrollBy({1},{2});",
				Script.GetObject(_id),
				x, y
			);
			_ExecuteJavaScript(script);
		}

		public void scrollTo(int xCoord, int yCoord) {
			string script = ScriptBuilder.Build(
				"{0}.scrollTo({1},{2});",
				Script.GetObject(_id),
				xCoord, yCoord
			);
			_ExecuteJavaScript(script);
		}

		/*
		public void sizeToContent() {
			string script = ScriptBuilder.Build(
				"{0}.sizeToContent();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}
		//*/

		public void stop() {
			string script = ScriptBuilder.Build(
				"{0}.stop();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}
	}
}
