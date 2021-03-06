﻿using System;

namespace Socketron.Electron {
	public class CrashReport {
		public DateTime date;
		public string id;

		public static CrashReport FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new CrashReport() {
				date = DateTime.Parse(json["date"] as string),
				id = json.String("id")
			};
		}

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static CrashReport Parse(string text) {
			return JSON.Parse<CrashReport>(text);
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

