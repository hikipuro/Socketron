namespace Socketron.Electron {
	/// <summary>
	/// Retrieve information about screen size, displays, cursor position, etc.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	public class Screen {
		/// <summary>
		/// Screen module events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when newDisplay has been added.
			/// </summary>
			public const string DisplayAdded = "display-added";
			/// <summary>
			/// Emitted when oldDisplay has been removed.
			/// </summary>
			public const string DisplayRemoved = "display-removed";
			/// <summary>
			/// Emitted when one or more metrics change in a display.
			/// <para>
			/// The changedMetrics is an array of strings that describe the changes.
			/// Possible changes are bounds, workArea, scaleFactor and rotation.
			/// </para>
			/// </summary>
			public const string DisplayMetricsChanged = "display-metrics-changed";
		}
	}
}
