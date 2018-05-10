using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Manage browser sessions, cookies, cache, proxy settings, etc.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class SessionModule : JSModule {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public SessionModule() {
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public SessionModule(SocketronClient client, int id) {
			API.client = client;
			API.id = id;
		}

		/// <summary>
		/// A Session object, the default session object of the app.
		/// </summary>
		public Session defaultSession {
			get {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = {0}.defaultSession;",
						"return {1};"
					),
					Script.GetObject(API.id),
					Script.AddObject("session")
				);
				int result = API._ExecuteBlocking<int>(script);
				return new Session(API.client, result);
			}
		}

		public Session fromPartition(string partition, JsonObject options = null) {
			string script = string.Empty;
			if (options == null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = {0}.fromPartition({1});",
						"return {2};"
					),
					Script.GetObject(API.id),
					partition.Escape(),
					Script.AddObject("session")
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var session = {0}.fromPartition({1},{2});",
						"return {3};"
					),
					Script.GetObject(API.id),
					partition.Escape(),
					options.Stringify(),
					Script.AddObject("session")
				);
			}
			int result = API._ExecuteBlocking<int>(script);
			return new Session(API.client, result);
		}
	}
}
