﻿using System.Web.Script.Serialization;

namespace Socketron {
	public class Rectangle {
		public int x;
		public int y;
		public int width;
		public int height;

		public static Rectangle FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new Rectangle() {
				x = json.Int32("x"),
				y = json.Int32("y"),
				width = json.Int32("width"),
				height = json.Int32("height")
			};
		}

		public static Rectangle Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<Rectangle>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}