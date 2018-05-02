namespace Socketron {
	public class Task {
		public string program;
		public string arguments;
		public string title;
		public string description;
		public string iconPath;
		public int? iconIndex;

		public static Task Parse(string text) {
			return JSON.Parse<Task>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
