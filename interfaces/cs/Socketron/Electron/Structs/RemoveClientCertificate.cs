﻿namespace Socketron {
	public class RemoveClientCertificate {
		/// <summary>
		/// clientCertificate.
		/// </summary>
		public string type;
		/// <summary>
		/// Origin of the server whose associated client certificate must be removed from the cache.
		/// </summary>
		public string origin;

		public static RemoveClientCertificate Parse(string text) {
			return JSON.Parse<RemoveClientCertificate>(text);
		}

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <returns></returns>
		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
