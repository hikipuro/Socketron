using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Socketron {
	class NullPropertiesConverter : JavaScriptConverter {
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer) {
			throw new NotImplementedException();
		}

		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer) {
			if (obj is Dictionary<string, object>) {
				/*
				foreach (var item in obj as Dictionary<string, object>) {
					json.Add(item.Key, item.Value);
				}
				return json;
				//*/
				return new Dictionary<string, object>(obj as Dictionary<string, object>);
			}
			var json = new Dictionary<string, object>();
			FieldInfo[] fields = obj.GetType().GetFields();
			foreach (FieldInfo field in fields) {
				//check if decorated with ScriptIgnore attribute
				//bool ignoreProp = field.IsDefined(typeof(ScriptIgnoreAttribute), true);
				object value = field.GetValue(obj);
				if (value == null) { // && !ignoreProp) {
					continue;
				}
				json.Add(field.Name, value);
			}
			return json;
		}

		public override IEnumerable<Type> SupportedTypes {
			get { return GetType().Assembly.GetTypes(); }
		}
	}
}
