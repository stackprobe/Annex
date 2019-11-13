using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.MultiLayerPerceptron;
using Charlotte.Tests;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{398d8920-6be3-453d-b78d-12dd82f26796}";
		public const string APP_TITLE = "MultiLayerPerceptron";

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
			//new MultiLayerTest().Test01();
			//new MultiLayerTest().Test02();
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0001().Test03();
			//new Test0001().Test04();
			//new Test20191026().Perform();
			new Test0002().Test01();
			//new Test0002().Test02(); // ng
		}
	}
}
