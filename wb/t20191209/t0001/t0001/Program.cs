using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using System.IO.Ports;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{689be949-9320-4a06-b08e-4f1ef878ed2c}";
		public const string APP_TITLE = "t0001";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
#endif
		}

		private const string SEND_PORT = "COM3";
		private const string RECV_PORT = "COM4";

		private void Main2(ArgsReader ar)
		{
			if (ar.ArgIs("/S"))
			{
				using (SerialPort sp = new SerialPort())
				{
					sp.BaudRate = 115200;
					sp.Parity = Parity.None;
					sp.DataBits = 8;
					sp.StopBits = StopBits.One;
					sp.Handshake = Handshake.None;
					sp.PortName = SEND_PORT;

					sp.ReadTimeout = 2000;
					sp.WriteTimeout = 2000;

					sp.Open();

					sp.Write("This is TEST String 0001\n");
					sp.Write("This is TEST String 0002\n");
					sp.Write("This is TEST String 0003\n");

					Console.WriteLine(sp.ReadLine());

					sp.Close();
				}
			}
			if (ar.ArgIs("/R"))
			{
				using (SerialPort sp = new SerialPort())
				{
					sp.BaudRate = 115200;
					sp.Parity = Parity.None;
					sp.DataBits = 8;
					sp.StopBits = StopBits.One;
					sp.Handshake = Handshake.None;
					sp.PortName = RECV_PORT;

					sp.ReadTimeout = 15000;
					sp.WriteTimeout = 2000;

					sp.Open();

					Console.WriteLine(sp.ReadLine());
					Console.WriteLine(sp.ReadLine());
					Console.WriteLine(sp.ReadLine());

					sp.Write("OK!\n");

					sp.Close();
				}
			}
		}
	}
}
