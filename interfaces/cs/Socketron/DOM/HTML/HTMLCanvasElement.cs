using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class HTMLCanvasElement : HTMLElement {
		public HTMLCanvasElement() {
		}

		public int height {
			get { return API.GetProperty<int>("height"); }
		}

		public int width {
			get { return API.GetProperty<int>("width"); }
		}

		/*
		public void captureStream() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var stream = {0}.captureStream();",
					"return {1};"
				),
				Script.GetObject(_id),
				Script.AddObject("stream")
			);
			int id = _ExecuteBlocking<int>(script);
		}
		//*/

		public RenderingContext getContext(string contextType) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var context = {0}.getContext({1});",
					"return {2};"
				),
				Script.GetObject(API.id),
				contextType.Escape(),
				Script.AddObject("context")
			);
			int id = API._ExecuteBlocking<int>(script);
			if (contextType == "2d") {
				return API.CreateObject<CanvasRenderingContext2D>(id);
			}
			if (contextType == "webgl") {
				return API.CreateObject<WebGLRenderingContext>(id);
			}
			return API.CreateObject<RenderingContext>(id);
		}
	}
}
