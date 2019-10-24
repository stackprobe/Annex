using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.Tests.MultiLayerPerceptron;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{175209a5-b011-4501-a5a1-e569719cc75e}";
		public const string APP_TITLE = "MultiLayerPerceptron";

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
			//new MultiLayerTest().Test01();
			//new MultiLayerTest().Test02();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0002().Test01();
			//new Test0002().Test01b();
			//new Test0003().Test01();
			//new Test0003().Test02();
			//new Test0004().Test01();
			new Test0004().Test02();
		}
	}
}
