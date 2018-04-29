using System.Web.Script.Serialization;

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
			var serializer = new JavaScriptSerializer();
			return serializer.Deserialize<Transaction>(text);
		}

		public string Stringify() {
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(this);
		}
	}
}

