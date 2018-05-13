using System;

namespace Socketron.Electron {
	/// <summary>
	/// TouchBar constructor options.
	/// </summary>
	public class TouchBarConstructorOptions {
		public TouchBarItem[] items;
		public TouchBarItem escapeItem;
	}

	/// <summary>
	/// TouchBarButton constructor options.
	/// </summary>
	public class TouchBarButtonConstructorOptions {
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

		/// <summary>
		/// iconPosition values.
		/// </summary>
		public class IconPosition {
			public const string Left = "left";
			public const string Right = "right";
			public const string Overlay = "overlay";
		}
	}

	/// <summary>
	/// TouchBarColorPicker constructor options.
	/// </summary>
	public class TouchBarColorPickerConstructorOptions {
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
	/// TouchBarGroup constructor options.
	/// </summary>
	public class TouchBarGroupConstructorOptions {
		/// <summary>
		/// Items to display as a group.
		/// </summary>
		public TouchBar items;
	}

	/// <summary>
	/// TouchBarLabel constructor options.
	/// </summary>
	public class TouchBarLabelConstructorOptions {
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
	/// TouchBarPopover constructor options.
	/// </summary>
	public class TouchBarPopoverConstructorOptions {
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
	/// TouchBarScrubber constructor options.
	/// </summary>
	public class TouchBarScrubberConstructorOptions {
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

		/// <summary>
		/// selectedStyle, overlayStyle values.
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
		/// mode values.
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
	}

	/// <summary>
	/// TouchBarSegmentedControl constructor options.
	/// </summary>
	public class TouchBarSegmentedControlConstructorOptions {
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

		/// <summary>
		/// segmentStyle values.
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
		/// mode values.
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
	}

	/// <summary>
	/// TouchBarSlider constructor options.
	/// </summary>
	public class TouchBarSliderConstructorOptions {
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
	/// TouchBarSpacer constructor options.
	/// </summary>
	public class TouchBarSpacerConstructorOptions {
		/// <summary>
		/// (optional) Size of spacer, possible values are:
		/// </summary>
		public string size;

		/// <summary>
		/// size values.
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
	}
}
