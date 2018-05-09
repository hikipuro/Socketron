using System.Text;

namespace Socketron {
	public class LocalConfig {
		public bool IsDebug = true;
		public bool EnableDebugPayloads = true;
		public int Timeout = 5000;
		public Encoding Encoding = Encoding.UTF8;
		public const int ReadBufferSize = 1024 * 8;
	}
}
