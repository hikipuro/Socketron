﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Query and modify a session's cookies.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Cookies : EventEmitter {
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
		public Cookies() {
		}

		/// <summary>
		/// Sends a request to get all cookies matching filter,
		/// callback will be called with callback(error, cookies) on complete.
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="callback"></param>
		public void get(Filter filter, Action<Error, Cookie[]> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_get";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				// TODO: check cookie[]
				Error error = API.CreateObject<Error>(args[0]);
				JSObject _cookies = API.CreateObject<JSObject>(args[1]);
				Cookie[] cookies = Array.ConvertAll(
					_cookies.API.GetValue() as object[],
					value => Cookie.FromObject(value)
				);
				callback?.Invoke(error, cookies);
			});
			API.Apply("get", filter, item);
		}

		/// <summary>
		/// Sets a cookie with details,
		/// callback will be called with callback(error) on complete.
		/// </summary>
		/// <param name="details"></param>
		/// <param name="callback"></param>
		public void set(Details details, Action<Error> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_set";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				Error error = API.CreateObject<Error>(args[0]);
				callback?.Invoke(error);
			});
			API.Apply("set", details, item);
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
			string eventName = "_remove";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				callback?.Invoke();
			});
			API.Apply("remove", url, name, item);
		}

		/// <summary>
		/// Writes any unwritten cookies data to disk.
		/// </summary>
		/// <param name="callback"></param>
		public void flushStore(Action callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_flushStore";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				callback?.Invoke();
			});
			API.Apply("flushStore", item);
		}
	}
}
