using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class URLModule : NodeBase {
		public int id;

		public URLModule(Socketron socketron) {
			_socketron = socketron;
		}

		public void require() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var module = this.require({0});",
					"return {1};"
				),
				"url".Escape(),
				Script.AddObject("module")
			);
			id = _ExecuteBlocking<int>(script);
		}

	}
}
