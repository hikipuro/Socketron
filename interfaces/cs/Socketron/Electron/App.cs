using System;

namespace Socketron {
	/// <summary>
	/// Control your application's event lifecycle.
	/// <para>Process: Main</para>
	/// </summary>
	public class App : EventEmitter {
		public const string Name = "App";
		const string Type = ProcessType.Browser;
		const string Prefix = "electron.app.";
		public AppCommandLine CommandLine;
		public AppDock Dock;

		public class Events {
			public const string WillFinishLaunching = "will-finish-launching";
			public const string Ready = "ready";
			public const string WindowAllClosed = "window-all-closed";
			public const string BeforeQuit = "before-quit";
			public const string WillQuit = "will-quit";
			public const string Quit = "quit";
			/// <summary>*macOS*</summary>
			public const string OpenFile = "open-file";
			/// <summary>*macOS*</summary>
			public const string OpenUrl = "open-url";
			/// <summary>*macOS*</summary>
			public const string Activate = "activate";
			/// <summary>*macOS*</summary>
			public const string ContinueActivity = "continue-activity";
			/// <summary>*macOS*</summary>
			public const string WillContinueActivity = "will-continue-activity";
			/// <summary>*macOS*</summary>
			public const string ContinueActivityError = "continue-activity-error";
			/// <summary>*macOS*</summary>
			public const string ActivityWasContinued = "activity-was-continued";
			/// <summary>*macOS*</summary>
			public const string UpdateActivityState = "update-activity-state";
			/// <summary>*macOS*</summary>
			public const string NewWindowForTab = "new-window-for-tab";
			public const string BrowserWindowBlur = "browser-window-blur";
			public const string BrowserWindowFocus = "browser-window-focus";
			public const string BrowserWindowCreated = "browser-window-created";
			public const string WebContentsCreated = "web-contents-created";
			public const string CertificateError = "certificate-error";
			public const string SelectClientCertificate = "select-client-certificate";
			public const string Login = "login";
			public const string GpuProcessCrashed = "gpu-process-crashed";
			/// <summary>*macOS Windows*</summary>
			public const string AccessibilitySupportChanged = "accessibility-support-changed";
			public const string SessionCreated = "session-created";
		}

		public App() {
			CommandLine = new AppCommandLine();
			CommandLine.On("text", (args) => {
				Emit("text", args[0], args[1]);
			});
			Dock = new AppDock();
			Dock.On("text", (args) => {
				Emit("text", args[0], args[1]);
			});
		}

		public void Quit() {
			_Exec("quit");
		}

		public void Exit(int exitCode = 0) {
			_Exec("exit", exitCode);
		}

		public void Relaunch(JsonObject options = null) {
			_Exec("relaunch", options);
		}

		public void IsReady(Callback callback) {
			_Exec("isReady", null, callback);
		}

		public void Focus() {
			_Exec("focus");
		}

		public void Hide() {
			_Exec("hide");
		}

		public void Show() {
			_Exec("show");
		}

		public void GetAppPath(Callback callback) {
			_Exec("getAppPath", null, callback);
		}

		public void GetPath(string name, Callback callback) {
			_Exec("getPath", new object[] { name }, callback);
		}

		//public void GetFileIcon(string path, JsonObject options, Callback callback) {
		//	_Exec("getFileIcon", new object[] { path, options }, callback);
		//}

		public void SetPath(string name, string path) {
			_Exec("setPath", name, path);
		}

		public void GetVersion(Callback callback) {
			_Exec("getVersion", null, callback);
		}

		public void GetName(Callback callback) {
			_Exec("getName", null, callback);
		}

		public void SetName(string name) {
			_Exec("setName", name);
		}

		public void GetLocale(Callback callback) {
			_Exec("getLocale", null, callback);
		}

		public void AddRecentDocument(string path) {
			_Exec("addRecentDocument", path);
		}

		public void ClearRecentDocuments() {
			_Exec("clearRecentDocuments");
		}

		public void SetAsDefaultProtocolClient(string protocol, string path = null, string args = null) {
			_Exec("setAsDefaultProtocolClient", protocol, path, args);
		}

		public void RemoveAsDefaultProtocolClient(string protocol, string path = null, string args = null) {
			_Exec("removeAsDefaultProtocolClient", protocol, path, args);
		}

		public void IsDefaultProtocolClient(string protocol, string path = null, string args = null) {
			_Exec("isDefaultProtocolClient", protocol, path, args);
		}

		//public void SetUserTasks() {
		//	_Exec("setUserTasks");
		//}

		public void GetJumpListSettings(Callback callback) {
			_Exec("getJumpListSettings", null, callback);
		}

		//public void SetJumpList() {
		//	_Exec("setJumpList");
		//}

		//public void MakeSingleInstance() {
		//	_Exec("makeSingleInstance");
		//}

		public void ReleaseSingleInstance() {
			_Exec("releaseSingleInstance");
		}

		public void SetUserActivity(string type, JsonObject userInfo, string webpageURL = null) {
			_Exec("setUserActivity", type, userInfo, webpageURL);
		}

		public void GetCurrentActivityType(Callback callback) {
			_Exec("getCurrentActivityType", null, callback);
		}

		public void InvalidateCurrentActivity(Callback callback) {
			_Exec("invalidateCurrentActivity", null, callback);
		}

		public void UpdateCurrentActivity(string type, JsonObject userInfo) {
			_Exec("updateCurrentActivity", type, userInfo);
		}

		public void SetAppUserModelId(string id) {
			_Exec("setAppUserModelId", id);
		}

		//public void ImportCertificate(JsonObject options, Callback callback) {
		//	_Exec("importCertificate", options);
		//}

		public void DisableHardwareAcceleration() {
			_Exec("disableHardwareAcceleration");
		}

		public void DisableDomainBlockingFor3DAPIs() {
			_Exec("disableDomainBlockingFor3DAPIs");
		}

		public void GetAppMemoryInfo(Callback callback) {
			_Exec("getAppMemoryInfo", null, callback);
		}

		public void GetAppMetrics(Callback callback) {
			_Exec("getAppMetrics", null, callback);
		}

		public void GetGPUFeatureStatus(Callback callback) {
			_Exec("getGPUFeatureStatus", null, callback);
		}

		public void SetBadgeCount(int count) {
			_Exec("setBadgeCount", count);
		}

		public void GetBadgeCount(Callback callback) {
			_Exec("getBadgeCount", null, callback);
		}

		public void IsUnityRunning(Callback callback) {
			_Exec("isUnityRunning", null, callback);
		}

		public void GetLoginItemSettings(JsonObject options, Callback callback) {
			_Exec("getLoginItemSettings", new object[] { options }, callback);
		}

		public void SetLoginItemSettings(JsonObject settings) {
			_Exec("setLoginItemSettings", settings);
		}

		public void IsAccessibilitySupportEnabled(Callback callback) {
			_Exec("isAccessibilitySupportEnabled", null, callback);
		}

		public void SetAccessibilitySupportEnabled(bool enabled) {
			_Exec("setAccessibilitySupportEnabled", enabled);
		}

		public void SetAboutPanelOptions(JsonObject options) {
			_Exec("setAboutPanelOptions", options);
		}

		public void EnableMixedSandbox() {
			_Exec("enableMixedSandbox");
		}

		public void IsInApplicationsFolder(Callback callback) {
			_Exec("isInApplicationsFolder", null, callback);
		}

		public void MoveToApplicationsFolder(Callback callback) {
			_Exec("moveToApplicationsFolder", null, callback);
		}

		protected void _Exec(string name, object[] args = null, Callback callback = null) {
			Emit("text", _Data(name, args), callback);
		}

		protected void _Exec(string name, params object[] args) {
			Emit("text", _Data(name, args), null);
		}

		protected SocketronData _Data(string name, object[] args) {
			return new SocketronData() {
				Type = Type,
				Func = Prefix + name,
				Params = args
			};
		}
	}

	public class AppCommandLine : EventEmitter {
		const string Type = ProcessType.Browser;
		const string Prefix = "electron.app.commandLine.";

		public void AppendSwitch(string _switch, string value = null) {
			_Exec("appendSwitch", _switch, value);
		}

		public void AppendArgument(string value) {
			_Exec("appendArgument", value);
		}

		protected void _Exec(string name, object[] args = null, Callback callback = null) {
			Emit("text", _Data(name, args), callback);
		}

		protected void _Exec(string name, params object[] args) {
			Emit("text", _Data(name, args), null);
		}

		protected SocketronData _Data(string name, object[] args) {
			return new SocketronData() {
				Type = Type,
				Func = Prefix + name,
				Params = args
			};
		}
	}

	public class AppDock : EventEmitter {
		const string Type = ProcessType.Browser;
		const string Prefix = "electron.app.dock.";

		public void Bounce(string type = null) {
			_Exec("bounce", type);
		}

		public void CancelBounce(int id) {
			_Exec("cancelBounce", id);
		}

		public void DownloadFinished(string filePath) {
			_Exec("downloadFinished", filePath);
		}

		public void SetBadge(string text) {
			_Exec("setBadge", text);
		}

		public void GetBadge(Callback callback) {
			_Exec("getBadge", callback);
		}

		public void Hide() {
			_Exec("hide");
		}

		public void Show() {
			_Exec("show");
		}

		public void IsVisible(Callback callback) {
			_Exec("isVisible", callback);
		}

		//public void SetMenu() {
		//	_Exec("setMenu");
		//}

		//public void SetIcon() {
		//	_Exec("setIcon");
		//}

		protected void _Exec(string name, object[] args = null, Callback callback = null) {
			Emit("text", _Data(name, args), callback);
		}

		protected void _Exec(string name, params object[] args) {
			Emit("text", _Data(name, args), null);
		}

		protected SocketronData _Data(string name, object[] args) {
			return new SocketronData() {
				Type = Type,
				Func = Prefix + name,
				Params = args
			};
		}
	}
}
