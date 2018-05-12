using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Node.js object.
	/// This object provides the Node APIs at global scope.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class NodeJS : JSObject {
		public NodeModules.Console console;
		public NodeModules.Process process;

		public virtual void Init(SocketronClient client) {
			console = require<NodeModules.Console>("console");
			process = require<NodeModules.Process>("process");
		}

		public T require<T>(string moduleName) where T: JSObject, new() {
			if (moduleName == null) {
				return null;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var module = this.require({0});",
					"return {1};"
				),
				moduleName.Escape(),
				Script.AddObject("module")
			);
			int result = API._ExecuteBlocking<int>(script);
			T module = new T();
			module.API.client = API.client;
			module.API.id = result;
			return module;
		}

		public int setTimeout(Action callback, int delay) {
			if (callback == null) {
				return -1;
			}
			string eventName = "setTimeout";
			CallbackItem item = null;
			item = API.client.Callbacks.Add(API.id, eventName, (object[] args) => {
				API.RemoveCallbackItem(eventName, item);
				callback?.Invoke();
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"{0};",
						"this.emit('__event',{1},{2},{3});",
					"}};",
					"var id = {4};",
					"return id;"
				),
				Script.RemoveObject("id"),
				API.id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			int objectId = API._ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var timer = setTimeout({0},{1});",
					"return {2};"
				),
				Script.GetObject(objectId),
				delay,
				Script.AddObject("timer")
			);
			return API._ExecuteBlocking<int>(script);
		}

		public void clearTimeout(int timeoutObject) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var timer = {0};",
					"clearTimeout(timer);",
					"{1};"
				),
				Script.GetObject(timeoutObject),
				Script.RemoveObject(timeoutObject)
			);
			API.ExecuteJavaScript(script);
		}

		public int setInterval(JSCallback callback, int delay) {
			if (callback == null) {
				return -1;
			}
			string eventName = "setInterval";
			CallbackItem item = null;
			item = API.client.Callbacks.Add(API.id, eventName, callback);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"this.emit('__event',{0},{1},{2});",
					"}};",
					"return {3};"
				),
				API.id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			int objectId = API._ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var timer = setInterval({0},{1});",
					"return {2};"
				),
				Script.GetObject(objectId),
				delay,
				Script.AddObject("timer")
			);
			return API._ExecuteBlocking<int>(script);
		}

		public void clearInterval(int intervalObject) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var timer = {0};",
					"clearInterval(timer);",
					"{1};"
				),
				Script.GetObject(intervalObject),
				Script.RemoveObject(intervalObject)
			);
			API.ExecuteJavaScript(script);
		}

		public int setImmediate(JSCallback callback) {
			if (callback == null) {
				return -1;
			}
			string eventName = "setImmediate";
			CallbackItem item = null;
			item = API.client.Callbacks.Add(API.id, eventName, callback);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"{0};",
						"this.emit('__event',{1},{2},{3});",
					"}};",
					"var id = {4};",
					"return id;"
				),
				Script.RemoveObject("id"),
				API.id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			int objectId = API._ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var timer = setImmediate({0});",
					"return {1};"
				),
				Script.GetObject(objectId),
				Script.AddObject("timer")
			);
			return API._ExecuteBlocking<int>(script);
		}

		public void clearImmediate(int immediate) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var timer = {0};",
					"clearImmediate(timer);",
					"{1};"
				),
				Script.GetObject(immediate),
				Script.RemoveObject(immediate)
			);
			API.ExecuteJavaScript(script);
		}
	}
}
