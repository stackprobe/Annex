﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{4add5930-b8f7-4070-bd65-e5cbad430a29}";

		static void Main(string[] args)
		{
			ProcMain.MainProc(new Program().Main2, APP_IDENT);
		}

		private void Main2(ArgsReader ar)
		{
			MessageBox.Show(ProcMain.APP_IDENT + "\r\n" + Directory.GetCurrentDirectory()); // TODO
		}
	}
}
