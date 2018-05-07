using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Socketron.Electron {
	/// <summary>
	/// Collect tracing data from Chromium's content module
	/// for finding performance bottlenecks and slow operations.
	/// <para>Process: Main</para>
	/// <para>
	/// This module does not include a web interface so you need to
	/// open chrome://tracing/ in a Chrome browser and load the
	/// generated file to view the result.
	/// </para>
	/// <para>
	/// Note: You should not use this module until
	/// the ready event of the app module is emitted.
	/// </para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class ContentTracingModule : JSModule {
		public const string Name = "ContentTracing";

		static ushort _callbackListId = 0;
		static Dictionary<ushort, Callback> _callbackList = new Dictionary<ushort, Callback>();

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public ContentTracingModule(SocketronClient client, int id) {
			_client = client;
			_id = id;
		}

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
		/// Get a set of category groups.
		/// The category groups can change as new code paths are reached.
		/// <para>
		/// Once all child processes have acknowledged the getCategories
		/// request the callback is invoked with an array of category groups.
		/// </para>
		/// </summary>
		/// <param name="callback"></param>
		public void getCategories(Action<string[]> callback) {
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
				string[] categories = (argsList[0] as object[]).Cast<string>().ToArray();
				callback?.Invoke(categories);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (categories) => {{",
						"this.emit('__event',{1},{2},categories);",
					"}};",
					"{0}.getCategories(callback);"
				),
				Script.GetObject(_id),
				Name.Escape(),
				_callbackListId
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Start recording on all processes.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="callback"></param>
		public void startRecording(JsonObject options, Action callback) {
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
				callback?.Invoke();
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"this.emit('__event',{1},{2});",
					"}};",
					"{0}.startRecording({3},callback);"
				),
				Script.GetObject(_id),
				Name.Escape(),
				_callbackListId,
				options.Stringify()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Stop recording on all processes.
		/// </summary>
		/// <param name="resultFilePath"></param>
		/// <param name="callback"></param>
		public void stopRecording(string resultFilePath, Action<string> callback) {
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
				string resultFilePath2 = argsList[0] as string;
				callback?.Invoke(resultFilePath2);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (resultFilePath) => {{",
						"this.emit('__event',{1},{2},resultFilePath);",
					"}};",
					"{0}.stopRecording({3},callback);"
				),
				Script.GetObject(_id),
				Name.Escape(),
				_callbackListId,
				resultFilePath.Escape()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Start monitoring on all processes.
		/// <para>
		/// Monitoring begins immediately locally and asynchronously
		/// on child processes as soon as they receive the startMonitoring request.
		/// </para>
		/// <para>
		/// Once all child processes have acknowledged the startMonitoring
		/// request the callback will be called.
		/// </para>
		/// </summary>
		/// <param name="options"></param>
		/// <param name="callback"></param>
		public void startMonitoring(JsonObject options, Action callback) {
			if (callback == null) {
				return;
			}
			ushort callbackId = _callbackListId;
			_callbackList.Add(_callbackListId, (object args) => {
				_callbackList.Remove(callbackId);
				callback?.Invoke();
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"this.emit('__event',{1},{2});",
					"}};",
					"{0}.startMonitoring({3},callback);"
				),
				Script.GetObject(_id),
				Name.Escape(),
				_callbackListId,
				options.Stringify()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Stop monitoring on all processes.
		/// <para>
		/// Once all child processes have acknowledged the stopMonitoring request the callback is called.
		/// </para>
		/// </summary>
		/// <param name="callback"></param>
		public void stopMonitoring(Action callback) {
			if (callback == null) {
				return;
			}
			ushort callbackId = _callbackListId;
			_callbackList.Add(_callbackListId, (object args) => {
				_callbackList.Remove(callbackId);
				callback?.Invoke();
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = () => {{",
						"this.emit('__event',{1},{2});",
					"}};",
					"{0}.stopMonitoring(callback);"
				),
				Script.GetObject(_id),
				Name.Escape(),
				_callbackListId
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Get the current monitoring traced data.
		/// </summary>
		/// <param name="resultFilePath"></param>
		/// <param name="callback"></param>
		public void captureMonitoringSnapshot(string resultFilePath, Action<string> callback) {
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
				string resultFilePath2 = argsList[0] as string;
				callback?.Invoke(resultFilePath2);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (resultFilePath) => {{",
						"this.emit('__event',{1},{2},resultFilePath);",
					"}};",
					"{0}.captureMonitoringSnapshot({3},callback);"
				),
				Script.GetObject(_id),
				Name.Escape(),
				_callbackListId,
				resultFilePath.Escape()
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Get the maximum usage across processes of trace buffer as a percentage of the full state.
		/// <para>
		/// When the TraceBufferUsage value is determined the callback is called.
		/// </para>
		/// </summary>
		/// <param name="callback"></param>
		public void getTraceBufferUsage(Action<double, double> callback) {
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
				double value = Convert.ToDouble(argsList[0]);
				double percentage = Convert.ToDouble(argsList[1]);
				callback?.Invoke(value, percentage);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (value,percentage) => {{",
						"this.emit('__event',{1},{2},value,percentage);",
					"}};",
					"{0}.getTraceBufferUsage(callback);"
				),
				Script.GetObject(_id),
				Name.Escape(),
				_callbackListId
			);
			_callbackListId++;
			_ExecuteJavaScript(script);
		}
	}
}
