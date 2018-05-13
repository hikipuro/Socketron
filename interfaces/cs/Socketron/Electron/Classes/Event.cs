using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Event : JSObject {
		public Event() {
		}

		public void preventDefault() {
			API.Apply("preventDefault");
		}

		public WebContents sender {
			get { return API.GetObject<WebContents>("sender"); }
		}

		public JSObject returnValue {
			get { return API.GetObject<JSObject>("returnValue"); }
			set { API.SetObject("returnValue", value); }
		}

		public bool ctrlKey {
			get { return API.GetProperty<bool>("ctrlKey"); }
		}

		public bool metaKey {
			get { return API.GetProperty<bool>("metaKey"); }
		}

		public bool shiftKey {
			get { return API.GetProperty<bool>("shiftKey"); }
		}

		public bool altKey {
			get { return API.GetProperty<bool>("altKey"); }
		}
	}
}
