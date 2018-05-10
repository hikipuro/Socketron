using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Access information about media sources that can be used
	/// to capture audio and video from the desktop using
	/// the navigator.mediaDevices.getUserMedia API.
	/// <para>Process: Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class DesktopCapturer : JSModule {
		public const string Name = "DesktopCapturer";

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public DesktopCapturer(SocketronClient client) {
			API.client = client;
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

		/// <summary>
		/// Starts gathering information about all available desktop media sources,
		/// and calls callback(error, sources) when finished.
		/// <para>
		/// sources is an array of DesktopCapturerSource objects,
		/// each DesktopCapturerSource represents a screen or an individual window that can be captured.
		/// </para>
		/// </summary>
		/// <param name="options"></param>
		/// <param name="callback"></param>
		public void getSources(JsonObject options, Action<Error, DesktopCapturerSource[]> callback) {
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
				Error error = new Error(API.client, (int)argsList[0]);
				DesktopCapturerSource[] sources = Array.ConvertAll(
					argsList[1] as object[],
					value => DesktopCapturerSource.Parse(value as string)
				);
				callback?.Invoke(error, sources);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (err,sources) => {{",
						"var errId = {0};",
						"this.emit('__event',{1},{2},errId,sources);",
					"}};",
					"electron.desktopCapturer.getSources({3},callback);"
				),
				Script.AddObject("err"),
				Name.Escape(),
				_callbackListId,
				options.Stringify()
			);
			_callbackListId++;
			API.ExecuteJavaScript(script);
		}
	}
}
