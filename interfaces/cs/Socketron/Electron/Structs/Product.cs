using System;

namespace Socketron.Electron {
	public class Product {
		/// <summary>
		/// The string that identifies the product to the Apple App Store.
		/// </summary>
		public string productIdentifier;
		/// <summary>
		/// A description of the product.
		/// </summary>
		public string localizedDescription;
		/// <summary>
		/// The name of the product.
		/// </summary>
		public string localizedTitle;
		/// <summary>
		/// A string that identifies the version of the content.
		/// </summary>
		public string contentVersion;
		/// <summary>
		/// The total size of the content, in bytes.
		/// </summary>
		public int[] contentLengths;
		/// <summary>
		/// The cost of the product in the local currency.
		/// </summary>
		public int? price;
		/// <summary>
		/// The locale formatted price of the product.
		/// </summary>
		public string formattedPrice;
		/// <summary>
		/// A Boolean value that indicates whether the App Store has downloadable content for this product.
		/// </summary>
		public bool? downloadable;

		public static Product FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new Product() {
				productIdentifier = json.String("productIdentifier"),
				localizedDescription = json.String("localizedDescription"),
				localizedTitle = json.String("localizedTitle"),
				contentVersion = json.String("contentVersion"),
				contentLengths = Array.ConvertAll(
					json["contentLengths"] as object[],
					value => Convert.ToInt32(value)
				),
				price = json.Int32("price"),
				formattedPrice = json.String("formattedPrice"),
				downloadable = json.Bool("downloadable")
			};
		}

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static Product Parse(string text) {
			return JSON.Parse<Product>(text);
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
