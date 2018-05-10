using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class HTMLElement : Element {
		public HTMLElement() {
		}

		public string accessKey {
			get { return API.GetProperty<string>("accessKey"); }
		}

		public string accessKeyLabel {
			get { return API.GetProperty<string>("accessKeyLabel"); }
		}

		public string contentEditable {
			get { return API.GetProperty<string>("contentEditable"); }
		}

		public bool isContentEditable {
			get { return API.GetProperty<bool>("isContentEditable"); }
		}

		/*
		public string contextMenu {
			get { return API.GetProperty<string>("contextMenu"); }
		}
		//*/

		/*
		public string dataset {
			get { return API.GetProperty<string>("dataset"); }
		}
		//*/

		public string dir {
			get { return API.GetProperty<string>("dir"); }
		}

		public bool draggable {
			get { return API.GetProperty<bool>("draggable"); }
		}

		/*
		public string dropzone {
			get { return API.GetProperty<string>("dropzone"); }
		}
		//*/

		public bool hidden {
			get { return API.GetProperty<bool>("hidden"); }
		}

		public bool itemScope {
			get { return API.GetProperty<bool>("itemScope"); }
		}

		/*
		public bool itemType {
			get { return API.GetProperty<bool>("itemType"); }
		}
		//*/

		public string itemId {
			get { return API.GetProperty<string>("itemId"); }
		}

		/*
		public bool itemRef {
			get { return API.GetProperty<bool>("itemRef"); }
		}
		//*/

		/*
		public bool itemProp {
			get { return API.GetProperty<bool>("itemProp"); }
		}
		//*/

		/*
		public bool itemValue {
			get { return API.GetProperty<bool>("itemValue"); }
		}
		//*/

		public string lang {
			get { return API.GetProperty<string>("lang"); }
		}

		public double offsetHeight {
			get { return API.GetProperty<double>("offsetHeight"); }
		}

		public double offsetLeft {
			get { return API.GetProperty<double>("offsetLeft"); }
		}

		public Element offsetParent {
			get { return API.GetObject<Element>("offsetParent"); }
		}

		public double offsetTop {
			get { return API.GetProperty<double>("offsetTop"); }
		}

		public double offsetWidth {
			get { return API.GetProperty<double>("offsetWidth"); }
		}

		/*
		public bool properties {
			get { return API.GetProperty<bool>("properties"); }
		}
		//*/

		public bool spellcheck {
			get { return API.GetProperty<bool>("hidden"); }
		}

		/*
		public bool style {
			get { return API.GetProperty<bool>("style"); }
		}
		//*/

		public long tabIndex {
			get { return API.GetProperty<long>("tabIndex"); }
		}

		public string title {
			get { return API.GetProperty<string>("title"); }
		}

		public bool translate {
			get { return API.GetProperty<bool>("translate"); }
		}

		public void blur() {
			API.Execute("self.blur()");
		}

		public void click() {
			API.Execute("self.click()");
		}

		public void focus() {
			API.Execute("self.focus()");
		}

		public void forceSpellCheck() {
			API.Execute("self.forceSpellCheck()");
		}
	}
}
