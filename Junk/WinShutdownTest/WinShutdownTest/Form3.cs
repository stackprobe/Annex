using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace WinFrmApp1
{
	public partial class Form3 : Form
	{
#if true
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
#endif

		public Form3()
		{
			InitializeComponent();
		}

		private void Form3_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void Form3_FormClosing(object sender, FormClosingEventArgs e)
		{
			Tools.WriteLog("Form3.closing");

			e.Cancel = this.checkBox1.Checked;
		}

		private void Form3_FormClosed(object sender, FormClosedEventArgs e)
		{
			Tools.WriteLog("Form3.closed");
		}

		private void button1_Click(object sender, EventArgs e)
		{
			G.I.CloseForm1Flag = true;
		}
	}
}
