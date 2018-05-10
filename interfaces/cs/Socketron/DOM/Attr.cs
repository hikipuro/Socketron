using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Attr : DOMModule {
		public Attr() {
		}

		public string name {
			get { return API.GetProperty<string>("name"); }
		}

		public string namespaceURI {
			get { return API.GetProperty<string>("namespaceURI"); }
		}

		public string localname {
			get { return API.GetProperty<string>("localname"); }
		}

		public string prefix {
			get { return API.GetProperty<string>("prefix"); }
		}

		public string specified {
			get { return API.GetProperty<string>("specified"); }
		}

		public string value {
			get { return API.GetProperty<string>("value"); }
		}
	}
}
