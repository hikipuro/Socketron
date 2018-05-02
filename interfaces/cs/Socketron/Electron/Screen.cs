namespace Socketron {
	/// <summary>
	/// Retrieve information about screen size, displays, cursor position, etc.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	public class Screen {
		/// <summary>
		/// Screen module events.
		/// </summary>
		public class Events {
			public const string DisplayAdded = "display-added";
			public const string DisplayRemoved = "display-removed";
			public const string DisplayMetricsChanged = "display-metrics-changed";
		}
	}
}
