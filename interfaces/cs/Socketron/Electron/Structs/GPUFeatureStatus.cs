namespace Socketron.Electron {
	public class GPUFeatureStatus {
		public class Values {
			/// <summary>
			/// Software only. Hardware acceleration disabled (yellow).
			/// </summary>
			public const string disabled_software = "disabled_software";
			/// <summary>
			/// Disabled(red).
			/// </summary>
			public const string disabled_off = "disabled_off";
			/// <summary>
			/// Disabled(yellow).
			/// </summary>
			public const string disabled_off_ok = "disabled_off_ok";
			/// <summary>
			/// Software only, hardware acceleration unavailable(yellow).
			/// </summary>
			public const string unavailable_software = "unavailable_software";
			/// <summary>
			/// Unavailable(red).
			/// </summary>
			public const string unavailable_off = "unavailable_off";
			/// <summary>
			/// Unavailable(yellow).
			/// </summary>
			public const string unavailable_off_ok = "unavailable_off_ok";
			/// <summary>
			/// Hardware accelerated but at reduced performance(yellow).
			/// </summary>
			public const string enabled_readback = "enabled_readback";
			/// <summary>
			/// Hardware accelerated on all pages(green).
			/// </summary>
			public const string enabled_force = "enabled_force";
			/// <summary>
			/// Hardware accelerated(green).
			/// </summary>
			public const string enabled = "enabled";
			/// <summary>
			/// Enabled(green).
			/// </summary>
			public const string enabled_on = "enabled_on";
			/// <summary>
			/// Force enabled(green).
			/// </summary>
			public const string enabled_force_on = "enabled_force_on";
		}

		/// <summary>
		/// Canvas.
		/// </summary>
		public string _2d_canvas;
		/// <summary>
		/// Flash.
		/// </summary>
		public string flash_3d;
		/// <summary>
		/// Flash Stage3D.
		/// </summary>
		public string flash_stage3d;
		/// <summary>
		/// Flash Stage3D Baseline profile.
		/// </summary>
		public string flash_stage3d_baseline;
		/// <summary>
		/// Compositing.
		/// </summary>
		public string gpu_compositing;
		/// <summary>
		/// Multiple Raster Threads.
		/// </summary>
		public string multiple_raster_threads;
		/// <summary>
		/// Native GpuMemoryBuffers.
		/// </summary>
		public string native_gpu_memory_buffers;
		/// <summary>
		/// Rasterization.
		/// </summary>
		public string rasterization;
		/// <summary>
		/// Video Decode.
		/// </summary>
		public string video_decode;
		/// <summary>
		/// Video Encode.
		/// </summary>
		public string video_encode;
		/// <summary>
		/// VPx Video Decode.
		/// </summary>
		public string vpx_decode;
		/// <summary>
		/// WebGL.
		/// </summary>
		public string webgl;
		/// <summary>
		/// WebGL2.
		/// </summary>
		public string webgl2;

		public static GPUFeatureStatus FromObject(object obj) {
			if (obj == null) {
				return null;
			}
			JsonObject json = new JsonObject(obj);
			return new GPUFeatureStatus() {
				_2d_canvas = json.String("2d_canvas"),
				flash_3d = json.String("flash_3d"),
				flash_stage3d = json.String("flash_stage3d"),
				flash_stage3d_baseline = json.String("flash_stage3d_baseline"),
				gpu_compositing = json.String("gpu_compositing"),
				multiple_raster_threads = json.String("multiple_raster_threads"),
				native_gpu_memory_buffers = json.String("native_gpu_memory_buffers"),
				rasterization = json.String("rasterization"),
				video_decode = json.String("video_decode"),
				video_encode = json.String("video_encode"),
				vpx_decode = json.String("vpx_decode"),
				webgl = json.String("webgl"),
				webgl2 = json.String("webgl2")
			};
		}

		/// <summary>
		/// Parse JSON text.
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static GPUFeatureStatus Parse(string text) {
			return JSON.Parse<GPUFeatureStatus>(text);
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

