namespace Socketron {
	public enum DataType {
		Text16 = 0,
		Text32 = 1,
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
