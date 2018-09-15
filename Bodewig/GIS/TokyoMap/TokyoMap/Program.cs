using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{a45b8c23-65b1-4a2b-92ec-117db3d0dc30}";
		public const string APP_TITLE = "TokyoMap";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			Console.WriteLine("Press ENTER.");
			Console.ReadLine();
#endif
		}

		private void Main2(ArgsReader ar)
		{
			Test01();
		}

		private const double LAT_MIN = 35.583333333;
		private const double LAT_MAX = 35.75;
		private const double LON_MIN = 139.625;
		private const double LON_MAX = 139.875;

		private void Test01()
		{
			//MakeMap();
		}
	}
}
