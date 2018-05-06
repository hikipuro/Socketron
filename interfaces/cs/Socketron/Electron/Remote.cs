﻿using System;
using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Use main process modules from the renderer process.
	/// <para>Process: Renderer</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Remote {
		/// <summary>
		/// The process object in the main process.
		/// This is the same as remote.getGlobal('process') but is cached.
		/// </summary>
		public Process process {
			get {
				// TODO: implement this
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Returns any - The object returned by require(module) in the main process.
		/// Modules specified by their relative path will resolve relative to
		/// the entrypoint of the main process.
		/// </summary>
		/// <param name="module"></param>
		/// <returns></returns>
		public JsonObject require(string module) {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns BrowserWindow - The window to which this web page belongs.
		/// </summary>
		/// <returns></returns>
		public BrowserWindow getCurrentWindow() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns WebContents - The web contents of this web page.
		/// </summary>
		/// <returns></returns>
		public WebContents getCurrentWebContents() {
			// TODO: implement this
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns any - The global variable of name (e.g. global[name]) in the main process.
		/// </summary>
		/// <returns></returns>
		public JsonObject getGlobal() {
			// TODO: implement this
			throw new NotImplementedException();
		}
	}
}
