using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class NodeModule : IDisposable {
		/// <summary>
		/// This id is used for internally by the library.
		/// </summary>
		public int _id;
		protected static List<NodeModule> _modules;
		protected SocketronClient _client;
		protected bool _disposeManually = false;

		/// <summary>
		/// Static constructor.
		/// This constructor is used for internally by the library.
		/// </summary>
		static NodeModule() {
			_modules = new List<NodeModule>();
		}

		public NodeModule() {
			//Console.WriteLine("NodeModule ###: " + GetType().Name);
			_modules.Add(this);
		}

		~NodeModule() {
			if (_id <= 0) {
				return;
			}
			Dispose();
		}

		/// <summary>
		/// This method is used for internally by the library.
		/// </summary>
		public static void DisposeAll() {
			foreach (NodeModule module in _modules) {
				if (module._id <= 0) {
					continue;
				}
				module.Dispose();
			}
			_modules.Clear();
		}

		/// <summary>
		/// This method is used for internally by the library.
		/// </summary>
		public void Dispose() {
			if (_disposeManually) {
				return;
			}
			if (_client == null) {
				return;
			}
			Console.WriteLine("NodeModule.Dispose ###: " + GetType().Name + ", " + _id);
			_client.RemoveObject(_id);
			_id = 0;
		}

		/// <summary>
		/// Execute JavaScript.
		/// <para>
		/// "self" keyword can be used to refer to the current object.
		/// </para>
		/// </summary>
		/// <param name="script"></param>
		public void Execute(string script) {
			string code = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var self = {0};",
					"{1};"
				),
				Script.GetObject( _id),
				script
			);
			_ExecuteJavaScript(code);
		}

		/// <summary>
		/// Listens to channel, when a new message arrives listener
		/// would be called with listener(event, args...).
		/// </summary>
		/// <param name="eventName"></param>
		/// <param name="callback"></param>
		public void on(string eventName, Callback callback) {
			if (callback == null) {
				return;
			}
			CallbackItem item = _on(eventName, callback);
			string script = ScriptBuilder.Build(
				"{0}.on({1},{2});",
				Script.GetObject(_id),
				eventName.Escape(),
				Script.GetObject(item.ObjectId)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Adds a one time listener function for the event.
		/// <para>
		/// This listener is invoked only the next time a message is sent to channel,
		/// after which it is removed.
		/// </summary>
		/// <param name="eventName"></param>
		/// <param name="callback"></param>
		public void once(string eventName, Callback callback) {
			if (callback == null) {
				return;
			}
			CallbackItem item = _once(eventName, callback);
			string script = ScriptBuilder.Build(
				"{0}.once({1},{2});",
				Script.GetObject(_id),
				eventName.Escape(),
				Script.GetObject(item.ObjectId)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Removes the specified listener from the listener array for the specified channel.
		/// </summary>
		/// <param name="eventName"></param>
		/// <param name="callback"></param>
		public void removeListener(string eventName, Callback callback) {
			if (callback == null) {
				return;
			}
			CallbackItem item = _client.Callbacks.GetItem(_id, eventName, callback);
			string script = ScriptBuilder.Build(
				"{0}.removeListener({1},{2});",
				Script.GetObject(_id),
				eventName.Escape(),
				Script.GetObject(item.ObjectId)
			);
			_ExecuteJavaScript(script);
			_client.Callbacks.RemoveItem(_id, eventName, item.CallbackId);
		}

		/// <summary>
		/// Removes all listeners, or those of the specified channel.
		/// </summary>
		/// <param name="eventName"></param>
		public void removeAllListeners(string eventName) {
			string script = ScriptBuilder.Build(
				"{0}.removeAllListeners({1});",
				Script.GetObject(_id),
				eventName.Escape()
			);
			_ExecuteJavaScript(script);
			_client.Callbacks.RemoveEvents(_id, eventName);
		}

		/// <summary>
		/// Removes all listeners.
		/// </summary>
		public void removeAllListeners() {
			string script = ScriptBuilder.Build(
				"{0}.removeAllListeners();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
			_client.Callbacks.RemoveInstanceEvents(_id);
		}

		protected CallbackItem _on(string eventName, Callback callback) {
			CallbackItem item = _client.Callbacks.Add(_id, eventName, callback);
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
			return item;
		}

		protected CallbackItem _once(string eventName, Callback callback) {
			CallbackItem item = _client.Callbacks.Add(_id, eventName, callback);
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
			return item;
		}

		protected void _ExecuteJavaScript(string script) {
			_client.Main.ExecuteJavaScript(script);
		}

		protected void _ExecuteJavaScript(string script, Callback success) {
			_client.Main.ExecuteJavaScript(script, success);
		}

		protected void _ExecuteJavaScript(string script, Callback success, Callback error) {
			_client.Main.ExecuteJavaScript(script, success, error);
		}

		protected T _ExecuteBlocking<T>(string script) {
			return _client.ExecuteJavaScriptBlocking<T>(script);
		}

		protected static ScriptHelper Script = new ScriptHelper();
		protected class ScriptHelper {
			public string GetObject(long id) {
				return string.Format("this._objRefs[{0}]", id);
			}
			public string GetObjectList(NodeModule[] list) {
				if (list == null) {
					return "null";
				}
				List<string> result = new List<string>();
				foreach (NodeModule obj in list) {
					result.Add(GetObject(obj._id));
				}
				return string.Join(",", result.ToArray());
			}
			public string GetObjectList(object[] list) {
				if (list == null) {
					return "null";
				}
				List<string> result = new List<string>();
				foreach (object obj in list) {
					result.Add(obj.ToString());
				}
				return string.Join(",", result.ToArray());
			}
			public string AddObject(string name) {
				return string.Format("this._addObjectReference({0})", name);
			}
			public string RemoveObject(long id) {
				return string.Format("this._removeObjectReference({0})", id);
			}
			public string RemoveObject(string name) {
				return string.Format("this._removeObjectReference({0})", name);
			}
		}
	}
}
