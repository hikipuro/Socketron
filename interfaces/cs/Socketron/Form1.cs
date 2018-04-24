using System;
using System.Windows.Forms;

namespace Socketron {
	public partial class Form1 : Form {
		Socketron socketron;
		int count = 0;

		public Form1() {
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e) {
			TestJQuery test = new TestJQuery();
			//test.Run();
			/*
			if (socketron != null && socketron.IsConnected) {
				Run();
				return;
			}
			socketron = new Socketron();
			socketron.On("connect", (args) => {
				Console.WriteLine("Connected");
				Run();
			});
			socketron.On("debug", (args) => {
				Console.WriteLine(args[0]);
			});
			socketron.On("data", (args) => {
				Packet packet = (Packet)args[0];
				Console.WriteLine("Test: {0}, {1}", packet.SequenceId, packet.GetStringData());
			});
			socketron.Connect("127.0.0.1");
			//*/
		}

		private void Run() {
			socketron.Renderer.Log("Test Client", (data) => {
				Console.WriteLine("Callback Test");
			});
			//socketron.Run("alert('test22');");
			//timer1.Enabled = true;
			//socketron.SendLog("Test Client2");
			//socketron.Run("console.log(document)");
			//socketron.ImportScript("https://code.jquery.com/jquery-3.3.1.min.js");
			//socketron.ImportScript("https://cdnjs.cloudflare.com/ajax/libs/pixi.js/4.7.3/pixi.js");
			//socketron.Run("var a = 11; Socketron.broadcast('Test Test ' + a + '\\n')");
			//socketron.Command("quit");
			//timer1.Enabled = true;

			string[] script = {
				"var _navigator = { };",
				"for (var i in navigator) _navigator[i] = navigator[i]",
				"Socketron.broadcast(JSON.stringify(_navigator) + '\\n');"
			};
			//socketron.Run(string.Join("\n", script));
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
			if (socketron != null) {
				socketron.Close();
			}
		}

		private void timer1_Tick(object sender, EventArgs e) {
			//socketron.Command("reload");
			//*
			var data = new byte[] { 2, 1, 0, 15, 0, 0, 0, 59, 97, 108, 101, 114, 116, 40, 39, 116, 101, 115, 116, 39, 41, 59 };
			Console.WriteLine("data: {0}", data[count]);

			socketron.Write(new byte[] { data[count], data[count + 1] });

			count += 2;
			if (data.Length <= count) {
				count = 0;
				timer1.Enabled = false;
			}
			//*/
		}
	}
}
