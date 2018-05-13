using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarItem : EventEmitter {
		public TouchBarItem() {
		}
	}

	/// <summary>
	/// Create a button in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarButton : TouchBarItem {
		/// <summary>
		/// TouchBarButton constructor options.
		/// </summary>
		public class Options {
			/// <summary>
			/// (optional) Button text.
			/// </summary>
			public string label;
			/// <summary>
			/// (optional) Button background color in hex format, i.e #ABCDEF.
			/// </summary>
			public string backgroundColor;
			/// <summary>
			/// (optional) Button icon.
			/// </summary>
			public NativeImage icon;
			/// <summary>
			/// (optional) Can be left, right or overlay.
			/// </summary>
			public string iconPosition;
			/// <summary>
			/// (optional) Function to call when the button is clicked.
			/// </summary>
			public string click;
		}

		/// <summary>
		/// Options.iconPosition values.
		/// </summary>
		public class IconPosition {
			public const string Left = "left";
			public const string Right = "right";
			public const string Overlay = "overlay";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the TouchBarButton constructors,
		/// please use electron.TouchBarButton.Create() method instead.
		/// </para>
		/// </summary>
		public TouchBarButton() {
		}

		/// <summary>
		/// A String representing the button's current text.
		/// Changing this value immediately updates the button in the touch bar.
		/// </summary>
		public string label {
			get { return API.GetProperty<string>("label"); }
			set { API.SetProperty("label", value); }
		}

		/// <summary>
		/// A String hex code representing the button's current background color.
		/// Changing this value immediately updates the button in the touch bar.
		/// </summary>
		public string backgroundColor {
			get { return API.GetProperty<string>("backgroundColor"); }
			set { API.SetProperty("backgroundColor", value); }
		}

		/// <summary>
		/// A NativeImage representing the button's current icon.
		/// Changing this value immediately updates the button in the touch bar.
		/// </summary>
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
		/// <summary>
		/// TouchBarColorPicker constructor options.
		/// </summary>
		public class Options {
			/// <summary>
			/// (optional) Array of hex color strings to appear as possible colors to select.
			/// </summary>
			public string[] availableColors;
			/// <summary>
			///  (optional) The selected hex color in the picker, i.e #ABCDEF.
			/// </summary>
			public string selectedColor;
			/// <summary>
			/// (optional) Function to call when a color is selected.
			/// <para>
			/// "color" String - The color that the user selected from the picker.
			/// </para>
			/// </summary>
			public JSCallback change;
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the TouchBarColorPicker constructors,
		/// please use electron.TouchBarColorPicker.Create() method instead.
		/// </para>
		/// </summary>
		public TouchBarColorPicker() {
		}

		/// <summary>
		/// A String[] array representing the color picker's available colors to select.
		/// Changing this value immediately updates the color picker in the touch bar.
		/// </summary>
		public string[] availableColors {
			get {
				object[] result = API.GetProperty<object[]>("availableColors");
				return Array.ConvertAll(result, value => Convert.ToString(value));
			}
			set { API.SetProperty("availableColors", JSON.Stringify(value)); }
		}

		/// <summary>
		/// A String hex code representing the color picker's currently selected color.
		/// Changing this value immediately updates the color picker in the touch bar.
		/// </summary>
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
		/// <summary>
		/// TouchBarGroup constructor options.
		/// </summary>
		public class Options {
			/// <summary>
			/// Items to display as a group.
			/// </summary>
			public TouchBar items;
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the TouchBarGroup constructors,
		/// please use electron.TouchBarGroup.Create() method instead.
		/// </para>
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
		/// <summary>
		/// TouchBarLabel constructor options.
		/// </summary>
		public class Options {
			/// <summary>
			/// (optional) Text to display.
			/// </summary>
			public string label;
			/// <summary>
			/// (optional) Hex color of text, i.e #ABCDEF.
			/// </summary>
			public string textColor;
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the TouchBarLabel constructors,
		/// please use electron.TouchBarLabel.Create() method instead.
		/// </para>
		/// </summary>
		public TouchBarLabel() {
		}

		/// <summary>
		/// A String representing the label's current text.
		/// Changing this value immediately updates the label in the touch bar.
		/// </summary>
		public string label {
			get { return API.GetProperty<string>("label"); }
			set { API.SetProperty("label", value); }
		}

		/// <summary>
		/// A String hex code representing the label's current text color.
		/// Changing this value immediately updates the label in the touch bar.
		/// </summary>
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
		/// <summary>
		/// TouchBarPopover constructor options.
		/// </summary>
		public class Options {
			/// <summary>
			/// (optional) Popover button text.
			/// </summary>
			public string label;
			/// <summary>
			/// (optional) Popover button icon.
			/// </summary>
			public NativeImage icon;
			/// <summary>
			/// (optional) Items to display in the popover.
			/// </summary>
			public TouchBar items;
			/// <summary>
			/// (optional) true to display a close button on the left of the popover,
			/// false to not show it. Default is true.
			/// </summary>
			public bool? showCloseButton;
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the TouchBarPopover constructors,
		/// please use electron.TouchBarPopover.Create() method instead.
		/// </para>
		/// </summary>
		public TouchBarPopover() {
		}

		/// <summary>
		/// A String representing the popover's current button text.
		/// Changing this value immediately updates the popover in the touch bar.
		/// </summary>
		public string label {
			get { return API.GetProperty<string>("label"); }
			set { API.SetProperty("label", value); }
		}

		/// <summary>
		/// A NativeImage representing the popover's current button icon.
		/// Changing this value immediately updates the popover in the touch bar.
		/// </summary>
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
		/// <summary>
		/// TouchBarScrubber constructor options.
		/// </summary>
		public class Options {
			/// <summary>
			/// An array of items to place in this scrubber.
			/// </summary>
			public ScrubberItem[] items;
			/// <summary>
			/// Called when the user taps an item that was not the last tapped item.
			/// <para>
			/// "selectedIndex" Integer - The index of the item the user selected.
			/// </para>
			/// </summary>
			public JSCallback select;
			/// <summary>
			/// Called when the user taps any item.
			/// <para>
			/// "highlightedIndex" Integer - The index of the item the user touched.
			/// </para>
			/// </summary>
			public JSCallback highlight;
			/// <summary>
			/// Selected item style. Defaults to null.
			/// </summary>
			public string selectedStyle;
			/// <summary>
			/// Selected overlay item style. Defaults to null.
			/// </summary>
			public string overlayStyle;
			/// <summary>
			/// Defaults to false.
			/// </summary>
			public bool? showArrowButtons;
			/// <summary>
			/// Defaults to free.
			/// </summary>
			public string mode;
			/// <summary>
			/// Defaults to true.
			/// </summary>
			public bool? continuous;
		}

		/// <summary>
		/// Options.selectedStyle, Options.overlayStyle values.
		/// </summary>
		public class Style {
			/// <summary>
			/// Maps to [NSScrubberSelectionStyle roundedBackgroundStyle].
			/// </summary>
			public const string Background = "background";
			/// <summary>
			/// Maps to [NSScrubberSelectionStyle outlineOverlayStyle].
			/// </summary>
			public const string Outline = "outline";
			/// <summary>
			/// Actually null, not a string, removes all styles.
			/// </summary>
			public const string Null = null;
		}

		/// <summary>
		/// Options.mode values.
		/// </summary>
		public class Mode {
			/// <summary>
			/// Maps to NSScrubberModeFixed.
			/// </summary>
			public const string Fixed = "fixed";
			/// <summary>
			/// Maps to NSScrubberModeFree.
			/// </summary>
			public const string Free = "free";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the TouchBarScrubber constructors,
		/// please use electron.TouchBarScrubber.Create() method instead.
		/// </para>
		/// </summary>
		public TouchBarScrubber() {
		}

		/// <summary>
		/// A ScrubberItem[] array representing the items in this scrubber.
		/// Updating this value immediately updates the control in the touch bar.
		/// Updating deep properties inside this array does not update the touch bar.
		/// </summary>
		public ScrubberItem[] items {
			get { return API.GetObjectList<ScrubberItem>("items"); }
			set { API.SetObjectList("items", value); }
		}

		/// <summary>
		/// A String representing the style that selected items in the scrubber should have.
		/// Updating this value immediately updates the control in the touch bar. 
		/// </summary>
		public string selectedStyle {
			get { return API.GetProperty<string>("selectedStyle"); }
			set { API.SetProperty("selectedStyle", value); }
		}

		/// <summary>
		/// A String representing the style that selected items in the scrubber should have.
		/// This style is overlayed on top of the scrubber item instead of being placed behind it.
		/// Updating this value immediately updates the control in the touch bar. 
		/// </summary>
		public string overlayStyle {
			get { return API.GetProperty<string>("overlayStyle"); }
			set { API.SetProperty("overlayStyle", value); }
		}

		/// <summary>
		/// A Boolean representing whether to show the left / right selection arrows in this scrubber.
		/// Updating this value immediately updates the control in the touch bar.
		/// </summary>
		public bool showArrowButtons {
			get { return API.GetProperty<bool>("showArrowButtons"); }
			set { API.SetProperty("showArrowButtons", value); }
		}

		/// <summary>
		/// A String representing the mode of this scrubber.
		/// Updating this value immediately updates the control in the touch bar. 
		/// </summary>
		public string mode {
			get { return API.GetProperty<string>("mode"); }
			set { API.SetProperty("mode", value); }
		}

		/// <summary>
		/// A Boolean representing whether this scrubber is continuous or not.
		/// Updating this value immediately updates the control in the touch bar.
		/// </summary>
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
		/// <summary>
		/// TouchBarSegmentedControl constructor options.
		/// </summary>
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
			/// <para>
			/// "selectedIndex" Integer - The index of the segment the user selected.
			/// </para>
			/// <para>
			/// "isSelected" Boolean - Whether as a result of user selection the segment is selected or not.
			/// </para>
			/// </summary>
			public Action<int, bool> change;
		}

		/// <summary>
		/// Options.segmentStyle values.
		/// </summary>
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

		/// <summary>
		/// Options.mode values.
		/// </summary>
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
		/// <para>
		/// If you are looking for the TouchBarSegmentedControl constructors,
		/// please use electron.TouchBarSegmentedControl.Create() method instead.
		/// </para>
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
			get { return API.GetObjectList<SegmentedControlSegment>("segments"); }
			set { API.SetObjectList("segments", value); }
		}

		/// <summary>
		/// An Integer representing the currently selected segment.
		/// Changing this value immediately updates the control in the touch bar.
		/// User interaction with the touch bar will update this value automatically.
		/// </summary>
		public int selectedIndex {
			get { return API.GetProperty<int>("selectedIndex"); }
			set { API.SetProperty("selectedIndex", value); }
		}
	}

	/// <summary>
	/// Create a slider in the touch bar for native macOS applications.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class TouchBarSlider : TouchBarItem {
		/// <summary>
		/// TouchBarSlider constructor options.
		/// </summary>
		public class Options {
			/// <summary>
			/// (optional) Label text.
			/// </summary>
			public string label;
			/// <summary>
			/// (optional) Selected value.
			/// </summary>
			public int value;
			/// <summary>
			/// (optional) Minimum value.
			/// </summary>
			public int minValue;
			/// <summary>
			/// (optional) Maximum value.
			/// </summary>
			public int maxValue;
			/// <summary>
			/// Function to call when the slider is changed.
			/// <para>
			/// "newValue" Number - The value that the user selected on the Slider.
			/// </para>
			/// </summary>
			public Action<int> change;
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the TouchBarSlider constructors,
		/// please use electron.TouchBarSlider.Create() method instead.
		/// </para>
		/// </summary>
		public TouchBarSlider() {
		}

		/// <summary>
		/// A String representing the slider's current text.
		/// Changing this value immediately updates the slider in the touch bar.
		/// </summary>
		public string label {
			get { return API.GetProperty<string>("label"); }
			set { API.SetProperty("label", value); }
		}

		/// <summary>
		/// A Number representing the slider's current value.
		/// Changing this value immediately updates the slider in the touch bar.
		/// </summary>
		public int value {
			get { return API.GetProperty<int>("value"); }
			set { API.SetProperty("value", value); }
		}

		/// <summary>
		/// A Number representing the slider's current minimum value.
		/// Changing this value immediately updates the slider in the touch bar.
		/// </summary>
		public int minValue {
			get { return API.GetProperty<int>("minValue"); }
			set { API.SetProperty("minValue", value); }
		}

		/// <summary>
		/// A Number representing the slider's current maximum value.
		/// Changing this value immediately updates the slider in the touch bar.
		/// </summary>
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
		/// <summary>
		/// TouchBarSpacer constructor options.
		/// </summary>
		public class Options {
			/// <summary>
			/// (optional) Size of spacer, possible values are:
			/// </summary>
			public string size;
		}

		/// <summary>
		/// Options.size values.
		/// </summary>
		public class Size {
			/// <summary>
			/// Small space between items.
			/// </summary>
			public const string Small = "small";
			/// <summary>
			/// Large space between items.
			/// </summary>
			public const string Large = "large";
			/// <summary>
			/// Take up all available space.
			/// </summary>
			public const string Flexible = "flexible";
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// <para>
		/// If you are looking for the TouchBarSpacer constructors,
		/// please use electron.TouchBarSpacer.Create() method instead.
		/// </para>
		/// </summary>
		public TouchBarSpacer() {
		}
	}
}
