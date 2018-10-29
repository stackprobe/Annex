using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.MultiLayerPerceptron;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{f3540861-9696-4caa-9c28-114eb83e9ca9}";
		public const string APP_TITLE = "MultiLayerPerceptron";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

			Console.WriteLine("Press ENTER.");
			Console.ReadLine();
		}

		private void Main2(ArgsReader ar)
		{
			//new MultiLayerTest().Test01();
			//new MultiLayerTest().Test02();
			//new Test0001().Test01();
			new Test0001().Test02();
		}
	}
}
