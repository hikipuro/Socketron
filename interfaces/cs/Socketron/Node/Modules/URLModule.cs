using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	public partial class NodeModules {
		[type: SuppressMessage("Style", "IDE1006")]
		public class URL : NodeModule {
			public URL() {
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
}
