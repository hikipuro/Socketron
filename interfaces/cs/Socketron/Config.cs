using System.Text;

namespace Socketron {
	public class Config {
		public bool IsDebug = true;
		public int Timeout = 5000;
		public Encoding Encoding = Encoding.UTF8;
		public const int ReadBufferSize = 1024 * 8;
	}
}
