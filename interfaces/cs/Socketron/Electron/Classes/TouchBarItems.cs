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
