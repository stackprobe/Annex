using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.MapDrawers;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{4fb854cf-8318-4e4d-ac8d-d1a8eabf0a12}";
		public const string APP_TITLE = "MapDrawers";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
		}

		private void Main2(ArgsReader ar)
		{
			new MeterLatLonTest().Test01();
		}
	}
}
