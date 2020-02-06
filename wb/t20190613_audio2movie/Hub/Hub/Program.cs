using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.Tests;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{67161bec-afbf-40e9-9ba2-23dc7c292a8c}";
		public const string APP_TITLE = "Hub";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
#endif
		}

		private void Main2(ArgsReader ar)
		{
			this.GoToHomeDir();

#if DEBUG
			//new MovieMakerTest().Test01(); // test
			new PictureListTest().Test01(); // test
#else
			new Hub().Perform(ar);
#endif
		}

		private void GoToHomeDir()
		{
			Directory.SetCurrentDirectory(ExtraTools.GetHomeDir("home.sig"));
		}
	}
}
