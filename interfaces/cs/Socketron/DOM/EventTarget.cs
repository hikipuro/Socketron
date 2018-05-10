using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class EventTarget : DOMModule {
		public EventTarget() {
		}

		public void addEventListener(string eventName, JSCallback listener) {
			if (listener == null) {
				return;
			}
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				listener?.Invoke(args);
			});
			string script = ScriptBuilder.Build(
				"{0}.addEventListener({1},{2});",
				Script.GetObject(API.id),
				eventName.Escape(),
				Script.GetObject(item.ObjectId)
			);
			API.ExecuteJavaScript(script);
		}

		/*
		public void removeEventListener(string eventName, JSCallback listener) {
		}

		public void dispatchEvent(string eventName, JSCallback listener) {
		}
		//*/
	}
}
