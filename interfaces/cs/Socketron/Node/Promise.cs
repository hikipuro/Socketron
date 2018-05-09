using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Promise : JSModule {
		public Promise(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

		public Promise then(JSCallback onFulfilled) {
			string eventName = "_then";
			CallbackItem onFulfilledItem = _CreateCallbackItem(eventName, onFulfilled);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var promise = {0}.then({1});",
					"return {2}"
				),
				Script.GetObject(_id),
				Script.GetObject(onFulfilledItem.ObjectId),
				Script.AddObject("promise")
			);
			int result = _ExecuteBlocking<int>(script);
			return new Promise(_client, result);
		}

		public Promise then(JSCallback onFulfilled, JSCallback onRejected) {
			string eventName = "_then";
			CallbackItem onFulfilledItem = _CreateCallbackItem(eventName, onFulfilled);
			CallbackItem onRejectedItem = _CreateCallbackItem(eventName, onRejected);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var promise = {0}.then({1},{2});",
					"return {3}"
				),
				Script.GetObject(_id),
				Script.GetObject(onFulfilledItem.ObjectId),
				Script.GetObject(onRejectedItem.ObjectId),
				Script.AddObject("promise")
			);
			int result = _ExecuteBlocking<int>(script);
			return new Promise(_client, result);
		}

		public Promise @catch(JSCallback onRejected) {
			string eventName = "_catch";
			CallbackItem onRejectedItem = _CreateCallbackItem(eventName, onRejected);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var promise = {0}.catch({1});",
					"return {2}"
				),
				Script.GetObject(_id),
				Script.GetObject(onRejectedItem.ObjectId),
				Script.AddObject("promise")
			);
			int result = _ExecuteBlocking<int>(script);
			return new Promise(_client, result);
		}

		public Promise @finally(JSCallback onFinally) {
			string eventName = "_finally";
			CallbackItem onFinallyItem = _CreateCallbackItem(eventName, onFinally);
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var promise = {0}.finally({1});",
					"return {2}"
				),
				Script.GetObject(_id),
				Script.GetObject(onFinallyItem.ObjectId),
				Script.AddObject("promise")
			);
			int result = _ExecuteBlocking<int>(script);
			return new Promise(_client, result);
		}
	}
}
