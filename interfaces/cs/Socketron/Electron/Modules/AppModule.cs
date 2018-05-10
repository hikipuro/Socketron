using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	/// <summary>
	/// Control your application's event lifecycle.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class AppModule : JSModule {
		public CommandLine commandLine;
		/// <summary>*macOS*</summary>
		public Dock dock;

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		public AppModule() {
		}

		/// <summary>
		/// This constructor is used for internally by the library.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="id"></param>
		public AppModule(SocketronClient client, int id) {
			API.client = client;
			API.id = id;
			commandLine = API.GetObject<CommandLine>("commandLine");
			dock = API.GetObject<Dock>("dock");
		}

		public class CommandLine : JSModule {
			/// <summary>
			/// This constructor is used for internally by the library.
			/// </summary>
			public CommandLine() {
			}

			/// <summary>
			/// Append a switch (with optional value) to Chromium's command line.
			/// <para>
			/// Note: This will not affect process.argv,
			/// and is mainly used by developers to control some low-level Chromium behaviors.
			/// </para>
			/// </summary>
			/// <param name="switch">A command-line switch.</param>
			/// <param name="value"> A value for the given switch.</param>
			public void appendSwitch(string @switch, string value = null) {
				if (value == null) {
					API.Apply("appendSwitch", @switch);
				} else {
					API.Apply("appendSwitch", @switch, value);
				}
			}

			/// <summary>
			/// Append an argument to Chromium's command line. The argument will be quoted correctly.
			/// <para>
			/// Note: This will not affect process.argv.
			/// </para>
			/// </summary>
			/// <param name="value">The argument to append to the command line.</param>
			public void appendArgument(string value) {
				API.Apply("appendArgument", value);
			}
		}

		public class Dock : JSModule {
			/// <summary>
			/// This constructor is used for internally by the library.
			/// </summary>
			public Dock() {
			}

			/// <summary>
			/// *macOS*
			/// When critical is passed, the dock icon will bounce until either
			/// the application becomes active or the request is canceled.
			/// <para>
			/// When informational is passed, the dock icon will bounce for one second.However,
			/// the request remains active until either the application becomes active
			/// or the request is canceled.
			/// </para>
			/// <para>
			/// Returns Integer an ID representing the request.
			/// </para>
			/// </summary>
			/// <param name="type">
			/// Can be critical or informational.
			/// The default is informational.
			/// </param>
			public void bounce(string type = null) {
				if (type == null) {
					API.Apply("bounce");
				} else {
					API.Apply("bounce", type);
				}
			}

			/// <summary>
			/// *macOS*
			/// Cancel the bounce of id.
			/// </summary>
			/// <param name="id"></param>
			public void cancelBounce(int id) {
				API.Apply("cancelBounce", id);
			}

			/// <summary>
			/// *macOS*
			/// Bounces the Downloads stack if the filePath is inside the Downloads folder.
			/// </summary>
			/// <param name="filePath"></param>
			public void downloadFinished(string filePath) {
				API.Apply("downloadFinished", filePath);
			}

			/// <summary>
			/// *macOS*
			/// Sets the string to be displayed in the dock’s badging area.
			/// </summary>
			/// <param name="text"></param>
			public void setBadge(string text) {
				API.Apply("setBadge", text);
			}

			/// <summary>
			/// *macOS*
			/// Returns String - The badge string of the dock.
			/// </summary>
			/// <returns></returns>
			public string getBadge() {
				return API.Apply<string>("getBadge");
			}

			/// <summary>
			/// *macOS*
			/// Hides the dock icon.
			/// </summary>
			public void hide() {
				API.Apply("hide");
			}

			/// <summary>
			/// *macOS*
			/// Shows the dock icon.
			/// </summary>
			public void show() {
				API.Apply("show");
			}

			/// <summary>
			/// *macOS*
			/// Returns Boolean - Whether the dock icon is visible.
			/// <para>
			/// The app.dock.show() call is asynchronous so this method
			/// might not return true immediately after that call.
			/// </para>
			/// </summary>
			/// <returns></returns>
			public bool isVisible() {
				return API.Apply<bool>("isVisible");
			}

			/// <summary>
			/// *macOS*
			/// Sets the application's dock menu.
			/// </summary>
			/// <param name="menu"></param>
			public void setMenu(Menu menu) {
				if (menu == null) {
					return;
				}
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var menu = {0};",
						"electron.app.dock.setMenu(menu);"
					),
					Script.GetObject(menu.API.id)
				);
				API.ExecuteJavaScript(script);
			}

			/// <summary>
			/// *macOS*
			/// Sets the image associated with this dock icon.
			/// </summary>
			/// <param name="image"></param>
			public void setIcon(NativeImage image) {
				if (image == null) {
					return;
				}
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var image = {0};",
						"electron.app.dock.setIcon(image);"
					),
					Script.GetObject(image.API.id)
				);
				API.ExecuteJavaScript(script);
			}
		}

		/// <summary>
		/// A Boolean property that returns true if the app is packaged, false otherwise.
		/// For many apps, this property can be used to distinguish development and production environments.
		/// </summary>
		public bool isPackaged {
			get { return API.GetProperty<bool>("isPackaged"); }
		}

		/// <summary>
		/// Try to close all windows.
		/// <para>
		/// The before-quit event will be emitted first.
		/// If all windows are successfully closed,
		/// the will-quit event will be emitted and by default the application will terminate.
		/// </para>
		/// <para>
		/// This method guarantees that all beforeunload and unload event handlers are correctly executed.
		/// It is possible that a window cancels the quitting by returning false
		/// in the beforeunload event handler.
		/// </para>
		/// </summary>
		public void quit() {
			API.Apply("quit");
		}

		/// <summary>
		/// Exits immediately with exitCode.
		/// exitCode defaults to 0.
		/// <para>
		/// All windows will be closed immediately without asking user 
		/// and the before-quit and will-quit events will not be emitted.
		/// </para>
		/// </summary>
		/// <param name="exitCode">(optional)</param>
		public void exit(int exitCode = 0) {
			API.Apply("exit", exitCode);
		}

		/// <summary>
		/// Relaunches the app when current instance exits.
		/// </summary>
		/// <param name="options">(optional)</param>
		public void relaunch(JsonObject options = null) {
			if (options == null) {
				API.Apply("relaunch");
			} else {
				API.Apply("relaunch", options);
			}
		}

		/// <summary>
		/// Returns Boolean - true if Electron has finished initializing, false otherwise.
		/// </summary>
		/// <returns></returns>
		public bool isReady() {
			return API.Apply<bool>("isReady");
		}

		/*
		public bool whenReady() {
			// TODO: implement this
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.whenReady();"
				)
			);
			return API.ExecuteJavaScriptBlocking<bool>(script);
		}
		//*/

		/// <summary>
		/// On Linux, focuses on the first visible window.
		/// On macOS, makes the application the active app.
		/// On Windows, focuses on the application's first window.
		/// </summary>
		public void focus() {
			API.Apply("focus");
		}

		/// <summary>
		/// *macOS*
		/// Hides all application windows without minimizing them.
		/// </summary>
		public void hide() {
			API.Apply("hide");
		}

		/// <summary>
		/// *macOS*
		/// Shows application windows after they were hidden. Does not automatically focus them.
		/// </summary>
		public void show() {
			API.Apply("show");
		}

		/// <summary>
		/// Returns String - The current application directory.
		/// </summary>
		/// <returns></returns>
		public string getAppPath() {
			return API.Apply<string>("getAppPath");
		}

		/// <summary>
		/// Returns String - A path to a special directory or file associated with name.
		/// On failure an Error is thrown.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string getPath(string name) {
			return API.Apply<string>("getPath", name);
		}

		/// <summary>
		/// Fetches a path's associated icon.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="callback"></param>
		public void getFileIcon(string path, Action<Error, NativeImage> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "getFileIcon";
			CallbackItem item = null;
			item = API.client.Callbacks.Add(API.id, eventName, (object[] args) => {
				API.client.Callbacks.RemoveItem(API.id, eventName, item.CallbackId);
				Error error = new Error(API.client, (int)args[0]);
				NativeImage image = new NativeImage(API.client, (int)args[1]);
				callback?.Invoke(error, image);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (error, icon) => {{",
						"this.emit('__event',{0},{1},{2},{3},{4});",
					"}};",
					"return {5};"
				),
				API.id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("error"),
				Script.AddObject("icon"),
				Script.AddObject("callback")
			);
			int objectId = API._ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.getFileIcon({1},{2});",
				Script.GetObject(API.id),
				path.Escape(),
				Script.GetObject(objectId)
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Fetches a path's associated icon.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="options"></param>
		/// <param name="callback"></param>
		public void getFileIcon(string path, JsonObject options, Action<Error, NativeImage> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "getFileIcon";
			CallbackItem item = null;
			item = API.client.Callbacks.Add(API.id, eventName, (object[] args) => {
				API.client.Callbacks.RemoveItem(API.id, eventName, item.CallbackId);
				Error error = new Error(API.client, (int)args[0]);
				NativeImage image = new NativeImage(API.client, (int)args[1]);
				callback?.Invoke(error, image);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (error, icon) => {{",
						"this.emit('__event',{0},{1},{2},{3},{4});",
					"}};",
					"return {5};"
				),
				API.id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("error"),
				Script.AddObject("icon"),
				Script.AddObject("callback")
			);
			int objectId = API._ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.getFileIcon({1},{2},{3});",
				Script.GetObject(API.id),
				path.Escape(),
				options.Stringify(),
				Script.GetObject(objectId)
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Overrides the path to a special directory or file associated with name.
		/// <para>
		/// If the path specifies a directory that does not exist,
		/// the directory will be created by this method.
		/// On failure an Error is thrown.
		/// </para>
		/// </summary>
		/// <param name="name"></param>
		/// <param name="path"></param>
		public void setPath(string name, string path) {
			API.Apply("setPath", name, path);
		}

		/// <summary>
		/// Returns String - The version of the loaded application.
		/// <para>
		/// If no version is found in the application's package.json file,
		/// the version of the current bundle or executable is returned.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public string getVersion() {
			return API.Apply<string>("getVersion");
		}

		/// <summary>
		/// Returns String - The current application's name,
		/// which is the name in the application's package.json file.
		/// </summary>
		/// <returns></returns>
		public string getName() {
			return API.Apply<string>("getName");
		}

		/// <summary>
		/// Overrides the current application's name.
		/// </summary>
		/// <param name="name"></param>
		public void setName(string name) {
			API.Apply("setName", name);
		}

		/// <summary>
		/// Returns String - The current application locale.
		/// <para>
		/// Note: When distributing your packaged app, you have to also ship the locales folder.
		/// </para>
		/// <para>
		/// Note: On Windows you have to call it after the ready events gets emitted.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public string getLocale() {
			return API.Apply<string>("getLocale");
		}

		/// <summary>
		/// *macOS Windows*
		/// Adds path to the recent documents list.
		/// <para>
		/// This list is managed by the OS.
		/// On Windows you can visit the list from the task bar,
		/// and on macOS you can visit it from dock menu.
		/// </para>
		/// </summary>
		/// <param name="path"></param>
		public void addRecentDocument(string path) {
			API.Apply("addRecentDocument", path);
		}

		/// <summary>
		/// *macOS Windows*
		/// Clears the recent documents list.
		/// </summary>
		public void clearRecentDocuments() {
			API.Apply("clearRecentDocuments");
		}

		/// <summary>
		/// Returns Boolean - Whether the call succeeded.
		/// </summary>
		/// <param name="protocol">
		/// The name of your protocol, without ://.
		/// If you want your app to handle electron:// links,
		/// call this method with electron as the parameter.
		/// </param>
		/// <param name="path">*Windows* Defaults to process.execPath</param>
		/// <param name="args">*Windows* Defaults to an empty array</param>
		public void setAsDefaultProtocolClient(string protocol, string path = null, string[] args = null) {
			string option = string.Empty;
			if (path == null) {
				option = protocol.Escape();
			} else {
				if (args == null) {
					option = ScriptBuilder.Params(
						protocol.Escape(),
						path.Escape()
					);
				} else {
					option = ScriptBuilder.Params(
						protocol.Escape(),
						path.Escape(),
						JSON.Stringify(args)
					);
				}
			}

			string script = ScriptBuilder.Build(
				"{0}.setAsDefaultProtocolClient({1});",
				Script.GetObject(API.id),
				option
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the call succeeded.
		/// <para>
		/// This method checks if the current executable
		/// as the default handler for a protocol (aka URI scheme).
		/// If so, it will remove the app as the default handler.
		/// </para>
		/// </summary>
		/// <param name="protocol">The name of your protocol, without ://.</param>
		/// <returns></returns>
		public bool removeAsDefaultProtocolClient(string protocol) {
			return API.Apply<bool>("removeAsDefaultProtocolClient", protocol);
		}

		/// <summary>
		/// Returns Boolean - Whether the call succeeded.
		/// </summary>
		/// <param name="protocol">The name of your protocol, without ://.</param>
		/// <param name="path">*Windows* Defaults to process.execPath</param>
		/// <returns></returns>
		public bool removeAsDefaultProtocolClient(string protocol, string path) {
			return API.Apply<bool>("removeAsDefaultProtocolClient", protocol, path);
		}

		/// <summary>
		/// Returns Boolean - Whether the call succeeded.
		/// </summary>
		/// <param name="protocol">The name of your protocol, without ://.</param>
		/// <param name="path">*Windows* Defaults to process.execPath</param>
		/// <param name="args">*Windows* Defaults to an empty array</param>
		/// <returns></returns>
		public bool removeAsDefaultProtocolClient(string protocol, string path, string[] args) {
			string script = ScriptBuilder.Build(
				"return {0}.removeAsDefaultProtocolClient({1},{2},{3});",
				Script.GetObject(API.id),
				protocol.Escape(),
				path.Escape(),
				args.Escape()
			);
			return API._ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// This method checks if the current executable
		/// is the default handler for a protocol (aka URI scheme).
		/// If so, it will return true. Otherwise, it will return false.
		/// </summary>
		/// <param name="protocol">The name of your protocol, without ://.</param>
		/// <returns></returns>
		public bool isDefaultProtocolClient(string protocol) {
			return API.Apply<bool>("isDefaultProtocolClient", protocol);
		}

		/// <summary>
		/// This method checks if the current executable
		/// is the default handler for a protocol (aka URI scheme).
		/// If so, it will return true. Otherwise, it will return false.
		/// </summary>
		/// <param name="protocol">The name of your protocol, without ://.</param>
		/// <param name="path">*Windows* Defaults to process.execPath</param>
		/// <returns></returns>
		public bool isDefaultProtocolClient(string protocol, string path) {
			return API.Apply<bool>("isDefaultProtocolClient", protocol, path);
		}

		/// <summary>
		/// This method checks if the current executable
		/// is the default handler for a protocol (aka URI scheme).
		/// If so, it will return true. Otherwise, it will return false.
		/// </summary>
		/// <param name="protocol">The name of your protocol, without ://.</param>
		/// <param name="path">*Windows* Defaults to process.execPath</param>
		/// <param name="args">*Windows* Defaults to an empty array</param>
		/// <returns></returns>
		public bool isDefaultProtocolClient(string protocol, string path, string[] args) {
			string script = ScriptBuilder.Build(
				"return {0}.isDefaultProtocolClient({1},{2},{3});",
				Script.GetObject(API.id),
				protocol.Escape(),
				path.Escape(),
				args.Escape()
			);
			return API._ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows*
		/// Adds tasks to the Tasks category of the JumpList on Windows.
		/// <para>
		/// Note: If you'd like to customize the Jump List
		/// even more use app.setJumpList(categories) instead.
		/// </para>
		/// </summary>
		/// <param name="tasks">Array of Task objects.</param>
		/// <returns>Whether the call succeeded.</returns>
		public bool setUserTasks(TaskObject[] tasks) {
			// TODO: check array arg
			string script = ScriptBuilder.Build(
				"return {0}.setUserTasks({1});",
				Script.GetObject(API.id),
				JSON.Stringify(tasks)
			);
			return API._ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows*
		/// Returns Object:
		/// </summary>
		/// <returns></returns>
		public JsonObject getJumpListSettings() {
			string script = ScriptBuilder.Build(
				"return {0}.getJumpListSettings();",
				Script.GetObject(API.id)
			);
			object result = API._ExecuteBlocking<object>(script);
			return new JsonObject(result);

		}

		/// <summary>
		/// *Windows*
		/// Sets or removes a custom Jump List for the application.
		/// </summary>
		/// <param name="categories"></param>
		public void setJumpList(JumpListCategory[] categories) {
			string script = ScriptBuilder.Build(
				"{0}.setJumpList({1});",
				Script.GetObject(API.id),
				JSON.Stringify(categories)
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// This method makes your application a Single Instance Application
		/// - instead of allowing multiple instances of your app to run,
		/// this will ensure that only a single instance of your app is running,
		/// and other instances signal this instance and exit.
		/// </summary>
		/// <param name="callback"></param>
		/// <returns></returns>
		public bool makeSingleInstance(Action<string[], string> callback) {
			if (callback == null) {
				return false;
			}
			string eventName = "makeSingleInstance";
			CallbackItem item = null;
			item = API.client.Callbacks.Add(API.id, eventName, (object[] args) => {
				API.client.Callbacks.RemoveItem(API.id, eventName, item.CallbackId);
				string[] commandLine = null;
				string workingDirectory = args[1] as string;
				if (args[0] != null) {
					commandLine =  Array.ConvertAll(
						args[0] as object[],
						value => Convert.ToString(value)
					);
				}
				callback?.Invoke(commandLine, workingDirectory);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (argv, workingDirectory) => {{",
						"this.emit('__event',{0},{1},{2},argv, workingDirectory);",
					"}};",
					"return {3};"
				),
				API.id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			int objectId = API._ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.makeSingleInstance({1});",
				Script.GetObject(API.id),
				Script.GetObject(objectId)
			);
			return API._ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Releases all locks that were created by makeSingleInstance.
		/// This will allow multiple instances of the application to once again run side by side.
		/// </summary>
		public void releaseSingleInstance() {
			API.Apply("releaseSingleInstance");
		}

		/// <summary>
		/// *macOS* 
		/// Creates an NSUserActivity and sets it as the current activity.
		/// The activity is eligible for Handoff to another device afterward.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="userInfo"></param>
		/// <param name="webpageURL"></param>
		public void setUserActivity(string type, JsonObject userInfo, string webpageURL = null) {
			string option = string.Empty;
			if (webpageURL == null) {
				option = ScriptBuilder.Params(
					type.Escape(),
					userInfo.Stringify()
				);
			} else {
				option = ScriptBuilder.Params(
					type.Escape(),
					userInfo.Stringify(),
					webpageURL.Escape()
				);
			}
			string script = ScriptBuilder.Build(
				"{0}.setUserActivity({1});",
				Script.GetObject(API.id),
				option
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Returns String - The type of the currently running activity.
		/// </summary>
		/// <returns></returns>
		public string getCurrentActivityType() {
			return API.Apply<string>("getCurrentActivityType");
		}

		/// <summary>
		/// *macOS*
		/// Invalidates the current Handoff user activity.
		/// </summary>
		public void invalidateCurrentActivity() {
			API.Apply("invalidateCurrentActivity");
		}

		/// <summary>
		/// *macOS*
		/// Updates the current activity if its type matches type,
		/// merging the entries from userInfo into its current userInfo dictionary.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="userInfo"></param>
		public void updateCurrentActivity(string type, JsonObject userInfo) {
			API.Apply("updateCurrentActivity", type, userInfo);
		}

		/// <summary>
		/// *Windows*
		/// Changes the Application User Model ID to id.
		/// </summary>
		/// <param name="id"></param>
		public void setAppUserModelId(string id) {
			API.Apply("setAppUserModelId", id);
		}

		/// <summary>
		/// *Linux*
		/// Imports the certificate in pkcs12 format into the platform certificate store.
		/// callback is called with the result of import operation,
		/// a value of 0 indicates success while any other value indicates failure
		/// according to chromium net_error_list.
		/// </summary>
		/// <param name="options">
		/// "certificate" String - Path for the pkcs12 file.
		/// "password" String - Passphrase for the certificate.
		/// </param>
		/// <param name="callback">
		/// "result" Integer - Result of import.
		/// </param>
		public void importCertificate(JsonObject options, Action<int> callback) {
			if (callback == null) {
				return;
			}
			string eventName = "importCertificate";
			CallbackItem item = null;
			item = API.client.Callbacks.Add(API.id, eventName, (object[] args) => {
				API.client.Callbacks.RemoveItem(API.id, eventName, item.CallbackId);
				callback?.Invoke((int)args[0]);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (result) => {{",
						"this.emit('__event',{0},{1},{2},result);",
					"}};",
					"return {3};"
				),
				API.id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			int objectId = API._ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.importCertificate({1},{2});",
				Script.GetObject(API.id),
				options.Stringify(),
				Script.GetObject(objectId)
			);
			API.ExecuteJavaScript(script);
		}

		/// <summary>
		/// Disables hardware acceleration for current app.
		/// <para>
		/// This method can only be called before app is ready.
		/// </para>
		/// </summary>
		public void disableHardwareAcceleration() {
			API.Apply("disableHardwareAcceleration");
		}

		/// <summary>
		/// By default, Chromium disables 3D APIs (e.g. WebGL) until restart
		/// on a per domain basis if the GPU processes crashes too frequently.
		/// This function disables that behaviour.
		/// <para>
		/// This method can only be called before app is ready.
		/// </para>
		/// </summary>
		public void disableDomainBlockingFor3DAPIs() {
			API.Apply("disableDomainBlockingFor3DAPIs");
		}

		/// <summary>
		/// Returns ProcessMetric[]: Array of ProcessMetric objects
		/// that correspond to memory and cpu usage statistics
		/// of all the processes associated with the app.
		/// </summary>
		/// <returns></returns>
		public List<ProcessMetric> getAppMetrics() {
			string script = ScriptBuilder.Build(
				"return {0}.getAppMetrics();",
				Script.GetObject(API.id)
			);
			object[] result = API._ExecuteBlocking<object[]>(script);
			List<ProcessMetric> metricList = new List<ProcessMetric>();
			foreach (object item in result) {
				ProcessMetric metric = ProcessMetric.FromObject(item);
				metricList.Add(metric);
			}
			return metricList;
		}

		/// <summary>
		/// Returns GPUFeatureStatus - The Graphics Feature Status from chrome://gpu/.
		/// </summary>
		/// <returns></returns>
		public GPUFeatureStatus getGPUFeatureStatus() {
			string script = ScriptBuilder.Build(
				"return {0}.getGPUFeatureStatus();",
				Script.GetObject(API.id)
			);
			object result = API._ExecuteBlocking<object>(script);
			return GPUFeatureStatus.FromObject(result);
		}

		/// <summary>
		/// *Linux macOS*
		/// Returns Boolean - Whether the call succeeded.
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public bool setBadgeCount(int count) {
			return API.Apply<bool>("setBadgeCount", count);
		}

		/// <summary>
		/// *Linux macOS*
		/// Returns Integer - The current value displayed in the counter badge.
		/// </summary>
		/// <returns></returns>
		public int getBadgeCount() {
			return API.Apply<int>("getBadgeCount");
		}

		/// <summary>
		/// *Linux*
		/// Returns Boolean - Whether the current desktop environment is Unity launcher.
		/// </summary>
		/// <returns></returns>
		public bool isUnityRunning() {
			return API.Apply<bool>("isUnityRunning");
		}

		/// <summary>
		/// *macOS Windows*
		/// If you provided path and args options to app.setLoginItemSettings
		/// then you need to pass the same arguments here for openAtLogin to be set correctly.
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public JsonObject getLoginItemSettings(JsonObject options = null) {
			string script = string.Empty;
			if (options == null) {
				ScriptBuilder.Build(
					"return {0}.getLoginItemSettings();",
					Script.GetObject(API.id)
				);
			} else {
				ScriptBuilder.Build(
					"return {0}.getLoginItemSettings({1});",
					Script.GetObject(API.id),
					options.Stringify()
				);
			}
			object result = API._ExecuteBlocking<object>(script);
			return new JsonObject(result);
		}

		/// <summary>
		/// *macOS Windows*
		/// Set the app's login item settings.
		/// </summary>
		/// <param name="settings"></param>
		public void setLoginItemSettings(JsonObject settings) {
			API.Apply("setLoginItemSettings", settings);
		}

		/// <summary>
		/// *macOS Windows*
		/// Returns Boolean - true if Chrome's accessibility support is enabled, false otherwise.
		/// <para>
		/// This API will return true if the use of assistive technologies,
		/// such as screen readers, has been detected.
		/// See https://www.chromium.org/developers/design-documents/accessibility for more details.
		/// </para>
		/// </summary>
		/// <returns></returns>
		public bool isAccessibilitySupportEnabled() {
			return API.Apply<bool>("isAccessibilitySupportEnabled");
		}

		/// <summary>
		/// *macOS Windows*
		/// Manually enables Chrome's accessibility support,
		/// allowing to expose accessibility switch to users in application settings. 
		/// </summary>
		/// <param name="enabled"></param>
		public void setAccessibilitySupportEnabled(bool enabled) {
			API.Apply("setAccessibilitySupportEnabled", enabled);
		}

		/// <summary>
		/// *macOS*
		/// Set the about panel options. This will override the values
		/// defined in the app's .plist file. See the Apple docs for more details.
		/// </summary>
		/// <param name="options"></param>
		public void setAboutPanelOptions(JsonObject options) {
			API.Apply("setAboutPanelOptions", options);
		}

		/// <summary>
		/// *macOS (mas)*
		/// Returns Function - This function must be called once you have finished
		/// accessing the security scoped file. 
		/// </summary>
		/// <param name="bookmarkData"></param>
		public void startAccessingSecurityScopedResource(string bookmarkData) {
			API.Apply("startAccessingSecurityScopedResource", bookmarkData);
		}

		/// <summary>
		/// *Experimental macOS Windows*
		/// Enables mixed sandbox mode on the app.
		/// This method can only be called before app is ready.
		/// </summary>
		public void enableMixedSandbox() {
			API.Apply("enableMixedSandbox");
		}

		/// <summary>
		/// *macOS*
		/// Returns Boolean - Whether the application is currently running
		/// from the systems Application folder.
		/// Use in combination with app.moveToApplicationsFolder()
		/// </summary>
		/// <returns></returns>
		public bool isInApplicationsFolder() {
			return API.Apply<bool>("isInApplicationsFolder");
		}

		/// <summary>
		/// *macOS*
		/// Returns Boolean - Whether the move was successful.
		/// Please note that if the move is successful your application will quit and relaunch.
		/// </summary>
		/// <returns></returns>
		public bool moveToApplicationsFolder() {
			return API.Apply<bool>("moveToApplicationsFolder");
		}
	}
}
