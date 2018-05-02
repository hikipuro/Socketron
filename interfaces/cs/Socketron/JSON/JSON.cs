using System.Text;
using System.Web.Script.Serialization;

namespace Socketron {
	public class JSON {
		protected static JavaScriptSerializer _serializer;
		protected static JavaScriptSerializer _deserializer;

		static JSON() {
			_serializer = new JavaScriptSerializer();
			_serializer.RegisterConverters(
				new JavaScriptConverter[] {
					new NullPropertiesConverter()
				}
			);
			_deserializer = new JavaScriptSerializer();
		}

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string Stringify(object obj) {
			return _serializer.Serialize(obj);
		}

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="pretty"></param>
		/// <returns></returns>
		public static string Stringify(object obj, bool pretty) {
			if (pretty) {
				return Prettify(Stringify(obj));
			}
			return Stringify(obj);
		}

		/// <summary>
		/// parse JSON text.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="text"></param>
		/// <returns></returns>
		public static T Parse<T>(string text) {
			return _deserializer.Deserialize<T>(text);
		}

		public static string Prettify(string text, string tab = "  ", string newLine = "\n") {
			StringBuilder builder = new StringBuilder();
			int tabs = 0;
			foreach (char ch in text) {
				switch (ch) {
					case '[':
						builder.Append(ch);
						builder.Append('\n');
						tabs++;
						builder.Append(new string('\t', tabs));
						break;
					case ']':
						builder.Append('\n');
						tabs--;
						builder.Append(new string('\t', tabs));
						builder.Append(ch);
						break;
					case '{':
						builder.Append(ch);
						builder.Append('\n');
						tabs++;
						builder.Append(new string('\t', tabs));
						break;
					case '}':
						builder.Append('\n');
						tabs--;
						builder.Append(new string('\t', tabs));
						builder.Append(ch);
						break;
					case ':':
						builder.Append(ch);
						builder.Append(' ');
						break;
					case ',':
						builder.Append(ch);
						builder.Append('\n');
						builder.Append(new string('\t', tabs));
						break;
					default:
						builder.Append(ch);
						break;
				}
			}
			if (tab != "\t") {
				builder.Replace("\t", tab);
			}
			if (newLine != "\n") {
				builder.Replace("\n", newLine);
			}
			return builder.ToString();
		}
	}
}
