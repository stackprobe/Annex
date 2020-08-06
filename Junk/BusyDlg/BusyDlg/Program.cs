using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace BusyDlg
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			string[] args = Environment.GetCommandLineArgs();

			Ground.I.SelfFile = args[0]; // HACK
			Ground.I.ExecuteMode = int.Parse(Utils.GetString(args, 1, "-1"));
			Ground.I.Message = Utils.GetString(args, 2, "しばらくお待ち下さい...");

			Ground.I.DlgOpened = new EventWaitHandle(false, EventResetMode.AutoReset, Ground.DLG_OPENED_EVENT_NAME);
			Ground.I.CloseDlg = new EventWaitHandle(false, EventResetMode.AutoReset, Ground.CLOSE_DLG_EVENT_NAME);
			Ground.I.DlgClosed = new EventWaitHandle(false, EventResetMode.AutoReset, Ground.DLG_CLOSED_EVENT_NAME);
			Ground.I.DlgOpenCloseMutex = new Mutex(false, Ground.DLG_OPEN_CLOSE_MUTEX_NAME);

			bool dlgOpened;

			switch (Ground.I.ExecuteMode)
			{
				case 0:
					Ground.I.DlgOpenCloseMutex.WaitOne();
					dlgOpened = Ground.I.IsProbablyDlgOpened();
					Ground.I.CloseDlg.Set();
					Ground.I.DlgClosed.WaitOne(dlgOpened ? Ground.CREDIBLE_TIMEOUT_MILLIS : Ground.INCREDIBLE_TIMEOUT_MILLIS);
					Ground.I.DlgOpenCloseMutex.ReleaseMutex();
					break;

				case 1:
					Ground.I.DlgOpenCloseMutex.WaitOne();
					Process.Start(Ground.I.SelfFile, "999 \"" + Ground.I.Message + "\"");
					Ground.I.DlgOpened.WaitOne(Ground.CREDIBLE_TIMEOUT_MILLIS);
					Ground.I.DlgOpenCloseMutex.ReleaseMutex();
					break;

				case 999:
					Main2();
					// この時点でdlgは閉じている。それが唯一のdlgだったら、この時点で Ground.I.IsDlgOpened() == false になる。
					Ground.I.DlgClosed.Set();
					break;

				default:
					throw new Exception("不明な実行モード: " + Ground.I.ExecuteMode);
			}

			Ground.I.DlgOpened.Close();
			Ground.I.CloseDlg.Close();
			Ground.I.DlgClosed.Close();
			Ground.I.DlgOpenCloseMutex.Close();
		}

		private static void Main2()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWin());
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			Program.Error("[Application_ThreadException]", e.Exception);
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Program.Error("[CurrentDomain_UnhandledException]", e.ExceptionObject);
		}

		private static void Error(string title, object e)
		{
			MessageBox.Show(title + "\n" + e, "エラーが発生しました", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Environment.Exit(1);
		}
	}
}
