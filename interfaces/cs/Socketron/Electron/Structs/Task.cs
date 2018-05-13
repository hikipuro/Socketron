namespace Socketron.Electron {
	public class Task {
		/// <summary>
		/// Path of the program to execute,
		/// usually you should specify process.execPath which opens the current program.
		/// </summary>
		public string program;
		/// <summary>
		/// The command line arguments when program is executed.
		/// </summary>
		public string arguments;
		/// <summary>
		/// The string to be displayed in a JumpList.
		/// </summary>
		public string title;
		/// <summary>
		/// Description of this task.
		/// </summary>
		public string description;
		/// <summary>
		/// The absolute path to an icon to be displayed in a JumpList,
		/// which can be an arbitrary resource file that contains an icon.
		/// You can usually specify process.execPath to show the icon of the program.
		/// </summary>
		public string iconPath;
		/// <summary>
		/// The icon index in the icon file.
		/// If an icon file consists of two or more icons,
		/// set this value to identify the icon.
		/// If an icon file consists of one icon, this value is 0.
		/// </summary>
		public int? iconIndex;

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static Task Parse(string text) {
			return JSON.Parse<Task>(text);
		}

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <returns></returns>
		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
