using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WinFrmApp1
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
#if true
			SystemEvents.SessionEnding += delegate
			{
				Tools.WriteLog("SessionEnding");
				Environment.Exit(1);
			};
#endif

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());

			Tools.WriteLog("Program.Main.end");
		}
	}
}
