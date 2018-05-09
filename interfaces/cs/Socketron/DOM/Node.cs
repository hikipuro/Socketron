using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Node : EventTarget {
		public Node() {
			_client = SocketronClient.GetCurrent();
		}

		public string baseURI {
			get { return GetProperty<string>("baseURI"); }
		}

		/*
		public string childNodes {
			get { return GetProperty<string>("childNodes"); }
		}
		//*/

		/*
		public string firstChild {
			get { return GetProperty<string>("firstChild"); }
		}
		//*/

		public bool isConnected {
			get { return GetProperty<bool>("isConnected"); }
		}

		/*
		public bool nextSibling {
			get { return GetProperty<bool>("nextSibling"); }
		}
		//*/

		public string nodeName {
			get { return GetProperty<string>("nodeName"); }
		}

		public ushort nodeType {
			get { return GetProperty<ushort>("nodeType"); }
		}

		/*
		public bool nodeValue {
			get { return GetProperty<bool>("nodeValue"); }
		}
		//*/

		/*
		public bool ownerDocument {
			get { return GetProperty<bool>("ownerDocument"); }
		}
		//*/

		/*
		public bool parentNode {
			get { return GetProperty<bool>("parentNode"); }
		}
		//*/

		/*
		public bool parentElement {
			get { return GetProperty<bool>("parentElement"); }
		}
		//*/

		/*
		public bool previousSibling {
			get { return GetProperty<bool>("previousSibling"); }
		}
		//*/

		/*
		public bool textContent {
			get { return GetProperty<bool>("textContent"); }
		}
		//*/

		/*
		public bool rootNode {
			get { return GetProperty<bool>("rootNode"); }
		}
		//*/
	}
}
