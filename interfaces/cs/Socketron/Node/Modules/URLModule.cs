using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class URLModule : NodeModule {
		public URLModule() {
			_client = SocketronClient.GetCurrent();
		}

		/*
		public void require() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var module = this.require({0});",
					"return {1};"
				),
				"url".Escape(),
				Script.AddObject("module")
			);
			_id = _ExecuteBlocking<int>(script);
		}
		//*/

	}
}
