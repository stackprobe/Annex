using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Permissions;

namespace BusyDlg
{
	public partial class MainWin : Form
	{
		#region ALT_F4 抑止

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
				return;

			base.WndProc(ref m);
		}

		#endregion

		public MainWin()
		{
			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			int w = this.Status.Width;
			this.Status.Text = Program.Message;
			int d = this.Status.Width - w;
			this.Width += d;
			this.Left -= d / 2;

			this.MinimumSize = this.Size;
			//this.MaximumSize = this.Size;

			this.Text = Program.Title;
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.MT_Enabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MT_Enabled = false;
		}

#if false // フォームを掴んで移動
		private Point _mouseDownPos;

		private void MainWin_MouseDown(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				_mouseDownPos = new Point(e.X, e.Y);
			}
		}

		private void MainWin_MouseMove(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				this.Location = new Point(
					this.Location.X + e.X - _mouseDownPos.X,
					this.Location.Y + e.Y - _mouseDownPos.Y
					);
			}
		}
#endif

		private bool MT_Enabled;
		private bool MT_Busy;
		private long MT_Count;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MT_Enabled == false || this.MT_Busy)
				return;

			this.MT_Busy = true;

			try
			{
				switch (this.MT_Count)
				{
					case 2:
						this.TopMost = false;
						break;

					case 3:
						this.TopMost = true;
						break;
				}
				if (5 <= this.MT_Count)
				{
					if (Program.EvStop.WaitOne(0) || IsParentProcessEnded())
					{
						this.MT_Enabled = false;
						this.Close();
						return;
					}
				}
			}
			finally
			{
				this.MT_Busy = false;
				this.MT_Count++;
			}
		}

		private static bool IsParentProcessEnded()
		{
			try
			{
				return Process.GetProcessById(Program.ParentProcessId) == null;
			}
			catch
			{ }

			return true;
		}
	}
}
