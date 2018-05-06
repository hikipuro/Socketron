using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class NodeJS : NodeModule {
		public ConsoleModule console;
		public ProcessModule process;

		public virtual void Init(SocketronClient client) {
			console = new ConsoleModule(client);
			process = new ProcessModule(client);
		}

		public T require<T>(string moduleName) where T: NodeModule, new() {
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
			int result = _ExecuteBlocking<int>(script);
			T module = new T() {
				_id = result
			};
			return module;
		}

		public int setTimeout(Action callback, int delay) {
			if (callback == null) {
				return -1;
			}
			string eventName = "setTimeout";
			CallbackItem item = null;
			item = _client.Callbacks.Add(_id, eventName, (object args) => {
				_client.Callbacks.RemoveItem(_id, eventName, item.CallbackId);
				callback?.Invoke();
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"{0};",
						"emit('__event',{1},{2},{3});",
					"}};",
					"var id = {4};",
					"return id;"
				),
				Script.RemoveObject("id"),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			long objectId = _ExecuteBlocking<long>(script);
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
			return _ExecuteBlocking<int>(script);
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
			_ExecuteJavaScript(script);
		}

		public int setInterval(Callback callback, int delay) {
			if (callback == null) {
				return -1;
			}
			string eventName = "setInterval";
			CallbackItem item = null;
			item = _client.Callbacks.Add(_id, eventName, callback);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"emit('__event',{0},{1},{2});",
					"}};",
					"return {3};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			long objectId = _ExecuteBlocking<long>(script);
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
			return _ExecuteBlocking<int>(script);
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
			_ExecuteJavaScript(script);
		}

		public int setImmediate(Callback callback) {
			if (callback == null) {
				return -1;
			}
			string eventName = "setImmediate";
			CallbackItem item = null;
			item = _client.Callbacks.Add(_id, eventName, callback);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"{0};",
						"emit('__event',{1},{2},{3});",
					"}};",
					"var id = {4};",
					"return id;"
				),
				Script.RemoveObject("id"),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			long objectId = _ExecuteBlocking<long>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var timer = setImmediate({0});",
					"return {1};"
				),
				Script.GetObject(objectId),
				Script.AddObject("timer")
			);
			return _ExecuteBlocking<int>(script);
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
			_ExecuteJavaScript(script);
		}
	}
}
