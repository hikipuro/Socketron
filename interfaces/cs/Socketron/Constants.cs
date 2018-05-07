namespace Socketron {
	public enum DataType {
		Text = 0,
	}

	public enum ReadState {
		Type = 0,
		CommandLength,
		Command
	}

	public class ProcessType {
		public const string Browser = "browser";
		public const string Renderer = "renderer";
	}
}
