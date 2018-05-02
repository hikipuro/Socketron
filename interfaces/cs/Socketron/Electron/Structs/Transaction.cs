namespace Socketron {
	public class Transaction {
		public string transactionIdentifier;
		public string transactionDate;
		public string originalTransactionIdentifier;
		public string transactionState;
		public int? errorCode;
		public string errorMessage;
		public object payment;

		public static Transaction Parse(string text) {
			return JSON.Parse<Transaction>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

