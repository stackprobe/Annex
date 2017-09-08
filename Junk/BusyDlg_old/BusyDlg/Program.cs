using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace BusyDlg
{
	static class Program
	{
		public static string SessionId;
		public static bool StopFlag;
		public static int ParentProcessId;
		public static string Message;
		public static string Title;
		public static EventWaitHandle EvStop;

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			int c = 1;

			SessionId = Environment.GetCommandLineArgs()[c++];
			StopFlag = int.Parse(Environment.GetCommandLineArgs()[c++]) != 0;
			ParentProcessId = int.Parse(Environment.GetCommandLineArgs()[c++]);
			Message = Environment.GetCommandLineArgs()[c++];
			Title = Environment.GetCommandLineArgs()[c++];

			using (EvStop = new EventWaitHandle(false, EventResetMode.AutoReset, SessionId))
			{
				if (StopFlag)
				{
					EvStop.Set();
					return;
				}

				// core >

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainWin());

				// < core
			}
		}
	}
}
