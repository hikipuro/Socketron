using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
		/// <param name="client"></param>
		/// <param name="id"></param>
		public AppModule(SocketronClient client, int id) {
			_client = client;
			_id = id;
			commandLine = new CommandLine(client);
			dock = new Dock(client);
		}

		public class CommandLine : JSModule {
			/// <summary>
			/// This constructor is used for internally by the library.
			/// </summary>
			/// <param name="client"></param>
			public CommandLine(SocketronClient client) {
				_client = client;
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
				string script = string.Empty;
				if (value == null) {
					script = ScriptBuilder.Build(
						ScriptBuilder.Script(
							"electron.app.commandLine.appendSwitch({0});"
						),
						@switch.Escape()
					);
				} else {
					script = ScriptBuilder.Build(
						ScriptBuilder.Script(
							"electron.app.commandLine.appendSwitch({0},{1});"
						),
						@switch.Escape(),
						value.Escape()
					);
				}
				_ExecuteJavaScript(script);
			}

			/// <summary>
			/// Append an argument to Chromium's command line. The argument will be quoted correctly.
			/// <para>
			/// Note: This will not affect process.argv.
			/// </para>
			/// </summary>
			/// <param name="value">The argument to append to the command line.</param>
			public void appendArgument(string value) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"electron.app.commandLine.appendArgument({0});"
					),
					value.Escape()
				);
				_ExecuteJavaScript(script);
			}
		}

		public class Dock : JSModule {
			/// <summary>
			/// This constructor is used for internally by the library.
			/// </summary>
			/// <param name="client"></param>
			public Dock(SocketronClient client) {
				_client = client;
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
				string script = string.Empty;
				if (type == null) {
					script = ScriptBuilder.Build(
						ScriptBuilder.Script(
							"electron.app.dock.bounce();"
						)
					);
				} else {
					script = ScriptBuilder.Build(
						ScriptBuilder.Script(
							"electron.app.dock.bounce({0});"
						),
						type.Escape()
					);
				}
				_ExecuteJavaScript(script);
			}

			/// <summary>
			/// *macOS*
			/// Cancel the bounce of id.
			/// </summary>
			/// <param name="id"></param>
			public void cancelBounce(int id) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"electron.app.dock.cancelBounce({0});"
					),
					id
				);
				_ExecuteJavaScript(script);
			}

			/// <summary>
			/// *macOS*
			/// Bounces the Downloads stack if the filePath is inside the Downloads folder.
			/// </summary>
			/// <param name="filePath"></param>
			public void downloadFinished(string filePath) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"electron.app.dock.downloadFinished({0});"
					),
					filePath.Escape()
				);
				_ExecuteJavaScript(script);
			}

			/// <summary>
			/// *macOS*
			/// Sets the string to be displayed in the dock’s badging area.
			/// </summary>
			/// <param name="text"></param>
			public void setBadge(string text) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"electron.app.dock.setBadge({0});"
					),
					text.Escape()
				);
				_ExecuteJavaScript(script);
			}

			/// <summary>
			/// *macOS*
			/// Returns String - The badge string of the dock.
			/// </summary>
			/// <returns></returns>
			public string getBadge() {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return electron.app.dock.getBadge();"
					)
				);
				return _ExecuteBlocking<string>(script);
			}

			/// <summary>
			/// *macOS*
			/// Hides the dock icon.
			/// </summary>
			public void hide() {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"electron.app.dock.hide();"
					)
				);
				_ExecuteJavaScript(script);
			}

			/// <summary>
			/// *macOS*
			/// Shows the dock icon.
			/// </summary>
			public void show() {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"electron.app.dock.show();"
					)
				);
				_ExecuteJavaScript(script);
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
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return electron.app.dock.isVisible();"
					)
				);
				return _ExecuteBlocking<bool>(script);
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
					Script.GetObject(menu._id)
				);
				_ExecuteJavaScript(script);
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
					Script.GetObject(image._id)
				);
				_ExecuteJavaScript(script);
			}
		}

		/// <summary>
		/// A Boolean property that returns true if the app is packaged, false otherwise.
		/// For many apps, this property can be used to distinguish development and production environments.
		/// </summary>
		public bool isPackaged {
			get {
				string script = ScriptBuilder.Build(
					"return {0}.isPackaged;",
					Script.GetObject(_id)
				);
				return _ExecuteBlocking<bool>(script);
			}
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
			string script = ScriptBuilder.Build(
				"{0}.quit();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
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
			string script = ScriptBuilder.Build(
				"{0}.exit({1});",
				Script.GetObject(_id),
				exitCode
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Relaunches the app when current instance exits.
		/// </summary>
		/// <param name="options">(optional)</param>
		public void relaunch(JsonObject options = null) {
			string script = string.Empty;
			if (options == null) {
				script = ScriptBuilder.Build(
					"{0}.relaunch();",
					Script.GetObject(_id)
				);
			} else {
				script = ScriptBuilder.Build(
					"{0}.relaunch({1});",
					Script.GetObject(_id),
					options.Stringify()
				);
			}
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - true if Electron has finished initializing, false otherwise.
		/// </summary>
		/// <returns></returns>
		public bool isReady() {
			string script = ScriptBuilder.Build(
				"return {0}.isReady();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/*
		public bool whenReady() {
			// TODO: implement this
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.whenReady();"
				)
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}
		//*/

		/// <summary>
		/// On Linux, focuses on the first visible window.
		/// On macOS, makes the application the active app.
		/// On Windows, focuses on the application's first window.
		/// </summary>
		public void focus() {
			string script = ScriptBuilder.Build(
				"{0}.focus();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Hides all application windows without minimizing them.
		/// </summary>
		public void hide() {
			string script = ScriptBuilder.Build(
				"{0}.hide();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Shows application windows after they were hidden. Does not automatically focus them.
		/// </summary>
		public void show() {
			string script = ScriptBuilder.Build(
				"{0}.show();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - The current application directory.
		/// </summary>
		/// <returns></returns>
		public string getAppPath() {
			string script = ScriptBuilder.Build(
				"return {0}.getAppPath();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns String - A path to a special directory or file associated with name.
		/// On failure an Error is thrown.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string getPath(string name) {
			string script = ScriptBuilder.Build(
				"return {0}.getPath({1});",
				Script.GetObject(_id),
				name.Escape()
			);
			return _ExecuteBlocking<string>(script);
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
			item = _client.Callbacks.Add(_id, eventName, (object[] args) => {
				_client.Callbacks.RemoveItem(_id, eventName, item.CallbackId);
				Error error = new Error(_client, (int)args[0]);
				NativeImage image = new NativeImage(_client, (int)args[1]);
				callback?.Invoke(error, image);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (error, icon) => {{",
						"this.emit('__event',{0},{1},{2},{3},{4});",
					"}};",
					"return {5};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("error"),
				Script.AddObject("icon"),
				Script.AddObject("callback")
			);
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.getFileIcon({1},{2});",
				Script.GetObject(_id),
				path.Escape(),
				Script.GetObject(objectId)
			);
			_ExecuteJavaScript(script);
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
			item = _client.Callbacks.Add(_id, eventName, (object[] args) => {
				_client.Callbacks.RemoveItem(_id, eventName, item.CallbackId);
				Error error = new Error(_client, (int)args[0]);
				NativeImage image = new NativeImage(_client, (int)args[1]);
				callback?.Invoke(error, image);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (error, icon) => {{",
						"this.emit('__event',{0},{1},{2},{3},{4});",
					"}};",
					"return {5};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("error"),
				Script.AddObject("icon"),
				Script.AddObject("callback")
			);
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.getFileIcon({1},{2},{3});",
				Script.GetObject(_id),
				path.Escape(),
				options.Stringify(),
				Script.GetObject(objectId)
			);
			_ExecuteJavaScript(script);
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
			string script = ScriptBuilder.Build(
				"{0}.setPath({1},{2});",
				Script.GetObject(_id),
				name.Escape(),
				path.Escape()
			);
			_ExecuteJavaScript(script);
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
			string script = ScriptBuilder.Build(
				"return {0}.getVersion();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Returns String - The current application's name,
		/// which is the name in the application's package.json file.
		/// </summary>
		/// <returns></returns>
		public string getName() {
			string script = ScriptBuilder.Build(
				"return {0}.getName();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// Overrides the current application's name.
		/// </summary>
		/// <param name="name"></param>
		public void setName(string name) {
			string script = ScriptBuilder.Build(
				"{0}.setName({1});",
				Script.GetObject(_id),
				name.Escape()
			);
			_ExecuteJavaScript(script);
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
			string script = ScriptBuilder.Build(
				"return {0}.getLocale();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
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
			string script = ScriptBuilder.Build(
				"{0}.addRecentDocument({1});",
				Script.GetObject(_id),
				path.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Clears the recent documents list.
		/// </summary>
		public void clearRecentDocuments() {
			string script = ScriptBuilder.Build(
				"{0}.clearRecentDocuments();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
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
				Script.GetObject(_id),
				option
			);
			_ExecuteJavaScript(script);
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
			string script = ScriptBuilder.Build(
				"return {0}.removeAsDefaultProtocolClient({1});",
				Script.GetObject(_id),
				protocol.Escape()
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Returns Boolean - Whether the call succeeded.
		/// </summary>
		/// <param name="protocol">The name of your protocol, without ://.</param>
		/// <param name="path">*Windows* Defaults to process.execPath</param>
		/// <returns></returns>
		public bool removeAsDefaultProtocolClient(string protocol, string path) {
			string script = ScriptBuilder.Build(
				"return {0}.removeAsDefaultProtocolClient({1},{2});",
				Script.GetObject(_id),
				protocol.Escape(),
				path.Escape()
			);
			return _ExecuteBlocking<bool>(script);
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
				Script.GetObject(_id),
				protocol.Escape(),
				path.Escape(),
				args.Escape()
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// This method checks if the current executable
		/// is the default handler for a protocol (aka URI scheme).
		/// If so, it will return true. Otherwise, it will return false.
		/// </summary>
		/// <param name="protocol">The name of your protocol, without ://.</param>
		/// <returns></returns>
		public bool isDefaultProtocolClient(string protocol) {
			string script = ScriptBuilder.Build(
				"return {0}.isDefaultProtocolClient({1});",
				Script.GetObject(_id),
				protocol.Escape()
			);
			return _ExecuteBlocking<bool>(script);
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
			string script = ScriptBuilder.Build(
				"return {0}.isDefaultProtocolClient({1},{2});",
				Script.GetObject(_id),
				protocol.Escape(),
				path.Escape()
			);
			return _ExecuteBlocking<bool>(script);
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
				Script.GetObject(_id),
				protocol.Escape(),
				path.Escape(),
				args.Escape()
			);
			return _ExecuteBlocking<bool>(script);
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
				Script.GetObject(_id),
				JSON.Stringify(tasks)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *Windows*
		/// Returns Object:
		/// </summary>
		/// <returns></returns>
		public JsonObject getJumpListSettings() {
			string script = ScriptBuilder.Build(
				"return {0}.getJumpListSettings();",
				Script.GetObject(_id)
			);
			object result = _ExecuteBlocking<object>(script);
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
				Script.GetObject(_id),
				JSON.Stringify(categories)
			);
			_ExecuteJavaScript(script);
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
			item = _client.Callbacks.Add(_id, eventName, (object[] args) => {
				_client.Callbacks.RemoveItem(_id, eventName, item.CallbackId);
				string[] commandLine = null;
				string workingDirectory = args[1] as string;
				if (args[0] != null) {
					commandLine = (args[0] as object[]).Cast<string>().ToArray();
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
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.makeSingleInstance({1});",
				Script.GetObject(_id),
				Script.GetObject(objectId)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// Releases all locks that were created by makeSingleInstance.
		/// This will allow multiple instances of the application to once again run side by side.
		/// </summary>
		public void releaseSingleInstance() {
			string script = ScriptBuilder.Build(
				"{0}.releaseSingleInstance();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
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
				Script.GetObject(_id),
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Returns String - The type of the currently running activity.
		/// </summary>
		/// <returns></returns>
		public string getCurrentActivityType() {
			string script = ScriptBuilder.Build(
				"return {0}.getCurrentActivityType();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<string>(script);
		}

		/// <summary>
		/// *macOS*
		/// Invalidates the current Handoff user activity.
		/// </summary>
		public void invalidateCurrentActivity() {
			string script = ScriptBuilder.Build(
				"{0}.invalidateCurrentActivity();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Updates the current activity if its type matches type,
		/// merging the entries from userInfo into its current userInfo dictionary.
		/// </summary>
		/// <param name="type"></param>
		/// <param name="userInfo"></param>
		public void updateCurrentActivity(string type, JsonObject userInfo) {
			string script = ScriptBuilder.Build(
				"{0}.updateCurrentActivity({1},{2});",
				Script.GetObject(_id),
				type.Escape(),
				userInfo.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows*
		/// Changes the Application User Model ID to id.
		/// </summary>
		/// <param name="id"></param>
		public void setAppUserModelId(string id) {
			string script = ScriptBuilder.Build(
				"{0}.setAppUserModelId({1});",
				Script.GetObject(_id),
				id.Escape()
			);
			_ExecuteJavaScript(script);
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
			item = _client.Callbacks.Add(_id, eventName, (object[] args) => {
				_client.Callbacks.RemoveItem(_id, eventName, item.CallbackId);
				callback?.Invoke((int)args[0]);
			});
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var callback = (result) => {{",
						"this.emit('__event',{0},{1},{2},result);",
					"}};",
					"return {3};"
				),
				_id,
				eventName.Escape(),
				item.CallbackId,
				Script.AddObject("callback")
			);
			int objectId = _ExecuteBlocking<int>(script);
			item.ObjectId = objectId;

			script = ScriptBuilder.Build(
				"{0}.importCertificate({1},{2});",
				Script.GetObject(_id),
				options.Stringify(),
				Script.GetObject(objectId)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Disables hardware acceleration for current app.
		/// <para>
		/// This method can only be called before app is ready.
		/// </para>
		/// </summary>
		public void disableHardwareAcceleration() {
			string script = ScriptBuilder.Build(
				"{0}.disableHardwareAcceleration();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
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
			string script = ScriptBuilder.Build(
				"{0}.disableDomainBlockingFor3DAPIs();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
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
				Script.GetObject(_id)
			);
			object[] result = _ExecuteBlocking<object[]>(script);
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
				Script.GetObject(_id)
			);
			object result = _ExecuteBlocking<object>(script);
			return GPUFeatureStatus.FromObject(result);
		}

		/// <summary>
		/// *Linux macOS*
		/// Returns Boolean - Whether the call succeeded.
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public bool setBadgeCount(int count) {
			string script = ScriptBuilder.Build(
				"return {0}.setBadgeCount({1});",
				Script.GetObject(_id),
				count
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *Linux macOS*
		/// Returns Integer - The current value displayed in the counter badge.
		/// </summary>
		/// <returns></returns>
		public int getBadgeCount() {
			string script = ScriptBuilder.Build(
				"return {0}.getBadgeCount();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<int>(script);
		}

		/// <summary>
		/// *Linux*
		/// Returns Boolean - Whether the current desktop environment is Unity launcher.
		/// </summary>
		/// <returns></returns>
		public bool isUnityRunning() {
			string script = ScriptBuilder.Build(
				"return {0}.isUnityRunning();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
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
					Script.GetObject(_id)
				);
			} else {
				ScriptBuilder.Build(
					"return {0}.getLoginItemSettings({1});",
					Script.GetObject(_id),
					options.Stringify()
				);
			}
			object result = _ExecuteBlocking<object>(script);
			return new JsonObject(result);
		}

		/// <summary>
		/// *macOS Windows*
		/// Set the app's login item settings.
		/// </summary>
		/// <param name="settings"></param>
		public void setLoginItemSettings(JsonObject settings) {
			string script = ScriptBuilder.Build(
				"{0}.setLoginItemSettings({1});",
				Script.GetObject(_id),
				settings.Stringify()
			);
			_ExecuteJavaScript(script);
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
			string script = ScriptBuilder.Build(
				"return {0}.isAccessibilitySupportEnabled();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Manually enables Chrome's accessibility support,
		/// allowing to expose accessibility switch to users in application settings. 
		/// </summary>
		/// <param name="enabled"></param>
		public void setAccessibilitySupportEnabled(bool enabled) {
			string script = ScriptBuilder.Build(
				"{0}.setAccessibilitySupportEnabled({1});",
				Script.GetObject(_id),
				enabled.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Set the about panel options. This will override the values
		/// defined in the app's .plist file. See the Apple docs for more details.
		/// </summary>
		/// <param name="options"></param>
		public void setAboutPanelOptions(JsonObject options) {
			string script = ScriptBuilder.Build(
				"{0}.setAboutPanelOptions({1});",
				Script.GetObject(_id),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS (mas)*
		/// Returns Function - This function must be called once you have finished
		/// accessing the security scoped file. 
		/// </summary>
		/// <param name="bookmarkData"></param>
		public void startAccessingSecurityScopedResource(string bookmarkData) {
			string script = ScriptBuilder.Build(
				"{0}.startAccessingSecurityScopedResource({1});",
				Script.GetObject(_id),
				bookmarkData.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Experimental macOS Windows*
		/// Enables mixed sandbox mode on the app.
		/// This method can only be called before app is ready.
		/// </summary>
		public void enableMixedSandbox() {
			string script = ScriptBuilder.Build(
				"{0}.enableMixedSandbox();",
				Script.GetObject(_id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Returns Boolean - Whether the application is currently running
		/// from the systems Application folder.
		/// Use in combination with app.moveToApplicationsFolder()
		/// </summary>
		/// <returns></returns>
		public bool isInApplicationsFolder() {
			string script = ScriptBuilder.Build(
				"return {0}.isInApplicationsFolder();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS*
		/// Returns Boolean - Whether the move was successful.
		/// Please note that if the move is successful your application will quit and relaunch.
		/// </summary>
		/// <returns></returns>
		public bool moveToApplicationsFolder() {
			string script = ScriptBuilder.Build(
				"return {0}.moveToApplicationsFolder();",
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}
	}
}
