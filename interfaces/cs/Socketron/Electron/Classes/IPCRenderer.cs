using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Communicate asynchronously from a renderer process to the main process.
	/// <para>Process: Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class IPCRenderer : EventEmitter {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public IPCRenderer() {
		}

		/// <summary>
		/// Send a message to the main process asynchronously via channel.
		/// </summary>
		/// <param name="channel"></param>
		/// <param name="args"></param>
		public void send(string channel, params object[] args) {
			if (args == null || args.Length <= 0) {
				API.Apply("send", channel);
			} else {
				object[] options = new object[args.Length + 1];
				options[0] = channel;
				args.CopyTo(options, 1);
				API.Apply("send", options);
			}
		}

		/// <summary>
		/// Returns any - The value sent back by the ipcMain handler.
		/// </summary>
		/// <param name="channel"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public JsonObject sendSync(string channel, params object[] args) {
			if (args == null || args.Length <= 0) {
				object result = API.Apply("sendSync", channel);
				return new JsonObject(result);
			} else {
				object[] options = new object[args.Length + 1];
				options[0] = channel;
				args.CopyTo(options, 1);
				object result = API.Apply("sendSync", options);
				return new JsonObject(result);
			}
		}

		/// <summary>
		/// Sends a message to a window with windowid via channel.
		/// </summary>
		/// <param name="windowId"></param>
		/// <param name="channel"></param>
		/// <param name="args"></param>
		public void sendTo(int windowId, string channel, params object[] args) {
			if (args == null || args.Length <= 0) {
				API.Apply("sendTo", windowId, channel);
			} else {
				object[] options = new object[args.Length + 2];
				options[0] = windowId;
				options[1] = channel;
				args.CopyTo(options, 2);
				API.Apply("sendTo", options);
			}
		}

		/// <summary>
		/// Like ipcRenderer.send but the event will be sent to
		/// the &lt;webview&gt; element in the host page instead of the main process.
		/// </summary>
		/// <param name="channel"></param>
		/// <param name="args"></param>
		public void sendToHost(string channel, params object[] args) {
			if (args == null || args.Length <= 0) {
				API.Apply("sendToHost", channel);
			} else {
				object[] options = new object[args.Length + 1];
				options[0] = channel;
				args.CopyTo(options, 1);
				API.Apply("sendToHost", options);
			}
		}
	}
}
