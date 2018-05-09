using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class EventTarget : DOMModule {
		public EventTarget() {
			_client = SocketronClient.GetCurrent();
		}

		public void addEventListener(string eventName, JSCallback listener) {
			if (listener == null) {
				return;
			}
			CallbackItem item = null;
			item = _CreateCallbackItem(eventName, (object[] args) => {
				listener?.Invoke(args);
			});
			string script = ScriptBuilder.Build(
				"{0}.addEventListener({1},{2});",
				Script.GetObject(_id),
				eventName.Escape(),
				Script.GetObject(item.ObjectId)
			);
			_ExecuteJavaScript(script);
		}

		/*
		public void removeEventListener(string eventName, JSCallback listener) {
		}

		public void dispatchEvent(string eventName, JSCallback listener) {
		}
		//*/
	}
}
