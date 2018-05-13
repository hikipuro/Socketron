using System;
using System.Diagnostics.CodeAnalysis;

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
	public class ContentTracing : EventEmitter {
		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public ContentTracing() {
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
			string eventName = "_getCategories";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				// TODO: check string[]
				JSObject _categories = API.CreateObject<JSObject>(args[0]);
				string[] categories = Array.ConvertAll(
					_categories.API.GetValue() as object[],
					value => Convert.ToString(value)
				);
				callback?.Invoke(categories);
			});
			API.Apply("getCategories", item);
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
			string eventName = "_startRecording";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				callback?.Invoke();
			});
			API.Apply("startRecording", options, item);
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
			string eventName = "_stopRecording";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				string resultFilePath2 = Convert.ToString(args[0]);
				callback?.Invoke(resultFilePath2);
			});
			API.Apply("stopRecording", resultFilePath, item);
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
			string eventName = "_startMonitoring";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				callback?.Invoke();
			});
			API.Apply("startMonitoring", options, item);
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
			string eventName = "_stopMonitoring";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				callback?.Invoke();
			});
			API.Apply("stopMonitoring", item);
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
			string eventName = "_captureMonitoringSnapshot";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				string resultFilePath2 = Convert.ToString(args[0]);
				callback?.Invoke(resultFilePath2);
			});
			API.Apply("captureMonitoringSnapshot", resultFilePath, item);
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
			string eventName = "_getTraceBufferUsage";
			CallbackItem item = null;
			item = API.CreateCallbackItem(eventName, (object[] args) => {
				double value = Convert.ToDouble(args[0]);
				double percentage = Convert.ToDouble(args[1]);
				callback?.Invoke(value, percentage);
			});
			API.Apply("getTraceBufferUsage", item);
		}
	}
}
