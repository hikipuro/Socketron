using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class DOMTokenList : DOMModule {
		public DOMTokenList() {
		}

		public int length {
			get { return API.GetProperty<int>("length"); }
		}

		public string value {
			get { return API.GetProperty<string>("value"); }
		}
	}
}
