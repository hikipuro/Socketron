using System;
using System.Collections.Generic;

namespace Socketron {
	public class JsonObject : Dictionary<string, object> {
		public JsonObject() {
		}

		public JsonObject(Dictionary<string, object> obj) {
			if (obj == null) {
				return;
			}
			foreach (var item in obj) {
				Add(item.Key, item.Value);
			}
		}

		public JsonObject(object obj) {
			if (obj is Dictionary<string, object>) {
				foreach (var item in obj as Dictionary<string, object>) {
					Add(item.Key, item.Value);
				}
			}
		}

		public static JsonObject Parse(string text) {
			return JSON.Parse<JsonObject>(text);
		}

		public static JsonObject FromObject(object obj) {
			if (obj is Dictionary<string, object>) {
				var json = new JsonObject(obj as Dictionary<string, object>);
				return json;
			}
			return null;
		}

		public static List<JsonObject> FromArray(object[] array) {
			if (array == null) {
				return null;
			}
			List<JsonObject> list = new List<JsonObject>();
			foreach (object item in array) {
				list.Add(FromObject(item));
			}
			return list;
		}

		public static object[] Array(params object[] args) {
			return args;
		}

		public new object this[string name] {
			get {
				if (!ContainsKey(name)) {
					return null;
				}
				return base[name];
			}
			set { base[name] = value; }
		}

		public string String(string name) {
			return this[name] as string;
		}

		public double Double(string name) {
			object obj = this[name];
			Type type = obj.GetType();
			if (type == typeof(int)) {
				return (int)obj;
			}
			if (type == typeof(decimal)) {
				return (double)(decimal)obj;
			}
			return (double)obj;
		}

		public long Int64(string name) {
			object obj = this[name];
			Type type = obj.GetType();
			if (type == typeof(decimal)) {
				return (long)(decimal)obj;
			}
			return (long)obj;
		}

		public int Int32(string name) {
			object obj = this[name];
			Type type = obj.GetType();
			if (type == typeof(decimal)) {
				return (int)(decimal)obj;
			}
			return (int)obj;
		}

		public bool Bool(string name) {
			object obj = this[name];
			return Convert.ToBoolean(obj);
		}

		/*
		public void Add(string name, JsonObject json) {
			this[name] = json;
		}

		public void Add(string name, ArrayList list) {
			this[name] = list;
		}

		public void Remove(string name) {
			_objects.Remove(name);
		}
		*/

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
