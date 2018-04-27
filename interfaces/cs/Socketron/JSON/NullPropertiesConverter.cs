using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Socketron {
	class NullPropertiesConverter : JavaScriptConverter {
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer) {
			throw new NotImplementedException();
		}

		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer) {
			var json = new Dictionary<string, object>();
			foreach (var prop in obj.GetType().GetFields()) {
				//check if decorated with ScriptIgnore attribute
				bool ignoreProp = prop.IsDefined(typeof(ScriptIgnoreAttribute), true);

				var value = prop.GetValue(obj);
				if (value != null && !ignoreProp) {
					json.Add(prop.Name, value);
				}
			}
			return json;
		}

		public override IEnumerable<Type> SupportedTypes {
			get { return GetType().Assembly.GetTypes(); }
		}
	}
}
