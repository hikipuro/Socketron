namespace Socketron.Electron {
	/// <summary>
	/// NativeImage.createFromBuffer() options.
	/// </summary>
	public class CreateFromBufferOptions {
		/// <summary>
		/// Required for bitmap buffers.
		/// </summary>
		public int width;
		/// <summary>
		/// Required for bitmap buffers.
		/// </summary>
		public int height;
		/// <summary>
		/// Defaults to 1.0.
		/// </summary>
		public double scaleFactor;
	}

	/// <summary>
	/// NativeImage.addRepresentation() options.
	/// </summary>
	public class AddRepresentationOptions {
		/// <summary>
		/// The scale factor to add the image representation for.
		/// </summary>
		public double scaleFactor;
		/// <summary>
		/// Defaults to 0. Required if a bitmap buffer is specified as buffer.
		/// </summary>
		public int? width;
		/// <summary>
		/// Defaults to 0. Required if a bitmap buffer is specified as buffer.
		/// </summary>
		public int? height;
		/// <summary>
		/// The buffer containing the raw image data.
		/// </summary>
		public Buffer buffer;
		/// <summary>
		/// The data URL containing either a base 64 encoded PNG or JPEG image.
		/// </summary>
		public string dataURL;
	}

	/// <summary>
	/// NativeImage.resize() options.
	/// </summary>
	public class ResizeOptions {
		/// <summary>
		/// Defaults to the image's width.
		/// </summary>
		public int? width;
		/// <summary>
		/// Defaults to the image's height.
		/// </summary>
		public int? height;
		/// <summary>
		/// The desired quality of the resize image. Possible values are good,
		/// better or best. The default is best.
		/// These values express a desired quality/speed tradeoff.
		/// They are translated into an algorithm-specific method that depends
		/// on the capabilities (CPU, GPU) of the underlying platform.
		/// It is possible for all three methods to be mapped to the same algorithm on a given platform.
		/// </summary>
		public string quality;
	}

	/// <summary>
	/// NativeImage.getBitmap() options.
	/// </summary>
	public class BitmapOptions {
		/// <summary>
		/// Defaults to 1.0.
		/// </summary>
		public double? scaleFactor;
	}

	/// <summary>
	/// NativeImage.toBitmap() options.
	/// </summary>
	public class ToBitmapOptions {
		/// <summary>
		/// Defaults to 1.0.
		/// </summary>
		public double? scaleFactor;
	}

	/// <summary>
	/// NativeImage.toDataURL() options.
	/// </summary>
	public class ToDataURLOptions {
		/// <summary>
		/// Defaults to 1.0.
		/// </summary>
		public double? scaleFactor;
	}

	/// <summary>
	/// NativeImage.toPNG() options.
	/// </summary>
	public class ToPNGOptions {
		/// <summary>
		/// Defaults to 1.0.
		/// </summary>
		public double? scaleFactor;
	}
}
