using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class ImageBitmap : DOMModule {
		public ImageBitmap() {
		}

		public uint height {
			get { return API.GetProperty<uint>("height"); }
		}

		public uint width {
			get { return API.GetProperty<uint>("width"); }
		}

		public void close() {
			string script = ScriptBuilder.Build(
				"{0}.close();",
				Script.GetObject(API.id)
			);
			API.ExecuteJavaScript(script);
		}
	}
}
