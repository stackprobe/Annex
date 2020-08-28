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
	public static class ProcMain
	{
		public static string APP_IDENT;

		public static ArgsReader ArgsReader;

		public static void MainProc(Action<ArgsReader> mainFunc, string ident)
		{
			try
			{
				APP_IDENT = ident;

				ArgsReader = new ArgsReader(Environment.GetCommandLineArgs(), 1);

				IntoHomeDir();

				mainFunc(ArgsReader);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);

				MessageBox.Show("" + e);
			}
		}

		private static void IntoHomeDir()
		{
			string self = Assembly.GetEntryAssembly().Location;

			int i = self.ToUpper().LastIndexOf(@"\PROGRAMS\");

			if (i == -1)
				throw new Exception("ホームディレクトリが見つかりません。");

			string dir = self.Substring(0, i);

			Directory.SetCurrentDirectory(dir);
		}
	}
}
