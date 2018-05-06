using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Communicate asynchronously from a renderer process to the main process.
	/// <para>Process: Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class IPCRenderer : NodeModule {
		public const string Name = "IPCRenderer";

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public IPCRenderer(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		/// <summary>
		/// Send a message to the main process asynchronously via channel.
		/// </summary>
		/// <param name="channel"></param>
		/// <param name="args"></param>
		public void send(string channel, params object[] args) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns any - The value sent back by the ipcMain handler.
		/// </summary>
		/// <param name="channel"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public JsonObject sendSync(string channel, params object[] args) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Sends a message to a window with windowid via channel.
		/// </summary>
		/// <param name="windowId"></param>
		/// <param name="channel"></param>
		/// <param name="args"></param>
		public void sendTo(int windowId, string channel, params object[] args) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Like ipcRenderer.send but the event will be sent to
		/// the &lt;webview&gt; element in the host page instead of the main process.
		/// </summary>
		/// <param name="channel"></param>
		/// <param name="args"></param>
		public void sendToHost(string channel, params object[] args) {
			// TODO: implement this
			throw new NotImplementedException();
		}
	}
}
