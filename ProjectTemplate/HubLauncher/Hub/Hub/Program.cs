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
		public const string APP_IDENT = "{ded4c1b1-732c-458b-be88-59ed69d19acb}";
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
			this.GoToHomeDir();

			new Hub().Perform(ar);
		}

		private void GoToHomeDir()
		{
			Directory.SetCurrentDirectory(ExtraTools.GetHomeDir("home.sig"));
		}
	}
}
