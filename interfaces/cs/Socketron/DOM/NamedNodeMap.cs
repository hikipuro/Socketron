using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class NamedNodeMap : DOMModule {
		public NamedNodeMap() {
		}

		public int length {
			get { return API.GetProperty<int>("length"); }
		}

		public string getNamedItem(string name) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return {0}.getNamedItem({1});"
				),
				Script.GetObject(API.id),
				name.Escape()
			);
			return API._ExecuteBlocking<string>(script);
		}

		public void setNamedItem(string name, string item) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.setNamedItem({1});"
				),
				Script.GetObject(API.id),
				name.Escape()
			);
			API.ExecuteJavaScript(script);
		}
	}
}
