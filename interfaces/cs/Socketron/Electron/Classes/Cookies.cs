using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Socketron.Electron {
	/// <summary>
	/// Query and modify a session's cookies.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Cookies : JSModule {
		public const string Name = "Cookies";

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		/// <summary>
		/// Cookies instance events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when a cookie is changed because
			/// it was added, edited, removed, or expired.
			/// </summary>
			public const string Changed = "changed";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public Cookies(SocketronClient client) {
			_client = client;
		}

		/// <summary>
		/// Sends a request to get all cookies matching filter,
		/// callback will be called with callback(error, cookies) on complete.
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="callback"></param>
		public void get(JsonObject filter, Action<Error, Cookie[]> callback) {
			if (callback == null) {
				return;
			}
			ushort callbackId = _callbackListId;
			_callbackList.Add(_callbackListId, (object args) => {
				_callbackList.Remove(callbackId);
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				Error error = new Error(_client, (int)argsList[0]);
				Cookie[] cookies = (argsList[1] as object[]).Cast<Cookie>().ToArray();
				callback?.Invoke(error, cookies);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (err,cookies) => {{",
						"var errId = {0};",
						"this.emit('__event',{1},{2},errId,cookies);",
					"}};",
					"{3}.get({4},callback);"
				),
				Script.AddObject("err"),
				Name.Escape(),
				_callbackListId,
				Script.GetObject(_id),
				filter.Stringify()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets a cookie with details,
		/// callback will be called with callback(error) on complete.
		/// </summary>
		/// <param name="details"></param>
		/// <param name="callback"></param>
		public void set(JsonObject details, Action<Error> callback) {
			if (callback == null) {
				return;
			}
			ushort callbackId = _callbackListId;
			_callbackList.Add(_callbackListId, (object args) => {
				_callbackList.Remove(callbackId);
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				Error error = new Error(_client, (int)argsList[0]);
				callback?.Invoke(error);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (err) => {{",
						"var errId = {0};",
						"this.emit('__event',{1},{2},errId);",
					"}};",
					"{3}.set({4},callback);"
				),
				Script.AddObject("err"),
				Name.Escape(),
				_callbackListId,
				Script.GetObject(_id),
				details.Stringify()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Removes the cookies matching url and name,
		/// callback will called with callback() on complete.
		/// </summary>
		/// <param name="url">The URL associated with the cookie.</param>
		/// <param name="name">The name of cookie to remove.</param>
		/// <param name="callback"></param>
		public void remove(string url, string name, Action callback) {
			if (callback == null) {
				return;
			}
			ushort callbackId = _callbackListId;
			_callbackList.Add(_callbackListId, (object args) => {
				_callbackList.Remove(callbackId);
				callback?.Invoke();
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"this.emit('__event',{0},{1});",
					"}};",
					"{2}.remove({3},{4},callback);"
				),
				Name.Escape(),
				_callbackListId,
				Script.GetObject(_id),
				url.Escape(),
				name.Escape()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Writes any unwritten cookies data to disk.
		/// </summary>
		/// <param name="callback"></param>
		public void flushStore(Action callback) {
			if (callback == null) {
				return;
			}
			ushort callbackId = _callbackListId;
			_callbackList.Add(_callbackListId, (object args) => {
				_callbackList.Remove(callbackId);
				callback?.Invoke();
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"this.emit('__event',{0},{1});",
					"}};",
					"{2}.flushStore(callback);"
				),
				Name.Escape(),
				_callbackListId,
				Script.GetObject(_id)
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}
	}
}
