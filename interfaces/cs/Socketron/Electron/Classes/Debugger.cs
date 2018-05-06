using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// An alternate transport for Chrome's remote debugging protocol.
	/// <para>Process: Main</para>
	/// <para>
	/// Chrome Developer Tools has a special binding available at JavaScript runtime
	/// that allows interacting with pages and instrumenting them.
	/// </para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Debugger : NodeModule {
		public const string Name = "Debugger";

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();
		
		/// <summary>
		/// This method is used for internally by the library.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Callback GetCallbackFromId(ushort id) {
			if (!_callbackList.ContainsKey(id)) {
				return null;
			}
			return _callbackList[id];
		}

		/// <summary>
		/// Debugger instance events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when debugging session is terminated.
			/// This happens either when webContents is closed
			/// or devtools is invoked for the attached webContents.
			/// </summary>
			public const string Detach = "detach";
			/// <summary>
			/// Emitted whenever debugging target issues instrumentation event.
			/// </summary>
			public const string Message = "message";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public Debugger(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// Attaches the debugger to the webContents.
		/// </summary>
		/// <param name="protocolVersion">
		/// (optional) Requested debugging protocol version.
		/// </param>
		public void attach(string protocolVersion = null) {
			string script = string.Empty;
			if (protocolVersion == null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var debugger = {0};",
						"debugger.attach();"
					),
					Script.GetObject(_id)
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var debugger = {0};",
						"debugger.attach({1});"
					),
					Script.GetObject(_id),
					protocolVersion.Escape()
				);
			}
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether a debugger is attached to the webContents.
		/// </summary>
		/// <returns></returns>
		public bool isAttached() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var debugger = {0};",
					"return debugger.isAttached();"
				),
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Detaches the debugger from the webContents.
		/// </summary>
		public void detach() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var debugger = {0};",
					"debugger.detach();"
				),
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Send given command to the debugging target.
		/// </summary>
		/// <param name="method"></param>
		public void sendCommand(string method) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var debugger = {0};",
					"debugger.sendCommand({1});"
				),
				Script.GetObject(_id),
				method.Escape()
			);
			_ExecuteJavaScript(script);
		}

		public void sendCommand(string method, JsonObject commandParams) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var debugger = {0};",
					"debugger.sendCommand({1},{2});"
				),
				Script.GetObject(_id),
				method.Escape(),
				commandParams.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		public void sendCommand(string method, JsonObject commandParams, Action<Error, JsonObject> callback) {
			ushort callbackId = _callbackListId;
			_callbackList.Add(_callbackListId, (object args) => {
				_callbackList.Remove(callbackId);
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				Error error = new Error(_client, (int)argsList[0]);
				JsonObject json = new JsonObject(argsList[1]);
				callback?.Invoke(error, json);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (err,result) => {{",
						"var errId = {0};",
						"emit('__event',{1},{2},errId,result);",
					"}};",
					"{3}.sendCommand({4},{5});"
				),
				Script.AddObject("err"),
				Name.Escape(),
				_callbackListId,
				Script.GetObject(_id),
				method.Escape(),
				commandParams.Stringify()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}
	}
}
