namespace Socketron {
	public class Transaction {
		/// <summary>
		/// A string that uniquely identifies a successful payment transaction.
		/// </summary>
		public string transactionIdentifier;
		/// <summary>
		/// The date the transaction was added to the App Store’s payment queue.
		/// </summary>
		public string transactionDate;
		/// <summary>
		/// The identifier of the restored transaction by the App Store.
		/// </summary>
		public string originalTransactionIdentifier;
		/// <summary>
		/// The transaction sate ("purchasing", "purchased", "failed", "restored", or "deferred")
		/// </summary>
		public string transactionState;
		/// <summary>
		/// The error code if an error occurred while processing the transaction.
		/// </summary>
		public int? errorCode;
		/// <summary>
		/// The error message if an error occurred while processing the transaction.
		/// </summary>
		public string errorMessage;
		/// <summary>
		/// - "productIdentifier" String - The identifier of the purchased product.
		/// - "quantity" Integer - The quantity purchased.
		/// </summary>
		public object payment;

		public static Transaction Parse(string text) {
			return JSON.Parse<Transaction>(text);
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

