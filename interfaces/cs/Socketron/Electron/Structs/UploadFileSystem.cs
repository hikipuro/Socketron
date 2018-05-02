namespace Socketron {
	public class UploadFileSystem {
		public string type;
		public string filsSystemURL;
		public int? offset;
		public int? length;
		public double? modificationTime;

		public static UploadFileSystem Parse(string text) {
			return JSON.Parse<UploadFileSystem>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

