using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class CanvasGradient : DOMModule {
		public CanvasGradient() {
		}

		public void addColorStop(double offset, string color) {
			string script = ScriptBuilder.Build(
				"{0}.addColorStop({1},{2});",
				Script.GetObject(API.id),
				offset,
				color.Escape()
			);
			API.ExecuteJavaScript(script);
		}
	}
}
