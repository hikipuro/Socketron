using System.Diagnostics.CodeAnalysis;

namespace Socketron {
	[type: SuppressMessage("Style", "IDE1006")]
	public class Readable : JSObject {
		public Readable() {
		}

		public int readableHighWaterMark {
			get { return API.GetProperty<int>("readableHighWaterMark"); }
		}

		public int readableLength {
			get { return API.GetProperty<int>("readableLength"); }
		}

		public Readable destroy(Error error = null) {
			if (error == null) {
				API.Apply("destroy");
			} else {
				API.Apply("destroy", error);
			}
			return this;
		}

		public bool isPaused() {
			return API.Apply<bool>("isPaused");
		}

		public Readable pause() {
			API.Apply("pause");
			return this;
		}

		/*
		public Writable pipe(Writable destination, JsonObject options) {
			return API.ApplyAndGetObject<Writable>("pipe", destination, options);
		}
		//*/

		public string read(int size) {
			return API.Apply<string>("read", size);
		}

		public Readable resume() {
			API.Apply("resume");
			return this;
		}

		public Readable setEncoding(string encoding) {
			API.Apply("setEncoding", encoding);
			return this;
		}

		/*
		public Readable unpipe(Writable destination = null) {
			if (destination == null) {
				API.Apply("unpipe");
			} else {
				API.Apply("unpipe", destination);
			}
			return this;
		}
		//*/

		public void unshift(string chunk) {
			API.Apply("unshift", chunk);
		}

		/*
		public Readable wrap(Stream stream) {
			API.Apply("wrap", stream);
			return this;
		}
		//*/
	}
}
