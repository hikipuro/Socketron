using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Path2D : DOMModule {
		public Path2D() {
		}

		public void addPath(Path2D path) {
			string script = ScriptBuilder.Build(
				"{0}.addPath({1});",
				Script.GetObject(API.id),
				Script.GetObject(path.API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public void closePath() {
			string script = ScriptBuilder.Build(
				"{0}.closePath();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}

		public void moveTo(double x, double y) {
			string script = ScriptBuilder.Build(
				"{0}.moveTo({1},{2});",
				Script.GetObject(API.id),
				x, y
			);
			API.ExecuteJavaScript(script);
		}

		public void lineTo(double x, double y) {
			string script = ScriptBuilder.Build(
				"{0}.lineTo({1},{2});",
				Script.GetObject(API.id),
				x, y
			);
			API.ExecuteJavaScript(script);
		}
	}
}
