using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class DesktopCapturerSource : JSObject {
		/// <summary>
		/// The identifier of a window or screen
		/// that can be used as a chromeMediaSourceId constraint
		/// when calling [navigator.webkitGetUserMedia].
		/// The format of the identifier will be window:XX or screen:XX,
		/// where XX is a random generated number.
		/// </summary>
		public string id {
			get { return API.GetProperty<string>("id"); }
		}
		/// <summary>
		/// A screen source will be named either Entire Screen or Screen &lt;index&gt;,
		/// while the name of a window source will match the window title.
		/// </summary>
		public string name {
			get { return API.GetProperty<string>("name"); }
		}
		/// <summary>
		/// A thumbnail image. Note: There is no guarantee that the size of the thumbnail
		/// is the same as the thumbnailSize specified in the options passed to desktopCapturer.getSources.
		/// The actual size depends on the scale of the screen or window.
		/// </summary>
		public NativeImage thumbnail {
			get { return API.GetObject<NativeImage>("thumbnail"); }
		}
		/// <summary>
		/// A unique identifier that will correspond to the id of the matching Display
		/// returned by the Screen API. On some platforms,
		/// this is equivalent to the XX portion of the id field above and on others
		/// it will differ. It will be an empty string if not available.
		/// </summary>
		public string display_id {
			get { return API.GetProperty<string>("display_id"); }
		}

		public DesktopCapturerSource() {
		}
	}
}

