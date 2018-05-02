namespace Socketron {
	public class UploadFile {
		public string type;
		public string filePath;
		public int? offset;
		public int? length;
		public double? modificationTime;

		public static UploadFile Parse(string text) {
			return JSON.Parse<UploadFile>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}
