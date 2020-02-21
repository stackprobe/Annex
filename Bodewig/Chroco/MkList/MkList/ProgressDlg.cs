using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using Charlotte.Tools;

namespace Charlotte
{
	public partial class ProgressDlg : Form
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

		public static void Perform(Action<Action<string>> userTask)
		{
			ExceptionDam.Section(eDam =>
			{
				using (ProgressDlg f = new ProgressDlg())
				{
					f.EDam = eDam;
					f.UserTask = userTask;
					f.ShowDialog();
				}
			});
		}

		public ExceptionDam EDam;
		public Action<Action<string>> UserTask;

		public ProgressDlg()
		{
			InitializeComponent();
		}

		private void ProgressDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void ProgressDlg_Shown(object sender, EventArgs e)
		{
			this.BeginInvoke((MethodInvoker)delegate
			{
				this.EDam.Invoke(() =>
				{
					this.UserTask(message =>
					{
						this.PDMessage.Text = message;

						Application.DoEvents();
					});
				});

				this.Close();
			});
		}

		private void MainPBar_Click(object sender, EventArgs e)
		{
			// noop
		}
	}
}
