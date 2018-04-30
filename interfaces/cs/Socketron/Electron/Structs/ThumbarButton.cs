using System;
using System.Web.Script.Serialization;

namespace Socketron {
	public class ThumbarButton {
		public class Flags {
			public const string enabled = "enabled";
			public const string disabled = "disabled";
			public const string dismissonclick = "dismissonclick";
			public const string nobackground = "nobackground";
			public const string hidden = "hidden";
			public const string noninteractive = "noninteractive";
		}

		public NativeImage icon;
		public Action click;
		public string tooltip;
		public string[] flags;

		public static ThumbarButton Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<ThumbarButton>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
