using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Script.Serialization;

namespace Socketron {
	public class Notification : IDisposable {
		public const string Name = "Notification";
		protected Socketron _socketron;
		protected int _ID;

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		public class Options {
			public string title;
			public string subtitle;
			public string body;
			public bool? silent;
			public string icon;
			public bool? hasReply;
			public string replyPlaceholder;
			public string sound;
			//public NotificationAction[] actions;
			public string closeButtonText;

			public string Stringify() {
				var serializer = new JavaScriptSerializer();
				serializer.RegisterConverters(new JavaScriptConverter[] { new NullPropertiesConverter() });
				return serializer.Serialize(this);
			}
		}

		protected Notification() {
		}

		public static Notification Create(Socketron socketron, Options options) {
			string[] script = new[] {
				"var notification = new electron.Notification(",
					options.Stringify(),
				");",
				"return this._addObjectReference(notification);"
			};
			int result = _ExecuteJavaScriptBlocking<int>(socketron, script);
			Notification notification = new Notification() {
				_socketron = socketron,
				_ID = result
			};
			return notification;
		}

		public static bool IsSupported(Socketron socketron) {
			string[] script = new[] {
				"return electron.Notification.isSupported();"
			};
			return _ExecuteJavaScriptBlocking<bool>(socketron, script);
		}

		public void Dispose() {
			string[] script = new[] {
				"this._removeObjectReference(" + _ID + ");"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public void On(string eventName, Callback callback) {
			if (callback == null) {
				return;
			}
			_callbackList.Add(_callbackListId, callback);
			string[] script = new[] {
				"var notification = this._objRefs[" + _ID + "];",
				"if (notification == null) {",
					"return;",
				"}",
				"var listener = () => {",
					"emit('__event'," + Name.Escape() + "," + _callbackListId + ");",
				"};",
				"this._addClientEventListener(" + Name.Escape() + "," + _callbackListId + ",listener);",
				"notification.on(" + eventName.Escape() + ", listener);"
			};
			_callbackListId++;
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public void Once(string eventName, Callback callback) {
			if (callback == null) {
				return;
			}
			_callbackList.Add(_callbackListId, callback);
			string[] script = new[] {
				"var notification = this._objRefs[" + _ID + "];",
				"if (notification == null) {",
					"return;",
				"}",
				"var listener = () => {",
					"emit('__event'," + Name.Escape() + "," + _callbackListId + ");",
				"};",
				"this._addClientEventListener(" + Name.Escape() + "," + _callbackListId + ",listener);",
				"notification.once(" + eventName.Escape() + ", listener);"
			};
			_callbackListId++;
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public void Show() {
			string[] script = new[] {
				"var notification = this._objRefs[" + _ID + "];",
				"if (notification == null) {",
					"return;",
				"}",
				"notification.show();"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		public void Close() {
			string[] script = new[] {
				"var notification = this._objRefs[" + _ID + "];",
				"if (notification == null) {",
					"return;",
				"}",
				"notification.close();"
			};
			_ExecuteJavaScript(_socketron, script, null, null);
		}

		protected static void _ExecuteJavaScript(Socketron socketron, string[] script, Callback success, Callback error) {
			socketron.Main.ExecuteJavaScript(script, success, error);
		}

		protected static T _ExecuteJavaScriptBlocking<T>(Socketron socketron, string[] script) {
			bool done = false;
			T value = default(T);

			_ExecuteJavaScript(socketron, script, (result) => {
				if (result == null) {
					done = true;
					return;
				}
				if (typeof(T) == typeof(double)) {
					//Console.WriteLine(result.GetType());
					if (result.GetType() == typeof(int)) {
						result = (double)(int)result;
					} else if (result.GetType() == typeof(Decimal)) {
						result = (double)(Decimal)result;
					}
				}
				value = (T)result;
				done = true;
			}, (result) => {
				Console.Error.WriteLine("error: Notification._ExecuteJavaScriptBlocking");
				throw new InvalidOperationException(result as string);
				//done = true;
			});

			while (!done) {
				Thread.Sleep(TimeSpan.FromTicks(1));
			}
			return value;
		}
	}
}
