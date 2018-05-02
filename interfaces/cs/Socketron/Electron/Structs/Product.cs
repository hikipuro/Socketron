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
			return JSON.Parse<Product>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
