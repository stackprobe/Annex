using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.LTools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{cefbadc2-8292-4ca2-a90c-5004653621c2}";

		static void Main(string[] args)
		{
			LCommon.MainProc(new Program().Main2, APP_IDENT);
		}

		private void Main2(ArgsReader ar)
		{
			MessageBox.Show(Common.APP_IDENT + "\r\n" + Common.APP_TITLE + "\r\n" + Directory.GetCurrentDirectory()); // TODO
		}
	}
}
