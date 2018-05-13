using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// In-app purchases on Mac App Store.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class InAppPurchaseModule : JSObject {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public InAppPurchaseModule() {
		}

		public EventEmitter on(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.on(eventName, listener);
		}

		public EventEmitter once(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.once(eventName, listener);
		}

		public EventEmitter addListener(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.addListener(eventName, listener);
		}

		public EventEmitter removeListener(string eventName, JSCallback listener) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.removeListener(eventName, listener);
		}

		public EventEmitter removeAllListeners(string eventName) {
			EventEmitter emitter = API.ConvertTypeTemporary<EventEmitter>();
			return emitter.removeAllListeners(eventName);
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
			API.Apply("purchaseProduct", productID);
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
			string eventName = "_getProducts";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				// TODO: check Product[]
				API.RemoveCallbackItem(eventName, item);
				JSObject _products = API.CreateObject<JSObject>(args[0]);
				Product[] products = Array.ConvertAll(
					_products.API.GetValue() as object[],
					value => Product.FromObject(value)
				);
				callback?.Invoke(products);
			});
			string script = ScriptBuilder.Build(
				"{0}.getProducts({1},{2});",
				Script.GetObject(API.id),
				productIDs.Stringify(),
				Script.GetObject(item.ObjectId)
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean, whether a user can make a payment.
		/// </summary>
		/// <returns></returns>
		public bool canMakePayments() {
			return API.Apply<bool>("canMakePayments");
		}

		/// <summary>
		/// Returns String, the path to the receipt.
		/// </summary>
		/// <returns></returns>
		public string getReceiptURL() {
			return API.Apply<string>("getReceiptURL");
		}

		/// <summary>
		/// Completes all pending transactions.
		/// </summary>
		public void finishAllTransactions() {
			API.Apply("finishAllTransactions");
		}

		/// <summary>
		/// Completes the pending transactions corresponding to the date.
		/// </summary>
		/// <param name="date">
		/// The ISO formatted date of the transaction to finish.
		/// </param>
		public void finishTransactionByDate(string date) {
			API.Apply("finishTransactionByDate", date);
		}
	}
}
