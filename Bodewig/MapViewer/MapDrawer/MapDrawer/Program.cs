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
		public const string APP_IDENT = "{c9527999-6887-474b-9f96-9c92557044cc}";
		public const string APP_TITLE = "MapDrawer";

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
			int w = int.Parse(ar.NextArg());
			int h = int.Parse(ar.NextArg());
			double lat = double.Parse(ar.NextArg());
			double lon = double.Parse(ar.NextArg());
			double meterPerDot = double.Parse(ar.NextArg());

			// TODO
		}
	}
}
