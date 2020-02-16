using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.MapLoader;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{ec0c3c31-cbd5-4c5c-8258-fbbc5bf14415}";
		public const string APP_TITLE = "MapLoader";

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
			new MapLoader0001Test().Test01(); // -- 0001
		}
	}
}
