using System.Diagnostics.CodeAnalysis;

namespace Socketron.Electron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class UploadData : JSObject {
		/// <summary>
		/// Content being sent.
		/// </summary>
		public Buffer bytes {
			get { return API.GetObject<Buffer>("bytes"); }
		}
		/// <summary>
		/// Path of file being uploaded.
		/// </summary>
		public string file {
			get { return API.GetProperty<string>("file"); }
		}
		/// <summary>
		/// UUID of blob data.
		/// Use ses.getBlobData method to retrieve the data.
		/// </summary>
		public string blobUUID {
			get { return API.GetProperty<string>("blobUUID"); }
		}

		/*
		public static UploadData FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new UploadData() {
				//bytes = json.Int32("bytes"),
				file = json.String("file"),
				blobUUID = json.String("blobUUID")
			};
		}
		//*/

		/// <summary>
		/// Create JSON text.
		/// </summary>
		/// <returns></returns>
		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

