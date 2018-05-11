using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Promise : JSModule {
		public Promise() {
		}

		public Promise then(JSCallback onFulfilled) {
			string eventName = "_then";
			CallbackItem onFulfilledItem = API.CreateCallbackItem(eventName, onFulfilled);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var promise = {0}.then({1});",
					"return {2}"
				),
				Script.GetObject(API.id),
				Script.GetObject(onFulfilledItem.ObjectId),
				Script.AddObject("promise")
			);
			int result = API._ExecuteBlocking<int>(script);
			return API.CreateObject<Promise>(result);
		}

		public Promise then(JSCallback onFulfilled, JSCallback onRejected) {
			string eventName = "_then";
			CallbackItem onFulfilledItem = API.CreateCallbackItem(eventName, onFulfilled);
			CallbackItem onRejectedItem = API.CreateCallbackItem(eventName, onRejected);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var promise = {0}.then({1},{2});",
					"return {3}"
				),
				Script.GetObject(API.id),
				Script.GetObject(onFulfilledItem.ObjectId),
				Script.GetObject(onRejectedItem.ObjectId),
				Script.AddObject("promise")
			);
			int result = API._ExecuteBlocking<int>(script);
			return API.CreateObject<Promise>(result);
		}

		public Promise @catch(JSCallback onRejected) {
			string eventName = "_catch";
			CallbackItem onRejectedItem = API.CreateCallbackItem(eventName, onRejected);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var promise = {0}.catch({1});",
					"return {2}"
				),
				Script.GetObject(API.id),
				Script.GetObject(onRejectedItem.ObjectId),
				Script.AddObject("promise")
			);
			int result = API._ExecuteBlocking<int>(script);
			return API.CreateObject<Promise>(result);
		}

		public Promise @finally(JSCallback onFinally) {
			string eventName = "_finally";
			CallbackItem onFinallyItem = API.CreateCallbackItem(eventName, onFinally);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var promise = {0}.finally({1});",
					"return {2}"
				),
				Script.GetObject(API.id),
				Script.GetObject(onFinallyItem.ObjectId),
				Script.AddObject("promise")
			);
			int result = API._ExecuteBlocking<int>(script);
			return API.CreateObject<Promise>(result);
		}
	}
}
