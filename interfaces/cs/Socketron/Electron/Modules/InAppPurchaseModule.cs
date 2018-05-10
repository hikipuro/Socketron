using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// In-app purchases on Mac App Store.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class InAppPurchaseModule : JSModule {
		public const string Name = "InAppPurchase";

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		/// <summary>
		/// This method is used for internally by the library.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Callback GetCallbackFromId(ushort id) {
			if (!_callbackList.ContainsKey(id)) {
				return null;
			}
			return _callbackList[id];
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public InAppPurchaseModule(SocketronClient client) {
			API.client = client;
		}

		/// <summary>
		/// You should listen for the transactions-updated event
		/// as soon as possible and certainly before you call purchaseProduct.
		/// </summary>
		/// <param name="productID">
		///  The identifiers of the product to purchase.
		///  (The identifier of com.example.app.product1 is product1).
		/// </param>
		public void purchaseProduct(string productID) {
			string script = ScriptBuilder.Build(
				"electron.inAppPurchase.purchaseProduct({0});",
				productID.Escape()
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Retrieves the product descriptions.
		/// </summary>
		/// <param name="productIDs">
		/// The identifiers of the products to get.
		/// </param>
		/// <param name="callback">
		/// The callback called with the products
		/// or an empty array if the products don't exist.
		/// </param>
		public void getProducts(string[] productIDs, Action<Product[]> callback) {
			if (callback == null) {
				return;
			}
			ushort callbackId = _callbackListId;
			_callbackList.Add(_callbackListId, (object args) => {
				_callbackList.Remove(callbackId);
				object[] argsList = args as object[];
				if (argsList == null) {
					return;
				}
				Product[] products = Array.ConvertAll(
					argsList[0] as object[],
					value => Product.Parse(value as string)
				);
				callback?.Invoke(products);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (products) => {{",
						"this.emit('__event',{0},{1},products);",
					"}};",
					"electron.inAppPurchase.getProducts({2},callback);"
				),
				Name.Escape(),
				_callbackListId,
				productIDs.Stringify()
			);
			_callbackListId++;
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean, whether a user can make a payment.
		/// </summary>
		/// <returns></returns>
		public bool canMakePayments() {
			string script = ScriptBuilder.Build(
				"return electron.inAppPurchase.canMakePayments();"
			);
			return API._ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Returns String, the path to the receipt.
		/// </summary>
		/// <returns></returns>
		public string getReceiptURL() {
			string script = ScriptBuilder.Build(
				"return electron.inAppPurchase.getReceiptURL();"
			);
			return API._ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Completes all pending transactions.
		/// </summary>
		public void finishAllTransactions() {
			string script = ScriptBuilder.Build(
				"electron.inAppPurchase.finishAllTransactions();"
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Completes the pending transactions corresponding to the date.
		/// </summary>
		/// <param name="date">
		/// The ISO formatted date of the transaction to finish.
		/// </param>
		public void finishTransactionByDate(string date) {
			string script = ScriptBuilder.Build(
				"electron.inAppPurchase.finishTransactionByDate({0});",
				date.Escape()
			);
			API.ExecuteJavaScript(script);
		}
	}
}
