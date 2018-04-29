using System.Web.Script.Serialization;

namespace Socketron {
	public class Product {
		public string productIdentifier;
		public string localizedDescription;
		public string localizedTitle;
		public string contentVersion;
		public int[] contentLengths;
		public int? price;
		public string formattedPrice;
		public bool? downloadable;

		public static Product Parse(string text) {
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<Product>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}
