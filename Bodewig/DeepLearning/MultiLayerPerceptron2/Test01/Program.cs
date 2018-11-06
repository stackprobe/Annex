using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.MultiLayerPerceptron2;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{62c90303-7328-4a4e-b878-fea7b04e8037}";
		public const string APP_TITLE = "MultiLayerPerceptron2";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

			Console.WriteLine("Press ENTER.");
			Console.ReadLine();
		}

		private void Main2(ArgsReader ar)
		{
			new MultiLayerTest().Test01();
		}
	}
}
