﻿namespace Socketron {
	/// <summary>
	/// Extensions to process object.
	/// <para>Process: Main, Renderer</para>
	/// </summary>
	public class Process {
		public class Events {
			/// <summary>
			/// Emitted when Electron has loaded its internal initialization script
			/// and is beginning to load the web page or the main script.
			/// </summary>
			public const string Loaded = "loaded";
		}
	}
}
