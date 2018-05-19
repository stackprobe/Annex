using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace Compress
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
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.Visible = false;

			this.BeginInvoke((MethodInvoker)delegate
			{
				try
				{
					callMainProgram();
				}
				catch
				{ }

				this.Close();
			});
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void callMainProgram()
		{
			string[] args = Environment.GetCommandLineArgs();
			string file = Assembly.GetEntryAssembly().Location;
			string dir = Path.GetDirectoryName(file);
			string wcFile = Path.Combine(dir, "WCluster.exe");
			string wcOpt = "/C";

			if (args.Length < 2)
			{
				ProcessStartInfo psi = new ProcessStartInfo();

				psi.FileName = wcFile;
				psi.Arguments = wcOpt;

				Process.Start(psi);
			}
			else
			{
				ProcessStartInfo psi = new ProcessStartInfo();

				psi.FileName = wcFile;
				psi.Arguments = wcOpt + " \"" + string.Join("\" \"", args, 1, args.Length - 1) + "\"";

				Process.Start(psi);
			}
		}
	}
}
