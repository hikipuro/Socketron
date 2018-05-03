using System.Collections.Generic;

namespace Socketron {
	/// <summary>
	/// Control your application's event lifecycle.
	/// <para>Process: Main</para>
	/// </summary>
	public class AppClass : ElectronBase {
		public AppCommandLine commandLine;
		/// <summary>*macOS*</summary>
		public AppDock dock;

		public class AppCommandLine : ElectronBase {
			public AppCommandLine(Socketron socketron) {
				_socketron = socketron;
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
			public void AppendSwitch(string @switch, string value = null) {
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
			public void AppendArgument(string value) {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"electron.app.commandLine.appendArgument({0});"
					),
					value.Escape()
				);
				_ExecuteJavaScript(script);
			}
		}

		public class AppDock : ElectronBase {
			public AppDock(Socketron socketron) {
				_socketron = socketron;
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
			public void Bounce(string type = null) {
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
			public void CancelBounce(int id) {
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
			public void DownloadFinished(string filePath) {
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
			public void SetBadge(string text) {
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
			public string GetBadge() {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return electron.app.dock.getBadge();"
					)
				);
				return _ExecuteJavaScriptBlocking<string>(script);
			}

			/// <summary>
			/// *macOS*
			/// Hides the dock icon.
			/// </summary>
			public void Hide() {
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
			public void Show() {
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
			public bool IsVisible() {
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return electron.app.dock.isVisible();"
					)
				);
				return _ExecuteJavaScriptBlocking<bool>(script);
			}

			/// <summary>
			/// *macOS*
			/// Sets the application's dock menu.
			/// </summary>
			/// <param name="menu"></param>
			public void SetMenu(Menu menu) {
				if (menu == null) {
					return;
				}
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var menu = {0};",
						"electron.app.dock.setMenu(menu);"
					),
					Script.GetObject(menu.ID)
				);
				_ExecuteJavaScript(script);
			}

			/// <summary>
			/// *macOS*
			/// Sets the image associated with this dock icon.
			/// </summary>
			/// <param name="image"></param>
			public void SetIcon(NativeImage image) {
				if (image == null) {
					return;
				}
				string script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"var image = {0};",
						"electron.app.dock.setIcon(image);"
					),
					Script.GetObject(image.ID)
				);
				_ExecuteJavaScript(script);
			}
		}

		public AppClass(Socketron socketron) {
			_socketron = socketron;
			commandLine = new AppCommandLine(socketron);
			dock = new AppDock(socketron);
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
		public void Quit() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.quit();"
				)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Exits immediately with exitCode. exitCode defaults to 0.
		/// <para>
		/// All windows will be closed immediately without asking user 
		/// and the before-quit and will-quit events will not be emitted.
		/// </para>
		/// </summary>
		/// <param name="exitCode"></param>
		public void Exit(int exitCode = 0) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.exit({0});"
				),
				exitCode
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Relaunches the app when current instance exits.
		/// </summary>
		/// <param name="options"></param>
		public void Relaunch(JsonObject options = null) {
			string script = string.Empty;
			if (options == null) {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"electron.app.relaunch();"
					)
				);
			} else {
				script = ScriptBuilder.Build(
					ScriptBuilder.Script(
						"electron.app.relaunch({0});"
					),
					options.Stringify()
				);
			}
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns Boolean - true if Electron has finished initializing, false otherwise.
		/// </summary>
		/// <returns></returns>
		public bool IsReady() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.isReady();"
				)
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/*
		public bool WhenReady() {
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
		public void Focus() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.focus();"
				)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Hides all application windows without minimizing them.
		/// </summary>
		public void Hide() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.hide();"
				)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Shows application windows after they were hidden. Does not automatically focus them.
		/// </summary>
		public void Show() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.show();"
				)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns String - The current application directory.
		/// </summary>
		/// <returns></returns>
		public string GetAppPath() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.getAppPath();"
				)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// Returns String - A path to a special directory or file associated with name.
		/// On failure an Error is thrown.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string GetPath(string name) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.getPath({0});"
				),
				name.Escape()
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/*
		public void GetFileIcon(string path, JsonObject options, Callback callback) {
			// TODO: implement this
		}
		//*/

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
		public void SetPath(string name, string path) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.setPath({0},{1});"
				),
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
		public string GetVersion() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.getVersion();"
				)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// Returns String - The current application's name,
		/// which is the name in the application's package.json file.
		/// </summary>
		/// <returns></returns>
		public string GetName() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.getName();"
				)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// Overrides the current application's name.
		/// </summary>
		/// <param name="name"></param>
		public void SetName(string name) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.setName({0});"
				),
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
		public string GetLocale() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.getLocale();"
				)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
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
		public void AddRecentDocument(string path) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.addRecentDocument({0});"
				),
				path.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Clears the recent documents list.
		/// </summary>
		public void ClearRecentDocuments() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.clearRecentDocuments();"
				)
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
		public void SetAsDefaultProtocolClient(string protocol, string path = null, string[] args = null) {
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
				ScriptBuilder.Script(
					"electron.app.setAsDefaultProtocolClient({0});"
				),
				option
			);
			_ExecuteJavaScript(script);
		}

		/*
		public void RemoveAsDefaultProtocolClient(string protocol, string path = null, string[] args = null) {
			// TODO: implement this
			_Exec("removeAsDefaultProtocolClient", protocol, path, args);
		}
		//*/

		/*
		public void IsDefaultProtocolClient(string protocol, string path = null, string[] args = null) {
			// TODO: implement this
			_Exec("isDefaultProtocolClient", protocol, path, args);
		}
		//*/

		/// <summary>
		/// *Windows*
		/// </summary>
		/// <param name="tasks"></param>
		/*
		public void SetUserTasks(Task[] tasks) {
			// TODO: implement this
			_Exec("setUserTasks");
		}
		//*/

		/// <summary>
		/// *Windows*
		/// Returns Object:
		/// </summary>
		/// <returns></returns>
		public JsonObject GetJumpListSettings() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.getJumpListSettings();"
				)
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return new JsonObject(result);

		}

		// *Windows*
		/*
		public void SetJumpList() {
			// TODO: implement this
			_Exec("setJumpList");
		}
		//*/

		/*
		public void MakeSingleInstance() {
			// TODO: implement this
			_Exec("makeSingleInstance");
		}
		//*/

		/// <summary>
		/// Releases all locks that were created by makeSingleInstance.
		/// This will allow multiple instances of the application to once again run side by side.
		/// </summary>
		public void ReleaseSingleInstance() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.releaseSingleInstance();"
				)
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
		public void SetUserActivity(string type, JsonObject userInfo, string webpageURL = null) {
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
				ScriptBuilder.Script(
					"electron.app.setUserActivity({0});"
				),
				option
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Returns String - The type of the currently running activity.
		/// </summary>
		/// <returns></returns>
		public string GetCurrentActivityType() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.getCurrentActivityType();"
				)
			);
			return _ExecuteJavaScriptBlocking<string>(script);
		}

		/// <summary>
		/// *macOS*
		/// Invalidates the current Handoff user activity.
		/// </summary>
		public void InvalidateCurrentActivity() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.invalidateCurrentActivity();"
				)
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
		public void UpdateCurrentActivity(string type, JsonObject userInfo) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.updateCurrentActivity({0},{1});"
				),
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
		public void SetAppUserModelId(string id) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.setAppUserModelId({0});"
				),
				id.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/*
		public void ImportCertificate(JsonObject options, Callback callback) {
			// TODO: implement this
			_Exec("importCertificate", options);
		}
		//*/

		/// <summary>
		/// Disables hardware acceleration for current app.
		/// <para>
		/// This method can only be called before app is ready.
		/// </para>
		/// </summary>
		public void DisableHardwareAcceleration() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.disableHardwareAcceleration();"
				)
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
		public void DisableDomainBlockingFor3DAPIs() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.disableDomainBlockingFor3DAPIs();"
				)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Returns ProcessMetric[]: Array of ProcessMetric objects
		/// that correspond to memory and cpu usage statistics
		/// of all the processes associated with the app.
		/// </summary>
		/// <returns></returns>
		public List<ProcessMetric> GetAppMetrics() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.getAppMetrics();"
				)
			);
			object[] result = _ExecuteJavaScriptBlocking<object[]>(script);
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
		public GPUFeatureStatus GetGPUFeatureStatus() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.getGPUFeatureStatus();"
				)
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return GPUFeatureStatus.FromObject(result);
		}

		/// <summary>
		/// *Linux macOS*
		/// Returns Boolean - Whether the call succeeded.
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public bool SetBadgeCount(int count) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.setBadgeCount({0});"
				),
				count
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *Linux macOS*
		/// Returns Integer - The current value displayed in the counter badge.
		/// </summary>
		/// <returns></returns>
		public int GetBadgeCount() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.getBadgeCount();"
				)
			);
			return _ExecuteJavaScriptBlocking<int>(script);
		}

		/// <summary>
		/// *Linux*
		/// Returns Boolean - Whether the current desktop environment is Unity launcher.
		/// </summary>
		/// <returns></returns>
		public bool IsUnityRunning() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.isUnityRunning();"
				)
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// </summary>
		/// <param name="options"></param>
		/// <returns></returns>
		public JsonObject GetLoginItemSettings(JsonObject options = null) {
			string script = string.Empty;
			if (options == null) {
				ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return electron.app.getLoginItemSettings();"
					)
				);
			} else {
				ScriptBuilder.Build(
					ScriptBuilder.Script(
						"return electron.app.getLoginItemSettings({0});"
					),
					options.Stringify()
				);
			}
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return new JsonObject(result);
		}

		/// <summary>
		/// *macOS Windows*
		/// </summary>
		/// <param name="settings"></param>
		public void SetLoginItemSettings(JsonObject settings) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.setLoginItemSettings({0});"
				),
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
		public bool IsAccessibilitySupportEnabled() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.isAccessibilitySupportEnabled();"
				)
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// </summary>
		/// <param name="enabled"></param>
		public void SetAccessibilitySupportEnabled(bool enabled) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.setAccessibilitySupportEnabled({0});"
				),
				enabled.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// </summary>
		/// <param name="options"></param>
		public void SetAboutPanelOptions(JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.setAboutPanelOptions({0});"
				),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// </summary>
		/// <param name="bookmarkData"></param>
		public void StartAccessingSecurityScopedResource(string bookmarkData) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.startAccessingSecurityScopedResource({0});"
				),
				bookmarkData.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Experimental macOS Windows*
		/// Enables mixed sandbox mode on the app.
		/// This method can only be called before app is ready.
		/// </summary>
		public void EnableMixedSandbox() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"electron.app.enableMixedSandbox();"
				)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// </summary>
		/// <returns></returns>
		public bool IsInApplicationsFolder() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.isInApplicationsFolder();"
				)
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

		/// <summary>
		/// *macOS*
		/// </summary>
		/// <returns></returns>
		public bool MoveToApplicationsFolder() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"return electron.app.moveToApplicationsFolder();"
				)
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}

	}
}
