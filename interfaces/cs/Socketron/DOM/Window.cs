using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Window : EventTarget {
		Dictionary<int, int> _requestAnimationFrameCache = new Dictionary<int, int>();

		public Window() {
		}

		public Window this[int i] {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var w = {0};",
						"return w[{1}];"
					),
					Script.GetObject(API.id), i
				);
				int id = API._ExecuteBlocking<int>(script);
				return API.CreateObject<Window>(id);
			}
		}

		/*
		public int applicationCache {
			get { return API.GetProperty<int>("applicationCache"); }
		}
		//*/

		public bool closed {
			get { return API.GetProperty<bool>("closed"); }
		}

		/*
		public bool console {
			get { return API.GetProperty<bool>("console"); }
		}
		//*/

		/*
		public bool content {
			get { return API.GetProperty<bool>("content"); }
		}
		//*/

		/*
		public bool crypto {
			get { return API.GetProperty<bool>("crypto"); }
		}
		//*/

		public double devicePixelRatio {
			get { return API.GetProperty<double>("devicePixelRatio"); }
		}

		/*
		public bool dialogArguments {
			get { return API.GetProperty<bool>("dialogArguments"); }
		}
		//*/
		
		public Document document {
			get { return API.GetObject<Document>("document"); }
		}

		/*
		public bool frameElement {
			get { return API.GetProperty<bool>("frameElement"); }
		}
		//*/

		/*
		public bool frames {
			get { return API.GetProperty<bool>("frames"); }
		}
		//*/

		public bool fullScreen {
			get { return API.GetProperty<bool>("fullScreen"); }
		}

		/*
		public bool history {
			get { return API.GetProperty<bool>("history"); }
		}
		//*/

		public int innerHeight {
			get { return API.GetProperty<int>("innerHeight"); }
		}

		public int innerWidth {
			get { return API.GetProperty<int>("innerWidth"); }
		}

		public bool isSecureContext {
			get { return API.GetProperty<bool>("isSecureContext"); }
		}

		public int length {
			get { return API.GetProperty<int>("length"); }
		}

		/*
		public string location {
			get { return API.GetProperty<string>("location"); }
		}
		//*/

		/*
		public string locationbar {
			get { return API.GetProperty<string>("locationbar"); }
		}
		//*/

		/*
		public string localStorage {
			get { return API.GetProperty<string>("localStorage"); }
		}
		//*/

		/*
		public string menubar {
			get { return API.GetProperty<string>("menubar"); }
		}
		//*/

		/*
		public string messageManager {
			get { return API.GetProperty<string>("messageManager"); }
		}
		//*/

		public string name {
			get { return API.GetProperty<string>("name"); }
		}

		public Navigator navigator {
			get { return API.GetObject<Navigator>("navigator"); }
		}

		/*
		public string opener {
			get { return API.GetProperty<string>("opener"); }
		}
		//*/

		public int orientation {
			get { return API.GetProperty<int>("orientation"); }
		}

		public int outerHeight {
			get { return API.GetProperty<int>("outerHeight"); }
		}

		public int outerWidth {
			get { return API.GetProperty<int>("outerWidth"); }
		}

		public int pageXOffset {
			get { return API.GetProperty<int>("pageXOffset"); }
		}

		public int pageYOffset {
			get { return API.GetProperty<int>("pageYOffset"); }
		}

		/*
		public string sessionStorage {
			get { return API.GetProperty<string>("sessionStorage"); }
		}
		//*/

		/*
		public string parent {
			get { return API.GetProperty<string>("parent"); }
		}
		//*/

		/*
		public string performance {
			get { return API.GetProperty<string>("performance"); }
		}
		//*/

		/*
		public string personalbar {
			get { return API.GetProperty<string>("personalbar"); }
		}
		//*/

		/*
		public string returnValue {
			get { return API.GetProperty<string>("returnValue"); }
		}
		//*/

		/*
		public string screen {
			get { return API.GetProperty<string>("screen"); }
		}
		//*/

		public int screenX {
			get { return API.GetProperty<int>("screenX"); }
		}

		public int screenY {
			get { return API.GetProperty<int>("screenY"); }
		}

		/*
		public string scrollbars {
			get { return API.GetProperty<string>("scrollbars"); }
		}
		//*/

		public int scrollX {
			get { return API.GetProperty<int>("scrollX"); }
		}

		public int scrollY {
			get { return API.GetProperty<int>("scrollY"); }
		}

		public Window self {
			get { return this; }
		}

		/*
		public string sessionStorage {
			get { return API.GetProperty<string>("sessionStorage"); }
		}
		//*/

		/*
		public string speechSynthesis {
			get { return API.GetProperty<string>("speechSynthesis"); }
		}
		//*/

		/*
		public string status {
			get { return API.GetProperty<string>("status"); }
		}
		//*/

		/*
		public string statusbar {
			get { return API.GetProperty<string>("statusbar"); }
		}
		//*/

		/*
		public string toolbar {
			get { return API.GetProperty<string>("toolbar"); }
		}
		//*/

		public int top {
			get { return API.GetProperty<int>("top"); }
		}

		public Window window {
			get { return this; }
		}

		/*
		public string caches {
			get { return API.GetProperty<string>("caches"); }
		}
		//*/

		/*
		public string indexedDB {
			get { return API.GetProperty<string>("indexedDB"); }
		}
		//*/

		/*
		public string origin {
			get { return API.GetProperty<string>("origin"); }
		}
		//*/

		public void alert(string message) {
			string script = ScriptBuilder.Build(
				"{0}.alert({1});",
				Script.GetObject(API.id),
				message.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		public void blur() {
			string script = ScriptBuilder.Build(
				"{0}.blur();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public void cancelAnimationFrame(int requestID) {
			string script = ScriptBuilder.Build(
				"{0}.cancelAnimationFrame({1});",
				Script.GetObject(API.id),
				requestID
			);
			API.ExecuteJavaScript(script);
		}

		public void cancelIdleCallback(int handle) {
			string script = ScriptBuilder.Build(
				"{0}.cancelIdleCallback({1});",
				Script.GetObject(API.id),
				handle
			);
			API.ExecuteJavaScript(script);
		}

		[Obsolete]
		public void captureEvents(string eventType) {
			string script = ScriptBuilder.Build(
				"{0}.captureEvents({1});",
				Script.GetObject(API.id),
				eventType.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		public void clearImmediate(int immediateID) {
			string script = ScriptBuilder.Build(
				"{0}.clearImmediate({1});",
				Script.GetObject(API.id),
				immediateID
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

		public bool confirm(string message) {
			string script = ScriptBuilder.Build(
				"return {0}.confirm({1});",
				Script.GetObject(API.id),
				message.Escape()
			);
			return API._ExecuteBlocking<bool>(script);
		}

		public void focus() {
			string script = ScriptBuilder.Build(
				"{0}.focus();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		/*
		public void getComputedStyle() {
			string script = ScriptBuilder.Build(
				"{0}.getComputedStyle();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void getSelection() {
			string script = ScriptBuilder.Build(
				"{0}.getSelection();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		/*
		public void matchMedia() {
			string script = ScriptBuilder.Build(
				"{0}.matchMedia();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		public void moveBy(int deltaX, int deltaY) {
			string script = ScriptBuilder.Build(
				"{0}.moveBy({1},{2});",
				Script.GetObject(API.id),
				deltaX, deltaY
			);
			API.ExecuteJavaScript(script);
		}

		public void moveTo(int x, int y) {
			string script = ScriptBuilder.Build(
				"{0}.moveTo({1},{2});",
				Script.GetObject(API.id),
				x, y
			);
			API.ExecuteJavaScript(script);
		}

		public void open(string url, string windowName) {
			string script = ScriptBuilder.Build(
				"{0}.open({1},{2});",
				Script.GetObject(API.id),
				url.Escape(),
				windowName.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		public void postMessage(string message, string targetOrigin) {
			string script = ScriptBuilder.Build(
				"{0}.postMessage({1},{2});",
				Script.GetObject(API.id),
				message.Escape(),
				targetOrigin.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		public void print() {
			string script = ScriptBuilder.Build(
				"{0}.print();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public string prompt(string text, string value) {
			string script = ScriptBuilder.Build(
				"return {0}.prompt({1});",
				Script.GetObject(API.id),
				text.Escape(),
				value.Escape()
			);
			return API._ExecuteBlocking<string>(script);
		}

		[Obsolete]
		public void releaseEvents(string eventType) {
			string script = ScriptBuilder.Build(
				"{0}.releaseEvents({1});",
				Script.GetObject(API.id),
				eventType.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		public int requestAnimationFrame(Action callback) {
			if (callback == null) {
				return 0;
			}
			int key = callback.Method.GetHashCode();
			if (_requestAnimationFrameCache.ContainsKey(key)) {
				return API.ExecuteCachedScript<int>(_requestAnimationFrameCache[key]);
			}
			string eventName = "_requestAnimationFrame";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				//_client.Callbacks.RemoveItem(API.id, eventName, item.CallbackId);
				callback?.Invoke();
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.requestAnimationFrame({1});"
				),
				Script.GetObject(API.id),
				Script.GetObject(item.ObjectId)
			);
			int cacheId = API.CacheScript(script);
			_requestAnimationFrameCache.Add(key, cacheId);
			return API.ExecuteCachedScript<int>(cacheId);
		}

		public int requestIdleCallback(Action callback) {
			string eventName = "_requestIdleCallback";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				API.client.Callbacks.RemoveItem(API.id, eventName, item.CallbackId);
				callback?.Invoke();
			});
			string script = ScriptBuilder.Build(
				"{0}.requestIdleCallback({1});",
				Script.GetObject(API.id),
				Script.GetObject(item.ObjectId)
			);
			return API._ExecuteBlocking<int>(script);
		}

		public void resizeBy(int xDelta, int yDelta) {
			string script = ScriptBuilder.Build(
				"{0}.resizeBy({1},{2});",
				Script.GetObject(API.id),
				xDelta, yDelta
			);
			API.ExecuteJavaScript(script);
		}

		public void resizeTo(int iWidth, int iHeight) {
			string script = ScriptBuilder.Build(
				"{0}.resizeTo({1},{2});",
				Script.GetObject(API.id),
				iWidth, iHeight
			);
			API.ExecuteJavaScript(script);
		}

		public void scroll(int xCoord, int yCoord) {
			string script = ScriptBuilder.Build(
				"{0}.scroll({1},{2});",
				Script.GetObject(API.id),
				xCoord, yCoord
			);
			API.ExecuteJavaScript(script);
		}

		public void scrollBy(int x, int y) {
			string script = ScriptBuilder.Build(
				"{0}.scrollBy({1},{2});",
				Script.GetObject(API.id),
				x, y
			);
			API.ExecuteJavaScript(script);
		}

		public void scrollTo(int xCoord, int yCoord) {
			string script = ScriptBuilder.Build(
				"{0}.scrollTo({1},{2});",
				Script.GetObject(API.id),
				xCoord, yCoord
			);
			API.ExecuteJavaScript(script);
		}

		/*
		public void sizeToContent() {
			string script = ScriptBuilder.Build(
				"{0}.sizeToContent();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}
		//*/

		public void stop() {
			string script = ScriptBuilder.Build(
				"{0}.stop();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}
	}
}
