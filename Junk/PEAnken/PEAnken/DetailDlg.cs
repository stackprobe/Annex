using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace Charlotte
{
	public partial class DetailDlg : Form
	{
		#region ALT_F4 抑止

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
			{
				MainWin.CloseWindowRequested = true;
				return;
			}
			base.WndProc(ref m);
		}

		#endregion

		public DetailDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void DetailDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		public void SetMainText(string text)
		{
			this.MainText.Text = text;
		}

		public string GetMainText()
		{
			return this.MainText.Text;
		}

		private void MainText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 1) // ctrl_a
			{
				this.MainText.SelectAll();
				e.Handled = true;
			}
		}
	}
}
