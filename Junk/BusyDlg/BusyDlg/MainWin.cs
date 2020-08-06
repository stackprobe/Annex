using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace BusyDlg
{
	public partial class MainWin : Form
	{
		// ---- [ALT]+[F4]抑止 ----

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
			{
				return;
			}
			base.WndProc(ref m);
		}

		// ----

		public MainWin()
		{
			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			this.Message.Text = Ground.I.Message;
		}

		private bool ActivatedFlag;

		private void MainWin_Activated(object sender, EventArgs e)
		{
			if (this.ActivatedFlag)
				return;

			this.ActivatedFlag = true;

			this.MT_Enabled = true;
			this.MainTimer.Enabled = true;
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MainTimer.Enabled = false;
			this.MT_Enabled = false;
		}

		private bool MT_Enabled;
		private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false)
				return;

			if (this.MT_Busy)
				return;

			this.MT_Busy = true;

			try
			{
				if (this.MT_Count == 0) // 初回のみ
				{
					int d = this.Message.Width - this.MainProBar.Width;

					if (0 < d)
					{
						this.Width += d;
						this.Location = new Point(this.Location.X - d / 2, this.Location.Y);
					}
					Ground.I.CloseDlg.WaitOne(0); // ごみイベント回収
					Ground.I.DlgOpened.Set();
				}
				if (Ground.I.CloseDlg.WaitOne(0))
				{
					this.MT_Enabled = false;
					this.Close();
					return;
				}
			}
			finally
			{
				this.MT_Busy = false;
				this.MT_Count++;
			}
		}
	}
}
