using System;
using System.Threading;

namespace Socketron {
	/// <summary>
	/// Add icons and context menus to the system's notification area.
	/// <para>Process: Main</para>
	/// </summary>
	public class Tray : ElectronBase {
		public const string Name = "Tray";
		public int ID;

		public class Events {
			public const string Click = "click";
			/// <summary>*macOS Windows*</summary>
			public const string RightClick = "right-click";
			/// <summary>*macOS Windows*</summary>
			public const string DoubleClick = "double-click";
			/// <summary>*Windows*</summary>
			public const string BalloonShow = "balloon-show";
			/// <summary>*Windows*</summary>
			public const string BalloonClick = "balloon-click";
			/// <summary>*Windows*</summary>
			public const string BalloonClosed = "balloon-closed";
			/// <summary>*macOS*</summary>
			public const string Drop = "drop";
			/// <summary>*macOS*</summary>
			public const string DropFiles = "drop-files";
			/// <summary>*macOS*</summary>
			public const string DropText = "drop-text";
			/// <summary>*macOS*</summary>
			public const string DragEnter = "drag-enter";
			/// <summary>*macOS*</summary>
			public const string DragLeave = "drag-leave";
			/// <summary>*macOS*</summary>
			public const string DragEnd = "drag-end";
			/// <summary>*macOS*</summary>
			public const string MouseEnter = "mouse-enter";
			/// <summary>*macOS*</summary>
			public const string MouseLeave = "mouse-leave";
			/// <summary>*macOS*</summary>
			public const string MouseMove = "mouse-move";
		}

		public Tray(Socketron socketron) {
			_socketron = socketron;
		}

		/// <summary>
		/// Destroys the tray icon immediately.
		/// </summary>
		public void Destroy() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"tray.destroy();",
					"{1};"
				),
				Script.GetObject(ID),
				Script.RemoveObject(ID)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets the image associated with this tray icon.
		/// </summary>
		/// <param name="image"></param>
		public void SetImage(NativeImage image) {
			if (image == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"var tray = {1};",
					"tray.setImage(image);"
				),
				Script.GetObject(image.ID),
				Script.GetObject(ID)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets the image associated with this tray icon.
		/// </summary>
		/// <param name="image"></param>
		public void SetImage(string image) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"tray.setImage({1});"
				),
				Script.GetObject(ID),
				image.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Sets the image associated with this tray icon when pressed on macOS.
		/// </summary>
		/// <param name="image"></param>
		public void SetPressedImage(NativeImage image) {
			if (image == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"var tray = {1};",
					"tray.setPressedImage(image);"
				),
				Script.GetObject(image.ID),
				Script.GetObject(ID)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets the hover text for this tray icon.
		/// </summary>
		/// <param name="toolTip"></param>
		public void SetToolTip(string toolTip) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"tray.setToolTip({1});"
				),
				Script.GetObject(ID),
				toolTip.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Sets the title displayed aside of the tray icon in the status bar (Support ANSI colors).
		/// </summary>
		/// <param name="title"></param>
		public void SetTitle(string title) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"tray.setToolTip({1});"
				),
				Script.GetObject(ID),
				title.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Sets when the tray's icon background becomes highlighted (in blue).
		/// </summary>
		/// <param name="mode"></param>
		public void SetHighlightMode(string mode) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"tray.setHighlightMode({1});"
				),
				Script.GetObject(ID),
				mode.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows*
		/// Displays a tray balloon.
		/// </summary>
		/// <param name="mode"></param>
		public void DisplayBalloon(JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"tray.displayBalloon({1});"
				),
				Script.GetObject(ID),
				options.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Pops up the context menu of the tray icon.
		/// When menu is passed, the menu will be shown instead of the tray icon's context menu.
		/// <para>
		/// The position is only available on Windows, and it is (0, 0) by default.
		/// </para>
		/// </summary>
		public void PopUpContextMenu() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"tray.popUpContextMenu();"
				),
				Script.GetObject(ID)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Pops up the context menu of the tray icon.
		/// When menu is passed, the menu will be shown instead of the tray icon's context menu.
		/// <para>
		/// The position is only available on Windows, and it is (0, 0) by default.
		/// </para>
		/// </summary>
		/// <param name="menu"></param>
		public void PopUpContextMenu(Menu menu) {
			if (menu == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"var menu = {1};",
					"tray.popUpContextMenu(menu);"
				),
				Script.GetObject(ID),
				Script.GetObject(menu.ID)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// Pops up the context menu of the tray icon.
		/// When menu is passed, the menu will be shown instead of the tray icon's context menu.
		/// <para>
		/// The position is only available on Windows, and it is (0, 0) by default.
		/// </para>
		/// </summary>
		/// <param name="menu"></param>
		/// <param name="position"></param>
		public void PopUpContextMenu(Menu menu, Point position) {
			if (menu == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"var menu = {1};",
					"tray.popUpContextMenu(menu, {2});"
				),
				Script.GetObject(ID),
				Script.GetObject(menu.ID),
				position.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets the context menu for this icon.
		/// </summary>
		/// <param name="menu"></param>
		public void SetContextMenu(Menu menu) {
			if (menu == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"var menu = {1};",
					"tray.setContextMenu(menu);"
				),
				Script.GetObject(ID),
				Script.GetObject(menu.ID)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// The bounds of this tray icon as Object.
		/// </summary>
		/// <returns></returns>
		public Rectangle GetBounds() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"return tray.getBounds();"
				),
				Script.GetObject(ID)
			);
			object result = _ExecuteJavaScriptBlocking<object>(script);
			return Rectangle.FromObject(result);
		}

		/// <summary>
		/// Returns Boolean - Whether the tray icon is destroyed.
		/// </summary>
		/// <returns></returns>
		public bool IsDestroyed() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"return tray.isDestroyed();"
				),
				Script.GetObject(ID)
			);
			return _ExecuteJavaScriptBlocking<bool>(script);
		}
	}
}
