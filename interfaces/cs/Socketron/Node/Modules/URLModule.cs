using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	public partial class NodeModules {
		/// <summary>
		/// URL module of the Node API.
		/// </summary>
		[type: SuppressMessage("Style", "IDE1006")]
		public class URL : JSObject {
			/// <summary>
			/// This constructor is used for internally by the library.
			/// </summary>
			public URL() {
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
