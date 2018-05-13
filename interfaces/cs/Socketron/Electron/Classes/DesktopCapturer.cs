using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Access information about media sources that can be used
	/// to capture audio and video from the desktop using
	/// the navigator.mediaDevices.getUserMedia API.
	/// <para>Process: Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class DesktopCapturer : EventEmitter {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public DesktopCapturer() {
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
		public void getSources(SourcesOptions options, Action<Error, DesktopCapturerSource[]> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_getSources";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				// TODO: check DesktopCapturerSource[]
				API.RemoveCallbackItem(eventName, item);
				Error error = API.CreateObject<Error>(args[0]);
				JSObject _sources = API.CreateObject<JSObject>(args[1]);
				DesktopCapturerSource[] sources = _sources.API.ToArray<DesktopCapturerSource>();
				callback?.Invoke(error, sources);
			});
			API.Apply("getSources", options, item);
		}
	}
}
