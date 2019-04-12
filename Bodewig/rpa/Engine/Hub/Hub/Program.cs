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
		public const string APP_IDENT = "{c1d64f87-edd4-4c83-8495-69b0d15fb300}";
		public const string APP_TITLE = "Hub";

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
			GoToHomeSig();

			new Hub().Perform(ar);
		}

		private void GoToHomeSig()
		{
			while (File.Exists("home.sig") == false)
			{
				if (Directory.GetCurrentDirectory().Length <= 3)
					throw new Exception("no home.sig");

				Directory.SetCurrentDirectory("..");
			}
		}
	}
}
