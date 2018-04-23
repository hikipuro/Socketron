﻿using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Socketron {
	public class JsonObject : Dictionary<string, object> {
		public JsonObject() {
		}

		public JsonObject(Dictionary<string, object> dictionary) {
			foreach (KeyValuePair<string, object> item in dictionary) {
				Add(item.Key, item.Value);
			}
		}

		public static JsonObject Parse(string text) {
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			var data = serializer.Deserialize<JsonObject>(text);
			//JsonObject json = new JsonObject();
			//json._objects = data;
			return data;
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
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}