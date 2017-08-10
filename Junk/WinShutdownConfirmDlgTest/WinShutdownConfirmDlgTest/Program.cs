using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace WinShutdownConfirmDlgTest
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());

			Common.WriteLog("Main");
		}
	}

	public class Common
	{
		private static object SYNCROOT = new object();
		private static bool WL_NotFirst = false;

		public static void WriteLog(object message)
		{
			lock (SYNCROOT)
			{
				using (StreamWriter sw = new StreamWriter(@"C:\appdata\WinShutdownConfirmDlgTest.txt", WL_NotFirst, Encoding.GetEncoding(932)))
				{
					sw.WriteLine("" + message);
				}
				WL_NotFirst = true;
			}
		}
	}
}
