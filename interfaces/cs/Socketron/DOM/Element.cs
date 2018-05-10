using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Element : Node {
		public Element() {
		}

		/*
		[Experimental]
		public string assignedSlot {
			get { return API.GetProperty<string>("assignedSlot"); }
		}
		//*/

		public NamedNodeMap attributes {
			get { return API.GetObject<NamedNodeMap>("attributes"); }
		}

		public DOMTokenList classList {
			get { return API.GetProperty<DOMTokenList>("classList"); }
		}

		public string className {
			get { return API.GetProperty<string>("className"); }
		}

		[Experimental]
		public int clientHeight {
			get { return API.GetProperty<int>("clientHeight"); }
		}

		[Experimental]
		public int clientLeft {
			get { return API.GetProperty<int>("clientLeft"); }
		}

		[Experimental]
		public int clientTop {
			get { return API.GetProperty<int>("clientTop"); }
		}

		[Experimental]
		public int clientWidth {
			get { return API.GetProperty<int>("clientWidth"); }
		}

		public string computedName {
			get { return API.GetProperty<string>("computedName"); }
		}

		public string computedRole {
			get { return API.GetProperty<string>("computedRole"); }
		}

		public string id {
			get { return API.GetProperty<string>("id"); }
			set { API.SetProperty("id", value); }
		}

		public string innerHTML {
			get { return API.GetProperty<string>("innerHTML"); }
			set { API.SetProperty("innerHTML", value); }
		}

		public string localName {
			get { return API.GetProperty<string>("localName"); }
		}

		public string namespaceURI {
			get { return API.GetProperty<string>("namespaceURI"); }
		}

		public Element nextElementSibling {
			get { return API.GetObject<Element>("nextElementSibling"); }
		}

		[Experimental]
		public string outerHTML {
			get { return API.GetProperty<string>("outerHTML"); }
		}

		public string prefix {
			get { return API.GetProperty<string>("prefix"); }
		}

		public Element previousElementSibling {
			get { return API.GetObject<Element>("previousElementSibling"); }
		}

		[Experimental]
		public int scrollHeight {
			get { return API.GetProperty<int>("scrollHeight"); }
		}

		[Experimental]
		public int scrollLeft {
			get { return API.GetProperty<int>("scrollLeft"); }
		}

		[Experimental]
		public int scrollTop {
			get { return API.GetProperty<int>("scrollTop"); }
		}

		[Experimental]
		public int scrollWidth {
			get { return API.GetProperty<int>("scrollWidth"); }
		}

		/*
		[Experimental]
		public int shadowRoot {
			get { return API.GetProperty<int>("shadowRoot"); }
		}
		//*/

		[Experimental]
		public string slot {
			get { return API.GetProperty<string>("slot"); }
		}

		public bool tabStop {
			get { return API.GetProperty<bool>("tabStop"); }
		}

		public string tagName {
			get { return API.GetProperty<string>("tagName"); }
		}

		/*
		[Experimental]
		public int undoManager {
			get { return API.GetProperty<int>("undoManager"); }
		}
		//*/

		/*
		[Experimental]
		public int undoScope {
			get { return API.GetProperty<int>("undoScope"); }
		}
		//*/

		/*
		public void attachShadow(JsonObject shadowRootInit) {
		}
		//*/

		/*
		public void animate(JsonObject keyframes, int keyframeOptions) {
		}
		//*/
	}
}
