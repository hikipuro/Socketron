using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace Socketron {
	/// <summary>
	/// A JavaScript object.
	/// JavaScript objects are extended from this class.
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class JSObject : IDisposable {
		/// <summary>
		/// Instances extended from this class.
		/// </summary>
		protected static List<JSObject> _instances;

		/// <summary>
		/// Static constructor.
		/// This method is used for internally by the library.
		/// </summary>
		static JSObject() {
			_instances = new List<JSObject>();
		}

		/// <summary>
		/// This method is used for internally by the library.
		/// Dispose all JavaScript objects in the Node side.
		/// </summary>
		public static void DisposeAll() {
			foreach (JSObject instance in _instances) {
				if (instance.API.id <= 0) {
					continue;
				}
				instance.Dispose();
			}
			_instances.Clear();
		}

		/// <summary>
		/// id property means object reference id in the Node side.
		/// There are no JavaScript object instances in the C# side.
		/// </summary>
		public class SocketronAPI {
			/// <summary>
			/// This id is used for internally by the library.
			/// </summary>
			public int id;

			/// <summary>
			/// Socketron connection.
			/// </summary>
			public SocketronClient client;

			/// <summary>
			/// Dispose manually.
			/// If true, not dispose the JavaScript instance in Dispose() method.
			/// </summary>
			public bool disposeManually = false;

			/// <summary>
			/// This method is intended for use in duck typing.
			/// There are currently few memory leaks while connecting.
			/// </summary>
			/// <typeparam name="T">
			/// The class extended from JSModule.
			/// It have to implement a no arguments constructor.
			/// </typeparam>
			/// <returns>Instance of T.</returns>
			public T ConvertType<T>() where T : JSObject, new() {
				// TODO: fix memory leak
				T converted = new T();
				converted.API.client = client;
				converted.API.id = id;
				converted.API.disposeManually = true;
				disposeManually = true;
				return converted;
			}

			public T ConvertTypeTemporary<T>() where T : JSObject, new() {
				T converted = CreateObject<T>(id);
				converted.API.disposeManually = true;
				return converted;
			}

			/// <summary>
			/// Execute JavaScript.
			/// This method is implemented in I/O non-blocking manner.
			/// <para>
			/// "self" keyword can be used to refer to the current object.
			/// </para>
			/// </summary>
			/// <param name="script"></param>
			public void Execute(string script) {
				string code = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var self = {0};",
						"{1};"
					),
					Script.GetObject(id),
					script
				);
				ExecuteJavaScript(code);
			}

			/// <summary>
			/// Execute JavaScript.
			/// It is used instead of Execute() when use a return value.
			/// This method is implemented in I/O blocking manner.
			/// <para>
			/// "self" keyword can be used to refer to the current object.
			/// </para>
			/// </summary>
			/// <typeparam name="T">Type of a return value.</typeparam>
			/// <param name="script"></param>
			/// <returns></returns>
			public T ExecuteBlocking<T>(string script) {
				string code = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var self = {0};",
						"{1};"
					),
					Script.GetObject(id),
					script
				);
				return _ExecuteBlocking<T>(code);
			}

			public object Invoke(params object[] args) {
				string options = CreateParams(args);
				string script = ScriptBuilder.Build(
					"var func = {0};",
					"return func({1});",
					Script.GetObject(id),
					options
				);
				return _ExecuteBlocking<object>(script);
			}

			public T Invoke<T>(params object[] args) {
				string options = CreateParams(args);
				string script = ScriptBuilder.Build(
					"var func = {0};",
					"return func({1});",
					Script.GetObject(id),
					options
				);
				return _ExecuteBlocking<T>(script);
			}

			/// <summary>
			/// Apply the method in the Node side.
			/// This method is implemented in I/O blocking manner.
			/// </summary>
			/// <param name="methodName">Method name.</param>
			/// <param name="args">Arguments for methodName method.</param>
			/// <returns></returns>
			public object Apply(string methodName, params object[] args) {
				string options = CreateParams(args);
				string script = ScriptBuilder.Build(
					"return {0}.{1}({2});",
					Script.GetObject(id),
					methodName,
					options
				);
				return _ExecuteBlocking<object>(script);
			}

			public T Apply<T>(string methodName, params object[] args) {
				string options = CreateParams(args);
				string script = ScriptBuilder.Build(
					"return {0}.{1}({2});",
					Script.GetObject(id),
					methodName,
					options
				);
				return _ExecuteBlocking<T>(script);
			}

			public T ApplyConstructor<T>(params object[] args) where T : JSObject, new() {
				string options = CreateParams(args);
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var ctor = {0};",
						"var obj = new ctor({1});",
						"return {2};"
					),
					Script.GetObject(id),
					options,
					Script.AddObject("obj")
				);
				int result = _ExecuteBlocking<int>(script);
				return CreateObject<T>(result);
			}

			public T ApplyAndGetObject<T>(string methodName, params object[] args) where T : JSObject, new() {
				string options = CreateParams(args);
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var obj = {0}.{1}({2});",
						"return {3};"
					),
					Script.GetObject(id),
					methodName,
					options,
					Script.AddObject("obj")
				);
				int result = _ExecuteBlocking<int>(script);
				return CreateObject<T>(result);
			}

			public T[] ApplyAndGetObjectList<T>(string methodName, params object[] args) where T : JSObject, new() {
				string options = CreateParams(args);
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var result = [];",
						"var list = {0}.{1}({2});",
						"for (var obj of list) {{",
							"result.push({3});",
						"}}",
						"return result;"
					),
					Script.GetObject(id),
					methodName,
					options,
					Script.AddObject("obj")
				);
				object[] result = _ExecuteBlocking<object[]>(script);
				return CreateObjectList<T>(result);
			}

			public object GetValue() {
				string script = ScriptBuilder.Build(
					"return {0};",
					Script.GetObject(id)
				);
				return _ExecuteBlocking<object>(script);
			}

			public object[] ToArray() {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var result = [];",
						"for (var item of {0}) {{",
							"result.push({1});",
						"}}",
						"return result;"
					),
					Script.GetObject(id),
					Script.AddObject("item")
				);
				return _ExecuteBlocking<object[]>(script);
			}

			public T[] ToArray<T>() where T: JSObject, new() {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var result = [];",
						"for (var item of {0}) {{",
							"result.push({1});",
						"}}",
						"return result;"
					),
					Script.GetObject(id),
					Script.AddObject("item")
				);
				object[] result = _ExecuteBlocking<object[]>(script);
				return CreateObjectList<T>(result);
			}

			/// <summary>
			/// Get a property in the Node side.
			/// This method is implemented in I/O blocking manner.
			/// </summary>
			/// <typeparam name="T">Type of a return value.</typeparam>
			/// <param name="propertyName">Property name.</param>
			/// <returns></returns>
			public T GetProperty<T>(string propertyName) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return {0}.{1};"
					),
					Script.GetObject(id),
					propertyName
				);
				return _ExecuteBlocking<T>(script);
			}

			public void SetPropertyNull(string propertyName) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"{0}.{1} = null;"
					),
					Script.GetObject(id),
					propertyName
				);
				ExecuteJavaScript(script);
			}

			public void SetProperty(string propertyName, string value) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"{0}.{1} = {2};"
					),
					Script.GetObject(id),
					propertyName,
					value.Escape()
				);
				ExecuteJavaScript(script);
			}

			public void SetProperty(string propertyName, bool value) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"{0}.{1} = {2};"
					),
					Script.GetObject(id),
					propertyName,
					value.Escape()
				);
				ExecuteJavaScript(script);
			}

			public void SetProperty(string propertyName, double value) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"{0}.{1} = {2};"
					),
					Script.GetObject(id),
					propertyName,
					value
				);
				ExecuteJavaScript(script);
			}

			public void SetProperty(string propertyName, CallbackItem value) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"{0}.{1} = {2};"
					),
					Script.GetObject(id),
					propertyName,
					Script.GetObject(value.ObjectId)
				);
				ExecuteJavaScript(script);
			}

			/// <summary>
			/// Create parameters for methods in the Node side.
			/// <para>
			/// For example, console.log() method has variable arguments.
			/// The variable arguments can contain any type of values in JavaScript.
			/// Each value have to convert the type when communicate between C# and JavaScript. 
			/// </para>
			/// </summary>
			/// <param name="args"></param>
			/// <returns></returns>
			public string CreateParams(object[] args) {
				if (args == null) {
					return string.Empty;
				}
				string[] strings = new string[args.Length];
				for (int i = 0; i < args.Length; i++) {
					object arg = args[i];
					if (arg == null) {
						strings[i] = "null";
						continue;
					}
					if (arg is string) {
						strings[i] = ((string)arg).Escape();
						continue;
					}
					if (arg is bool) {
						strings[i] = ((bool)arg).Escape();
						continue;
					}
					if (arg is JsonObject) {
						strings[i] = (arg as JsonObject).Stringify();
						continue;
					}
					if (arg is JSObject) {
						JSObject obj = arg as JSObject;
						strings[i] = string.Format("this.getObject({0})", obj.API.id);
						continue;
					}
					if (arg is CallbackItem) {
						strings[i] = string.Format("this.getObject({0})", (arg as CallbackItem).ObjectId);
						continue;
					}
					strings[i] = JSON.Stringify(arg);
				}
				return string.Join(",", strings);
			}

			public CallbackItem CreateCallbackItem(string eventName, JSCallback callback) {
				if (callback == null) {
					return null;
				}
				CallbackItem item = client.Callbacks.Add(id, eventName, callback);
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var callback = (...args) => {{",
							"var params = [];",
							"for (var arg of args) {{",
								"if (arg == null) {{",
									"params.push(null);",
									"continue;",
								"}}",
								"var type = typeof(arg);",
								"switch (type) {{",
									"case 'number':",
									"case 'boolean':",
									"case 'string':",
										"params.push(arg);",
										"break;",
									"default:",
										"params.push({3});",
										"break;",
								"}}",
							"}}",
							"params = ['__event',{0},{1},{2}].concat(params);",
							"this.emit.apply(this, params);",
						"}};",
						"return {4};"
					),
					id,
					eventName.Escape(),
					item.CallbackId,
					Script.AddObject("arg"),
					Script.AddObject("callback")
				);
				int objectId = _ExecuteBlocking<int>(script);
				item.ObjectId = objectId;
				return item;
			}

			public void RemoveCallbackItem(string eventName, CallbackItem item) {
				if (item == null) {
					return;
				}
				client.Callbacks.RemoveItem(id, eventName, item.CallbackId);
			}

			public void RemoveCallbackItem(string eventName, JSCallback callback) {
				if (callback == null) {
					return;
				}
				CallbackItem item = GetCallbackItem(eventName, callback);
				RemoveCallbackItem(eventName, item);
			}

			public CallbackItem GetCallbackItem(string eventName, JSCallback callback) {
				if (callback == null) {
					return null;
				}
				return client.Callbacks.GetItem(id, eventName, callback);
			}

			public virtual void ExecuteJavaScript(string script) {
				if (client.LocalConfig.IsDebug) {
					script = script + "\n/* " + GetDebugInfo() + " */";
				}
				client.Main.ExecuteJavaScript(script);
			}

			public virtual void ExecuteJavaScript(string script, Callback success) {
				client.Main.ExecuteJavaScript(script, success);
			}

			public virtual void ExecuteJavaScript(string script, Callback success, Callback error) {
				client.Main.ExecuteJavaScript(script, success, error);
			}

			public virtual T _ExecuteBlocking<T>(string script) {
				if (client.LocalConfig.IsDebug) {
					script = script + "\n/* " + GetDebugInfo() + " */";
				}
				return client.ExecuteJavaScriptBlocking<T>(script);
			}

			public virtual int CacheScript(string script) {
				throw new NotImplementedException();
			}

			public virtual T ExecuteCachedScript<T>(int script) {
				throw new NotImplementedException();
			}

			public virtual T CreateObject<T>(int id) where T : JSObject, new() {
				if (id <= 0) {
					return null;
				}
				T obj = new T();
				obj.API.client = client;
				obj.API.id = id;
				return obj;
			}

			public virtual T CreateObject<T>(object id) where T : JSObject, new() {
				if (id == null || !(id is int)) {
					return null;
				}
				int intId = (int)id;
				if (intId <= 0) {
					return null;
				}
				T obj = new T();
				obj.API.client = client;
				obj.API.id = intId;
				return obj;
			}

			public virtual T[] CreateObjectList<T>(object[] idList) where T : JSObject, new() {
				if (idList == null) {
					return null;
				}
				int length = idList.Length;
				T[] result = new T[length];
				for (int i = 0; i < length; i++) {
					object id = idList[i];
					result[i] = CreateObject<T>((int)id);
				}
				return result;
			}

			protected string GetDebugInfo() {
				StringBuilder builder = new StringBuilder();
				StackTrace stackTrace = new StackTrace(true);

				int startIndex = 2;
				int count = 3;
				int endIndex = startIndex + count;
				int frameCount = stackTrace.FrameCount;

				builder.AppendFormat("Client stack: ");
				for (int i = startIndex; i < frameCount && i <= endIndex; i++) {
					StackFrame frame = stackTrace.GetFrame(i);
					MethodBase method = frame.GetMethod();
					builder.AppendFormat(
						"\n{0}.{1} ({2}:{3}:{4})",
						method.ReflectedType.Name,
						method.Name,
						frame.GetFileName(),
						frame.GetFileLineNumber(),
						frame.GetFileColumnNumber()
					);
				}
				return builder.ToString();
			}

			public T GetObject<T>(string moduleName) where T : JSObject, new() {
				if (moduleName == null) {
					return null;
				}
				string script = string.Empty;
				if (id <= 0) {
					script = ScriptBuilder.Build(
						ScriptBuilder.Script(
							"return {0};"
						),
						Script.AddObject(moduleName)
					);
				} else {
					script = ScriptBuilder.Build(
						ScriptBuilder.Script(
							"var m = {0}.{1};",
							"return {2};"
						),
						Script.GetObject(id),
						moduleName,
						Script.AddObject("m")
					);
				}
				int result = _ExecuteBlocking<int>(script);
				return CreateObject<T>(result);
			}

			public T[] GetObjectList<T>(string moduleName) where T : JSObject, new() {
				if (moduleName == null) {
					return null;
				}
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var result = [];",
						"var list = {0}.{1};",
						"for (var obj of list) {{",
							"result.push({2});",
						"}}",
						"return result;"
					),
					Script.GetObject(id),
					moduleName,
					Script.AddObject("obj")
				);
				object[] result = _ExecuteBlocking<object[]>(script);
				return CreateObjectList<T>(result);
			}

			public void SetObject(string propertyName, JSObject value) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"{0}.{1} = {2};"
					),
					Script.GetObject(id),
					propertyName,
					Script.GetObject(value.API.id)
				);
				ExecuteJavaScript(script);
			}
		}

		public SocketronAPI API;


		/// <summary>
		/// Constructor.
		/// </summary>
		public JSObject() {
			//Console.WriteLine("NodeModule ###: " + GetType().Name);
			API = new SocketronAPI();
			_instances.Add(this);
		}

		/// <summary>
		/// Destructor.
		/// </summary>
		~JSObject() {
			if (API.id <= 0) {
				return;
			}
			Dispose();
		}

		/// <summary>
		/// This method is used for internally by the library.
		/// <para>
		/// If API.disposeManually is false, dispose the JavaScript object in the Node side.
		/// This method is also called by the destructor.
		/// </para>
		/// </summary>
		public void Dispose() {
			if (API.disposeManually) {
				return;
			}
			if (API.client == null) {
				return;
			}
			//Debug.WriteLine("NodeModule.Dispose ###: " + GetType().Name + ", " + _id);
			API.client.RemoveObject(this);
			API.id = 0;
		}

		protected static ScriptHelper Script = new ScriptHelper();
		protected class ScriptHelper {
			public string GetObject(long id) {
				return string.Format("this.getObject({0})", id);
			}
			public string GetObjectList(JSObject[] list) {
				if (list == null) {
					return "null";
				}
				List<string> result = new List<string>();
				foreach (JSObject obj in list) {
					result.Add(GetObject(obj.API.id));
				}
				return string.Join(",", result.ToArray());
			}
			public string GetObjectList(object[] list) {
				if (list == null) {
					return "null";
				}
				List<string> result = new List<string>();
				foreach (object obj in list) {
					result.Add(obj.ToString());
				}
				return string.Join(",", result.ToArray());
			}
			public string AddObject(string name) {
				return string.Format("this.addObject({0})", name);
			}
			public string RemoveObject(long id) {
				return string.Format("this.removeObject({0})", id);
			}
			public string RemoveObject(string name) {
				return string.Format("this.removeObject({0})", name);
			}
		}
	}
}
