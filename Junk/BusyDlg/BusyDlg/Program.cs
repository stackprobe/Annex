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

			Gnd.I.SelfFile = args[0]; // HACK
			Gnd.I.ExecuteMode = int.Parse(Tools.GetString(args, 1, "-1"));
			Gnd.I.Message = Tools.GetString(args, 2, "しばらくお待ち下さい...");

			Gnd.I.DlgOpened = new EventWaitHandle(false, EventResetMode.AutoReset, Gnd.DLG_OPENED_EVENT_NAME);
			Gnd.I.CloseDlg = new EventWaitHandle(false, EventResetMode.AutoReset, Gnd.CLOSE_DLG_EVENT_NAME);
			Gnd.I.DlgClosed = new EventWaitHandle(false, EventResetMode.AutoReset, Gnd.DLG_CLOSED_EVENT_NAME);
			Gnd.I.DlgOpenCloseMutex = new Mutex(false, Gnd.DLG_OPEN_CLOSE_MUTEX_NAME);

			bool dlgOpened;

			switch (Gnd.I.ExecuteMode)
			{
				case 0:
					Gnd.I.DlgOpenCloseMutex.WaitOne();
					dlgOpened = Gnd.I.IsDlgOpened();
					Gnd.I.CloseDlg.Set();
					Gnd.I.DlgClosed.WaitOne(dlgOpened ? Gnd.CREDIBLE_TIMEOUT_MILLIS : Gnd.INCREDIBLE_TIMEOUT_MILLIS);
					Gnd.I.DlgOpenCloseMutex.ReleaseMutex();
					break;

				case 1:
					Gnd.I.DlgOpenCloseMutex.WaitOne();
					Process.Start(Gnd.I.SelfFile, "999 \"" + Gnd.I.Message + "\"");
					Gnd.I.DlgOpened.WaitOne(Gnd.CREDIBLE_TIMEOUT_MILLIS);
					Gnd.I.DlgOpenCloseMutex.ReleaseMutex();
					break;

				case 999:
					Main2();
					// この時点でdlgは閉じている。それが唯一のdlgだったら、この時点で G.I.IsDlgOpened() == false になる。
					Gnd.I.DlgClosed.Set();
					break;

				default:
					throw new Exception("不明な実行モード: " + Gnd.I.ExecuteMode);
			}

			Gnd.I.DlgOpened.Close();
			Gnd.I.CloseDlg.Close();
			Gnd.I.DlgClosed.Close();
			Gnd.I.DlgOpenCloseMutex.Close();
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
