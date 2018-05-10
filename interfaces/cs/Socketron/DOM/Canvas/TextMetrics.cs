using System.Diagnostics.CodeAnalysis;

namespace Socketron.DOM {
	[type: SuppressMessage("Style", "IDE1006")]
	public class TextMetrics : DOMModule {
		public TextMetrics() {
		}

		public double width {
			get { return API.GetProperty<double>("width"); }
		}

		public double actualBoundingBoxLeft {
			get { return API.GetProperty<double>("actualBoundingBoxLeft"); }
		}

		public double actualBoundingBoxRight {
			get { return API.GetProperty<double>("actualBoundingBoxRight"); }
		}

		public double fontBoundingBoxAscent {
			get { return API.GetProperty<double>("fontBoundingBoxAscent"); }
		}

		public double fontBoundingBoxDescent {
			get { return API.GetProperty<double>("fontBoundingBoxDescent"); }
		}

		public double actualBoundingBoxAscent {
			get { return API.GetProperty<double>("actualBoundingBoxAscent"); }
		}

		public double actualBoundingBoxDescent {
			get { return API.GetProperty<double>("actualBoundingBoxDescent"); }
		}

		public double emHeightDescent {
			get { return API.GetProperty<double>("emHeightDescent"); }
		}

		public double hangingBaseline {
			get { return API.GetProperty<double>("hangingBaseline"); }
		}

		public double alphabeticBaseline {
			get { return API.GetProperty<double>("alphabeticBaseline"); }
		}

		public double ideographicBaseline {
			get { return API.GetProperty<double>("ideographicBaseline"); }
		}
	}
}
