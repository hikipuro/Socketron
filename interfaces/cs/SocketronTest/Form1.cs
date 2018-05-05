using System;
using System.Threading;
using System.Windows.Forms;

namespace SocketronTest {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
			Thread.CurrentThread.Name = "UI Thread";
			DoubleBuffered = true;
#if DEBUG
			textBox1.Text = "Debug mode\n";
#else
			textBox1.Text = "Release mode\n";
#endif
		}

		private void button1_Click(object sender, EventArgs e) {
			if (Program.test != null) {
				Program.test.Close();
			}
			Program.test = new TestJQuery();
			Program.test.Log += _OnLog;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
			if (Program.test != null) {
				Program.test.Log -= _OnLog;
				Program.test.Close();
			}
		}

		private void _OnLog(string text) {
			//*
			if (IsDisposed) {
				return;
			}
			textBox1.Invoke((MethodInvoker)(() => {
				textBox1.AppendText(text + Environment.NewLine);
			}));
			//*/
		}
	}
}
