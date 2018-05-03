using System;

namespace Socketron {
	public class ThumbarButton {
		public class Flags {
			/// <summary>
			/// The button is active and available to the user.
			/// </summary>
			public const string enabled = "enabled";
			/// <summary>
			/// The button is disabled.
			/// It is present, but has a visual state indicating it will not respond to user action.
			/// </summary>
			public const string disabled = "disabled";
			/// <summary>
			/// When the button is clicked, the thumbnail window closes immediately.
			/// </summary>
			public const string dismissonclick = "dismissonclick";
			/// <summary>
			/// Do not draw a button border, use only the image.
			/// </summary>
			public const string nobackground = "nobackground";
			/// <summary>
			/// The button is not shown to the user.
			/// </summary>
			public const string hidden = "hidden";
			/// <summary>
			/// The button is enabled but not interactive;
			/// no pressed button state is drawn.
			/// This value is intended for instances where the button is used in a notification.
			/// </summary>
			public const string noninteractive = "noninteractive";
		}

		/// <summary>
		/// The icon showing in thumbnail toolbar.
		/// </summary>
		public NativeImage icon;
		public Action click;
		/// <summary>
		/// (optional) The text of the button's tooltip.
		/// </summary>
		public string tooltip;
		/// <summary>
		/// (optional) Control specific states and behaviors of the button.
		/// By default, it is ['enabled'].
		/// </summary>
		public string[] flags;

		public static ThumbarButton Parse(string text) {
			return JSON.Parse<ThumbarButton>(text);
		}

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <returns></returns>
		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
