using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	public class Common
	{
		public static string APP_IDENT;

		public static ArgsReader ArgsReader;

		public static void MainProc(Action<ArgsReader> mainFunc, string ident)
		{
			try
			{
				APP_IDENT = ident;

				ArgsReader = new ArgsReader(Environment.GetCommandLineArgs(), 1);

				mainFunc(ArgsReader);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);

				MessageBox.Show("" + e);
			}
		}
	}
}
