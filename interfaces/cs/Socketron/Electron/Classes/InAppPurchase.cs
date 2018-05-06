namespace Socketron.Electron {
	/// <summary>
	/// In-app purchases on Mac App Store.
	/// <para>Process: Main</para>
	/// </summary>
	public class InAppPurchase {
		/// <summary>
		/// InAppPurchase module events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when one or more transactions have been updated.
			/// </summary>
			public const string TransactionsUpdated = "transactions-updated";
		}
	}
}
