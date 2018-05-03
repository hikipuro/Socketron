namespace Socketron {
	/// <summary>
	/// Define keyboard shortcuts.
	/// </summary>
	public class Accelerator {
		public const string Command = "Command";
		public const string Control = "Control";
		public const string CommandOrControl = "CommandOrControl";
		public const string CmdOrCtrl = "CmdOrCtrl";
		public const string Alt = "Alt";
		public const string Option = "Option";
		public const string AltGr = "AltGr";
		public const string Shift = "Shift";
		public const string Super = "Super";

		public const string Plus = "Plus";
		public const string Space = "Space";
		public const string Tab = "Tab";
		public const string Backspace = "Backspace";
		public const string Delete = "Delete";
		public const string Insert = "Insert";
		public const string Return = "Return";
		public const string Enter = "Enter";
		public const string Up = "Up";
		public const string Down = "Down";
		public const string Left = "Left";
		public const string Right = "Right";
		public const string Home = "Home";
		public const string End = "End";
		public const string PageUp = "PageUp";
		public const string PageDown = "PageDown";
		public const string Escape = "Escape";
		public const string Esc = "Esc";
		public const string VolumeUp = "VolumeUp";
		public const string VolumeDown = "VolumeDown";
		public const string VolumeMute = "VolumeMute";
		public const string MediaNextTrack = "MediaNextTrack";
		public const string MediaPreviousTrack = "MediaPreviousTrack";
		public const string MediaStop = "MediaStop";
		public const string MediaPlayPause = "MediaPlayPause";
		public const string PrintScreen = "PrintScreen";

		public static string Concat(params string[] keys) {
			return string.Join("+", keys);
		}
	}
}
