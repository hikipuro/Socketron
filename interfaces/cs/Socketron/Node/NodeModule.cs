using System;
using System.Collections.Generic;

namespace Socketron {
	public class NodeModule : IDisposable {
		public int _id;
		protected static List<NodeModule> _modules;
		protected SocketronClient _client;
		protected bool _disposeManually = false;

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
		/// Used Internally by the library.
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
			public string GetObject(int id) {
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
			public string RemoveObject(int id) {
				return string.Format("this._removeObjectReference({0})", id);
			}
			public string RemoveObject(string name) {
				return string.Format("this._removeObjectReference({0})", name);
			}
		}
	}
}
