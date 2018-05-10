using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class CanvasPattern : DOMModule {
		public CanvasPattern() {
		}

		/*
		[Experimental]
		public void setTransform(SVGMatrix matrix) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"{0}.setTransform({1});"
				),
				Script.GetObject(_id),
				matrix
			);
			_ExecuteJavaScript(script);
		}
		*/
	}
}
