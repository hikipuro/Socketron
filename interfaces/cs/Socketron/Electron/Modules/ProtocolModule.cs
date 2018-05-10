using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Register a custom protocol and intercept existing protocol requests.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class ProtocolModule : JSModule {
		public const string Name = "Protocol";

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public ProtocolModule() {
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public ProtocolModule(SocketronClient client, int id) {
			API.client = client;
			API.id = id;
		}

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

		public void registerStandardSchemes(string[] schemes, JsonObject options = null) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerServiceWorkerSchemes(string[] schemes) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerFileProtocol(string scheme, Action handler, Action completion = null) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerBufferProtocol(string scheme, Action handler, Action completion = null) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerStringProtocol(string scheme, Action handler, Action completion = null) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerHttpProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void registerStreamProtocol() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Unregisters the custom protocol of scheme.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="completion"></param>
		public void unregisterProtocol(string scheme, Action<Error> completion = null) {
			ushort callbackId = _callbackListId;
			_callbackList.Add(_callbackListId, (object args) => {
				_callbackList.Remove(callbackId);
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				int errId = (int)argsList[0];
				completion?.Invoke(new Error(API.client, errId));
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (err) => {{",
						"var errId = {0};",
						"this.emit('__event',{1},{2},errId);",
					"}};",
					"electron.protocol.unregisterProtocol({3},callback);"
				),
				Script.AddObject("err"),
				Name.Escape(),
				_callbackListId,
				scheme.Escape()
			);
			_callbackListId++;
			API.ExecuteJavaScript(script);
		}

		public void isProtocolHandled(string scheme, Action<string> callback) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void interceptFileProtocol(string scheme) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void interceptStringProtocol(string scheme) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void interceptBufferProtocol(string scheme) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void interceptHttpProtocol(string scheme) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		public void interceptStreamProtocol(string scheme, Action<JsonObject, Action<object>> handler, Action<Error> completion = null) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Remove the interceptor installed for scheme and restore its original handler.
		/// </summary>
		/// <param name="scheme"></param>
		/// <param name="completion">(optional)</param>
		public void uninterceptProtocol(string scheme, Action<Error> completion = null) {
			ushort callbackId = _callbackListId;
			_callbackList.Add(_callbackListId, (object args) => {
				_callbackList.Remove(callbackId);
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				int errId = (int)argsList[0];
				completion?.Invoke(new Error(API.client, errId));
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (err) => {{",
						"var errId = {0};",
						"this.emit('__event',{1},{2},errId);",
					"}};",
					"electron.protocol.uninterceptProtocol({3},callback);"
				),
				Script.AddObject("err"),
				Name.Escape(),
				_callbackListId,
				scheme.Escape()
			);
			_callbackListId++;
			API.ExecuteJavaScript(script);
		}
	}
}
