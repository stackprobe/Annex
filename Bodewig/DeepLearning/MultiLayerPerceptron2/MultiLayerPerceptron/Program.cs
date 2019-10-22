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
		public const string APP_IDENT = "{96477ba2-659b-42ee-8ac4-78ecf458ebe6}";
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
			new MultiLayerTest().Test01();
		}
	}
}
