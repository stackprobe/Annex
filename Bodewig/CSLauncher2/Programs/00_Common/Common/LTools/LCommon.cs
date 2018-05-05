using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Charlotte.Tools;
using System.Windows.Forms;

namespace Charlotte.LTools
{
	public class LCommon
	{
		public static void MainProc(Action<ArgsReader> mainFunc, string ident)
		{
			Common.CUIMain(
				ar =>
				{
					try
					{
						IntoHomeDir();

						mainFunc(ar);
					}
					catch (Exception e)
					{
						Console.WriteLine(e);

						MessageBox.Show("" + e);
					}
				},
				ident,
				Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)
				);
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
