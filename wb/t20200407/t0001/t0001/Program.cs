using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{66957fc3-6642-4250-8b67-3187109fb836}";
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

		private void Main2(ArgsReader ar)
		{
			Test01();
		}

		private void Test01()
		{
			for (int c = 0; c < 24; c++)
			{
				double d = c * (Math.PI * 2.0) / 24.0;
				double s = 0.0;

				for (double a = 0.0; a < Math.PI * 2.0 * 10.0; a += 0.001)
					s += Math.Sin(a) * Math.Sin(a + d);

				Console.WriteLine(DoubleTools.ToInt(d * 360.0 / (Math.PI * 2.0)) + " ==> " + s.ToString("F9"));
			}
		}
	}
}
