using System;

namespace Socketron {
	/// <summary>
	/// Get system preferences.
	/// <para>Process: Main</para>
	/// </summary>
	public class SystemPreferences {
		public class Events {
			/// <summary>*Windows*</summary>
			public const string AccentColorChanged = "accent-color-changed";
			/// <summary>*Windows*</summary>
			public const string ColorChanged = "color-changed";
			/// <summary>*Windows*</summary>
			public const string InvertedColorSchemeChanged = "inverted-color-scheme-changed";
		}

		public class WindowsColors {
			/// <summary>Dark shadow for three-dimensional display elements.</summary>
			public const string _3DDarkShadow = "3d-dark-shadow";

			/// <summary>Face color for three-dimensional display elements and for dialog box backgrounds.</summary>
			public const string _3DFace = "3d-face";

			/// <summary>Highlight color for three-dimensional display elements.</summary>
			public const string _3DHighlight = "3d-highlight";

			/// <summary>Light color for three-dimensional display elements.</summary>
			public const string _3DLight = "3d-light";

			/// <summary>Shadow color for three-dimensional display elements.</summary>
			public const string _3Dshadow = "3d-shadow";

			/// <summary>Active window border.</summary>
			public const string ActiveBorder = "active-border";

			/// <summary>Active window title bar. Specifies the left side color in the color gradient of an active window's title bar if the gradient effect is enabled.</summary>
			public const string ActiveCaption = "active-caption";

			/// <summary>Right side color in the color gradient of an active window's title bar.</summary>
			public const string ActiveCaptionGradient = "active-caption-gradient";

			/// <summary>Background color of multiple document interface (MDI) applications.</summary>
			public const string AppWorkspace = "app-workspace";

			/// <summary>Text on push buttons.</summary>
			public const string ButtonText = "button-text";

			/// <summary>Text in caption, size box, and scroll bar arrow box.</summary>
			public const string CaptionText = "caption-text";

			/// <summary>Desktop background color.</summary>
			public const string Desktop = "desktop";

			/// <summary>Grayed (disabled) text.</summary>
			public const string DisabledText = "disabled-text";

			/// <summary>Item(s) selected in a control.</summary>
			public const string Highlight = "highlight";

			/// <summary>Text of item(s) selected in a control.</summary>
			public const string HighlightText = "highlight-text";

			/// <summary>Color for a hyperlink or hot-tracked item.</summary>
			public const string Hotlight = "hotlight";

			/// <summary>Inactive window border.</summary>
			public const string InactiveBorder = "inactive-border";

			/// <summary>Inactive window caption. Specifies the left side color in the color gradient of an inactive window's title bar if the gradient effect is enabled.</summary>
			public const string InactiveCaption = "inactive-caption";

			/// <summary>Right side color in the color gradient of an inactive window's title bar.</summary>
			public const string InactiveCaptionGradient = "inactive-caption-gradient";

			/// <summary>Color of text in an inactive caption.</summary>
			public const string InactiveCaptionText = "inactive-caption-text";

			/// <summary>Background color for tooltip controls.</summary>
			public const string InfoBackground = "info-background";

			/// <summary>Text color for tooltip controls.</summary>
			public const string InfoText = "info-text";

			/// <summary>Menu background.</summary>
			public const string Menu = "menu";

			/// <summary>The color used to highlight menu items when the menu appears as a flat menu.</summary>
			public const string MenuHighlight = "menu-highlight";

			/// <summary>The background color for the menu bar when menus appear as flat menus.</summary>
			public const string Menubar = "menubar";

			/// <summary>Text in menus.</summary>
			public const string MenuText = "menu-text";

			/// <summary>Scroll bar gray area.</summary>
			public const string Scrollbar = "scrollbar";

			/// <summary>Window background.</summary>
			public const string Window = "window";

			/// <summary>Window frame.</summary>
			public const string WindowFrame = "window-frame";

			/// <summary>Text in windows.</summary>
			public const string WindowText = "window-text";
		}

		public void IsDarkMode() {
			throw new NotImplementedException();
		}

		public void IsSwipeTrackingFromScrollEventsEnabled() {
			throw new NotImplementedException();
		}

		public void PostNotification() {
			throw new NotImplementedException();
		}

		public void PostLocalNotification() {
			throw new NotImplementedException();
		}

		public void PostWorkspaceNotification() {
			throw new NotImplementedException();
		}

		public void SubscribeNotification() {
			throw new NotImplementedException();
		}

		public void SubscribeLocalNotification() {
			throw new NotImplementedException();
		}

		public void SubscribeWorkspaceNotification() {
			throw new NotImplementedException();
		}

		public void UnsubscribeNotification() {
			throw new NotImplementedException();
		}

		public void UnsubscribeLocalNotification() {
			throw new NotImplementedException();
		}

		public void UnsubscribeWorkspaceNotification() {
			throw new NotImplementedException();
		}

		public void RegisterDefaults() {
			throw new NotImplementedException();
		}

		public void GetUserDefault() {
			throw new NotImplementedException();
		}

		public void SetUserDefault() {
			throw new NotImplementedException();
		}

		public void RemoveUserDefault() {
			throw new NotImplementedException();
		}

		public void IsAeroGlassEnabled() {
			throw new NotImplementedException();
		}

		public void GetAccentColor() {
			throw new NotImplementedException();
		}

		public void GetColor() {
			throw new NotImplementedException();
		}

		public void IsInvertedColorScheme() {
			throw new NotImplementedException();
		}
	}
}
