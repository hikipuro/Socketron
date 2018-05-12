using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarItem : JSObject {
		public TouchBarItem() {
		}
	}

	/// <summary>
	/// Create a button in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarButton : TouchBarItem {
		public class Options {
			public string label;
			public string backgroundColor;
			public NativeImage icon;
			public string iconPosition;
			public string click;
		}

		public class IconPosition {
			public const string Left = "left";
			public const string Right = "right";
			public const string Overlay = "overlay";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarButton() {
		}

		public string label {
			get { return API.GetProperty<string>("label"); }
			set { API.SetProperty("label", value); }
		}

		public string backgroundColor {
			get { return API.GetProperty<string>("backgroundColor"); }
			set { API.SetProperty("backgroundColor", value); }
		}

		public NativeImage icon {
			get { return API.GetObject<NativeImage>("icon"); }
			set { API.SetObject("icon", value); }
		}
	}

	/// <summary>
	/// Create a color picker in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarColorPicker : TouchBarItem {
		public class Options {
			public string[] availableColors;
			public string selectedColor;
			public JSCallback change;
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarColorPicker() {
		}

		public string[] availableColors {
			get {
				object[] result = API.GetProperty<object[]>("availableColors");
				return Array.ConvertAll(result, value => Convert.ToString(value));
			}
			set {
				API.SetProperty("availableColors", JSON.Stringify(value));
			}
		}

		public string selectedColor {
			get { return API.GetProperty<string>("selectedColor"); }
			set { API.SetProperty("selectedColor", value); }
		}
	}

	/// <summary>
	/// Create a group in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarGroup : TouchBarItem {
		public class Options {
			public TouchBar items;
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarGroup() {
		}
	}

	/// <summary>
	/// Create a label in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarLabel : TouchBarItem {
		public class Options {
			public string label;
			public string textColor;
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarLabel() {
		}

		public string label {
			get { return API.GetProperty<string>("label"); }
			set { API.SetProperty("label", value); }
		}

		public string textColor {
			get { return API.GetProperty<string>("textColor"); }
			set { API.SetProperty("textColor", value); }
		}
	}

	/// <summary>
	/// Create a popover in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarPopover : TouchBarItem {
		public class Options {
			public string label;
			public NativeImage icon;
			public TouchBar items;
			public bool showCloseButton;
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarPopover() {
		}

		public string label {
			get { return API.GetProperty<string>("label"); }
			set { API.SetProperty("label", value); }
		}

		public NativeImage icon {
			get { return API.GetObject<NativeImage>("icon"); }
			set { API.SetObject("icon", value); }
		}
	}

	/// <summary>
	/// Create a scrubber (a scrollable selector).
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarScrubber : TouchBarItem {
		public class Options {
			public ScrubberItem[] items;
			public JSCallback select;
			public JSCallback highlight;
			public string selectedStyle;
			public string overlayStyle;
			public bool showArrowButtons;
			public string mode;
			public bool continuous;
		}

		public class Style {
			public const string Background = "background";
			public const string Outline = "outline";
			public const string Null = null;
		}

		public class Mode {
			public const string Fixed = "fixed";
			public const string Free = "free";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarScrubber() {
		}

		public ScrubberItem[] items {
			get { return API.GetObjectList<ScrubberItem>("items"); }
			// TODO: fix this
			//set { API.SetObjectList("label", value); }
		}

		public string selectedStyle {
			get { return API.GetProperty<string>("selectedStyle"); }
			set { API.SetProperty("selectedStyle", value); }
		}

		public string overlayStyle {
			get { return API.GetProperty<string>("overlayStyle"); }
			set { API.SetProperty("overlayStyle", value); }
		}

		public bool showArrowButtons {
			get { return API.GetProperty<bool>("showArrowButtons"); }
			set { API.SetProperty("showArrowButtons", value); }
		}

		public string mode {
			get { return API.GetProperty<string>("mode"); }
			set { API.SetProperty("mode", value); }
		}

		public bool continuous {
			get { return API.GetProperty<bool>("continuous"); }
			set { API.SetProperty("continuous", value); }
		}
	}

	/// <summary>
	/// Create a segmented control (a button group) where one button has a selected state.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarSegmentedControl : TouchBarItem {
		public class Options {
			/// <summary>
			/// Style of the segments.
			/// </summary>
			public string segmentStyle;
			/// <summary>
			/// (optional) The selection mode of the control.
			/// </summary>
			public string mode;
			/// <summary>
			/// An array of segments to place in this control.
			/// </summary>
			public SegmentedControlSegment[] segments;
			/// <summary>
			/// (optional) The index of the currently selected segment,
			/// will update automatically with user interaction.
			/// When the mode is multiple it will be the last selected item.
			/// </summary>
			public int selectedIndex;
			/// <summary>
			/// Called when the user selects a new segment.
			/// </summary>
			public Action<int, bool> change;
		}

		public class SegmentStyle {
			/// <summary>
			/// Default. The appearance of the segmented control
			/// is automatically determined based on the type of window
			/// in which the control is displayed and the position within the window.
			/// </summary>
			public const string Automatic = "automatic";
			/// <summary>
			/// The control is displayed using the rounded style.
			/// </summary>
			public const string Rounded = "rounded";
			/// <summary>
			/// The control is displayed using the textured rounded style.
			/// </summary>
			public const string TexturedRounded = "textured-rounded";
			/// <summary>
			/// The control is displayed using the round rect style.
			/// </summary>
			public const string RoundRect = "round-rect";
			/// <summary>
			/// The control is displayed using the textured square style.
			/// </summary>
			public const string TexturedSquare = "textured-square";
			/// <summary>
			/// The control is displayed using the capsule style.
			/// </summary>
			public const string Capsule = "capsule";
			/// <summary>
			/// The control is displayed using the small square style.
			/// </summary>
			public const string SmallSquare = "small-square";
			/// <summary>
			/// The segments in the control are displayed very close to each other but not touching.
			/// </summary>
			public const string Separated = "separated";
		}

		public class Mode {
			/// <summary>
			/// Default. One item selected at a time,
			/// selecting one deselects the previously selected item.
			/// </summary>
			public const string Single = "single";
			/// <summary>
			/// Multiple items can be selected at a time.
			/// </summary>
			public const string Multiple = "multiple";
			/// <summary>
			/// Make the segments act as buttons,
			/// each segment can be pressed and released but never marked as active.
			/// </summary>
			public const string Buttons = "buttons";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarSegmentedControl() {
		}

		/// <summary>
		/// A String representing the controls current segment style.
		/// Updating this value immediately updates the control in the touch bar.
		/// </summary>
		public string segmentStyle {
			get { return API.GetProperty<string>("segmentStyle"); }
			set { API.SetProperty("segmentStyle", value); }
		}

		/// <summary>
		/// A SegmentedControlSegment[] array representing the segments in this control.
		/// Updating this value immediately updates the control in the touch bar.
		/// Updating deep properties inside this array does not update the touch bar.
		/// </summary>
		public SegmentedControlSegment[] segments {
			get { return API.GetProperty<string>("segments"); }
			set { API.SetProperty("segments", value); }
		}

		public int selectedIndex {
			get { return API.GetProperty<int>("selectedIndex"); }
			set { API.SetProperty("selectedIndex", int); }
		}
	}

	/// <summary>
	/// Create a slider in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarSlider : TouchBarItem {
		public class Options {
			public string label;
			public int value;
			public int minValue;
			public int maxValue;
			public Action<int> change;
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarSlider() {
		}

		public string label {
			get { return API.GetProperty<string>("label"); }
			set { API.SetProperty("label", value); }
		}

		public int value {
			get { return API.GetProperty<int>("value"); }
			set { API.SetProperty("value", value); }
		}

		public int minValue {
			get { return API.GetProperty<int>("minValue"); }
			set { API.SetProperty("minValue", value); }
		}

		public int maxValue {
			get { return API.GetProperty<int>("maxValue"); }
			set { API.SetProperty("maxValue", value); }
		}
	}

	/// <summary>
	/// Create a spacer between two items in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarSpacer : TouchBarItem {
		public class Options {
			public string size;
		}

		public class Size {
			public const string Small = "small";
			public const string Large = "large";
			public const string Flexible = "flexible";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public TouchBarSpacer() {
		}
	}
}
