using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	/// <summary>
	/// Add icons and context menus to the system's notification area.
	/// <para>Process: Main</para>
	/// </summary>
	[type: SuppressMessage("Style", "IDE1006")]
	public class Tray : NodeModule {
		public const string Name = "Tray";

		/// <summary>
		/// Tray instance events.
		/// </summary>
		public class Events {
			/// <summary>
			/// Emitted when the tray icon is clicked.
			/// </summary>
			public const string Click = "click";
			/// <summary>
			/// *macOS Windows*
			/// Emitted when the tray icon is right clicked.
			/// </summary>
			public const string RightClick = "right-click";
			/// <summary>
			/// *macOS Windows*
			/// Emitted when the tray icon is double clicked.
			/// </summary>
			public const string DoubleClick = "double-click";
			/// <summary>
			/// *Windows*
			/// Emitted when the tray balloon shows.
			/// </summary>
			public const string BalloonShow = "balloon-show";
			/// <summary>
			/// *Windows*
			/// Emitted when the tray balloon is clicked.
			/// </summary>
			public const string BalloonClick = "balloon-click";
			/// <summary>
			/// *Windows*
			/// Emitted when the tray balloon is closed
			/// because of timeout or user manually closes it.
			/// </summary>
			public const string BalloonClosed = "balloon-closed";
			/// <summary>
			/// *macOS*
			/// Emitted when any dragged items are dropped on the tray icon.
			/// </summary>
			public const string Drop = "drop";
			/// <summary>
			/// *macOS*
			/// Emitted when dragged files are dropped in the tray icon.
			/// </summary>
			public const string DropFiles = "drop-files";
			/// <summary>
			/// *macOS*
			/// Emitted when dragged text is dropped in the tray icon.
			/// </summary>
			public const string DropText = "drop-text";
			/// <summary>
			/// *macOS*
			/// Emitted when a drag operation enters the tray icon.
			/// </summary>
			public const string DragEnter = "drag-enter";
			/// <summary>
			/// *macOS*
			/// Emitted when a drag operation exits the tray icon.
			/// </summary>
			public const string DragLeave = "drag-leave";
			/// <summary>
			/// *macOS*
			/// Emitted when a drag operation ends on the tray or ends at another location.
			/// </summary>
			public const string DragEnd = "drag-end";
			/// <summary>
			/// *macOS*
			/// Emitted when the mouse enters the tray icon.
			/// </summary>
			public const string MouseEnter = "mouse-enter";
			/// <summary>
			/// *macOS*
			/// Emitted when the mouse exits the tray icon.
			/// </summary>
			public const string MouseLeave = "mouse-leave";
			/// <summary>
			/// *macOS*
			/// Emitted when the mouse moves in the tray icon.
			/// </summary>
			public const string MouseMove = "mouse-move";
		}

		/// <summary>
		/// Used Internally by the library.
		/// </summary>
		/// <param name="client"></param>
		public Tray(SocketronClient client) {
			_client = client;
		}

		/// <summary>
		/// Creates a new tray icon associated with the image.
		/// </summary>
		/// <param name="image"></param>
		public Tray(NativeImage image) {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var image = {0};",
					"var tray = new electron.Tray(image);",
					"return {1};"
				),
				Script.GetObject(image._id),
				Script.AddObject("tray")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			_client = client;
			_id = result;
		}

		/// <summary>
		/// Creates a new tray icon associated with the image.
		/// </summary>
		/// <param name="image"></param>
		public Tray(string image) {
			SocketronClient client = SocketronClient.GetCurrent();
			string script = ScriptBuilder.Build(
				ScriptBuilder.Script(
					"var tray = new electron.Tray({0});",
					"return {1};"
				),
				image.Escape(),
				Script.AddObject("tray")
			);
			int result = client.ExecuteJavaScriptBlocking<int>(script);
			_client = client;
			_id = result;
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
				Script.GetObject(_id),
				Script.RemoveObject(_id)
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
				Script.GetObject(image._id),
				Script.GetObject(_id)
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
				Script.GetObject(_id),
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
				Script.GetObject(image._id),
				Script.GetObject(_id)
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
				Script.GetObject(_id),
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
				Script.GetObject(_id),
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
				Script.GetObject(_id),
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
				Script.GetObject(_id),
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
				Script.GetObject(_id)
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
				Script.GetObject(_id),
				Script.GetObject(menu._id)
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
				Script.GetObject(_id),
				Script.GetObject(menu._id),
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
				Script.GetObject(_id),
				Script.GetObject(menu._id)
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
				Script.GetObject(_id)
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
				Script.GetObject(_id)
			);
			return _ExecuteBlocking<bool>(script);
		}
	}
}
