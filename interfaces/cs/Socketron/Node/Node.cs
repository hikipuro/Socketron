using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Node : ElectronBase {
		public const string Name = "Node";
		public NodeConsole console;
		public ProcessClass process;
		public OSModule os;
		public PathModule path;
		public URLModule url;

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Delegate> _callbackList = new Dictionary<ushort, Delegate>();

		public static Delegate GetCallbackFromId(ushort id) {
			if (!_callbackList.ContainsKey(id)) {
				return null;
			}
			return _callbackList[id];
		}

		public virtual void Init(Socketron socketron) {
			console = new NodeConsole(socketron);
			process = new ProcessClass(socketron);
			os = new OSModule(socketron);
			path = new PathModule(socketron);
			url = new URLModule(socketron);
		}

		/*
		public static string GetDirname(Socketron socketron) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return global.__dirname;"
				)
			);
			return _ExecuteJavaScriptBlocking<string>(socketron, script);
		}
		//*/

		public int require(string module) {
			if (module == null) {
				return 0;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var module = this.require({0});",
					"return {1};"
				),
				module.Escape(),
				Script.AddObject("module")
			);
			return _ExecuteJavaScriptBlocking<int>(script);
		}

		public int setTimeout(Callback callback, int delay) {
			if (callback == null) {
				return -1;
			}
			_callbackList.Add(_callbackListId, callback);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"{4};",
						"emit('__event',{1},{2});",
					"}}",
					"var timer = setTimeout(callback,{0});",
					"var id = {3};",
					"return id;"
				),
				delay,
				Name.Escape(),
				_callbackListId,
				Script.AddObject("timer"),
				Script.RemoveObject("id")
			);
			_callbackListId++;
			return _ExecuteJavaScriptBlocking<int>(script);
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
			_callbackList.Add(_callbackListId, callback);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"emit('__event',{1},{2});",
					"}}",
					"var timer = setInterval(callback,{0});",
					"var id = {3};",
					"return id;"
				),
				delay,
				Name.Escape(),
				_callbackListId,
				Script.AddObject("timer")
			);
			_callbackListId++;
			return _ExecuteJavaScriptBlocking<int>(script);
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
			_callbackList.Add(_callbackListId, callback);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"{3};",
						"emit('__event',{0},{1});",
					"}}",
					"var timer = setImmediate(callback);",
					"var id = {2};",
					"return id;"
				),
				Name.Escape(),
				_callbackListId,
				Script.AddObject("timer"),
				Script.RemoveObject("id")
			);
			_callbackListId++;
			return _ExecuteJavaScriptBlocking<int>(script);
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
