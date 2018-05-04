using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Socketron {
	/// <summary>
	/// Add icons and context menus to the system's notification area.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Tray : NodeBase {
		public const string Name = "Tray";

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
		public void destroy() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"tray.destroy();",
					"{1};"
				),
				Script.GetObject(id),
				Script.RemoveObject(id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets the image associated with this tray icon.
		/// </summary>
		/// <param name="image"></param>
		public void setImage(NativeImage image) {
			if (image == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"var tray = {1};",
					"tray.setImage(image);"
				),
				Script.GetObject(image.id),
				Script.GetObject(id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets the image associated with this tray icon.
		/// </summary>
		/// <param name="image"></param>
		public void setImage(string image) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"tray.setImage({1});"
				),
				Script.GetObject(id),
				image.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Sets the image associated with this tray icon when pressed on macOS.
		/// </summary>
		/// <param name="image"></param>
		public void setPressedImage(NativeImage image) {
			if (image == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"var tray = {1};",
					"tray.setPressedImage(image);"
				),
				Script.GetObject(image.id),
				Script.GetObject(id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets the hover text for this tray icon.
		/// </summary>
		/// <param name="toolTip"></param>
		public void setToolTip(string toolTip) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"tray.setToolTip({1});"
				),
				Script.GetObject(id),
				toolTip.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Sets the title displayed aside of the tray icon in the status bar (Support ANSI colors).
		/// </summary>
		/// <param name="title"></param>
		public void setTitle(string title) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"tray.setToolTip({1});"
				),
				Script.GetObject(id),
				title.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS*
		/// Sets when the tray's icon background becomes highlighted (in blue).
		/// </summary>
		/// <param name="mode"></param>
		public void setHighlightMode(string mode) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"tray.setHighlightMode({1});"
				),
				Script.GetObject(id),
				mode.Escape()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *Windows*
		/// Displays a tray balloon.
		/// </summary>
		/// <param name="mode"></param>
		public void displayBalloon(JsonObject options) {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"tray.displayBalloon({1});"
				),
				Script.GetObject(id),
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
		public void popUpContextMenu() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"tray.popUpContextMenu();"
				),
				Script.GetObject(id)
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
		public void popUpContextMenu(Menu menu) {
			if (menu == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"var menu = {1};",
					"tray.popUpContextMenu(menu);"
				),
				Script.GetObject(id),
				Script.GetObject(menu.id)
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
		public void popUpContextMenu(Menu menu, Point position) {
			if (menu == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"var menu = {1};",
					"tray.popUpContextMenu(menu, {2});"
				),
				Script.GetObject(id),
				Script.GetObject(menu.id),
				position.Stringify()
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// Sets the context menu for this icon.
		/// </summary>
		/// <param name="menu"></param>
		public void setContextMenu(Menu menu) {
			if (menu == null) {
				return;
			}
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"var menu = {1};",
					"tray.setContextMenu(menu);"
				),
				Script.GetObject(id),
				Script.GetObject(menu.id)
			);
			_ExecuteJavaScript(script);
		}

		/// <summary>
		/// *macOS Windows*
		/// The bounds of this tray icon as Object.
		/// </summary>
		/// <returns></returns>
		public Rectangle getBounds() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"return tray.getBounds();"
				),
				Script.GetObject(id)
			);
			object result = _ExecuteBlocking<object>(script);
			return Rectangle.FromObject(result);
		}

		/// <summary>
		/// Returns Boolean - Whether the tray icon is destroyed.
		/// </summary>
		/// <returns></returns>
		public bool isDestroyed() {
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = {0};",
					"return tray.isDestroyed();"
				),
				Script.GetObject(id)
			);
			return _ExecuteBlocking<bool>(script);
		}
	}
}
