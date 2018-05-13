using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Create a button in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarButtonModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarButtonModule() {
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public TouchBarButton Create(TouchBarButton.Options options) {
			return API.ApplyConstructor<TouchBarButton>(options);
		}
	}

	/// <summary>
	/// Create a color picker in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarColorPickerModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarColorPickerModule() {
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public TouchBarColorPicker Create(TouchBarColorPicker.Options options) {
			return API.ApplyConstructor<TouchBarColorPicker>(options);
		}
	}

	/// <summary>
	/// Create a group in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarGroupModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarGroupModule() {
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public TouchBarGroup Create(TouchBarGroup.Options options) {
			return API.ApplyConstructor<TouchBarGroup>(options);
		}
	}

	/// <summary>
	/// Create a label in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarLabelModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarLabelModule() {
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public TouchBarLabel Create(TouchBarLabel.Options options) {
			return API.ApplyConstructor<TouchBarLabel>(options);
		}
	}

	/// <summary>
	/// Create a popover in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarPopoverModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarPopoverModule() {
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public TouchBarPopover Create(TouchBarPopover.Options options) {
			return API.ApplyConstructor<TouchBarPopover>(options);
		}
	}

	/// <summary>
	/// Create a scrubber (a scrollable selector).
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarScrubberModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarScrubberModule() {
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public TouchBarScrubber Create(TouchBarScrubber.Options options) {
			return API.ApplyConstructor<TouchBarScrubber>(options);
		}
	}

	/// <summary>
	/// Create a segmented control (a button group) where one button has a selected state.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarSegmentedControlModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarSegmentedControlModule() {
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public TouchBarSegmentedControl Create(TouchBarSegmentedControl.Options options) {
			return API.ApplyConstructor<TouchBarSegmentedControl>(options);
		}
	}

	/// <summary>
	/// Create a slider in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarSliderModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarSliderModule() {
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public TouchBarSlider Create(TouchBarSlider.Options options) {
			return API.ApplyConstructor<TouchBarSlider>(options);
		}
	}

	/// <summary>
	/// Create a spacer between two items in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarSpacerModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarSpacerModule() {
		}

		/// <summary>
		/// *Experimental*
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public TouchBarSpacer Create(TouchBarSpacer.Options options) {
			return API.ApplyConstructor<TouchBarSpacer>(options);
		}
	}
}
