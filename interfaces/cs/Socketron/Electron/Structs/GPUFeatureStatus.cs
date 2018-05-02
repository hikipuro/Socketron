namespace Socketron {
	public class GPUFeatureStatus {
		public string _2d_canvas;
		public string flash_3d;
		public string flash_stage3d;
		public string flash_stage3d_baseline;
		public string gpu_compositing;
		public string multiple_raster_threads;
		public string native_gpu_memory_buffers;
		public string rasterization;
		public string video_decode;
		public string video_encode;
		public string vpx_decode;
		public string webgl;
		public string webgl2;

		public static GPUFeatureStatus Parse(string text) {
			return JSON.Parse<GPUFeatureStatus>(text);
		}

		public string Stringify() {
			return JSON.Stringify(this);
		}
	}
}

