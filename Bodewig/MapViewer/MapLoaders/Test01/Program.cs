using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.MapLoaders;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{0503683c-afe9-499b-b534-30be166a68cf}";
		public const string APP_TITLE = "MapLoaders";

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
			//new GeoAreaStorageTest().Test01();
			new GeoRoadStorageTest().Test01();
		}
	}
}
