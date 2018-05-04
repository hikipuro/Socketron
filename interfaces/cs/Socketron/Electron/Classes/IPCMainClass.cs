﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Socketron {
	/// <summary>
	/// Communicate asynchronously from the main process to renderer processes.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class IPCMainClass : NodeBase {
		public const string Name = "ipcMain";

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		public IPCMainClass(Socketron socketron) {
			_socketron = socketron;
		}

		public void on(string channel, Callback listener) {
			if (listener == null) {
				return;
			}
			_callbackList.Add(_callbackListId, listener);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var listener = () => {{",
						"emit('__event',{0},{1});",
					"}};",
					"this._addClientEventListener({0},{1},listener);",
					"electron.ipcMain.on({2}, listener);"
				),
				Name.Escape(),
				_callbackListId,
				channel.Escape()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		public void once(string channel, Callback listener) {
			if (listener == null) {
				return;
			}
			_callbackList.Add(_callbackListId, listener);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var listener = () => {{",
						"emit('__event',{0},{1});",
					"}};",
					"this._addClientEventListener({0},{1},listener);",
					"electron.ipcMain.once({2}, listener);"
				),
				Name.Escape(),
				_callbackListId,
				channel.Escape()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		public void removeListener(string channel, Callback listener) {
			var item = _callbackList.FirstOrDefault(x => x.Value == listener);
			if (item.Equals(default(KeyValuePair<ushort, Callback>))) {
				return;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var listener = this._getClientEventListener({0},{1});",
					"electron.ipcMain.removeListener({2},listener);"
				),
				Name.Escape(),
				item.Key,
				channel.Escape()
			);
			_ExecuteJavaScript(script);
			_callbackList.Remove(item.Key);
		}

		public void removeAllListeners(string channel = null) {
			string script = string.Empty;
			if (channel == null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"electron.ipcMain.removeAllListeners();"
					)
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"electron.ipcMain.removeAllListeners({0});"
					),
					channel
				);
			}
			_ExecuteJavaScript(script);
		}
	}
}
