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
	public class DesktopCapturer : JSModule {
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
		public void getSources(JsonObject options, Action<Error, DesktopCapturerSource[]> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "_getSources";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				// TODO: check DesktopCapturerSource[]
				API.RemoveCallbackItem(eventName, item);
				Error error = API.CreateObject<Error>((int)args[0]);
				DesktopCapturerSource[] sources = Array.ConvertAll(
					args[1] as object[],
					value => DesktopCapturerSource.Parse(value as string)
				);
				callback?.Invoke(error, sources);
			});
			API.Apply("getSources", options, item);
		}
	}
}
