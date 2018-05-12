using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class ThumbarButton : JSObject {
		public class Flags {
			/// <summary>
			/// The button is active and available to the user.
			/// </summary>
			public const string Enabled = "enabled";
			/// <summary>
			/// The button is disabled.
			/// It is present, but has a visual state indicating it will not respond to user action.
			/// </summary>
			public const string Disabled = "disabled";
			/// <summary>
			/// When the button is clicked, the thumbnail window closes immediately.
			/// </summary>
			public const string Dismissonclick = "dismissonclick";
			/// <summary>
			/// Do not draw a button border, use only the image.
			/// </summary>
			public const string Nobackground = "nobackground";
			/// <summary>
			/// The button is not shown to the user.
			/// </summary>
			public const string Hidden = "hidden";
			/// <summary>
			/// The button is enabled but not interactive;
			/// no pressed button state is drawn.
			/// This value is intended for instances where the button is used in a notification.
			/// </summary>
			public const string Noninteractive = "noninteractive";
		}

		/// <summary>
		/// The icon showing in thumbnail toolbar.
		/// </summary>
		public NativeImage icon {
			get { return API.GetObject<NativeImage>("icon"); }
		}
		public JSCallback click {
			get {
				// TODO: implement this
				throw new NotImplementedException();
			}
		}
		/// <summary>
		/// (optional) The text of the button's tooltip.
		/// </summary>
		public string tooltip {
			get { return API.GetProperty<string>("tooltip"); }
		}
		/// <summary>
		/// (optional) Control specific states and behaviors of the button.
		/// By default, it is ['enabled'].
		/// </summary>
		public string[] flags {
			get {
				object[] result = API.GetProperty<object[]>("flags");
				return Array.ConvertAll(result, value => Convert.ToString(value));
			}
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
