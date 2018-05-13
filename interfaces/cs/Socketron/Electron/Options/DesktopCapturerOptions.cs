namespace Socketron.Electron {
	/// <summary>
	/// DesktopCapturer.getSources() options.
	/// </summary>
	public class SourcesOptions {
		/// <summary>
		/// An array of Strings that lists the types of desktop sources to be captured,
		/// available types are screen and window.
		/// </summary>
		public string[] types;
		/// <summary>
		/// The size that the media source thumbnail should be scaled to.
		/// Default is 150 x 150.
		/// </summary>
		public Size thumbnailSize;
	}
}
