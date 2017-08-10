using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WCluster
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			// orig >

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainWin());

			// < orig

			if (MainWin.MainProcEx != null)
			{
				if (MainWin.MainProcEx is Clusterizer.Cancelled)
				{
					MessageBox.Show(
						"キャンセルしました。",
						"WCluster / キャンセル",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information
						);
				}
				else
				{
					MessageBox.Show(
						MainWin.MainProcEx.Message,
						"WCluster / エラー",
						MessageBoxButtons.OK,
						MessageBoxIcon.Error
						);
				}
			}
		}
	}
}
