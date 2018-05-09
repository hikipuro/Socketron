using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Navigator : DOMModule {
		public Navigator() {
			_client = SocketronClient.GetCurrent();
		}

		public string userAgent {
			get {
				string script = ScriptBuilder.Build(
					"return {0}.userAgent;",
					Script.GetObject(_id)
				);
				return _ExecuteBlocking<string>(script);
			}
		}
	}
}
